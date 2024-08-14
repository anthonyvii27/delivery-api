using System;
using System.Collections.Generic;

namespace basic_delivery_api.Domain.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<SaleItem> SaleItems { get; set; }
        
        public void CalculateTotalAmount()
        {
            TotalAmount = SaleItems.Sum(si => si.Quantity * si.UnitPrice);
        }
    }

    public class SaleItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int SaleId { get; set; }
        public Sale Sale { get; set; }
    }
}