using System;
using System.Collections.Generic;

namespace PetKingdomFN.Models;

public partial class GroupProduct
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
