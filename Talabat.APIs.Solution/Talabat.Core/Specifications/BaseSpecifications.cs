﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; } = null;
        public List<Expression<Func<T, object>>> Includes { get; set ; } = new List<Expression<Func<T, object>>> ();
        public Expression<Func<T, object>> OrderBy { get; set; } = null;
        public Expression<Func<T, object>> OrderByDesc { get; set; } = null;

        public int Skip { get; set; }
        public int Take { get; set; }

        public bool  IsPaginationEnabled { get; set; }




        // this constructor for Get all Products
        public BaseSpecifications()
        {
            // Criteria =null
            //Includes= new List<Expression<Func<T, object>>>();
        }
        // this constructor Get Product By Id
        public BaseSpecifications(Expression<Func<T, bool>> criteriaExpression )
        {
            Criteria = criteriaExpression;
            //Includes= new List<Expression<Func<T, object>>>();
        }

        public void AddOrderBy(  Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        public void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDesc = orderByDescExpression;
        }


        public  void ApplyPagination(int skip , int take)
        {
            IsPaginationEnabled = true;
            Skip = skip;
            Take = take;
        }

    }
}
