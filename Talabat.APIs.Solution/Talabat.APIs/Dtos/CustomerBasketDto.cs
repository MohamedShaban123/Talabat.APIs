using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.Dtos
{
    public class CustomerBasketDto
    {
        [Required]  // contain default id message ok 
        public string Id { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
        public List<BasketItemDto> Items { get; set; }
    }
}
