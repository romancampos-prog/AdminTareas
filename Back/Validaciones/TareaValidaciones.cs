using Back.Modelos;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Back.Validaciones;

public static class TareaValidaciones {
    private static readonly string[] prioridadesValidas = {"Alta", "Media", "Baja"};
    private static readonly string[] estatusValidos = {"Pendiente", "Completado"};
    
    //MICRO VALIDACIONES

    //IdTarea
    public static string? ValidarIdTarea(long id) {
        if(id <= 0) return "El id de la Tarea no es valida";
        return null;        
    }

    //NombreTarea
    public static string? ValidarNombreTarea (string nombreTarea) {
        if (string.IsNullOrEmpty(nombreTarea)) return "Es obligatorio un nombre de tarea";
        if (nombreTarea.Length > 50) return "El nombre de Tarea supera los 50 caracteres";
        return null;
    }

    
    //validar DescripcionTarea
    public static string? ValidarDescripcionTarea(string descripcionTarea) {
        if (string.IsNullOrEmpty(descripcionTarea)) return null;
        if (descripcionTarea.Length > 150) return "La descripcion de la tarea supera los 150 caracteres";
        return null; 
    }


    //validar Prioridad
    public static string? ValidarPrioridad(string prioridadTarea) {
        if (string.IsNullOrEmpty(prioridadTarea)) return "La prioridad es obligatoria";
        if (!prioridadesValidas.Contains(prioridadTarea)) return "Priporidad invalida, validas = (Alta, Baja, Media)";
        return null;
    }


    //validar estatus
    public static string? ValidarEstatus(string estatus) {
        if (string.IsNullOrEmpty(estatus)) return "El estatus es obligatorio";
        if (!estatusValidos.Contains(estatus)) return "Estatus invalido, solo puede ser 'Pendiente o Completado'";
        return null;
    }


    //validar FechaRegistro
    public static string? ValidarFechaRegistro(DateTime fechaRegistro) {
        if (fechaRegistro.Date != DateTime.Today) return "La fecha debe ser estrictamente la de hoy";
        return null;
    }


    //VALIDACIONES PARA FORMULARIOS:
    
    //nueva tarea
    public static List<string> ValidarNuevaTarea (Tarea nuevaTarea) {
        var errores = new List<string>();

        if(nuevaTarea == null) {
            errores.Add("No se encontraron datos de la tarea");
            return errores;
        }

        if (nuevaTarea.IdTarea != 0) errores.Add("Para una nueva tarea, no debes incluir id, o debe ser 0'");
        if (ValidarNombreTarea(nuevaTarea.NombreTarea) is string e1) errores.Add(e1);  
        if (ValidarDescripcionTarea(nuevaTarea.DescripcionTarea) is string e2) errores.Add(e2);
        if (ValidarPrioridad(nuevaTarea.Prioridad) is string e3) errores.Add(e3);
        if (ValidarEstatus(nuevaTarea.Estatus) is string e4) errores.Add(e4);
        if (ValidarFechaRegistro(nuevaTarea.FechaRegistro) is string e5) errores.Add(e5);
        return errores;
    }


    public static List<string> ValidarCambioEstatusTarea(TareaEstatus tareaEstatus, long id) {
        var errores = new List<string>();

        if (tareaEstatus == null) {
            errores.Add("No hay dato sobre el estatus de la tarea");
            return errores;
        }

        if(ValidarIdTarea(id) is string e1) errores.Add(e1);
        if (ValidarEstatus(tareaEstatus.Estatus) is string e2) errores.Add(e2);
        return errores;
    }


    public static List<string> ValidarEditarTarea(TareaEditar tareaEditar, long id)  {
        var errores = new List<string>();

        if (tareaEditar == null) {
            errores.Add("No existe informacion de la tarea a editar");
            return errores;
        }

        if (ValidarIdTarea(id) is string e1) errores.Add(e1);
        if (ValidarNombreTarea(tareaEditar.NombreTarea) is string e2) errores.Add(e2);
        if (ValidarDescripcionTarea(tareaEditar.DescripcionTarea) is string e3) errores.Add(e3);
        if (ValidarPrioridad(tareaEditar.Prioridad) is string e4) errores.Add(e4);
        if (ValidarEstatus(tareaEditar.Estatus) is string e5) errores.Add(e5);
        return errores;
    }
    
}