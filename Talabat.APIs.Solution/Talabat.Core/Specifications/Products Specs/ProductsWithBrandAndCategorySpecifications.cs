using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.Products_Specs;

namespace Talabat.Core.Products_Specs
{
    public class ProductsWithBrandAndCategorySpecifications : BaseSpecifications<Product>
    {
        //This Constructor  will be used  for creating an object , that will be used to get all Products
        public ProductsWithBrandAndCategorySpecifications( ProductSpecParams specParams ) 
             :base
            (P => 
             (string.IsNullOrEmpty(specParams.Search) || P.Name.ToLower().Contains(specParams.Search)) &&
             (!specParams.BrandId.HasValue || P.BrandId == specParams.BrandId.Value) && 
             (!specParams.CategoryId.HasValue || P.CategoryId == specParams.CategoryId.Value) 
             )


        {
             
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
            if(!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "priceAsc":
                        OrderBy= P=>P.Price;
                        AddOrderBy(P=>P.Price);
                        break;
                    case "priceDesc":
                        //OrderByDesc = P => P.Price;
                        AddOrderByDesc(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P=>P.Name);
                        break;      
                }

            }
            else
            {
                AddOrderBy(P => P.Name);
            }

            // Here we need to apply Pagination
            // total products = 18 ~ 20
            // page size = 5
            // page index =3
            ApplyPagination((specParams.PageIndex - 1)*specParams.PageSize,specParams.PageSize);


        }
        //This Constructor  will be used  for creating an object , that will be used to get a specific  product  
        public ProductsWithBrandAndCategorySpecifications(int id):base(P=>P.Id ==id)
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
        }

    
    }
}
