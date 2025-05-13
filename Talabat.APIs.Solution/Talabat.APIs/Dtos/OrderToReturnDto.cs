using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.APIs.Dtos
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } 
        public OrderStatus Status { get; set; } 
        public Address ShippingAddress { get; set; }   // Note Map  into table in Database


        public string DeliveryMethod { get; set; }  // Navigational Property [One]
        public decimal DeliveryMethodCost { get; set; }  // Navigational Property [One]

        public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>(); // Navigational Property [Many]

        public decimal SubTotal { get; set; }



        public decimal Total { get; set; }


        public string PaymentIntentId { get; set; } = string.Empty;


 
    }
}
