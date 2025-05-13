using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    internal class SpecificationsEvaluator <TEntity> where TEntity:BaseEntity 
    {

        // IQueryable<TEntity> inputQuery : DbSet like _dbContext.Set<Product>()
        // ISpecifications<TEntity> spec  : object that implement ISpecifications<TEntity>
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity> spec)
        {
            var query = inputQuery; //_dbContext.Set<Product>()
            // this if for check if the user need to get specific product or not
            if(spec.Criteria != null) // P=>P.Id==1
                query = query.Where(spec.Criteria);//query=_dbContext.Set<Product>().Where(P=>P.Id == 1)

            //  // this if for check if the user need to orderby or not
            if (spec.OrderBy != null)
                query = query.OrderBy(spec.OrderBy);
            else if(spec.OrderByDesc != null)
                query = query.OrderByDescending(spec.OrderByDesc);

            if(spec.IsPaginationEnabled )
                query = query.Skip(spec.Skip).Take(spec.Take);
            //query = _dbContext.Set<Product>().Where();
            // now we need to accomulate 2 includes to where (use aggregate function)
            //Includes
            // 1.P=>P.Brand
            // 2.P=>P.Category

            // currentquery = _dbContext.Set<Product>().Where(P=>P.Id==1).Include(1.P=>P.Brand)
            // currentquery = _dbContext.Set<Product>().Where(P=>P.Id==1).Include(1.P=>P.Brand).Include(1.P=>P.Category)
            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
            return query;
        }

    }
}
