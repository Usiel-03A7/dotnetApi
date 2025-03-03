using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Importar EntityFrameworkCore
using System.Collections.Generic;
using VehicleCatalog.API.Data;
using VehicleCatalog.API.Models;

namespace VehicleCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly VehicleCatalogDbContext _context;

        public BrandsController(VehicleCatalogDbContext context)
        {
            _context = context;
        }

        // GET: api/brands
        [HttpGet]
        public ActionResult<IEnumerable<Brand>> GetBrands()
        {
            return _context.Brands.ToList();
        }

        // GET: api/brands/5
        [HttpGet("{id}")]
        public ActionResult<Brand> GetBrand(int id)
        {
            var brand = _context.Brands.Find(id);

            if (brand == null)
            {
                return NotFound();
            }

            return brand;
        }

        // POST: api/brands
        [HttpPost]
        public ActionResult<Brand> PostBrand(Brand brand)
        {
            _context.Brands.Add(brand);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetBrand), new { id = brand.Id }, brand);
        }

        // PUT: api/brands/5
        [HttpPut("{id}")]
        public IActionResult PutBrand(int id, Brand brand)
        {
            if (id != brand.Id)
            {
                return BadRequest();
            }

            _context.Entry(brand).State = EntityState.Modified; // Usar EntityState
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/brands/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBrand(int id)
        {
            var brand = _context.Brands.Find(id);

            if (brand == null)
            {
                return NotFound();
            }

            _context.Brands.Remove(brand);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
