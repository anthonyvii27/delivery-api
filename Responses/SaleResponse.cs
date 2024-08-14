using basic_delivery_api.Domain.Models;

namespace basic_delivery_api.Responses;

public class SaleResponse
{
    public bool Success { get; private set; }
    public string Message { get; private set; }
    public Sale Sale { get; private set; }

    private SaleResponse(bool success, string message, Sale sale)
    {
        Success = success;
        Message = message;
        Sale = sale;
    }

    public static SaleResponse SuccessResponse(Sale sale) =>
        new SaleResponse(true, string.Empty, sale);

    public static SaleResponse FailureResponse(string message) =>
        new SaleResponse(false, message, null);
}