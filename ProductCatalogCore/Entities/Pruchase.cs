using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductCatalogCore.Entities;

[Table("Pruchase")]
public partial class Pruchase
{
    [Key]
    [Column("PVno")]
    [StringLength(50)]
    public string Pvno { get; set; } = null!;

    public long? ProductId { get; set; }

    public long? SupId { get; set; }

    public int? Qty { get; set; }

    public double? Price { get; set; }

    public double? SubTotal { get; set; }

    public double? DiscountAmount { get; set; }

    public double? NetTotal { get; set; }

    public double? PayAmount { get; set; }

    public double? LeftAmount { get; set; }

    public double? SalePrice { get; set; }

    public string? Remark { get; set; }

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

    public string? Note { get; set; }
}
