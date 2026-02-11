using Back.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Back.Data;
public class TareaContext : DbContext {

    public TareaContext(DbContextOptions<TareaContext> options): base(options) {}

    public DbSet<Tarea> Tareas => Set<Tarea>();
}