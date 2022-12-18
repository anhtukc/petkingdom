using System;
using System.Collections.Generic;

namespace PetKingdomFN.Models;

public partial class SellBillDetail
{
    public string Id { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public int ReceiptPrice { get; set; }

    public int UnitPrice { get; set; }

    public int Quantity { get; set; }

    public double Discount { get; set; }

    public long GrandPaid { get; set; }

    public int? Status { get; set; }

    public string? ProductId { get; set; }

    public string? SellBillId { get; set; }

    public virtual Product? Product { get; set; }

    public virtual SellBill? SellBill { get; set; }
}
