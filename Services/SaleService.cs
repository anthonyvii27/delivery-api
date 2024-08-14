using basic_delivery_api.Domain.Models;
using basic_delivery_api.Domain.Services;
using basic_delivery_api.Responses;
using basic_delivery_api.Repositories;

namespace basic_delivery_api.Services;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SaleService(ISaleRepository saleRepository, IUnitOfWork unitOfWork)
    {
        _saleRepository = saleRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<Sale>> ListAsync()
    {
        return await _saleRepository.ListAsync();
    }
    
    public async Task<Sale?> FindByIdAsync(int id)
    {
        return await _saleRepository.FindByIdAsync(id);
    }

    public async Task<CreateSaleResponse> Create(Sale sale)
    {
        try
        {
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