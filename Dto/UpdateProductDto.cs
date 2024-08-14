using System.ComponentModel.DataAnnotations;
using basic_delivery_api.Domain.Models;

namespace basic_delivery_api.Dto;

public class UpdateProductDto
{
    [Required]
    [MaxLength(60)]
    public string Name { get; set; }

    [Required]
    public EUnitOfMeasurement UnitOfMeasurement { get; set; }
}