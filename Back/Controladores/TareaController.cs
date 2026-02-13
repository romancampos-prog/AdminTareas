using Back.Interfaces;
using Back.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Tokens;

namespace Back.Controladores;

[ApiController]
[Route("[controller]")]
public class TareaController : ControllerBase {
    
    private readonly ITareaService _tareasService;
    

    public TareaController(ITareaService itareas) {
        _tareasService = itareas;
         
    }

    [HttpPost]
    public async Task<IActionResult> AgregarNuevaTarea(Tarea tarea) {
        if (tarea == null) {
            return StatusCode(400, new{error = "NO HAY TAREA QUE AGREGAR"});
        }

        if (string.IsNullOrWhiteSpace(tarea.NombreTarea)) {
            return StatusCode(400, new {error = "No hay nombre de tarea"});
        }

        try {
            long  idGenerado = await _tareasService.AgregarNuevaTarea(tarea);

            return StatusCode(201, new {
                exito = true,
                mensaje = "Tarea agregada con exito", 
                idTarea = idGenerado
            });
        }
        catch (Exception ex) {
            var detalleError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            
            return StatusCode(500, new {
                exito = false,
                mensaje = "Ocurrio un error inseperado al procesar la solicitud",
                detalle = detalleError
            });
        } 
    }
    

    [HttpPatch("{id}")]
    public async Task<IActionResult> TareaCompletada([FromRoute] long id, [FromBody] TareaEstatus nuevoEstatus ) {
        if (id <= 0 ) return StatusCode(400, new {exito = false, mensaje = "Id de tarea invalido"});
        if (string.IsNullOrEmpty(nuevoEstatus.Estatus)) return StatusCode(400, new {exito = false, mensaje = "El estatus es requerido"});
        if (!estatusValidos.Contains(nuevoEstatus.Estatus)) return StatusCode(400, new {exito = false, mensaje = "Estatus Invalido"});


        try {
            bool fueModificado = await _tareasService.TareaCompletada(id, nuevoEstatus.Estatus);
            
           if (fueModificado) {
                return StatusCode(200, new {
                   exito = true,
                   mensaje = $"Tarea marcada a: {nuevoEstatus.Estatus}",
                   
                });
            } 

            return StatusCode(404, new { exito = false, mensaje = "No se encontro la tarea" });

        } catch (Exception ex){
            var detalleError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            
            return StatusCode(500, new
            {
                exito = false,
                mensaje = "Ocurrio un error al editar la tarea",
                detalle = detalleError
            });
        }
    }

    
    [HttpPut("/editar/{id}")]
    public async Task<IActionResult> EditarTarea([FromRoute] long id, [FromBody] TareaEditar nuevaTareaEditada) {
        //verficar todos los datos sean correctos
        
    }

}