namespace VehicleCatalog.API.DTOs
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public int BrandId { get; set; } // Asegúrate de que esta propiedad exista
        public BrandDto? Brand { get; set; }
        public ICollection<ImageDto> Images { get; set; } = new List<ImageDto>();
    }
}
