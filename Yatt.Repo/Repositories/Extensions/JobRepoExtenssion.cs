using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yatt.Models.Entities;
using System.Linq.Dynamic.Core;

namespace Yatt.Repo.Repositories.Extensions
{
    public static class JobRepoExtenssion
    {
        public static IQueryable<Job> Search(this IQueryable<Job> Jobs, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return Jobs;
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return Jobs.Where(p => p.Title!.ToLower().Contains(lowerCaseSearchTerm)
            || p.Salary.ToString().Contains(lowerCaseSearchTerm)
            || p.Level.ToString().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<Job> Sort(this IQueryable<Job> Jobs, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return Jobs.OrderBy(e => e.Title);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Job).GetProperties(BindingFlags.Public | BindingFlags.Instance);
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
                return Jobs.OrderBy(e => e.Title);

            return Jobs.OrderBy(orderQuery);
        }
    }
}
