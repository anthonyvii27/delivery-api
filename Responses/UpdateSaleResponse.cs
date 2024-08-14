using basic_delivery_api.Domain.Models;

namespace basic_delivery_api.Responses;

public class UpdateSaleResponse: BaseResponse
{
    public Sale Sale { get; private set; }

    private UpdateSaleResponse(bool success, string message, Sale sale) : base(success, message)
    {
        Sale = sale;
    }
    
    public UpdateSaleResponse(Sale sale) : this(true, string.Empty, sale)
    { }
    
    public UpdateSaleResponse(string message) : this(false, message, null)
    { }
}