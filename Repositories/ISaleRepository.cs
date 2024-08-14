using basic_delivery_api.Domain.Models;

namespace basic_delivery_api.Repositories;

public interface ISaleRepository
{
    Task<IEnumerable<Sale>> ListAsync();
    Task<Sale?> FindByIdAsync(int id);
    Task AddAsync(Sale sale);
    void Remove(Sale sale);
}