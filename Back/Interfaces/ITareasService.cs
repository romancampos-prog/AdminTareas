using Back.Modelos;
namespace Back.Interfaces;

public interface ITareaService
{
    //Agregar una nueva tarea
    Task<long> AgregarNuevaTarea(Tarea tarea);

    //cambiar el estatus de una tarea a completada
    Task<bool>TareaCambiarEstatus(long id, string status);
    
    //editar una tarea en especifico
    Task<bool>EditarTarea(long id, TareaEditar nuevaTareaEditada);

    //retorna una lista ordenada de tareas pendientes
    Task<ICollection<Tarea>> ObtenerTareasPendientes();

    //retorna una lista ordenada de tareas completadas
    Task<ICollection<Tarea>> TareasCompletadas();

    //retorna una tarea por Id
    Task<Tarea?>ObtenerUnaTarea(long id);

}