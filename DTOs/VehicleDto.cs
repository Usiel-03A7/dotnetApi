namespace VehicleCatalog.API.DTOs
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public BrandDto? Brand { get; set; } // Usa BrandDto en lugar de Brand
        public ICollection<ImageDto> Images { get; set; } = new List<ImageDto>();
    }
}
