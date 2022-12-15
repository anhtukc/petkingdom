using System;
using System.Collections.Generic;

namespace PetKingdomFN.Models
{
    public partial class PetService
    {
        public PetService()
        {
            ServiceOptions = new HashSet<ServiceOption>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string FullDesciption { get; set; } = null!;
        public string BriefDescription { get; set; } = null!;
        public string Thumbnail { get; set; } = null!;
        public string Icon { get; set; } = null!;
        public int? Status { get; set; }

        public virtual ICollection<ServiceOption> ServiceOptions { get; set; }
    }
}
