using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class Product :BaseEntity
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        


        //[ForeignKey(nameof(Product.Brand))]
        public int BrandId { get; set; } // Foreign key Column =>productbrand
        public ProductBrand Brand { get; set; } // Navigation property => One , 

        public int CategoryId { get; set; }// Foreign key Column =>productCategory


        public ProductCategory Category { get; set; }// Navigation property => One

    }
}
