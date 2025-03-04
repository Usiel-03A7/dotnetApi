using AutoMapper;
using VehicleCatalog.API.DTOs;
using VehicleCatalog.API.Models;

namespace VehicleCatalog.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<Vehicle, VehicleDto>().ReverseMap();
            CreateMap<Image, ImageDto>().ReverseMap();
        }
    }
}
