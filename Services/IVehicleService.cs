using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleCatalog.API.DTOs;

namespace VehicleCatalog.API.Services
{
    public interface IVehicleService
    {
        Task<List<VehicleDto>> GetVehiclesAsync();
        Task<VehicleDto> GetVehicleByIdAsync(int id);
        Task<VehicleDto> CreateVehicleAsync(VehicleDto vehicleDto);
        Task<VehicleDto> UpdateVehicleAsync(int id, VehicleDto vehicleDto);
        Task<bool> DeleteVehicleAsync(int id);
    }
}
