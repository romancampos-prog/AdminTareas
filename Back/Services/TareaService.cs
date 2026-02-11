using Back.Data;
using Back.Interfaces;
using Back.Modelos;

namespace Back.Services;

public class TareaService : ITareaService
{
    private readonly TareaContext _context;

    public TareaService(TareaContext tareaContext)
    {
        _context = tareaContext;
    }

    //Todas las posibles consultas que puedo hacer con el Tareas



    /// <summary>
    /// Registra una nueva tarea asiganandole el estatus por defecto pendiente
    /// </summary>
    /// <param name="tarea"> Objeto de informacion tarea</param>
    /// <returns>Retorna el Id con el que se genero la nueva tarea</returns>
    public async Task<long> AgregarNuevaTarea(Tarea tarea)  {
        
        await _context.Tareas.AddAsync(tarea);

        await _context.SaveChangesAsync();

        return tarea.IdTarea;
    }


    //Eliminar una tarea (status a completado)

    //Modificar la prioridad de una tarea

    //Modificar el nombre de una tarea

    //Modificar las descripcion de una tarea

    //Mostrar todas las tareas en orden por prioridad(Alta, Media, Baja) y en orden de tarea mas antogua registrada 

    //Mostar todas las tareas completadas en orden de registro mas antiguo

    //Mostrar una tarea en especifico por id


}