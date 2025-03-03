namespace VehicleCatalog.API.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public int VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }
    }
}
