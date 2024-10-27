using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductCatalogCore.Entities;

[Keyless]
public partial class ViStock
{
    public long StockId { get; set; }

    public long ProductId { get; set; }

    [StringLength(50)]
    public string Code { get; set; } = null!;

    [StringLength(100)]
    public string ProductName { get; set; } = null!;

    [StringLength(50)]
    public string SubName { get; set; } = null!;

    [StringLength(50)]
    public string CatName { get; set; } = null!;

    public string? Description { get; set; }

    [StringLength(100)]
    public string? ImgUrl { get; set; }

    public int? Qty { get; set; }

    public int? SaleQty { get; set; }

    public double? PurchasePrice { get; set; }

    public double? SalePrice { get; set; }

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
