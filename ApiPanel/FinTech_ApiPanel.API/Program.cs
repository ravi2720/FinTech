using FinTech_ApiPanel.API.Middleware;
using FinTech_ApiPanel.Application.Handlers.UserMasters;
using FinTech_ApiPanel.Domain.DTOs.Mails;
using FinTech_ApiPanel.Domain.DTOs.Tokens;
using FinTech_ApiPanel.Domain.DTOs.URLs;
using FinTech_ApiPanel.Domain.Shared.Common;
using FinTech_ApiPanel.Domain.Shared.Goterpay;
using FinTech_ApiPanel.Infrastructure.Repositories.UserMasters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Data;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------------------------------
// Configuration: strongly-typed settings
// -----------------------------------------------------
builder.Services.Configure<MailSettingDto>(builder.Configuration.GetSection("MailSettings"));

// -----------------------------------------------------
// Services
// -----------------------------------------------------
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(CreateUserHandler).Assembly);

// Bind PanelSettings using the Options pattern
builder.Configuration.AddInMemoryCollection(new Dictionary<string, string>
{
    { "PanelSettings:IsMasterPanel", "true" }
});

var isMasterPanel = builder.Configuration.GetValue<bool>("PanelSettings:IsMasterPanel");

var baseUrl = isMasterPanel
    ? "https://api.instantpay.in"
    : "https://api.nwsglobal.in";

builder.Configuration.AddInMemoryCollection(new Dictionary<string, string>
{
    { "APISetting:BaseUrl", baseUrl }
});

builder.Services.Configure<PanelSettings>(builder.Configuration.GetSection("PanelSettings"));
builder.Services.Configure<ApiSetting>(builder.Configuration.GetSection("APISetting"));
builder.Services.Configure<GoterPayConfig>(builder.Configuration.GetSection("GoterPay"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("corspolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// -----------------------------------------------------
// Database (Dapper)
builder.Services.AddTransient<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

// -----------------------------------------------------
// JWT Authentication
// Add configuration
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettingDto>(jwtSettings);

// Add authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("JwtSettings");

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
    };
});

builder.Services.AddAuthorization();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

// -----------------------------------------------------
// MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly); // API Layer
    cfg.RegisterServicesFromAssembly(typeof(CreateUserHandler).Assembly); // Application Layer
});

// Sctutor 
builder.Services.Scan(scan => scan
    .FromAssembliesOf(typeof(UserRepository))
    .AddClasses()
    .AsImplementedInterfaces()
    .WithScopedLifetime());

// -----------------------------------------------------
// Swagger + JWT Auth UI
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter JWT token only (no 'Bearer' prefix required).",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FinTech API Panel",
        Version = "v1"
    });
});

// -----------------------------------------------------
// Build & Run
// -----------------------------------------------------
var app = builder.Build();

// Logging to file
var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
loggerFactory.AddFile("Logs/mylog-{Date}.txt");

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FinTech API V1");
    });
}
//comment out the above lines if you want to disable Swagger in production
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FinTech API V1");
});

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
    }
});

app.UseCors("corspolicy");

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Custom Middleware for Admin User Seeding
await AdminSeeder.SeedAdminUserAsync(app.Services);

app.Run();