using System;
using System.Collections.Generic;

namespace PetKingdomFN.Models;

public partial class Banner
{
    public string Id { get; set; } = null!;

    public string Thumbnail { get; set; } = null!;

    public string Link { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public int? Status { get; set; }

    public bool? ActiveAsSlide { get; set; }

    public string? ProductCategoryId { get; set; }

    public virtual ProductCategory? ProductCategory { get; set; }
}
