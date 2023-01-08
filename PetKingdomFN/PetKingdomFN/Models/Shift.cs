using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetKingdomFN.Models;

public partial class Shift
{
    public string? Id { get; set; } = null!;

    public DateTime WorkingDate { get; set; }

    public TimeSpan StartedHour { get; set; }

    public TimeSpan EndedHour { get; set; }

    public string? ScheduleId { get; set; }

    public string? CaringStaffId { get; set; }
    [NotMapped]
    public string? WorkingDateFormat { get; set; }

    public int? Status { get; set; }

    [NotMapped]
    public Pet? pet { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual Account? CaringStaff { get; set; }

    public virtual ICollection<ProcessShift> ProcessShifts { get; } = new List<ProcessShift>();

    public virtual Schedule? Schedule { get; set; }
}
