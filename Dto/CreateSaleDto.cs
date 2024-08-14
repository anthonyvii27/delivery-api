namespace basic_delivery_api.Dto;

public class CreateSaleDto
{
    public DateTime SaleDate { get; set; }
    public decimal TotalAmount { get; set; }
    public IEnumerable<CreateSaleItemDto> SaleItems { get; set; }
}

public class CreateSaleItemDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}