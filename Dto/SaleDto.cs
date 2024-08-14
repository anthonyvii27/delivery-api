using basic_delivery_api.Resources;

namespace basic_delivery_api.Dto;

public class SaleDto
{
    public int Id { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal TotalAmount { get; set; }
    public IEnumerable<SaleItemDto> SaleItems { get; set; }
}

public class SaleItemDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public ProductDto Product { get; set; } 
}