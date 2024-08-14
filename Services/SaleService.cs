using basic_delivery_api.Domain.Models;
using basic_delivery_api.Domain.Services;
using basic_delivery_api.Repositories;
using basic_delivery_api.Responses;

namespace basic_delivery_api.Services;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductService _productService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IShippingService _shippingService;

    public SaleService(ISaleRepository saleRepository, IProductService productService, IUnitOfWork unitOfWork, IShippingService shippingService)
    {
        _saleRepository = saleRepository;
        _productService = productService;
        _unitOfWork = unitOfWork;
        _shippingService = shippingService;
    }
    
    public async Task<IEnumerable<Sale>> ListAsync()
    {
        return await _saleRepository.ListAsync();
    }
    
    public async Task<Sale?> FindByIdAsync(int id)
    {
        return await _saleRepository.FindByIdAsync(id);
    }

    public async Task<CreateSaleResponse> Create(Sale sale, string zipCode)
    {
        try
        {
            var productIds = sale.SaleItems.Select(si => si.ProductId).Distinct();
            var products = await _productService.GetProductsByIdsAsync(productIds);
            
            foreach (var item in sale.SaleItems)
            {
                var product = products.Single(p => p.Id == item.ProductId);
                item.UnitPrice = product.Price;
            }

            sale.ShippingCost = await _shippingService.CalculateShippingCostAsync(zipCode);
            sale.CalculateTotalAmount();

            await _saleRepository.AddAsync(sale);
            await _unitOfWork.CompleteAsync();
            return new CreateSaleResponse(sale);
        }
        catch (Exception ex)
        {
            return new CreateSaleResponse($"An error occurred while creating the sale: {ex.Message}");
        }
    }

    public async Task<DeleteSaleResponse> Delete(int id)
    {
        var existingSale = await _saleRepository.FindByIdAsync(id);
        if (existingSale == null)
            return new DeleteSaleResponse("Sale not found.");

        try
        {
            _saleRepository.Remove(existingSale);
            await _unitOfWork.CompleteAsync();
            return new DeleteSaleResponse(existingSale);
        }
        catch (Exception ex)
        {
            return new DeleteSaleResponse($"An error occurred while deleting the sale: {ex.Message}");
        }
    }
}