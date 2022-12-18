using System;
using System.Collections.Generic;

namespace PetKingdomFN.Models;

public partial class Inventory
{
    public string Id { get; set; } = null!;

    public DateTime ManufactureDate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public int? ReceiptPrice { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? Status { get; set; }

    public string? ProductId { get; set; }

    public virtual Product? Product { get; set; }
}
