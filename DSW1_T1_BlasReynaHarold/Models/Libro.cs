using System.ComponentModel.DataAnnotations;

namespace DSW1_T1_BlasReynaHarold.Models;

public class Libro
{
    [Display(Name = "Código")]
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre del libro es obligatorio.")]
    [StringLength(120, ErrorMessage = "El nombre del libro no puede superar los 120 caracteres.")]
    [Display(Name = "Nombre del libro")]
    public string NombreLibro { get; set; } = string.Empty;

    [Required(ErrorMessage = "El autor es obligatorio.")]
    [StringLength(100, ErrorMessage = "El autor no puede superar los 100 caracteres.")]
    [Display(Name = "Autor")]
    public string Autor { get; set; } = string.Empty;

    [Required(ErrorMessage = "La editorial es obligatoria.")]
    [StringLength(100, ErrorMessage = "La editorial no puede superar los 100 caracteres.")]
    [Display(Name = "Editorial")]
    public string NombreEditorial { get; set; } = string.Empty;

    [Required(ErrorMessage = "El ISDN es obligatorio.")]
    [StringLength(20, ErrorMessage = "El ISDN no puede superar los 20 caracteres.")]
    [Display(Name = "ISDN")]
    public string ISDN { get; set; } = string.Empty;
}