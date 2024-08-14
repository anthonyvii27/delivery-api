using basic_delivery_api.Domain.Models;
using basic_delivery_api.Domain.Services;
using basic_delivery_api.Responses;
using basic_delivery_api.Repositories;

namespace basic_delivery_api.Services
{
    public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SaleService(ISaleRepository saleRepository, IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _saleRepository = saleRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<Sale>> ListAsync()
    {
        return await _saleRepository.ListAsync();
    }
    
    public async Task<Sale> FindByIdAsync(int id)
    {
        return await _saleRepository.FindByIdAsync(id);
    }

    public async Task<SaleResponse> Create(Sale sale)
    {
        try
        {
            sale.CalculateTotalAmount();

            await _saleRepository.AddAsync(sale);
            await _unitOfWork.CompleteAsync();
            return SaleResponse.SuccessResponse(sale);
        }
        catch (Exception ex)
        {
            return SaleResponse.FailureResponse($"An error occurred while creating the sale: {ex.Message}");
        }
    }

    public async Task<SaleResponse> Update(int id, Sale sale)
    {
        var existingSale = await _saleRepository.FindByIdAsync(id);
        if (existingSale == null)
            return SaleResponse.FailureResponse("Sale not found.");

        try
        {
            existingSale.SaleDate = sale.SaleDate;
            existingSale.ShippingCost = sale.ShippingCost;
            existingSale.ZipCode = sale.ZipCode;
            existingSale.SaleItems = sale.SaleItems;

            existingSale.CalculateTotalAmount();

            _saleRepository.Update(existingSale);
            await _unitOfWork.CompleteAsync();
            return SaleResponse.SuccessResponse(existingSale);
        }
        catch (Exception ex)
        {
            return SaleResponse.FailureResponse($"An error occurred while updating the sale: {ex.Message}");
        }
    }

    public async Task<SaleResponse> Delete(int id)
    {
        var existingSale = await _saleRepository.FindByIdAsync(id);
        if (existingSale == null)
            return SaleResponse.FailureResponse("Sale not found.");

        try
        {
            _saleRepository.Remove(existingSale);
            await _unitOfWork.CompleteAsync();
            return SaleResponse.SuccessResponse(existingSale);
        }
        catch (Exception ex)
        {
            return SaleResponse.FailureResponse($"An error occurred while deleting the sale: {ex.Message}");
        }
    }
}

}
