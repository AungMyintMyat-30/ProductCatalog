using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductCatalogCore.Entities;

[Table("Product")]
public partial class Product
{
    [Key]
    public long ProductId { get; set; }

    public long SubId { get; set; }

    public long BrandId { get; set; }

    [StringLength(50)]
    public string Code { get; set; } = null!;

    [StringLength(100)]
    public string ProductName { get; set; } = null!;

    public double? Price { get; set; }

    public string? Description { get; set; }

    [StringLength(100)]
    public string? ImgUrl { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [StringLength(256)]
    public string? CreatedUser { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedDate { get; set; }

    [StringLength(256)]
    public string? UpdatedUser { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeletedDate { get; set; }

    [StringLength(256)]
    public string? DeletedUser { get; set; }
}
