using System.Reflection;
using System.Text;
using Yatt.Models.Entities;
using System.Linq.Dynamic.Core;

namespace Yatt.Repo.Repositories.Extensions
{
    public static class CandidateRepositoryExtenssion
    {
        public static IQueryable<Candidate> Search(this IQueryable<Candidate> candidates, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return candidates;
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return candidates.Where(p => p.FirstName!.ToLower().Contains(lowerCaseSearchTerm)
            || p.User!.Email!.ToLower().Contains(lowerCaseSearchTerm)
            || p.MobilePhone!.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<Candidate> Sort(this IQueryable<Candidate> candidates, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return candidates.OrderBy(e => e.FirstName);

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
                return candidates.OrderBy(e => e.FirstName);

            return candidates.OrderBy(orderQuery);
        }
    }
}
