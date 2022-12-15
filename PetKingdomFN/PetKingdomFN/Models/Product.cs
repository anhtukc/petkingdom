using System;
using System.Collections.Generic;

namespace PetKingdomFN.Models
{
    public partial class Product
    {
        public Product()
        {
            Comments = new HashSet<Comment>();
            Inventories = new HashSet<Inventory>();
            ProductDiscounts = new HashSet<ProductDiscount>();
            ProductImages = new HashSet<ProductImage>();
            ProductSellPrices = new HashSet<ProductSellPrice>();
            Ratings = new HashSet<Rating>();
            ReceiptBillDetails = new HashSet<ReceiptBillDetail>();
            SellBillDetails = new HashSet<SellBillDetail>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Weight { get; set; }
        public string? BriefDescription { get; set; }
        public string? FullDescription { get; set; }
        public string Thumbnail { get; set; } = null!;
        public int TotalQuatity { get; set; }
        public int? LowestInventoryLevel { get; set; }
        public int? HighestInventoryLevel { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int Status { get; set; }
        public string? ProductCategoryId { get; set; }
        public string? GroupProductId { get; set; }
        public string? BrandId { get; set; }

        public virtual Brand? Brand { get; set; }
        public virtual GroupProduct? GroupProduct { get; set; }
        public virtual ProductCategory? ProductCategory { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<ProductDiscount> ProductDiscounts { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductSellPrice> ProductSellPrices { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<ReceiptBillDetail> ReceiptBillDetails { get; set; }
        public virtual ICollection<SellBillDetail> SellBillDetails { get; set; }
    }
}
