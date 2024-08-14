namespace basic_delivery_api.Request
{
    public class UpdateSaleRequest
    {
        public DateTime SaleDate { get; set; }
        public decimal ShippingCost { get; set; }
        public string ZipCode { get; set; }
        public IEnumerable<UpdateSaleItemRequest> SaleItems { get; set; }
    }

    public class UpdateSaleItemRequest
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}