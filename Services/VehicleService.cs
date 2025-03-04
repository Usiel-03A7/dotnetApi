using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleCatalog.API.Data;
using VehicleCatalog.API.DTOs;
using VehicleCatalog.API.Models;
using AutoMapper;

namespace VehicleCatalog.API.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly VehicleCatalogDbContext _context;
        private readonly IMapper _mapper;

        public VehicleService(VehicleCatalogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<VehicleDto>> GetVehiclesAsync()
        {
            var vehicles = await _context.Vehicles
                .Include(v => v.Brand)
                .Include(v => v.Images)
                .ToListAsync();

            return _mapper.Map<List<VehicleDto>>(vehicles);
        }

        public async Task<VehicleDto> GetVehicleByIdAsync(int id)
        {
            var vehicle = await _context.Vehicles
                .Include(v => v.Brand)
                .Include(v => v.Images)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vehicle == null) return null;

            return _mapper.Map<VehicleDto>(vehicle);
        }

        public async Task<VehicleDto> CreateVehicleAsync(VehicleDto vehicleDto)
        {
            var vehicle = _mapper.Map<Vehicle>(vehicleDto);
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            vehicleDto.Id = vehicle.Id;
            return vehicleDto;
        }

        public async Task<VehicleDto> UpdateVehicleAsync(int id, VehicleDto vehicleDto)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null) return null;

            _mapper.Map(vehicleDto, vehicle);
            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();

            return _mapper.Map<VehicleDto>(vehicle);
        }

        public async Task<bool> DeleteVehicleAsync(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null) return false;

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
