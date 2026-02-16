using Back.Interfaces;
using Back.Modelos;
using Back.Validaciones;
using Microsoft.AspNetCore.Mvc;

namespace Back.Controladores;

[ApiController]
[Route("[controller]")]
public class TareaController : ControllerBase {
    
    private readonly ITareaService _tareasService;
    

    public TareaController(ITareaService itareas) {
        _tareasService = itareas;
         
    }

    [HttpPost]
    public async Task<IActionResult> AgregarNuevaTarea(Tarea nuevaTarea) {
        var erroresT = TareaValidaciones.ValidarNuevaTarea(nuevaTarea);

        if (erroresT.Any()) { //hay errores?
            return StatusCode(400, new {
                exito = false,
                mensaje = "Error al capturar la tarea",
                detalle = erroresT
            });
        }

        try {
            long  idGenerado = await _tareasService.AgregarNuevaTarea(nuevaTarea);

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
    public async Task<IActionResult> TareaCambiarEstatus([FromRoute] long id, [FromBody] TareaEstatus nuevoEstatus ) {
        
        var erroresE = TareaValidaciones.ValidarCambioEstatusTarea(nuevoEstatus, id);

        if (erroresE.Any()) {
            return StatusCode(400, new {
                exito = false,
                mensaje = "Ocurrio un error al cambiar el estatus de la tarea",
                detalle = erroresE
            });
        }

        try {
            bool fueModificado = await _tareasService.TareaCambiarEstatus(id, nuevoEstatus.Estatus);
            
           if (fueModificado) {
                return StatusCode(200, new {
                   exito = true,
                   mensaje = $"Tarea marcada a: {nuevoEstatus.Estatus}",
                   
                });
            } 

            return StatusCode(404, new { exito = false, mensaje = "No se encontro la tarea" });

        } catch (Exception ex){
            var detalleError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            
            return StatusCode(500, new {
                exito = false,
                mensaje = "Ocurrio un error al editar el estatus de la tarea",
                detalle = detalleError
            });
        }
    }

    
    [HttpPut("editar/{id}")]
    public async Task<IActionResult> EditarTarea([FromRoute] long id, [FromBody] TareaEditar nuevaTareaEditada) {
        var erroresET = TareaValidaciones.ValidarEditarTarea(nuevaTareaEditada, id);

        if (erroresET.Any()) {
            return StatusCode(400, new {
                exito = false,
                mensaje = "Algo fallo al Actualizar la tarea",
                detalle = erroresET
            });
        }

        try {
            bool fueEditada = await _tareasService.EditarTarea(id, nuevaTareaEditada);

            if (!fueEditada) {
                return StatusCode (400, new {
                    exito = false,
                    mensaje = "Tarea no encontrada"
                });
            } 

            return StatusCode (201, new {
                exito = true,
                mensaje = "Tarea editada con exito"
            });
            
        } catch (Exception ex) {
            var detalleError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            
            return StatusCode(500, new {
                exito = false,
                mensaje = "Ocurrio un error al editar la tarea",
                detalle = detalleError
            });
        }
        
    } 


    [HttpGet("pendientes")]
    public async Task<IActionResult> ObrtenerTareasPendientes() {
        try {
            var tareas = await _tareasService.ObtenerTareasPendientes();
            
            if (!tareas.Any())  {
                return StatusCode(200 , new {
                    exito = true,
                    mensaje = "No hay tareas registradas por el momento"
                });
            }

            return StatusCode(200, new {
                exito = true,
                mensaje = "Tareas pendientes obtenidas con exito",
                detalle = tareas
            });

        } catch (Exception ex){
            var detalleError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            
            return StatusCode(500, new {
                exito = false,
                mensaje = "Ocurrio un error al obtener las tareas pendientes",
                detalle = detalleError
            });
        }
    }


}