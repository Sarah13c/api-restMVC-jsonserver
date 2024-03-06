using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PricatMVC.Models;

public class Category
{
    [Required(ErrorMessage = "El id es requerido")]
    [DisplayName("Id")]
    public int? Id { get; set; } = 0;


    [Required(ErrorMessage = "La descripción es requerida")]
    [StringLength(50, ErrorMessage = "La Longitud maxima de la descripción es de 50 caracteres")]
    [DisplayName("Descripción")]
    public string? Description { get; set; } = null!;
    public List<Product> Products { get; set; }

}