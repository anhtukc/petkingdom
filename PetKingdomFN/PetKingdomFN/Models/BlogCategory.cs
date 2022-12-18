﻿using System;
using System.Collections.Generic;

namespace PetKingdomFN.Models;

public partial class BlogCategory
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int Status { get; set; }

    public virtual ICollection<Blog> Blogs { get; } = new List<Blog>();
}
