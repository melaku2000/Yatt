using System.Reflection;
using System.Text;
using Yatt.Models.Entities;
using System.Linq.Dynamic.Core;

namespace Yatt.Repo.Repositories.Extensions
{
    public static class AdminRepositoryExtenssion
    {
        public static IQueryable<Admin> Search(this IQueryable<Admin> admins, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return admins;
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return admins.Where(p => p.FirstName!.ToLower().Contains(lowerCaseSearchTerm)
            || p.User!.Email!.ToLower().Contains(lowerCaseSearchTerm)
            || p.MobilePhone!.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<Admin> Sort(this IQueryable<Admin> admins, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return admins.OrderBy(e => e.FirstName);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(User).GetProperties(BindingFlags.Public | BindingFlags.Instance);
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
                return admins.OrderBy(e => e.FirstName);

            return admins.OrderBy(orderQuery);
        }
    }
}
