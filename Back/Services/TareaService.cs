using Microsoft.EntityFrameworkCore;
using Back.Data;
using Back.Interfaces;
using Back.Modelos;

namespace Back.Services;

public class TareaService : ITareaService
{
    private const bool V = false;
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
    public async Task<long> AgregarNuevaTarea(Tarea nuevaTarea)  {
        
        await _context.Tarea.AddAsync(nuevaTarea);

        await _context.SaveChangesAsync();

        return nuevaTarea.IdTarea;
    }


     /// <summary>
     /// Marca una tarea como completada, cambiando su estatus a 'Completado' y actualizando la fecha de registro a la fecha actual
     /// </summary>
     /// <param name="id">Identificador unico de la tarea</param>
     /// <param name="estatus">Nuevo estatus de la tarea (ej: Completado)</param>
     /// <returns>true si se completó la tarea, false si no se encontró la tarea</returns>
    public async Task<bool> TareaCompletada(long id, string estatus) {
        var tarea = await _context.Tarea.FindAsync(id);

        if (tarea == null) {
            return false;
        }

        tarea.Estatus = estatus;

        await _context.SaveChangesAsync();

        return true;
    }

    
    /// <summary>
    /// Edita una tarea, solo campos editables (Nombre tarea, DescripcionTarea, Prioridad, Estatus)
    /// </summary>
    /// <param name="id">Identificador unico de la tarea</param>
    /// <param name="nuevaTareaEditada">Objeto de la tarea</param>
    /// <returns>false si la tarea no se modifico con exito, true si la tarea fue modificada con exito</returns>
    public async Task<bool> EditarTarea(long id, Tarea nuevaTareaEditada) {
        var tareaEditada =  await _context.Tarea.FindAsync(id);

        if (tareaEditada == null) {
            return false;
        }

        tareaEditada.NombreTarea = nuevaTareaEditada.NombreTarea;
        tareaEditada.DescripcionTarea = nuevaTareaEditada.DescripcionTarea;
        tareaEditada.Prioridad = nuevaTareaEditada.Prioridad;
        tareaEditada.Estatus = nuevaTareaEditada.Estatus;

        await _context.SaveChangesAsync();

        return true;
    }

  
    ///<summary>
    /// Me entrga todas las tarea pendientes en orden de prioridad empezando por alta a baja y en orden de fecha de registro
    ///<summary>
    /// <returns>una lista ordenada de tareas<returns>
    public async Task<ICollection<Tarea>> ObtenerTareasPendientes() {
        return await _context.Tarea
                    .Where(t => t.Estatus == "Pendiente")
                    .OrderBy(t => t.Prioridad == "Alta" ? 1:
                                  t.Prioridad == "Media" ? 2:
                                  t.Prioridad == "Baja" ? 3 : 4)
                    .ThenBy(t => t.FechaRegistro) 
                    .ToListAsync();

    }


    ///<summary>
    /// Me entrga todas las tarea xon estatus completado en un orden de fecha de registro
    ///<summary>
    /// <returns>una lista ordenada de tareas completadas <returns>
    public async Task<ICollection<Tarea>> TareasCompletadas () {
        return await _context.Tarea
                        .Where(t => t.Estatus == "Completado")
                        .OrderBy(t => t.FechaRegistro)
                        .ToListAsync();
    }


    ///<summary>
    /// Devuelve una tarea en especifico segun su id
    ///<summary>
    /// <paramref name="id"/> Llave unica de la tarea
    /// <returns>un objeto especifico de tarea<returns>
    public async Task<Tarea?> ObtenerUnaTarea (long id ) {
        var tareaId = await _context.Tarea.FindAsync(id);

        if (tareaId == null) {
            return null;
        }
        
        return tareaId;
    }


}