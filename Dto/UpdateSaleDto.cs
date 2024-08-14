namespace basic_delivery_api.Dto
{
    public class UpdateSaleDto
    {
        public DateTime SaleDate { get; set; }
        public decimal ShippingCost { get; set; }
        public string ZipCode { get; set; }
        public IEnumerable<UpdateSaleItemDto> SaleItems { get; set; }
    }

    public class UpdateSaleItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}