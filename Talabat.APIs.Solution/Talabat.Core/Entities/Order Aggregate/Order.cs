using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregate
{
    public class Order : BaseEntity
    {
        public Order()
        {
            
        }
     
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Pending; 
        public Address ShippingAddress { get; set; }   // Note Map  into table in Database


        //public int DeliveryMethodId { get; set; } // Foreign key

        public DeliveryMethod DeliveryMethod { get; set; }  // Navigational Property [One]

        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>(); // Navigational Property [Many]

        public decimal SubTotal { get; set; }

        ////[NotMapped]
        ////public decimal Total => SubTotal + DeliveryMethod.Cost;     
        

        // THis Derived Attribute
        public decimal GetTotal()
            => SubTotal + DeliveryMethod.Cost;


        public string PaymentIntentId { get; set; } 



        // Generated Constructor
        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal , string paymentIntentId )
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
            
        }


    }
}