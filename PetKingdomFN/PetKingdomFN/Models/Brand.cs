using System;
using System.Collections.Generic;

namespace PetKingdomFN.Models
{
    public partial class Brand
    {
        public Brand()
        {
            Products = new HashSet<Product>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Thumbnail { get; set; } = null!;
        public string? Description { get; set; }
        public int Status { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
