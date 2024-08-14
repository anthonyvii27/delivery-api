namespace basic_delivery_api.Domain.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public EUnitOfMeasurement UnitOfMeasurement { get; set; }
    public decimal Price { get; set; }
}