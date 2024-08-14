using System.ComponentModel.DataAnnotations;
using basic_delivery_api.Domain.Models;

namespace basic_delivery_api.Requests;

public class UpdateProductRequest
{
    [Required]
    [MaxLength(60)]
    public string Name { get; set; }

    [Required]
    public EUnitOfMeasurement UnitOfMeasurement { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
    public decimal Price { get; set; }
}