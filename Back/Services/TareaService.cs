using Microsoft.EntityFrameworkCore;
using Back.Data;
using Back.Interfaces;
using Back.Modelos;

namespace Back.Services;

public class TareaService : ITareaService {
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

        if (tarea == null) return false;

        if (tarea.Estatus == estatus) return true;

        tarea.Estatus = estatus;

        try {
            await _context.SaveChangesAsync();
            return true;

        } catch (DbUpdateConcurrencyException) { //port si se borro la tarea en el proceso
            return false;
        }  
    }

    
    /// <summary>
    /// Edita una tarea, solo campos editables (Nombre tarea, DescripcionTarea, Prioridad, Estatus)
    /// </summary>
    /// <param name="id">Identificador unico de la tarea</param>
    /// <param name="nuevaTareaEditada">Objeto de la tarea</param>
    /// <returns>false si la tarea no se modifico con exito, true si la tarea fue modificada con exito</returns>
    public async Task<bool> EditarTarea(long id, TareaEditar nuevaTareaEditada) {
        var tarea =  await _context.Tarea.FindAsync(id);

        if (tarea == null) return false;

        tarea.NombreTarea = nuevaTareaEditada.NombreTarea;
        tarea.DescripcionTarea = nuevaTareaEditada.DescripcionTarea;
        tarea.Prioridad = nuevaTareaEditada.Prioridad;
        tarea.Estatus = nuevaTareaEditada.Estatus;

        try {
            await _context.SaveChangesAsync();
            return true;

        } catch (DbUpdateConcurrencyException) {
            return false; //la tarea desaparecio mientras editabas
        
        } catch (DbUpdateException ex) {
            throw new Exception("Error de la integridad en la bd", ex);
        
        } catch (Exception ex) {
            throw new Exception("Ocurrio un error inesperado", ex);
        } 
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