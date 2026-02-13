using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Back.Modelos;

/// <summary>
/// MODELO "TAREA"
/// IdTarea -> Identificador unico de la tarea, es auto incremental en la bd
/// NombreTarea -> Obligatorio nombre de la misma tarea
/// DescripcionTarea -> Descripcion breve sobrel la tarea especifica no obligatoria 
/// Prioridad -> Obligatorio solo tres prtioridades obligatorias por la bd (Alta, Media, Baja)
/// Status -> Obligatorio dos estatus (Pendiente, Completadio) por defecto 'Pendiente'
/// FechaRegistro -> Obligatoro automatico la fecha en la que la tarea se registra
/// </summary>


//Modelo principal
public class Tarea
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long IdTarea {get; set;}

    [Required]
    public required string NombreTarea {get; set;}

    public string? DescripcionTarea {get; set;}

    [Required]
    public required string Prioridad {get; set;} 

    [Required]
    public required string Estatus {get; set;}

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime FechaRegistro {get; set;} 
}


//Modelo tarea solo para modificar etstaus
public class TareaEstatus {
    [Required]
    public required string Estatus {get; set;}
}


//Modelo de tarea solo para editar propiedades especificas
public class TareaEditar {
    [Required]
    public required string NombreTarea {get; set;}

    public string? DescripcionTarea {get; set;}

    [Required]
    public required string Prioridad {get; set;}  

    [Required]
    public required string Estatus {get; set;}  
}