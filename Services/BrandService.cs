using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleCatalog.API.Data;
using VehicleCatalog.API.DTOs;
using VehicleCatalog.API.Models;
using AutoMapper;
using System;

namespace VehicleCatalog.API.Services
{
    public class BrandService : IBrandService
    {
        private readonly VehicleCatalogDbContext _context;
        private readonly IMapper _mapper;

        public BrandService(VehicleCatalogDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<BrandDto>> GetBrandsAsync()
        {
            try
            {
                var brands = await _context.Brands.ToListAsync();
                return _mapper.Map<List<BrandDto>>(brands);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog or NLog)
                throw new ApplicationException("An error occurred while retrieving brands.", ex);
            }
        }

        public async Task<BrandDto?> GetBrandByIdAsync(int id)
        {
            try
            {
                var brand = await _context.Brands.FindAsync(id);
                if (brand == null) return null;

                return _mapper.Map<BrandDto>(brand);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new ApplicationException($"An error occurred while retrieving the brand with ID {id}.", ex);
            }
        }

        public async Task<BrandDto> CreateBrandAsync(BrandDto brandDto)
        {
            try
            {
                var brand = _mapper.Map<Brand>(brandDto);
                _context.Brands.Add(brand);
                await _context.SaveChangesAsync();

                brandDto.Id = brand.Id;
                return brandDto;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new ApplicationException("An error occurred while creating the brand.", ex);
            }
        }

        public async Task<BrandDto?> UpdateBrandAsync(int id, BrandDto brandDto)
        {
            try
            {
                var brand = await _context.Brands.FindAsync(id);
                if (brand == null) return null;

                _mapper.Map(brandDto, brand);
                _context.Brands.Update(brand);
                await _context.SaveChangesAsync();

                return _mapper.Map<BrandDto>(brand);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new ApplicationException($"An error occurred while updating the brand with ID {id}.", ex);
            }
        }

        public async Task<bool> DeleteBrandAsync(int id)
        {
            try
            {
                var brand = await _context.Brands.FindAsync(id);
                if (brand == null) return false;

                _context.Brands.Remove(brand);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new ApplicationException($"An error occurred while deleting the brand with ID {id}.", ex);
            }
        }
    }
}
