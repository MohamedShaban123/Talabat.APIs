using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public interface ISpecifications<T>  where T: BaseEntity 
    {
        // where
        public Expression<Func<T,bool>> Criteria { get; set; }
        //inlcudes
        public List<Expression<Func<T,Object>>> Includes { get; set; }


       // 2 new spec for (OrderBy ,OrderByDescending)
       public Expression<Func<T,object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }


        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }



    }
}
