using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleCatalog.API.Data;
using VehicleCatalog.API.Models;

namespace VehicleCatalog.API.Controllers
{
    [Route("api/vehicles")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly VehicleCatalogDbContext _context;

        public VehiclesController(VehicleCatalogDbContext context)
        {
            _context = context;
        }

        // 1️⃣ Obtener vehículos con paginación y filtros
        [HttpGet]
        public async Task<IActionResult> GetVehicles([FromQuery] string? term, [FromQuery] int? year, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = _context.Vehicles
                .Include(v => v.Brand)
                .Include(v => v.Images)
                .AsQueryable();

            if (!string.IsNullOrEmpty(term))
            {
                query = query.Where(v => v.Model.Contains(term) || v.Brand.Name.Contains(term));
            }

            if (year.HasValue)
            {
                query = query.Where(v => v.Year == year.Value);
            }

            var totalRecords = await query.CountAsync();
            var vehicles = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            Response.Headers.Add("Pagination", totalRecords.ToString());
            Response.Headers.Add("Access-Control-Expose-Headers", "Pagination");

            return Ok(vehicles);
        }

        // 2️⃣ Obtener un vehículo por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleById(int id)
        {
            var vehicle = await _context.Vehicles
                .Include(v => v.Brand)
                .Include(v => v.Images)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vehicle == null)
                return NotFound(new { message = $"No existe vehículo con ID {id}" });

            return Ok(vehicle);
        }

        // 3️⃣ Crear un vehículo
        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] Vehicle vehicle)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == vehicle.BrandId);
            if (brand == null)
                return BadRequest(new { message = $"No existe marca con ID {vehicle.BrandId}" });

            vehicle.Brand = brand;

            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            return Ok(vehicle);
        }

        // 4️⃣ Editar un vehículo
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] Vehicle vehicle)
        {
            var existingVehicle = await _context.Vehicles.FindAsync(id);
            if (existingVehicle == null)
                return BadRequest(new { message = $"No existe vehículo con ID {id}" });

            existingVehicle.Model = vehicle.Model;
            existingVehicle.Year = vehicle.Year;
            existingVehicle.BrandId = vehicle.BrandId;

            await _context.SaveChangesAsync();
            return Ok(existingVehicle);
        }

        // 5️⃣ Eliminar un vehículo
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
                return NotFound(new { message = $"No existe vehículo con ID {id}" });

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
