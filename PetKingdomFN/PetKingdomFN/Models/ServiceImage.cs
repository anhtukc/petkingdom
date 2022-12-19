using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetKingdomFN.Models;

public partial class ServiceImage
{
    public string? Name { get; set; } = null!;

    public string Id { get; set; } = null!;

    public string? Link { get; set; }

    public int? Status { get; set; }

    public string? ServiceId { get; set; }

    [NotMapped]
    public IFormFile img { get; set; }

    public virtual PetService? Service { get; set; }
}
