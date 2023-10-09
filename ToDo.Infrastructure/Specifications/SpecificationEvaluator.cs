using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Models;
using ToDo.Domain.Specifications;
using ToDo.Infrastructure.DI;

namespace ToDo.Infrastructure.Specifications
{
    internal class SpecificationEvaluator<T> where T : BaseModel
    {
        public static (IQueryable<T> data, int count) GetQuery(IQueryable<T> inputQuery,
            BaseSpecifications<T> specifications)
        {
            var query = inputQuery;
            int Count = 0;

            if(specifications.Criteria.Count > 0)
                query = query.Where(specifications.Criteria.Aggregate((x, y) => x.And(y)));

            if (specifications.OrderBy != null)
                query = query.OrderBy(specifications.OrderBy);

            if (specifications.OrderByDescending != null)
                query = query.OrderByDescending(specifications.OrderByDescending);

            if (specifications.IsTotalCountEnable)
                Count = query.Count();

            if (specifications.IsPagingEnabled)
                query = query.Skip(specifications.Skip).Take(specifications.Take);

            query = specifications.Includes
                .Aggregate(query, (currentQuery, include) => currentQuery.Include(include));

            return (query, Count);
        }
    }
}
