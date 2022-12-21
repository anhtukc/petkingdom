using System;
using System.Collections.Generic;

namespace PetKingdomFN.Models;

public partial class ScheduleAvailable
{
    public string Id { get; set; } = null!;

    public DateTime StartedDate { get; set; }

    public DateTime EndedDate { get; set; }

    public TimeSpan AvailableHour { get; set; }

    public int? Status { get; set; }

    public string? ServiceOptionId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ServiceOption? ServiceOption { get; set; }
}
