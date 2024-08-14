namespace basic_delivery_api.Requests;

public class CreateSaleItemRequest
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}

public class CreateSaleRequest
{
    public DateTime SaleDate { get; set; }
    public string ZipCode { get; set; }
    public IEnumerable<CreateSaleItemRequest> SaleItems { get; set; }
}