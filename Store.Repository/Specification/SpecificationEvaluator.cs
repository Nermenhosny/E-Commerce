using Microsoft.EntityFrameworkCore;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specification
{
    public class SpecificationEvaluator<TEntity,TKey>where TEntity:BaseEntity<TKey>
    {
        public static IQueryable<TEntity>GetQuery(IQueryable <TEntity>inputQuery ,ISpecification<TEntity>Specs)
        {
            var query = inputQuery;
            if(Specs.Criteria is not null)
                query=query.Where(Specs.Criteria);

            if (Specs.OrderBy is not null)
                query = query.OrderBy(Specs.OrderBy);

            if (Specs.OrderByDescending is not null)
                query = query.OrderByDescending(Specs.OrderByDescending);

            if(Specs.IsPaginated)
                query = query.Skip(Specs.Skip).Take(Specs.Take);    

            query = Specs.Includes.Aggregate(query, (current, includeExpression) => current.Include(includeExpression));
            return query;
        }
    }
}
