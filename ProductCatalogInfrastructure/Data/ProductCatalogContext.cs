using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProductCatalogCore.Entities;

namespace ProductCatalogInfrastructure.Data;

public partial class ProductCatalogContext : DbContext
{
    public ProductCatalogContext(DbContextOptions<ProductCatalogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<SubCategory> SubCategories { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<ViProduct> ViProducts { get; set; }

    public virtual DbSet<ViPurchase> ViPurchases { get; set; }

    public virtual DbSet<ViStock> ViStocks { get; set; }

    public virtual DbSet<ViSubCategory> ViSubCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.ProductId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.Property(e => e.StockId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.Property(e => e.SubId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<ViProduct>(entity =>
        {
            entity.ToView("VI_Product");
        });

        modelBuilder.Entity<ViPurchase>(entity =>
        {
            entity.ToView("VI_Purchase");
        });

        modelBuilder.Entity<ViStock>(entity =>
        {
            entity.ToView("VI_Stock");
        });

        modelBuilder.Entity<ViSubCategory>(entity =>
        {
            entity.ToView("VI_SubCategory");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
