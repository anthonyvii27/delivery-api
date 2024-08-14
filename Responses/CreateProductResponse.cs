using basic_delivery_api.Domain.Models;

namespace basic_delivery_api.Responses;

public class CreateProductResponse: BaseResponse
{
    public Product Product { get; private set; }

    private CreateProductResponse(bool success, string message, Product product) : base(success, message)
    {
        Product = product;
    }
    
    public CreateProductResponse(Product product) : this(true, string.Empty, product)
    { }
    
    public CreateProductResponse(string message) : this(false, message, null)
    { }
}