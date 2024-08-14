using System.Text.Json;
using basic_delivery_api.Domain.Models;
using basic_delivery_api.Domain.Services;
using basic_delivery_api.Responses;

namespace basic_delivery_api.Services;

public class ShippingService : IShippingService
{
    private const string StoreCity = "Rio de Janeiro";
    private const string StoreState = "RJ";

    private const decimal LocalShippingCost = 10.00m;
    private const decimal StateShippingCost = 20.00m;
    private const decimal DefaultShippingCost = 40.00m;

    private readonly HttpClient _httpClient;
    private readonly string _brasilApiBaseUrl;

    public ShippingService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _brasilApiBaseUrl = configuration["BrasilApi:BaseUrl"];
    }

    public async Task<decimal> CalculateShippingCostAsync(string zipCode)
    {
        var location = await GetLocationByZipCodeAsync(zipCode);
        if (location == null)
            return DefaultShippingCost;

        if (location.State == StoreState)
        {
            if (location.City == StoreCity)
                return LocalShippingCost;
            return StateShippingCost;
        }
        return DefaultShippingCost;
    }

    private async Task<Location?> GetLocationByZipCodeAsync(string zipCode)
    {
        var response = await _httpClient.GetAsync($"{_brasilApiBaseUrl}{zipCode}");

        if (!response.IsSuccessStatusCode)
            return null;

        var jsonString = await response.Content.ReadAsStringAsync();
        var locationResponse = JsonSerializer.Deserialize<LocationResponse>(jsonString, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        return locationResponse == null ? null : new Location
        {
            City = locationResponse.City,
            State = locationResponse.State
        };
    }
}