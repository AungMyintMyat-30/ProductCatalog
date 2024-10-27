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

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<SubCategory> SubCategories { get; set; }

    public virtual DbSet<ViProduct> ViProducts { get; set; }

    public virtual DbSet<ViSubCategory> ViSubCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ViProduct>(entity =>
        {
            entity.ToView("VI_Product");
        });

        modelBuilder.Entity<ViSubCategory>(entity =>
        {
            entity.ToView("VI_SubCategory");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
