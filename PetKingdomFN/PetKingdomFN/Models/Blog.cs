using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetKingdomFN.Models;

public partial class Blog
{
    public string? Id { get; set; }

    public string Name { get; set; } = null!;

    public string Author { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string Thumbnail { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int Status { get; set; }

    public string? BlogCategoryId { get; set; }
    [NotMapped]
    public IFormFile? file { get; set; }
   
    [NotMapped]
    public string? BlogCategoryName { get; set; }

    public virtual BlogCategory? BlogCategory { get; set; }
}
