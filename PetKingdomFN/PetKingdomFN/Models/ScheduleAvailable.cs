using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PetKingdomFN.Models;

public partial class ScheduleAvailable
{
    public string? Id { get; set; }

    public DateTime StartedDate { get; set; }

    public DateTime EndedDate { get; set; }

    public TimeSpan AvailableHour { get; set; }

    public int? Status { get; set; }

    public string ServiceOptionId { get; set; }

    [NotMapped]
    public string startedDateFormat { get; set; }
    [NotMapped]
    public string endedDateFormat { get; set; }
    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ServiceOption? ServiceOption { get; set; }
}
