using basic_delivery_api.Domain.Models;

namespace basic_delivery_api.Responses;

public class SaleResponse: BaseResponse
{
    public Sale Sale { get; private set; }

    private SaleResponse(bool success, string message, Sale sale) : base(success, message)
    {
        Sale = sale;
    }
    
    public SaleResponse(Sale sale) : this(true, string.Empty, sale)
    { }
    
    public SaleResponse(string message) : this(false, message, new Sale())
    { }
}