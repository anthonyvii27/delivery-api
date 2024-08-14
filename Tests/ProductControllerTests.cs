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

public class ProductsControllerTests
{
    private readonly ProductsController _controller;
    private readonly Mock<IProductService> _mockProductService;
    private readonly IMapper _mapper;

    public ProductsControllerTests()
    {
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
        _controller = new ProductsController(_mockProductService.Object, _mapper);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithListOfProducts()
    {
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Product 1", UnitOfMeasurement = EUnitOfMeasurement.Kilogram},
            new Product { Id = 2, Name = "Product 2", UnitOfMeasurement = EUnitOfMeasurement.Unity}
        };

        _mockProductService.Setup(s => s.ListAsync()).ReturnsAsync(products);

        var result = await _controller.GetAll();

        var actionResult = Assert.IsType<ActionResult<IEnumerable<ProductRequest>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnProducts = Assert.IsAssignableFrom<IEnumerable<ProductRequest>>(okResult.Value);
        Assert.Equal(products.Count, returnProducts.Count());
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WithProductRequest()
    {
        var product = new Product { Id = 1, Name = "Product 1", UnitOfMeasurement = EUnitOfMeasurement.Kilogram };
        _mockProductService.Setup(s => s.FindByIdAsync(product.Id)).ReturnsAsync(product);

        var result = await _controller.GetById(product.Id);

        var actionResult = Assert.IsType<ActionResult<ProductRequest>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnProduct = Assert.IsType<ProductRequest>(okResult.Value);
        Assert.Equal(product.Id, returnProduct.Id);
        Assert.Equal(product.Name, returnProduct.Name);
    }

    [Fact]
    public async Task Update_ReturnsOkResult_WithProductRequest()
    {
        var updateProductRequest = new UpdateProductRequest { Name = "Updated Product", UnitOfMeasurement = EUnitOfMeasurement.Kilogram };
        var product = new Product { Id = 1, Name = "Product 1", UnitOfMeasurement = EUnitOfMeasurement.Kilogram };
        _mockProductService.Setup(s => s.FindByIdAsync(product.Id)).ReturnsAsync(product);
        _mockProductService.Setup(s => s.Update(product.Id, product)).ReturnsAsync(new UpdateProductResponse(product));

        var result = await _controller.Update(product.Id, updateProductRequest);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnProduct = Assert.IsType<ProductRequest>(okResult.Value);
        Assert.Equal(product.Name, returnProduct.Name);
    }

    [Fact]
    public async Task Delete_ReturnsNoContentResult()
    {
        var product = new Product { Id = 1, Name = "Product 1", UnitOfMeasurement = EUnitOfMeasurement.Kilogram };
        _mockProductService.Setup(s => s.Delete(product.Id)).ReturnsAsync(new DeleteProductResponse(product));

        var result = await _controller.Delete(product.Id);

        Assert.IsType<NoContentResult>(result);
    }
}