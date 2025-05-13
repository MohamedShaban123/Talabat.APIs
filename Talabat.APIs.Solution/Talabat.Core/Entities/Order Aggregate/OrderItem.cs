using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregate
{
    public class OrderItem : BaseEntity
    {
        public ProdcutItemOrdered Prodcut {  get; set; }
        public decimal Price { get; set; }
        public int Quantity  { get; set; }

        public OrderItem()
        {
            
        }
        // Generated Constructor
        public OrderItem(ProdcutItemOrdered prodcut, decimal price, int quantity)
        {
            Prodcut = prodcut;
            Price = price;
            Quantity = quantity;
        }


    }
}
