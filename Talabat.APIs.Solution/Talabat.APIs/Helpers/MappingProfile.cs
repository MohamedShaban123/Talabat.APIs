using AutoMapper;
using System.Drawing;
using Talabat.APIs.Dtos;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.APIs.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping  from Product To ProductToReturnDto
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(destination => destination.Brand, Options => Options.MapFrom(source => source.Brand.Name))
                .ForMember(destination => destination.Category, Options => Options.MapFrom(source => source.Category.Name))
                .ForMember(destination=> destination.PictureUrl , Options =>Options.MapFrom<ProductPictureUrlResolver>() );



            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();

            CreateMap<AddressDto,Core.Entities.Order_Aggregate.Address>();

            CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();


            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d=>d.DeliveryMethod , O=>O.MapFrom(s=>s.DeliveryMethod.ShortName))
                 .ForMember(d=>d.DeliveryMethodCost,O=>O.MapFrom(s=>s.DeliveryMethod.Cost));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d=>d.ProductName,O=>O.MapFrom(s=>s.Prodcut.ProductName))
                 .ForMember(d=>d.ProductId,O=>O.MapFrom(s=>s.Prodcut.ProductId))
                 .ForMember(d=>d.PictureUrl,O=>O.MapFrom(s=>s.Prodcut.PictureUrl))
                 .ForMember(d=>d.PictureUrl,O=>O.MapFrom<OrderItemPictureUrlResolver>());


        }   
    }
}
