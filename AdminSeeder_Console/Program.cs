using DbUp;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

partial class Program
{
    static int Main(string[] args)
    {
        // Build config from appsettings.json
        var configuration = new ConfigurationBuilder()
            .SetBasePath(GetProjectRoot()) // Important: base path of the executable
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        try
        {
            // Read connection strings from config
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            Console.WriteLine("Applying migrations for Internal DB...");
            RunMigrations(connectionString);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("All migrations applied successfully!");
            Console.ResetColor();

            return 0;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Migration failed: {ex.Message}");
            Console.ResetColor();
            return -1;
        }
    }

    static void RunMigrations(string connectionString)
    {
        string migrationPath = Path.Combine(GetProjectRoot(), "Migrations", "InternalDb");

        List<string> existingTables = GetAllTables(connectionString);

        if (existingTables.Count == 0)
        {
            Console.WriteLine("No tables found in the database.");
        }
        else
        {
            Console.WriteLine("Existing tables in the database:");
            for (int i = 0; i < existingTables.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {existingTables[i]}");
            }

            Console.WriteLine("\nDo you want to delete:");
            Console.WriteLine("1. All tables");
            Console.WriteLine("2. Specific tables (enter comma-separated table numbers)");
            Console.Write("Enter your choice (1 or 2): ");
            string input = Console.ReadLine()?.Trim();

            if (input == "1")
            {
                DropTables(connectionString, existingTables);
            }
            else if (input == "2")
            {
                Console.Write("Enter table numbers to delete (e.g., 1,3,5): ");
                var tableIndexes = Console.ReadLine()?.Split(',').Select(s => s.Trim()).ToList();

                var selectedTables = tableIndexes
                    .Select(index =>
                    {
                        if (int.TryParse(index, out int idx) && idx > 0 && idx <= existingTables.Count)
                            return existingTables[idx - 1];
                        return null;
                    })
                    .Where(name => name != null)
                    .ToList();

                DropTables(connectionString, selectedTables);
            }
            else
            {
                Console.WriteLine("No tables deleted. Migration will proceed.");
            }
        }

        // Ensure DB exists
        EnsureDatabase.For.SqlDatabase(connectionString);

        var upgrader = DeployChanges.To
            .SqlDatabase(connectionString)
            .WithScriptsFromFileSystem(migrationPath)
            .LogToConsole()
            .Build();

        var result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            throw new Exception(result.Error.Message, result.Error);
        }
    }



    public static string GetProjectRoot()
    {
        string baseDir = AppContext.BaseDirectory;

        if (baseDir.Contains(Path.Combine("bin", "Debug")) || baseDir.Contains(Path.Combine("bin", "Release")))
        {
            return Directory.GetParent(baseDir)     // net6.0/
                            ?.Parent                // Debug/
                            ?.Parent                // bin/
                            ?.Parent                // Project root
                            ?.FullName ?? baseDir;
        }

        return baseDir;
    }

    static List<string> GetAllTables(string connectionString)
    {
        var tables = new List<string>();

        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var command = new SqlCommand(@"
        SELECT TABLE_SCHEMA + '.' + TABLE_NAME 
        FROM INFORMATION_SCHEMA.TABLES 
        WHERE TABLE_TYPE = 'BASE TABLE'
        ORDER BY TABLE_SCHEMA, TABLE_NAME", connection);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            tables.Add(reader.GetString(0));
        }

        return tables;
    }
    static void DropTables(string connectionString, List<string> tablesToDrop)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var allTables = GetAllTables(connectionString);
        bool dropAll = allTables.Count == tablesToDrop.Count;

        Console.WriteLine("Dropping selected tables...");

        var dropScript = new System.Text.StringBuilder();

        // Drop FKs
        dropScript.AppendLine(@"
            DECLARE @sql NVARCHAR(MAX) = '';
            SELECT @sql += 'ALTER TABLE [' + s.name + '].[' + t.name + '] DROP CONSTRAINT [' + fk.name + '];'
            FROM sys.foreign_keys fk
            JOIN sys.tables t ON fk.parent_object_id = t.object_id
            JOIN sys.schemas s ON t.schema_id = s.schema_id;
            EXEC sp_executesql @sql;
        ");

        foreach (var fullName in tablesToDrop)
        {
            var parts = fullName.Split('.');
            if (parts.Length == 2)
            {
                dropScript.AppendLine($"DROP TABLE [{parts[0]}].[{parts[1]}];");
            }
        }

        var command = connection.CreateCommand();
        command.CommandText = dropScript.ToString();
        command.ExecuteNonQuery();

        DeleteJournalEntries(connection, tablesToDrop, dropAll);

        Console.WriteLine("Selected tables and matching journal entries deleted.");
    }

    static void DeleteJournalEntries(SqlConnection connection, List<string> tablesToDrop, bool deleteAll = false)
    {
        if (deleteAll)
        {
            Console.WriteLine("Deleting entire migration journal...");
            var dropCommand = connection.CreateCommand();
            dropCommand.CommandText = @"
            IF OBJECT_ID('dbo.SchemaVersions', 'U') IS NOT NULL
                DROP TABLE dbo.SchemaVersions;";
            dropCommand.ExecuteNonQuery();
            return;
        }

        // Partial delete: remove only relevant entries
        Console.WriteLine("Removing journal entries for dropped tables...");
        var getScriptNames = connection.CreateCommand();
        getScriptNames.CommandText = "SELECT ScriptName FROM dbo.SchemaVersions";

        var toDelete = new List<string>();

        using (var reader = getScriptNames.ExecuteReader())
        {
            while (reader.Read())
            {
                var scriptName = reader.GetString(0);
                // Match table name in script name (adjust based on your naming convention)
                if (tablesToDrop.Any(tbl => scriptName.Contains(tbl.Split('.').Last(), StringComparison.OrdinalIgnoreCase)))
                {
                    toDelete.Add(scriptName);
                }
            }
        }

        foreach (var scriptName in toDelete)
        {
            var deleteCmd = connection.CreateCommand();
            deleteCmd.CommandText = "DELETE FROM dbo.SchemaVersions WHERE ScriptName = @name";
            deleteCmd.Parameters.AddWithValue("@name", scriptName);
            deleteCmd.ExecuteNonQuery();
        }

        Console.WriteLine($"{toDelete.Count} journal entries deleted.");
    }

}