using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MyEcommerceBook.Models;

public partial class MyEcommerceDbContext : DbContext
{
    public MyEcommerceDbContext()
    {
    }

    public MyEcommerceDbContext(DbContextOptions<MyEcommerceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdminEmployee> AdminEmployees { get; set; }

    public virtual DbSet<AdminLogin> AdminLogins { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<GenMainSlider> GenMainSliders { get; set; }

    public virtual DbSet<GenPromoRight> GenPromoRights { get; set; }

    public virtual DbSet<MigrationHistory> MigrationHistories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentType> PaymentTypes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<RecentlyView> RecentlyViews { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<ShippingDetail> ShippingDetails { get; set; }

    public virtual DbSet<SubCategory> SubCategories { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Wishlist> Wishlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=host.docker.internal,1433;Initial Catalog=ecommerce;User Id=Princy;Password=Princy24;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminEmployee>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PK_dbo.admin_Employee");

            entity.ToTable("admin_Employee");

            entity.Property(e => e.EmpId).HasColumnName("EmpID");
            entity.Property(e => e.DateofBirth).HasColumnType("datetime");
        });

        modelBuilder.Entity<AdminLogin>(entity =>
        {
            entity.HasKey(e => e.LoginId).HasName("PK_dbo.admin_Login");

            entity.ToTable("admin_Login");

            entity.HasIndex(e => e.EmpId, "IX_EmpID");

            entity.HasIndex(e => e.RoleRoleId, "IX_Role_RoleID");

            entity.Property(e => e.LoginId).HasColumnName("LoginID");
            entity.Property(e => e.EmpId).HasColumnName("EmpID");
            entity.Property(e => e.RoleRoleId).HasColumnName("Role_RoleID");

            entity.HasOne(d => d.Emp).WithMany(p => p.AdminLogins)
                .HasForeignKey(d => d.EmpId)
                .HasConstraintName("FK_dbo.admin_Login_dbo.admin_Employee_EmpID");

            entity.HasOne(d => d.RoleRole).WithMany(p => p.AdminLogins)
                .HasForeignKey(d => d.RoleRoleId)
                .HasConstraintName("FK_dbo.admin_Login_dbo.Roles_Role_RoleID");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK_dbo.Categories");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK_dbo.Customers");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.DateofBirth).HasColumnType("datetime");
            entity.Property(e => e.FirstName).HasColumnName("First_Name");
            entity.Property(e => e.LastLogin).HasColumnType("datetime");
            entity.Property(e => e.LastName).HasColumnName("Last_Name");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<GenMainSlider>(entity =>
        {
            entity.HasKey(e => e.MainSliderId).HasName("PK_dbo.genMainSliders");

            entity.ToTable("genMainSliders");

            entity.Property(e => e.MainSliderId).HasColumnName("MainSliderID");
            entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
        });

        modelBuilder.Entity<GenPromoRight>(entity =>
        {
            entity.HasKey(e => e.PromoRightId).HasName("PK_dbo.genPromoRights");

            entity.ToTable("genPromoRights");

            entity.HasIndex(e => e.CategoryId, "IX_CategoryID");

            entity.Property(e => e.PromoRightId).HasColumnName("PromoRightID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

            entity.HasOne(d => d.Category).WithMany(p => p.GenPromoRights)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_dbo.genPromoRights_dbo.Categories_CategoryID");
        });

        modelBuilder.Entity<MigrationHistory>(entity =>
        {
            entity.HasKey(e => new { e.MigrationId, e.ContextKey }).HasName("PK_dbo.__MigrationHistory");

            entity.ToTable("__MigrationHistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ContextKey).HasMaxLength(300);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK_dbo.Orders");

            entity.HasIndex(e => e.CustomerId, "IX_CustomerID");

            entity.HasIndex(e => e.PaymentId, "IX_PaymentID");

            entity.HasIndex(e => e.ShippingId, "IX_ShippingID");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.DeliveryDate).HasColumnType("datetime");
            entity.Property(e => e.Dispatched).HasColumnName("DIspatched");
            entity.Property(e => e.DispatchedDate).HasColumnType("datetime");
            entity.Property(e => e.IsCompleted).HasColumnName("isCompleted");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.ShippingDate).HasColumnType("datetime");
            entity.Property(e => e.ShippingId).HasColumnName("ShippingID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_dbo.Orders_dbo.Customers_CustomerID");

            entity.HasOne(d => d.Payment).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK_dbo.Orders_dbo.Payments_PaymentID");

            entity.HasOne(d => d.Shipping).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ShippingId)
                .HasConstraintName("FK_dbo.Orders_dbo.ShippingDetails_ShippingID");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailsId).HasName("PK_dbo.OrderDetails");

            entity.HasIndex(e => e.OrderId, "IX_OrderID");

            entity.HasIndex(e => e.ProductId, "IX_ProductID");

            entity.Property(e => e.OrderDetailsId).HasColumnName("OrderDetailsID");
            entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_dbo.OrderDetails_dbo.Orders_OrderID");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_dbo.OrderDetails_dbo.Products_ProductID");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK_dbo.Payments");

            entity.HasIndex(e => e.PaymentTypePayTypeId, "IX_PaymentType_PayTypeID");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreditAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DebitAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PaymentDateTime).HasColumnType("datetime");
            entity.Property(e => e.PaymentTypePayTypeId).HasColumnName("PaymentType_PayTypeID");

            entity.HasOne(d => d.PaymentTypePayType).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PaymentTypePayTypeId)
                .HasConstraintName("FK_dbo.Payments_dbo.PaymentTypes_PaymentType_PayTypeID");
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.HasKey(e => e.PayTypeId).HasName("PK_dbo.PaymentTypes");

            entity.Property(e => e.PayTypeId).HasColumnName("PayTypeID");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK_dbo.Products");

            entity.HasIndex(e => e.CategoryId, "IX_CategoryID");

            entity.HasIndex(e => e.SubCategoryId, "IX_SubCategoryID");

            entity.HasIndex(e => e.SupplierId, "IX_SupplierID");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.OldPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SubCategoryId).HasColumnName("SubCategoryID");
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_dbo.Products_dbo.Categories_CategoryID");

            entity.HasOne(d => d.SubCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.SubCategoryId)
                .HasConstraintName("FK_dbo.Products_dbo.SubCategories_SubCategoryID");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK_dbo.Products_dbo.Suppliers_SupplierID");
        });

        modelBuilder.Entity<RecentlyView>(entity =>
        {
            entity.HasKey(e => e.RviewId).HasName("PK_dbo.RecentlyViews");

            entity.HasIndex(e => e.CustomerId, "IX_CustomerID");

            entity.HasIndex(e => e.ProductId, "IX_ProductID");

            entity.Property(e => e.RviewId).HasColumnName("RViewID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ViewDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.RecentlyViews)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_dbo.RecentlyViews_dbo.Customers_CustomerID");

            entity.HasOne(d => d.Product).WithMany(p => p.RecentlyViews)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_dbo.RecentlyViews_dbo.Products_ProductID");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK_dbo.Reviews");

            entity.HasIndex(e => e.CustomerId, "IX_CustomerID");

            entity.HasIndex(e => e.ProductId, "IX_ProductID");

            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_dbo.Reviews_dbo.Customers_CustomerID");

            entity.HasOne(d => d.Product).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_dbo.Reviews_dbo.Products_ProductID");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK_dbo.Roles");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
        });

        modelBuilder.Entity<ShippingDetail>(entity =>
        {
            entity.HasKey(e => e.ShippingId).HasName("PK_dbo.ShippingDetails");

            entity.Property(e => e.ShippingId).HasColumnName("ShippingID");
        });

        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.HasKey(e => e.SubCategoryId).HasName("PK_dbo.SubCategories");

            entity.HasIndex(e => e.CategoryId, "IX_CategoryID");

            entity.Property(e => e.SubCategoryId).HasColumnName("SubCategoryID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.IsActive).HasColumnName("isActive");

            entity.HasOne(d => d.Category).WithMany(p => p.SubCategories)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_dbo.SubCategories_dbo.Categories_CategoryID");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK_dbo.Suppliers");

            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
        });

        modelBuilder.Entity<Wishlist>(entity =>
        {
            entity.HasKey(e => e.WishlistId).HasName("PK_dbo.Wishlists");

            entity.HasIndex(e => e.CustomerId, "IX_CustomerID");

            entity.HasIndex(e => e.ProductId, "IX_ProductID");

            entity.Property(e => e.WishlistId).HasColumnName("WishlistID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_dbo.Wishlists_dbo.Customers_CustomerID");

            entity.HasOne(d => d.Product).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_dbo.Wishlists_dbo.Products_ProductID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
