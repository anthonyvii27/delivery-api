namespace basic_delivery_api.Domain.Services;

public interface IShippingService
{
    Task<decimal> CalculateShippingCostAsync(string zipCode);
}