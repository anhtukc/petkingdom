using System;
using System.Collections.Generic;

namespace PetKingdomFN.Models;

public partial class ProductCategory
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int Status { get; set; }

    public virtual ICollection<Banner> Banners { get; } = new List<Banner>();

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
