using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PetKingdomFN.Models;

public partial class PetKingdomContext : DbContext
{
    public PetKingdomContext()
    {
    }

    public PetKingdomContext(DbContextOptions<PetKingdomContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Banner> Banners { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<BlogCategory> BlogCategories { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<GroupProduct> GroupProducts { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Pet> Pets { get; set; }

    public virtual DbSet<PetService> PetServices { get; set; }

    public virtual DbSet<ProcessShift> ProcessShifts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductDiscount> ProductDiscounts { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductSellPrice> ProductSellPrices { get; set; }

    public virtual DbSet<Provider> Providers { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<ReceiptBill> ReceiptBills { get; set; }

    public virtual DbSet<ReceiptBillDetail> ReceiptBillDetails { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<ScheduleAvailable> ScheduleAvailables { get; set; }

    public virtual DbSet<SellBill> SellBills { get; set; }

    public virtual DbSet<SellBillDetail> SellBillDetails { get; set; }

    public virtual DbSet<ServiceImage> ServiceImages { get; set; }

    public virtual DbSet<ServiceOption> ServiceOptions { get; set; }

    public virtual DbSet<ServiceSellPrice> ServiceSellPrices { get; set; }

    public virtual DbSet<Shift> Shifts { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=PetKingdom;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_account");

            entity.ToTable("account");

            entity.HasIndex(e => e.Username, "UQ__account__F3DBC572504E289B").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.AccessFailedCount).HasColumnName("Access_failed_count");
            entity.Property(e => e.ConcurrencyStamp)
                .HasMaxLength(100)
                .HasColumnName("concurrency_stamp");
            entity.Property(e => e.LockAccount).HasColumnName("lock_account");
            entity.Property(e => e.LockTime)
                .HasColumnType("datetime")
                .HasColumnName("lock_time");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Permission)
                .HasMaxLength(100)
                .HasColumnName("permission");
            entity.Property(e => e.PhoneConfirmed).HasColumnName("phone_confirmed");
            entity.Property(e => e.SecurityStamp)
                .HasMaxLength(100)
                .HasColumnName("security_stamp");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Banner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_banner");

            entity.ToTable("banner");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.ActiveAsSlide).HasColumnName("active_as_slide");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("date")
                .HasColumnName("created_date");
            entity.Property(e => e.Link)
                .HasMaxLength(300)
                .HasColumnName("link");
            entity.Property(e => e.ProductCategoryId)
                .HasMaxLength(50)
                .HasColumnName("product_category_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Thumbnail)
                .HasMaxLength(200)
                .HasColumnName("thumbnail");

            entity.HasOne(d => d.ProductCategory).WithMany(p => p.Banners)
                .HasForeignKey(d => d.ProductCategoryId)
                .HasConstraintName("FK__banner__product___33D4B598");
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_blog");

            entity.ToTable("blog");

            entity.HasIndex(e => e.Name, "UQ__blog__72E12F1B3193B83F").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.Author)
                .HasMaxLength(100)
                .HasColumnName("author");
            entity.Property(e => e.BlogCategoryId)
                .HasMaxLength(50)
                .HasColumnName("blog_category_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("date")
                .HasColumnName("Created_date");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Thumbnail)
                .HasMaxLength(300)
                .HasColumnName("thumbnail");

            entity.HasOne(d => d.BlogCategory).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.BlogCategoryId)
                .HasConstraintName("FK__blog__blog_categ__286302EC");
        });

        modelBuilder.Entity<BlogCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_blog_category");

            entity.ToTable("blog_category");

            entity.HasIndex(e => e.Name, "UQ__blog_cat__72E12F1BA507C23E").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_brand");

            entity.ToTable("brand");

            entity.HasIndex(e => e.Name, "UQ__brand__72E12F1B2A24D276").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Thumbnail)
                .HasMaxLength(300)
                .HasColumnName("thumbnail");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_comment");

            entity.ToTable("comment");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("content");
            entity.Property(e => e.ParentCommentId)
                .HasMaxLength(50)
                .HasColumnName("parent_comment_id");
            entity.Property(e => e.PosterEmail)
                .HasMaxLength(100)
                .HasColumnName("poster_email");
            entity.Property(e => e.PosterName)
                .HasMaxLength(100)
                .HasColumnName("poster_name");
            entity.Property(e => e.PosterPhonenumber)
                .HasMaxLength(30)
                .HasColumnName("poster_phonenumber");
            entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .HasColumnName("product_id");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Product).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__comment__product__3E52440B");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_customer");

            entity.ToTable("customer");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.AccountId)
                .HasMaxLength(50)
                .HasColumnName("account_id");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.Image)
                .HasMaxLength(100)
                .HasColumnName("image");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(30)
                .HasColumnName("phonenumber");
            entity.Property(e => e.Sex)
                .HasMaxLength(10)
                .HasColumnName("sex");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Account).WithMany(p => p.Customers)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__customer__accoun__52593CB8");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_employee");

            entity.ToTable("employee");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.AccountId)
                .HasMaxLength(50)
                .HasColumnName("account_id");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .HasColumnName("address");
            entity.Property(e => e.Birthday)
                .HasColumnType("date")
                .HasColumnName("birthday");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.IdentityCardNumber)
                .HasMaxLength(30)
                .HasColumnName("identity_card_number");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(30)
                .HasColumnName("phonenumber");
            entity.Property(e => e.Sex)
                .HasMaxLength(10)
                .HasColumnName("sex");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Account).WithMany(p => p.Employees)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__employee__accoun__4F7CD00D");
        });

        modelBuilder.Entity<GroupProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_group_product");

            entity.ToTable("group_product");

            entity.HasIndex(e => e.Name, "UQ__group_pr__72E12F1BC8E0221A").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_consignment");

            entity.ToTable("inventory");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("date")
                .HasColumnName("Created_date");
            entity.Property(e => e.ExpirationDate)
                .HasColumnType("date")
                .HasColumnName("expiration_date");
            entity.Property(e => e.ManufactureDate)
                .HasColumnType("date")
                .HasColumnName("manufacture_date");
            entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .HasColumnName("product_id");
            entity.Property(e => e.ReceiptPrice).HasColumnName("receipt_price");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Product).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__inventory__produ__49C3F6B7");
        });

        modelBuilder.Entity<Pet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_pet");

            entity.ToTable("pet");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.Birthday)
                .HasColumnType("date")
                .HasColumnName("birthday");
            entity.Property(e => e.Breed)
                .HasMaxLength(100)
                .HasColumnName("breed");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(50)
                .HasColumnName("customer_id");
            entity.Property(e => e.HealthNote).HasColumnName("health_note");
            entity.Property(e => e.Image)
                .HasMaxLength(100)
                .HasColumnName("image");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Sex)
                .HasMaxLength(100)
                .HasColumnName("sex");
            entity.Property(e => e.Specices)
                .HasMaxLength(100)
                .HasColumnName("specices");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.Customer).WithMany(p => p.Pets)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__pet__customer_id__5535A963");
        });

        modelBuilder.Entity<PetService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_pet_service");

            entity.ToTable("pet_service");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.BriefDescription).HasColumnName("brief_description");
            entity.Property(e => e.FullDesciption).HasColumnName("full_desciption");
            entity.Property(e => e.Icon)
                .HasMaxLength(100)
                .HasColumnName("icon");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Thumbnail)
                .HasMaxLength(300)
                .HasColumnName("thumbnail");
        });

        modelBuilder.Entity<ProcessShift>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_process_shift");

            entity.ToTable("process_shift");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("date")
                .HasColumnName("created_date");
            entity.Property(e => e.Link)
                .HasMaxLength(200)
                .HasColumnName("link");
            entity.Property(e => e.ShiftId)
                .HasMaxLength(50)
                .HasColumnName("shift_id");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Shift).WithMany(p => p.ProcessShifts)
                .HasForeignKey(d => d.ShiftId)
                .HasConstraintName("FK__process_s__shift__7C4F7684");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_product");

            entity.ToTable("product");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.BrandId)
                .HasMaxLength(50)
                .HasColumnName("brand_id");
            entity.Property(e => e.BriefDescription).HasColumnName("brief_description");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("date")
                .HasColumnName("Created_date");
            entity.Property(e => e.FullDescription).HasColumnName("full__description");
            entity.Property(e => e.GroupProductId)
                .HasMaxLength(50)
                .HasColumnName("group_product_id");
            entity.Property(e => e.HighestInventoryLevel).HasColumnName("highest_inventory_level");
            entity.Property(e => e.LowestInventoryLevel).HasColumnName("lowest_inventory_level");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.ProductCategoryId)
                .HasMaxLength(50)
                .HasColumnName("product_category_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Thumbnail)
                .HasMaxLength(300)
                .HasColumnName("thumbnail");
            entity.Property(e => e.TotalQuatity).HasColumnName("total_quatity");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("date")
                .HasColumnName("Update_date");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK__product__brand_i__38996AB5");

            entity.HasOne(d => d.GroupProduct).WithMany(p => p.Products)
                .HasForeignKey(d => d.GroupProductId)
                .HasConstraintName("FK__product__group_p__37A5467C");

            entity.HasOne(d => d.ProductCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductCategoryId)
                .HasConstraintName("FK__product__product__36B12243");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_product_category");

            entity.ToTable("product_category");

            entity.HasIndex(e => e.Name, "UQ__product___72E12F1BB8D005D5").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<ProductDiscount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_product_discount");

            entity.ToTable("product_discount");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.DiscountAmount).HasColumnName("discount_amount");
            entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .HasColumnName("product_id");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductDiscounts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__product_d__produ__46E78A0C");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_product_image");

            entity.ToTable("product_image");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.Link)
                .HasMaxLength(200)
                .HasColumnName("link");
            entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .HasColumnName("product_id");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__product_i__produ__412EB0B6");
        });

        modelBuilder.Entity<ProductSellPrice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_sell_price");

            entity.ToTable("product_sell_price");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .HasColumnName("product_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UnitPrice).HasColumnName("unit_price");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductSellPrices)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__product_s__produ__440B1D61");
        });

        modelBuilder.Entity<Provider>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_provider");

            entity.ToTable("provider");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(30)
                .HasColumnName("phonenumber");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TaxNumber)
                .HasMaxLength(30)
                .HasColumnName("tax_number");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_rating");

            entity.ToTable("rating");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(300)
                .HasColumnName("content");
            entity.Property(e => e.MadePurchase).HasColumnName("made_purchase");
            entity.Property(e => e.PosterEmail)
                .HasMaxLength(100)
                .HasColumnName("poster_email");
            entity.Property(e => e.PosterName)
                .HasMaxLength(100)
                .HasColumnName("poster_name");
            entity.Property(e => e.PosterPhonenumber)
                .HasMaxLength(30)
                .HasColumnName("poster_phonenumber");
            entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .HasColumnName("product_id");
            entity.Property(e => e.RecommendToRelatives).HasColumnName("recommend_to_relatives");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Product).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__rating__product___3B75D760");
        });

        modelBuilder.Entity<ReceiptBill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_receipt_bill");

            entity.ToTable("receipt_bill");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("date")
                .HasColumnName("Created_date");
            entity.Property(e => e.EmployeeAccountId)
                .HasMaxLength(50)
                .HasColumnName("employee_account_id");
            entity.Property(e => e.ProviderId)
                .HasMaxLength(50)
                .HasColumnName("provider_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TotalPaid).HasColumnName("Total_paid");

            entity.HasOne(d => d.EmployeeAccount).WithMany(p => p.ReceiptBills)
                .HasForeignKey(d => d.EmployeeAccountId)
                .HasConstraintName("FK__receipt_b__emplo__5AEE82B9");

            entity.HasOne(d => d.Provider).WithMany(p => p.ReceiptBills)
                .HasForeignKey(d => d.ProviderId)
                .HasConstraintName("FK__receipt_b__provi__59FA5E80");
        });

        modelBuilder.Entity<ReceiptBillDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_receipt_bill_details");

            entity.ToTable("receipt_bill_details");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.Discount).HasColumnName("discount");
            entity.Property(e => e.GrandPaid).HasColumnName("grand_paid");
            entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .HasColumnName("product_id");
            entity.Property(e => e.ProductName)
                .HasMaxLength(200)
                .HasColumnName("product_name");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ReceiptBillId)
                .HasMaxLength(50)
                .HasColumnName("receipt_bill_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UnitPrice).HasColumnName("unit_price");

            entity.HasOne(d => d.Product).WithMany(p => p.ReceiptBillDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__receipt_b__produ__5DCAEF64");

            entity.HasOne(d => d.ReceiptBill).WithMany(p => p.ReceiptBillDetails)
                .HasForeignKey(d => d.ReceiptBillId)
                .HasConstraintName("FK__receipt_b__recei__5EBF139D");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_schedule");

            entity.ToTable("schedule");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.BookingDate)
                .HasColumnType("date")
                .HasColumnName("booking_date");
            entity.Property(e => e.BookingHour).HasColumnName("booking_hour");
            entity.Property(e => e.GrandPaid).HasColumnName("grand_paid");
            entity.Property(e => e.PetId)
                .HasMaxLength(50)
                .HasColumnName("pet_id");
            entity.Property(e => e.SellBillId)
                .HasMaxLength(50)
                .HasColumnName("sell_bill_id");
            entity.Property(e => e.ServiceOptionId)
                .HasMaxLength(50)
                .HasColumnName("service_option_id");
            entity.Property(e => e.ServiceOptionName)
                .HasMaxLength(200)
                .HasColumnName("service_option_name");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Pet).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.PetId)
                .HasConstraintName("FK__schedule__pet_id__75A278F5");

            entity.HasOne(d => d.SellBill).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.SellBillId)
                .HasConstraintName("FK__schedule__sell_b__74AE54BC");

            entity.HasOne(d => d.ServiceOption).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.ServiceOptionId)
                .HasConstraintName("FK__schedule__servic__73BA3083");
        });

        modelBuilder.Entity<ScheduleAvailable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_booking_available");

            entity.ToTable("schedule_available");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.AvailableHour).HasColumnName("available_hour");
            entity.Property(e => e.EndedDate)
                .HasColumnType("date")
                .HasColumnName("ended_date");
            entity.Property(e => e.ServiceOptionId)
                .HasMaxLength(50)
                .HasColumnName("service_option_id");
            entity.Property(e => e.StartedDate)
                .HasColumnType("date")
                .HasColumnName("started_date");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.ServiceOption).WithMany(p => p.ScheduleAvailables)
                .HasForeignKey(d => d.ServiceOptionId)
                .HasConstraintName("FK__schedule___servi__70DDC3D8");
        });

        modelBuilder.Entity<SellBill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_sell_bill");

            entity.ToTable("sell_bill");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("date")
                .HasColumnName("Created_date");
            entity.Property(e => e.CustomerAccountId)
                .HasMaxLength(50)
                .HasColumnName("customer_account_id");
            entity.Property(e => e.EmployeeAccountId)
                .HasMaxLength(50)
                .HasColumnName("employee_account_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TotalPaid).HasColumnName("Total_paid");

            entity.HasOne(d => d.CustomerAccount).WithMany(p => p.SellBillCustomerAccounts)
                .HasForeignKey(d => d.CustomerAccountId)
                .HasConstraintName("FK__sell_bill__custo__619B8048");

            entity.HasOne(d => d.EmployeeAccount).WithMany(p => p.SellBillEmployeeAccounts)
                .HasForeignKey(d => d.EmployeeAccountId)
                .HasConstraintName("FK__sell_bill__emplo__628FA481");
        });

        modelBuilder.Entity<SellBillDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_sell_bill_details");

            entity.ToTable("sell_bill_details");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.Discount).HasColumnName("discount");
            entity.Property(e => e.GrandPaid).HasColumnName("grand_paid");
            entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .HasColumnName("product_id");
            entity.Property(e => e.ProductName)
                .HasMaxLength(200)
                .HasColumnName("product_name");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ReceiptPrice).HasColumnName("receipt_price");
            entity.Property(e => e.SellBillId)
                .HasMaxLength(50)
                .HasColumnName("sell_bill_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UnitPrice).HasColumnName("unit_price");

            entity.HasOne(d => d.Product).WithMany(p => p.SellBillDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__sell_bill__produ__656C112C");

            entity.HasOne(d => d.SellBill).WithMany(p => p.SellBillDetails)
                .HasForeignKey(d => d.SellBillId)
                .HasConstraintName("FK__sell_bill__sell___66603565");
        });

        modelBuilder.Entity<ServiceImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_service_image");

            entity.ToTable("service_image");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.Link)
                .HasMaxLength(200)
                .HasColumnName("link");
            entity.Property(e => e.ServiceId)
                .HasMaxLength(50)
                .HasColumnName("service_id");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Service).WithMany(p => p.ServiceImages)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__service_i__servi__02FC7413");
        });

        modelBuilder.Entity<ServiceOption>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_service_option");

            entity.ToTable("service_option");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EstimatedCompletionTime).HasColumnName("estimated_completion_time");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.PetServiceId)
                .HasMaxLength(50)
                .HasColumnName("pet_service_id");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.PetService).WithMany(p => p.ServiceOptions)
                .HasForeignKey(d => d.PetServiceId)
                .HasConstraintName("FK__service_o__pet_s__6B24EA82");
        });

        modelBuilder.Entity<ServiceSellPrice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_service_sell_price");

            entity.ToTable("service_sell_price");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.PetMaximumWeight).HasColumnName("pet_maximum_weight");
            entity.Property(e => e.PetMinimumWeight).HasColumnName("pet_minimum_weight");
            entity.Property(e => e.ServiceOptionId)
                .HasMaxLength(50)
                .HasColumnName("service_option_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UnitPrice).HasColumnName("unit_price");

            entity.HasOne(d => d.ServiceOption).WithMany(p => p.ServiceSellPrices)
                .HasForeignKey(d => d.ServiceOptionId)
                .HasConstraintName("FK__service_s__servi__6E01572D");
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_shift");

            entity.ToTable("shift");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.CaringStaffId)
                .HasMaxLength(50)
                .HasColumnName("caring_staff_id");
            entity.Property(e => e.EndedHour).HasColumnName("ended_hour");
            entity.Property(e => e.ScheduleId)
                .HasMaxLength(50)
                .HasColumnName("schedule_id");
            entity.Property(e => e.StartedHour).HasColumnName("started_hour");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.WorkingDate)
                .HasColumnType("date")
                .HasColumnName("working_date");

            entity.HasOne(d => d.CaringStaff).WithMany(p => p.Shifts)
                .HasForeignKey(d => d.CaringStaffId)
                .HasConstraintName("FK__shift__caring_st__797309D9");

            entity.HasOne(d => d.Schedule).WithMany(p => p.Shifts)
                .HasForeignKey(d => d.ScheduleId)
                .HasConstraintName("FK__shift__schedule___787EE5A0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
