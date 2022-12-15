using System;
using System.Collections.Generic;

namespace PetKingdomFN.Models
{
    public partial class ServiceOption
    {
        public ServiceOption()
        {
            ScheduleAvailables = new HashSet<ScheduleAvailable>();
            Schedules = new HashSet<Schedule>();
            ServiceSellPrices = new HashSet<ServiceSellPrice>();
        }

        public string Id { get; set; } = null!;
        public string? PetServiceId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int? EstimatedCompletionTime { get; set; }
        public int? Status { get; set; }

        public virtual PetService? PetService { get; set; }
        public virtual ICollection<ScheduleAvailable> ScheduleAvailables { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<ServiceSellPrice> ServiceSellPrices { get; set; }
    }
}
