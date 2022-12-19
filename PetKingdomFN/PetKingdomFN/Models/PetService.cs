using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetKingdomFN.Models;

public partial class PetService
{
    public string id { get; set; } = null!;

    public string name { get; set; } = null!;

    public string fullDescription { get; set; } = null!;

    public string briefDescription { get; set; } = null!;

    public string? icon { get; set; } = null!;

    public int? status { get; set; }

    [NotMapped]
    public IFormFile? iconFile { get; set; }

    public virtual ICollection<ServiceImage> ServiceImages { get; } = new List<ServiceImage>();

    public virtual ICollection<ServiceOption> ServiceOptions { get; } = new List<ServiceOption>();
}
