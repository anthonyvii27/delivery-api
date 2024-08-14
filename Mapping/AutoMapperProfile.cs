using AutoMapper;
using basic_delivery_api.Domain.Models;
using basic_delivery_api.Dto;
using basic_delivery_api.Extensions;
using basic_delivery_api.Resources;

namespace basic_delivery_api.Mapping;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(src => src.UnitOfMeasurement,
                opt => opt.MapFrom(src => src.UnitOfMeasurement.ToDescriptionString()));
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
        CreateMap<Sale, SaleDto>()
            .ForMember(src => src.SaleItems, opt => opt.MapFrom(src => src.SaleItems))
            .ReverseMap();
        CreateMap<SaleItem, SaleItemDto>()
            .ForMember(src => src.Product, opt => opt.MapFrom(src => src.Product))
            .ReverseMap();
        CreateMap<CreateSaleDto, Sale>();
        CreateMap<CreateSaleItemDto, SaleItem>();
        CreateMap<UpdateSaleDto, Sale>();
        CreateMap<UpdateSaleItemDto, SaleItem>();
    }
}