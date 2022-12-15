using System;
using System.Collections.Generic;

namespace PetKingdomFN.Models
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            Banners = new HashSet<Banner>();
            Products = new HashSet<Product>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Status { get; set; }

        public virtual ICollection<Banner> Banners { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
