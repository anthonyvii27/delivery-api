using basic_delivery_api.Domain.Models;
using basic_delivery_api.Responses;

namespace basic_delivery_api.Domain.Services
{
    public interface ISaleService
    {
        Task<IEnumerable<Sale>> ListAsync();
        Task<Sale?> FindByIdAsync(int id);
        Task<CreateSaleResponse> Create(Sale sale);
        Task<DeleteSaleResponse> Delete(int id);
    }
}