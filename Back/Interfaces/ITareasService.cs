using Back.Modelos;
namespace Back.Interfaces;

public interface ITareaService
{
    //Agregar una nueva tarea
    Task<long> AgregarNuevaTarea(Tarea tarea);
    

}