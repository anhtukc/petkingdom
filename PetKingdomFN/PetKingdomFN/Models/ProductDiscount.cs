using System;
using System.Collections.Generic;

namespace PetKingdomFN.Models;

public partial class ProductDiscount
{
    public string Id { get; set; } = null!;

    public double? DiscountAmount { get; set; }

    public int? Status { get; set; }

    public string? ProductId { get; set; }

    public virtual Product? Product { get; set; }
}
