using basic_delivery_api.Domain.Models;

namespace basic_delivery_api.Domain.Services.Communication;

public class UpdateProductResponse: BaseResponse
{
    public Product Product { get; private set; }

    private UpdateProductResponse(bool success, string message, Product product) : base(success, message)
    {
        Product = product;
    }
    
    public UpdateProductResponse(Product product) : this(true, string.Empty, product)
    { }
    
    public UpdateProductResponse(string message) : this(false, message, null)
    { }
}