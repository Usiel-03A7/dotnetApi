using Microsoft.EntityFrameworkCore;
using VehicleCatalog.API.Models;

namespace VehicleCatalog.API.Data
{
    public class VehicleCatalogDbContext : DbContext
    {
        public VehicleCatalogDbContext(DbContextOptions<VehicleCatalogDbContext> options)
            : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar la relación Brand -> Vehicles
            modelBuilder.Entity<Brand>()
                .HasMany(b => b.Vehicles) // Un Brand tiene muchos Vehicles
                .WithOne(v => v.Brand)    // Un Vehicle pertenece a un Brand
                .HasForeignKey(v => v.BrandId); // La clave foránea es BrandId

            // Configurar la relación Vehicle -> Images
            modelBuilder.Entity<Vehicle>()
                .HasMany(v => v.Images) // Un Vehicle tiene muchas Images
                .WithOne(i => i.Vehicle) // Una Image pertenece a un Vehicle
                .HasForeignKey(i => i.VehicleId); // La clave foránea es VehicleId

            // Agregar datos iniciales para la tabla Brands
            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = 1, Name = "Toyota" },
                new Brand { Id = 2, Name = "Ford" },
                new Brand { Id = 3, Name = "Chevrolet" },
                new Brand { Id = 4, Name = "Honda" },
                new Brand { Id = 5, Name = "BMW" },
                new Brand { Id = 6, Name = "Mercedes-Benz" },
                new Brand { Id = 7, Name = "Audi" },
                new Brand { Id = 8, Name = "Nissan" },
                new Brand { Id = 9, Name = "Hyundai" },
                new Brand { Id = 10, Name = "Volkswagen" }
            );
        }
    }
}
