using System;
using System.Collections.Generic;

namespace PetKingdomFN.Models;

public partial class Schedule
{
    public string? Id { get; set; } 

    public string ServiceOptionName { get; set; } = null!;

    public DateTime BookingDate { get; set; }

    public TimeSpan BookingHour { get; set; }

    public int? GrandPaid { get; set; }

    public int? Status { get; set; }

    public string? ServiceOptionId { get; set; }

    public string? SellBillId { get; set; }

    public string? PetId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual Pet? Pet { get; set; }

    public virtual SellBill? SellBill { get; set; }

    public virtual ServiceOption? ServiceOption { get; set; }

    public virtual ICollection<Shift> Shifts { get; } = new List<Shift>();
}
