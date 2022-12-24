using System;
using System.Collections.Generic;

namespace PetKingdomFN.Models;

public partial class Product
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? ShortName { get; set; }

    public int Weight { get; set; }

    public string? BriefDescription { get; set; }

    public string? FullDescription { get; set; }

    public int? LowestInventoryLevel { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int Status { get; set; }

    public string? ProductCategoryId { get; set; }

    public string? GroupProductId { get; set; }

    public string? BrandId { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual GroupProduct? GroupProduct { get; set; }

    public virtual ICollection<Inventory> Inventories { get; } = new List<Inventory>();

    public virtual ProductCategory? ProductCategory { get; set; }

    public virtual ICollection<ProductDiscount> ProductDiscounts { get; } = new List<ProductDiscount>();

    public virtual ICollection<ProductImage> ProductImages { get; } = new List<ProductImage>();

    public virtual ICollection<ProductSellPrice> ProductSellPrices { get; } = new List<ProductSellPrice>();

    public virtual ICollection<Rating> Ratings { get; } = new List<Rating>();

    public virtual ICollection<ReceiptBillDetail> ReceiptBillDetails { get; } = new List<ReceiptBillDetail>();

    public virtual ICollection<SellBillDetail> SellBillDetails { get; } = new List<SellBillDetail>();
}
