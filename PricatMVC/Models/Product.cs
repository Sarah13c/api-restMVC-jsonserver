using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PricatMVC.Models;


public class Product
{
    [Required(ErrorMessage = "El id es requerido")]
    [DisplayName("Id")]
    [RegularExpression(@"^\d+$", ErrorMessage = "El Id debe ser un número")]
    public int? Id { get; set; } = 0;

    [Required(ErrorMessage = "El CategoryId es requerido")]
    [DisplayName("Category Id")]
    [RegularExpression(@"^\d+$", ErrorMessage = "El CategoryId debe ser un número")]
    public int? CategoryId { get; set; } = 0;

    [Required(ErrorMessage = "La descripción es requerida")]
    [StringLength(50, ErrorMessage = "La Longitud maxima de la descripción es de 50 caracteres")]
    [DisplayName("Description")]
    public string? Description { get; set; } = null!;

    [Required(ErrorMessage = "El Eancode es requerido")]
    [DisplayName("Ean Code")]
    [RegularExpression(@"^\d+$", ErrorMessage = "El price debe ser un número")]
    public string? EanCode { get; set; } = null!;

    [Required(ErrorMessage = "El price es requerido")]
    [DisplayName("Price")]
    [RegularExpression(@"^\d+$", ErrorMessage = "El price debe ser un número")]
    public double? Price { get; set; } = 0;

    [Required(ErrorMessage = "El Unit es requerido")]
    [DisplayName("Unit")]
    public string? Unit { get; set; } = null!;

    [ForeignKey("CategoryId")]
    public Category Category { get; set; }




}