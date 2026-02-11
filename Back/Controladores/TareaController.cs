using Back.Interfaces;
using Back.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Back.Controladores;

[ApiController]
[Route("[controller]")]
public class TareaController : ControllerBase {
    
    public readonly ITareaService _tareasService;

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
                idTarea = idGenerado}
            );
        }
        catch (Exception ex) {
            return StatusCode(500, new {
                exito = false,
                mensaje = "Ocurrio un error inseperado al procesar la solicitud",
                detalle = ex.Message
            });
        } 
    }

}