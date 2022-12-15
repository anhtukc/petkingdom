using System;
using System.Collections.Generic;

namespace PetKingdomFN.Models
{
    public partial class Comment
    {
        public string Id { get; set; } = null!;
        public string? PosterName { get; set; }
        public string? PosterEmail { get; set; }
        public string? PosterPhonenumber { get; set; }
        public string? Content { get; set; }
        public string? ProductId { get; set; }
        public string? ParentCommentId { get; set; }
        public int Status { get; set; }

        public virtual Product? Product { get; set; }
    }
}
