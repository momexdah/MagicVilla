using MagicVilla_API.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Datos;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {
        
    }
    
    public DbSet<Villa> Villas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Villa>().HasData(
            new Villa()
            {
                id=1,
                nombre = "Villa Real",
                detalle = "Detalle...",
                imagen_url = "",
                ocupantes = 4,
                metros_cuadrados = 50,
                Tarifa = 200,
                amenidad = "",
                fecha_creacion = DateTime.Now,
                fecha_actualizacion = DateTime.Now
            },
            new Villa()
            {
                id=2,
                nombre = "Villa Premium",
                detalle = "Detalle...",
                imagen_url = "",
                ocupantes = 4,
                metros_cuadrados = 40,
                Tarifa = 150,
                amenidad = "",
                fecha_creacion = DateTime.Now,
                fecha_actualizacion = DateTime.Now
            }
            );
    }
}