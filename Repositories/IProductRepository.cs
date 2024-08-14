using basic_delivery_api.Domain.Models;

namespace basic_delivery_api.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> ListAsync();
    Task AddAsync(Product product);
    Task<Product> FindByIdAsync(int id);
    void Update(Product product);
    void Remove(Product product);
}