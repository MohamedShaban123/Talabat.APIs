using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public   class ProductCategory : BaseEntity
    {
        public string Name { get; set; }




        // we configure this relationship using fluent APIs
        //public ICollection<Product> Products { get; set; } =new HashSet<Product>();
    }
}
