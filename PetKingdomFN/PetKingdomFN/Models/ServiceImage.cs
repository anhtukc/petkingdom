using System;
using System.Collections.Generic;

namespace PetKingdomFN.Models;

public partial class ServiceImage
{
    public string Id { get; set; } = null!;

    public string? Link { get; set; }

    public int? Status { get; set; }

    public string? ServiceId { get; set; }

    public virtual PetService? Service { get; set; }
}
