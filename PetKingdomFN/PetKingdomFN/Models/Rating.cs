using System;
using System.Collections.Generic;

namespace PetKingdomFN.Models
{
    public partial class Rating
    {
        public string Id { get; set; } = null!;
        public int Score { get; set; }
        public string? PosterName { get; set; }
        public string? PosterEmail { get; set; }
        public string? PosterPhonenumber { get; set; }
        public string? Content { get; set; }
        public bool? MadePurchase { get; set; }
        public bool? RecommendToRelatives { get; set; }
        public string? ProductId { get; set; }
        public int Status { get; set; }

        public virtual Product? Product { get; set; }
    }
}
