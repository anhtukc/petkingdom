using System;
using System.Collections.Generic;

namespace PetKingdomFN.Models;

public partial class ReceiptBill
{
    public string Id { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public long TotalPaid { get; set; }

    public string? ProviderId { get; set; }

    public string? EmployeeAccountId { get; set; }

    public int? Status { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual Account? EmployeeAccount { get; set; }

    public virtual Provider? Provider { get; set; }

    public virtual ICollection<ReceiptBillDetail> ReceiptBillDetails { get; } = new List<ReceiptBillDetail>();
}
