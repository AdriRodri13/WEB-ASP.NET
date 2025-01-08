using Microsoft.EntityFrameworkCore;
using WEB_ASP.NET.Models;

namespace WEB_ASP.NET.Data
{
    public class TarifasContext : DbContext
    {
        public TarifasContext(DbContextOptions<TarifasContext> options) : base(options)
        {
        }

        public DbSet<Tarifas> Tarifas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la tabla
            modelBuilder.Entity<Tarifas>().ToTable("Tarifas");

            // Configura el ValueConverter para el campo Atributos
            modelBuilder.Entity<Tarifas>()
                .Property(t => t.Atributos)
                .HasConversion(
                    v => string.Join(",", v),  // Convierte la lista a un string (separado por comas)
                    v => v.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries) // Convierte el string en una lista (separado por comas)
                );
        }
    }
}
