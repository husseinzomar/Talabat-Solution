using AutoMapper;
using Talabat.API.Dtos;
using Talabat.DAL.Entities;
using Talabat.DAL.Entities.Order;

namespace Talabat.API.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
            CreateMap<DAL.Entities.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<AddressDto, DAL.Entities.Order.Address>();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, O => O.MapFrom(S => S.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryCost, O => O.MapFrom(S => S.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, O => O.MapFrom(S => S.ItemOrdered.ProductItemId))
                .ForMember(d => d.ProductName, O=> O.MapFrom(S => S.ItemOrdered.ProductItemName))
                .ForMember(d => d.PictureUrl, O=> O.MapFrom(S => S.ItemOrdered.PictureUrl))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>());

        }
    }
}
