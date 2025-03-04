using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleCatalog.API.DTOs;

namespace VehicleCatalog.API.Services
{
    public interface IBrandService
    {
        Task<List<BrandDto>> GetBrandsAsync();
        Task<BrandDto?> GetBrandByIdAsync(int id);
        Task<BrandDto> CreateBrandAsync(BrandDto brandDto);
        Task<BrandDto?> UpdateBrandAsync(int id, BrandDto brandDto);
        Task<bool> DeleteBrandAsync(int id);
    }
}
    