using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;

namespace FinTech_ApiPanel.Domain.Shared.SortHelper
{
    public static class SortHelper
    {
        public static IQueryable<T> ApplySort<T>(IQueryable<T> entities, string orderByQueryString)
        {
            if (!entities.Any())
                return entities;

            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return entities;

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi =>
                    pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var sortingOrder = param.EndsWith(" desc", StringComparison.OrdinalIgnoreCase) ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name} {sortingOrder}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            return string.IsNullOrWhiteSpace(orderQuery)
                ? entities
                : entities.OrderBy(orderQuery); // Using System.Linq.Dynamic.Core
        }
    }
}
