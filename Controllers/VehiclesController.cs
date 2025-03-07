using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleCatalog.API.Data;
using VehicleCatalog.API.DTOs;
using VehicleCatalog.API.Models;
using VehicleCatalog.API.Services;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace VehicleCatalog.API.Controllers
{
    [Route("api/vehicles")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetVehicles([FromQuery] string? term, [FromQuery] int? year, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var vehicles = await _vehicleService.GetVehiclesAsync(term, year, page, pageSize);
            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleById(int id)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle == null)
                return NotFound(new { message = $"No existe vehículo con ID {id}" });

            return Ok(vehicle);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] VehicleDto vehicleDto)
        {
            var createdVehicle = await _vehicleService.CreateVehicleAsync(vehicleDto);
            return Ok(createdVehicle);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] VehicleDto vehicleDto)
        {
            var updatedVehicle = await _vehicleService.UpdateVehicleAsync(id, vehicleDto);
            return Ok(updatedVehicle);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var result = await _vehicleService.DeleteVehicleAsync(id);
            if (!result)
                return NotFound(new { message = $"No existe vehículo con ID {id}" });

            return Ok();
        }
    }
}
