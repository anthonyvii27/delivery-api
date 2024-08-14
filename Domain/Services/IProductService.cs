using basic_delivery_api.Domain.Models;
using basic_delivery_api.Responses;

namespace basic_delivery_api.Domain.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> ListAsync();
        Task<Product?> FindByIdAsync(int id);
        Task<CreateProductResponse> Create(Product product);
        Task<UpdateProductResponse> Update(int id, Product product);
        Task<DeleteProductResponse> Delete(int id);
        Task<bool> HasAssociatedSalesAsync(int productId);
        Task<IEnumerable<Product>> GetProductsByIdsAsync(IEnumerable<int> productIds);
    }
}