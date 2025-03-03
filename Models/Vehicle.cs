namespace VehicleCatalog.API.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public Brand? Brand { get; set; }
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
