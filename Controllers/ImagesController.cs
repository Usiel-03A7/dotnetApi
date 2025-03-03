using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleCatalog.API.Data;
using VehicleCatalog.API.Models;

namespace VehicleCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly VehicleCatalogDbContext _context;

        public ImagesController(VehicleCatalogDbContext context)
        {
            _context = context;
        }

        // GET: api/images
        [HttpGet]
        public ActionResult<IEnumerable<Image>> GetImages()
        {
            return _context.Images
                .Include(i => i.Vehicle) // Incluir el vehículo relacionado
                .ToList();
        }

        // GET: api/images/5
        [HttpGet("{id}")]
        public ActionResult<Image> GetImage(int id)
        {
            var image = _context.Images
                .Include(i => i.Vehicle) // Incluir el vehículo relacionado
                .FirstOrDefault(i => i.Id == id);

            if (image == null)
            {
                return NotFound();
            }

            return image;
        }

        // POST: api/images
        [HttpPost]
        public ActionResult<Image> PostImage(Image image)
        {
            _context.Images.Add(image);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetImage), new { id = image.Id }, image);
        }

        // PUT: api/images/5
        [HttpPut("{id}")]
        public IActionResult PutImage(int id, Image image)
        {
            if (id != image.Id)
            {
                return BadRequest();
            }

            _context.Entry(image).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/images/5
        [HttpDelete("{id}")]
        public IActionResult DeleteImage(int id)
        {
            var image = _context.Images.Find(id);

            if (image == null)
            {
                return NotFound();
            }

            _context.Images.Remove(image);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
