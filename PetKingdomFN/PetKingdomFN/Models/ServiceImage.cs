using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetKingdomFN.Models;

public partial class ServiceImage
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? Link { get; set; }

    public int? Status { get; set; }

    public string? PetServiceId { get; set; }
    [NotMapped]
    public IFormFile img { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual PetService? PetService { get; set; }
}
