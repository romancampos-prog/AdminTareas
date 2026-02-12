using Back.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Back.Data;
public class TareaContext : DbContext {

    public TareaContext(DbContextOptions<TareaContext> options): base(options) {}

    public DbSet<Tarea> Tarea => Set<Tarea>();   //aqui la variabkle debe ser el mismo nombre de la tabla, sin no decirle a entity cual tabla busacr en la bd
}