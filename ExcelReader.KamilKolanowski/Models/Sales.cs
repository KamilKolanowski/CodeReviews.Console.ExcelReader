using System.ComponentModel.DataAnnotations;

namespace ExcelReader.KamilKolanowski.Models;

public class Sales
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime SalesDate { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public decimal Tax { get; set; }
    public decimal Discount { get; set; }

    [Required]
    public decimal Total { get; set; }

    [Required]
    [StringLength(3)]
    public string? Currency { get; set; }

    [Required]
    [StringLength(50)]
    public string? Market { get; set; }
}
