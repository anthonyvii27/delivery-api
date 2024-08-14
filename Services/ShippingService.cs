using System.Text.Json;
using basic_delivery_api.Domain.Services;

public class ShippingService : IShippingService
{
    private const string StoreCity = "Rio de Janeiro";
    private const string StoreState = "RJ";

    private readonly HttpClient _httpClient;

    public ShippingService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<decimal> CalculateShippingCostAsync(string zipCode)
    {
        var location = await GetLocationByZipCodeAsync(zipCode);
        if (location == null)
            return 40.00m; // Default to R$ 40.00 if location not found

        if (location.State == StoreState)
        {
            if (location.City == StoreCity)
                return 10.00m; // Same city
            return 20.00m; // Same state, other cities
        }
        return 40.00m; // Other states
    }

    private async Task<Location> GetLocationByZipCodeAsync(string zipCode)
    {
        var response = await _httpClient.GetAsync($"https://brasilapi.com.br/api/cep/v2/{zipCode}");

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

public class Location
{
    public string City { get; set; }
    public string State { get; set; }
}

public class LocationResponse
{
    public string City { get; set; }
    public string State { get; set; }
}