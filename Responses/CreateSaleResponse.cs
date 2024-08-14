using basic_delivery_api.Domain.Models;

namespace basic_delivery_api.Responses;

public class CreateSaleResponse: BaseResponse
{
    public Sale Sale { get; private set; }

    private CreateSaleResponse(bool success, string message, Sale sale) : base(success, message)
    {
        Sale = sale;
    }
    
    public CreateSaleResponse(Sale sale) : this(true, string.Empty, sale)
    { }
    
    public CreateSaleResponse(string message) : this(false, message, null)
    { }
}