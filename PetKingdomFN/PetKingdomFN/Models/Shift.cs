using System;
using System.Collections.Generic;

namespace PetKingdomFN.Models
{
    public partial class Shift
    {
        public Shift()
        {
            ProcessShifts = new HashSet<ProcessShift>();
        }

        public string Id { get; set; } = null!;
        public DateTime WorkingDate { get; set; }
        public TimeSpan StartedHour { get; set; }
        public TimeSpan EndedHour { get; set; }
        public string? ScheduleId { get; set; }
        public string? CaringStaffId { get; set; }
        public int? Status { get; set; }

        public virtual Account? CaringStaff { get; set; }
        public virtual Schedule? Schedule { get; set; }
        public virtual ICollection<ProcessShift> ProcessShifts { get; set; }
    }
}
