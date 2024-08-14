using basic_delivery_api.Domain.Models;
using basic_delivery_api.Responses;

namespace basic_delivery_api.Domain.Services
{
    public interface ISaleService
    {
        Task<IEnumerable<Sale>> ListAsync();
        Task<Sale> FindByIdAsync(int id);
        Task<SaleResponse> Create(Sale sale);
        Task<SaleResponse> Update(int id, Sale sale);
        Task<SaleResponse> Delete(int id);
    }
}