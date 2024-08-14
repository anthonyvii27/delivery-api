using basic_delivery_api.Domain.Models;
using basic_delivery_api.Domain.Services;
using basic_delivery_api.Domain.Services.Communication;
using basic_delivery_api.Repositories;

namespace basic_delivery_api.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Product>> ListAsync()
    {
        return await _productRepository.ListAsync();
    }
    
    public async Task<Product> FindByIdAsync(int id)
    {
        return await _productRepository.FindByIdAsync(id);
    }
    
    public async Task<CreateProductResponse> Create(Product product)
    {
        try
        {
            await _productRepository.AddAsync(product);
            await _unitOfWork.CompleteAsync();
            
            return new CreateProductResponse(product);
        }
        catch (Exception ex)
        {
            return new CreateProductResponse($"An error occurred: {ex.Message}");
        }
    }
            
    public async Task<UpdateProductResponse> Update(int id, Product product)
    {
        var currentProduct = await _productRepository.FindByIdAsync(id);

        if (currentProduct == null)
            return new UpdateProductResponse("Product not found.");
    
        try
        {
            _productRepository.Update(currentProduct);
            await _unitOfWork.CompleteAsync();

            return new UpdateProductResponse(currentProduct);
        }
        catch (Exception ex)
        {
            return new UpdateProductResponse($"An error occurred when updating the product: {ex.Message}");
        }
    }
            
    public async Task<DeleteProductResponse> Delete(int id)
    {
        var existingProduct = await _productRepository.FindByIdAsync(id);
    
        if (existingProduct == null)
            return new DeleteProductResponse("Product not found.");
    
        try
        {
            _productRepository.Remove(existingProduct);
            await _unitOfWork.CompleteAsync();
    
            return new DeleteProductResponse(existingProduct);
        }
        catch (Exception ex)
        {
            return new DeleteProductResponse($"An error occurred when deleting the product: {ex.Message}");
        }
    }
    
    public async Task<bool> HasAssociatedSalesAsync(int id)
    {
        return await _productRepository.HasAssociatedSalesAsync(id);
    }
    
    public async Task<IEnumerable<Product>> GetProductsByIdsAsync(IEnumerable<int> productIds)
    {
        return await _productRepository.GetProductsByIdsAsync(productIds);
    }
}