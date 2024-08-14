using basic_delivery_api.Domain.Models;

namespace basic_delivery_api.Domain.Services.Communication;

public class DeleteProductResponse: BaseResponse
{
    public Product Product { get; private set; }

    private DeleteProductResponse(bool success, string message, Product product) : base(success, message)
    {
        Product = product;
    }
    
    public DeleteProductResponse(Product product) : this(true, string.Empty, product)
    { }
    
    public DeleteProductResponse(string message) : this(false, message, null)
    { }
}