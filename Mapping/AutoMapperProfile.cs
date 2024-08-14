using AutoMapper;
using basic_delivery_api.Domain.Models;
using basic_delivery_api.Extensions;
using basic_delivery_api.Requests;

namespace basic_delivery_api.Mapping;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Product, ProductRequest>()
            .ForMember(src => src.UnitOfMeasurement,
                opt => opt.MapFrom(src => src.UnitOfMeasurement.ToDescriptionString()));
        CreateMap<CreateProductRequest, Product>();
        CreateMap<UpdateProductRequest, Product>();
        CreateMap<Sale, SaleRequest>()
            .ForMember(src => src.SaleItems, opt => opt.MapFrom(src => src.SaleItems))
            .ReverseMap();
        CreateMap<SaleItem, SaleItemRequest>()
            .ForMember(src => src.Product, opt => opt.MapFrom(src => src.Product))
            .ReverseMap();
        CreateMap<CreateSaleRequest, Sale>();
        CreateMap<CreateSaleItemRequest, SaleItem>();
    }
}