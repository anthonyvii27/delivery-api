namespace basic_delivery_api.Requests;

public class SaleRequest
{
    public int Id { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal TotalAmount { get; set; }
    public IEnumerable<SaleItemRequest> SaleItems { get; set; }
}

public class SaleItemRequest
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public ProductRequest Product { get; set; } 
}