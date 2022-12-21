using System;
using System.Collections.Generic;

namespace PetKingdomFN.Models;

public partial class ServiceSellPrice
{
    public string Id { get; set; } = null!;

    public int? UnitPrice { get; set; }

    public double? PetMinimumWeight { get; set; }

    public double? PetMaximumWeight { get; set; }

    public int? Status { get; set; }

    public string? ServiceOptionId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ServiceOption? ServiceOption { get; set; }
}
