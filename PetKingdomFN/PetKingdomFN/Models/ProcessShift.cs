using System;
using System.Collections.Generic;

namespace PetKingdomFN.Models;

public partial class ProcessShift
{
    public string Id { get; set; } = null!;

    public string Link { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public int? Status { get; set; }

    public string? ShiftId { get; set; }

    public virtual Shift? Shift { get; set; }
}
