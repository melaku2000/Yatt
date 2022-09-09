using System.Reflection;
using System.Text;
using Yatt.Models.Entities;
using System.Linq.Dynamic.Core;

namespace Yatt.Api.Repo.Repositories.Extensions
{
    public static class CompanyRepositoryExtenssion
    {
        public static IQueryable<CompanyDetail> Search(this IQueryable<CompanyDetail> companies, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return companies;
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return companies.Where(p => p.CompanyName!.ToLower().Contains(lowerCaseSearchTerm)
            || p.Company.CompanyTin!.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<CompanyDetail> Sort(this IQueryable<CompanyDetail> companies, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return companies.OrderBy(e => e.CompanyName);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(CompanyDetail).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name} {direction}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            if (string.IsNullOrWhiteSpace(orderQuery))
                return companies.OrderBy(e => e.CompanyName);

            return companies.OrderBy(orderQuery);
        }
    }
}
