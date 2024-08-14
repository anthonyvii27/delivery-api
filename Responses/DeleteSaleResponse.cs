using basic_delivery_api.Domain.Models;

namespace basic_delivery_api.Responses;

public class DeleteSaleResponse: BaseResponse
{
    public Sale Sale { get; private set; }

    private DeleteSaleResponse(bool success, string message, Sale sale) : base(success, message)
    {
        Sale = sale;
    }
    
    public DeleteSaleResponse(Sale sale) : this(true, string.Empty, sale)
    { }
    
    public DeleteSaleResponse(string message) : this(false, message, new Sale())
    { }
}