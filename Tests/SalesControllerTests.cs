using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using basic_delivery_api.Controllers;
using basic_delivery_api.Domain.Models;
using basic_delivery_api.Domain.Services;
using basic_delivery_api.Extensions;
using basic_delivery_api.Requests;
using basic_delivery_api.Responses;

public class SalesControllerTests
{
    private readonly SalesController _controller;
    private readonly Mock<ISaleService> _mockSaleService;
    private readonly Mock<IShippingService> _mockShippingService;
    private readonly Mock<IProductService> _mockProductService;
    private readonly IMapper _mapper;

    public SalesControllerTests()
    {
        _mockSaleService = new Mock<ISaleService>();
        _mockShippingService = new Mock<IShippingService>();
        _mockProductService = new Mock<IProductService>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Product, ProductRequest>()
                .ForMember(src => src.UnitOfMeasurement,
                    opt => opt.MapFrom(src => src.UnitOfMeasurement.ToDescriptionString()));
            cfg.CreateMap<CreateProductRequest, Product>();
            cfg.CreateMap<UpdateProductRequest, Product>();
            cfg.CreateMap<Sale, SaleRequest>()
                .ForMember(src => src.SaleItems, opt => opt.MapFrom(src => src.SaleItems))
                .ReverseMap();
            cfg.CreateMap<SaleItem, SaleItemRequest>()
                .ForMember(src => src.Product, opt => opt.MapFrom(src => src.Product))
                .ReverseMap();
            cfg.CreateMap<CreateSaleRequest, Sale>();
            cfg.CreateMap<CreateSaleItemRequest, SaleItem>();
        });

        _mapper = config.CreateMapper();
        _controller = new SalesController(_mockSaleService.Object, _mockShippingService.Object, _mockProductService.Object, _mapper);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithListOfSales()
    {
        var sales = new List<Sale> { new Sale { Id = 1 }, new Sale { Id = 2 } };
        _mockSaleService.Setup(s => s.ListAsync()).ReturnsAsync(sales);

        var result = await _controller.GetAll();

        var actionResult = Assert.IsType<ActionResult<IEnumerable<SaleRequest>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnSales = Assert.IsAssignableFrom<IEnumerable<SaleRequest>>(okResult.Value);
        Assert.Equal(sales.Count, returnSales.Count());
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WithSaleRequest()
    {
        var sale = new Sale { Id = 1 };
        _mockSaleService.Setup(s => s.FindByIdAsync(sale.Id)).ReturnsAsync(sale);

        var result = await _controller.GetById(sale.Id);

        var actionResult = Assert.IsType<ActionResult<SaleRequest>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnSale = Assert.IsType<SaleRequest>(okResult.Value);
        Assert.Equal(sale.Id, returnSale.Id);
    }

    [Fact]
    public async Task Delete_ReturnsNoContentResult()
    {
        var sale = new Sale { Id = 1 };
        _mockSaleService.Setup(s => s.Delete(sale.Id)).ReturnsAsync(new DeleteSaleResponse(sale));

        var result = await _controller.Delete(sale.Id);

        Assert.IsType<NoContentResult>(result);
    }
}
