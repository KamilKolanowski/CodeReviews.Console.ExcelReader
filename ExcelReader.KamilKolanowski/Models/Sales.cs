using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExcelReader.KamilKolanowski.Models;

public class Sales
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime SalesDate { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Tax { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Discount { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Total { get; set; }

    [Required]
    [StringLength(3)]
    public string? Currency { get; set; }

    [Required]
    [StringLength(50)]
    public string? Market { get; set; }

    [Required]
    [StringLength(100)]
    public string? ProductName { get; set; }
}
