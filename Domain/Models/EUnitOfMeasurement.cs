using System.ComponentModel;

namespace basic_delivery_api.Domain.Models;

public enum EUnitOfMeasurement: byte
{
    [Description("UN")]
    Unity = 1,

    [Description("KG")]
    Kilogram = 2,

    [Description("L")]
    Liter = 3
}