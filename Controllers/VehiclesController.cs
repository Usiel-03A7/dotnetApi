using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleCatalog.API.Data;
using VehicleCatalog.API.Models;

namespace VehicleCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly VehicleCatalogDbContext _context;

        public VehiclesController(VehicleCatalogDbContext context)
        {
            _context = context;
        }

        // GET: api/vehicles
        [HttpGet]
        public ActionResult<IEnumerable<Vehicle>> GetVehicles()
        {
            return _context.Vehicles
                .Include(v => v.Brand) // Incluir la marca relacionada
                .Include(v => v.Images) // Incluir las imágenes relacionadas
                .ToList();
        }

        // GET: api/vehicles/5
        [HttpGet("{id}")]
        public ActionResult<Vehicle> GetVehicle(int id)
        {
            var vehicle = _context.Vehicles
                .Include(v => v.Brand) // Incluir la marca relacionada
                .Include(v => v.Images) // Incluir las imágenes relacionadas
                .FirstOrDefault(v => v.Id == id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return vehicle;
        }

        // POST: api/vehicles
        [HttpPost]
        public ActionResult<Vehicle> PostVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetVehicle), new { id = vehicle.Id }, vehicle);
        }

        // PUT: api/vehicles/5
        [HttpPut("{id}")]
        public IActionResult PutVehicle(int id, Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return BadRequest();
            }

            _context.Entry(vehicle).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/vehicles/5
        [HttpDelete("{id}")]
        public IActionResult DeleteVehicle(int id)
        {
            var vehicle = _context.Vehicles.Find(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            _context.Vehicles.Remove(vehicle);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
