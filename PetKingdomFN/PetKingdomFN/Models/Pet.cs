using System;
using System.Collections.Generic;

namespace PetKingdomFN.Models
{
    public partial class Pet
    {
        public Pet()
        {
            Schedules = new HashSet<Schedule>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int? Weight { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Image { get; set; }
        public string? Specices { get; set; }
        public string? Breed { get; set; }
        public string Sex { get; set; } = null!;
        public string? HealthNote { get; set; }
        public int? Status { get; set; }
        public string? CustomerId { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
