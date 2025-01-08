using WEB_ASP.NET.Models;
using Microsoft.EntityFrameworkCore;

namespace WEB_ASP.NET.Data
{
    public class ServiciosContext : DbContext
    {
        public ServiciosContext(DbContextOptions<ServiciosContext> options) : base(options) 
        {
            
        }

        public DbSet <Servicios> Servicios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Servicios>().ToTable("Servicios");
        }
    }
}
