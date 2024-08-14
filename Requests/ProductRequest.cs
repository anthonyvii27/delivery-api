namespace basic_delivery_api.Requests;

public class ProductRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? UnitOfMeasurement { get; set; }
    public decimal Price { get; set; }
}