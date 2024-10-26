using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductCatalogCore.Entities;

[Table("Supplier")]
public partial class Supplier
{
    [Key]
    public long SupId { get; set; }

    [StringLength(256)]
    public string SupName { get; set; } = null!;

    [StringLength(50)]
    public string PrimaryPhone { get; set; } = null!;

    [StringLength(50)]
    public string? SecondaryPhone { get; set; }

    [StringLength(500)]
    public string? Address { get; set; }

    public double? CreditAmount { get; set; }

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

    public string? Remark { get; set; }
}
