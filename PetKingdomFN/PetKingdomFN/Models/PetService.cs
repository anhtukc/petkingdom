using System;
using System.Collections.Generic;

namespace PetKingdomFN.Models;

public partial class PetService
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string FullDesciption { get; set; } = null!;

    public string BriefDescription { get; set; } = null!;

    public string Thumbnail { get; set; } = null!;

    public string Icon { get; set; } = null!;

    public int? Status { get; set; }

    public virtual ICollection<ServiceImage> ServiceImages { get; } = new List<ServiceImage>();

    public virtual ICollection<ServiceOption> ServiceOptions { get; } = new List<ServiceOption>();
}
