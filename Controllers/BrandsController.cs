using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleCatalog.API.Data;
using VehicleCatalog.API.Models;

namespace VehicleCatalog.API.Controllers
{
    [Route("api/brands")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly VehicleCatalogDbContext _context;

        public BrandsController(VehicleCatalogDbContext context)
        {
            _context = context;
        }

        // Obtener todas las marcas
        [HttpGet]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await _context.Brands.ToListAsync();
            return Ok(brands);
        }
    }
}
