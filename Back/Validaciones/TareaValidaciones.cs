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
        if (string.IsNullOrEmpty(nombreTarea)) return "Es requerido un nombre de tarea";
        if (nombreTarea.Length > 50) return "El nombre de Tarea supera los 50 caracteres";
        return null;
    }

    
    //validar DescripcionTarea
    public static string? ValidarDescripcionTarea(string descripcionTarea) {
        if (descripcionTarea.Length > 150) return "La descripcion de la tarea supera los 150 caracteres";
        return null; 
    }


    //validar Prioridad
    public static string? ValidarPrioridad(string prioridadTarea) {
        if (string.IsNullOrEmpty(prioridadTarea)) return "La prioridad es requerida";
        if (!prioridadesValidas.Contains(prioridadTarea)) return "Priporidad invalida, validas = (Alta, Baja, Media)";
        return null;
    }
    
}