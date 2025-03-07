using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleCatalog.API.Data;
using VehicleCatalog.API.DTOs;
using VehicleCatalog.API.Models;
using AutoMapper;
using Microsoft.Extensions.Logging;
using FluentValidation;
using System;
using VehicleCatalog.API.Validators;

namespace VehicleCatalog.API.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly VehicleCatalogDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<VehicleService> _logger;

        public VehicleService(VehicleCatalogDbContext context, IMapper mapper, ILogger<VehicleService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<VehicleDto>> GetVehiclesAsync(string? term, int? year, int page = 1, int pageSize = 10)
        {
            try
            {
                var query = _context.Vehicles
                    .Include(v => v.Brand)
                    .Include(v => v.Images)
                    .AsQueryable();

                // Aplicar filtros
                if (!string.IsNullOrEmpty(term))
                {
                    query = query.Where(v => v.Model.Contains(term) || v.Brand.Name.Contains(term));
                }

                if (year.HasValue)
                {
                    query = query.Where(v => v.Year == year.Value);
                }

                // Aplicar paginación
                var vehicles = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return _mapper.Map<List<VehicleDto>>(vehicles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los vehículos.");
                throw;
            }
        }

        public async Task<VehicleDto> GetVehicleByIdAsync(int id)
        {
            try
            {
                var vehicle = await _context.Vehicles
                    .Include(v => v.Brand)
                    .Include(v => v.Images)
                    .FirstOrDefaultAsync(v => v.Id == id);

                if (vehicle == null)
                {
                    _logger.LogWarning($"No se encontró el vehículo con ID {id}.");
                    return null;
                }

                return _mapper.Map<VehicleDto>(vehicle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el vehículo con ID {id}.");
                throw;
            }
        }

        public async Task<VehicleDto> CreateVehicleAsync(VehicleDto vehicleDto)
        {
            try
            {
                // Validar el DTO
                var validator = new VehicleValidator();
                var validationResult = await validator.ValidateAsync(vehicleDto);
                if (!validationResult.IsValid)
                {
                    _logger.LogWarning("Validación fallida al crear un vehículo: {Errors}", validationResult.Errors);
                    throw new ValidationException(validationResult.Errors);
                }

                // Validar que no se exceda el límite de 5 imágenes
                if (vehicleDto.Images.Count > 5)
                {
                    _logger.LogWarning("Se intentó crear un vehículo con más de 5 imágenes.");
                    throw new InvalidOperationException("Un vehículo no puede tener más de 5 imágenes.");
                }

                // Verificar si la marca existe
                var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == vehicleDto.BrandId);
                if (brand == null)
                {
                    _logger.LogWarning($"No se encontró la marca con ID {vehicleDto.BrandId}.");
                    throw new KeyNotFoundException($"No existe marca con ID {vehicleDto.BrandId}.");
                }

                // Mapear el DTO a la entidad Vehicle
                var vehicle = _mapper.Map<Vehicle>(vehicleDto);
                vehicle.Brand = brand;

                // Agregar las imágenes al vehículo
                foreach (var imageDto in vehicleDto.Images)
                {
                    vehicle.Images.Add(new Image { Url = imageDto.Url });
                }

                _context.Vehicles.Add(vehicle);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Vehículo creado exitosamente con ID {vehicle.Id}.");
                return _mapper.Map<VehicleDto>(vehicle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear un vehículo.");
                throw;
            }
        }

        public async Task<VehicleDto> UpdateVehicleAsync(int id, VehicleDto vehicleDto)
        {
            try
            {
                // Validar el DTO
                var validator = new VehicleValidator();
                var validationResult = await validator.ValidateAsync(vehicleDto);
                if (!validationResult.IsValid)
                {
                    _logger.LogWarning("Validación fallida al actualizar un vehículo: {Errors}", validationResult.Errors);
                    throw new ValidationException(validationResult.Errors);
                }

                // Validar que no se exceda el límite de 5 imágenes
                if (vehicleDto.Images.Count > 5)
                {
                    _logger.LogWarning("Se intentó actualizar un vehículo con más de 5 imágenes.");
                    throw new InvalidOperationException("Un vehículo no puede tener más de 5 imágenes.");
                }

                // Verificar si el vehículo existe
                var existingVehicle = await _context.Vehicles
                    .Include(v => v.Brand)
                    .Include(v => v.Images)
                    .FirstOrDefaultAsync(v => v.Id == id);

                if (existingVehicle == null)
                {
                    _logger.LogWarning($"No se encontró el vehículo con ID {id}.");
                    throw new KeyNotFoundException($"No existe vehículo con ID {id}.");
                }

                // Verificar si la marca existe
                var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == vehicleDto.BrandId);
                if (brand == null)
                {
                    _logger.LogWarning($"No se encontró la marca con ID {vehicleDto.BrandId}.");
                    throw new KeyNotFoundException($"No existe marca con ID {vehicleDto.BrandId}.");
                }

                // Actualizar los campos del vehículo
                existingVehicle.Model = vehicleDto.Model;
                existingVehicle.Year = vehicleDto.Year;
                existingVehicle.BrandId = vehicleDto.BrandId;
                existingVehicle.Brand = brand;

                // Actualizar las imágenes
                existingVehicle.Images.Clear();
                foreach (var imageDto in vehicleDto.Images)
                {
                    existingVehicle.Images.Add(new Image { Url = imageDto.Url });
                }

                _context.Vehicles.Update(existingVehicle);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Vehículo con ID {id} actualizado exitosamente.");
                return _mapper.Map<VehicleDto>(existingVehicle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar el vehículo con ID {id}.");
                throw;
            }
        }

        public async Task<bool> DeleteVehicleAsync(int id)
        {
            try
            {
                var vehicle = await _context.Vehicles.FindAsync(id);
                if (vehicle == null)
                {
                    _logger.LogWarning($"No se encontró el vehículo con ID {id}.");
                    return false;
                }

                _context.Vehicles.Remove(vehicle);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Vehículo con ID {id} eliminado exitosamente.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar el vehículo con ID {id}.");
                throw;
            }
        }
    }
}
