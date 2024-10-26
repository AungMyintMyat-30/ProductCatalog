using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductCatalogCore.Entities;

[PrimaryKey("StockId", "ProductId")]
[Table("Stock")]
public partial class Stock
{
    [Key]
    public long StockId { get; set; }

    [Key]
    public long ProductId { get; set; }

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
