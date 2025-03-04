using FluentValidation;
using VehicleCatalog.API.DTOs;

namespace VehicleCatalog.API.Validators
{
    public class BrandValidator : AbstractValidator<BrandDto>
    {
        public BrandValidator()
        {
            RuleFor(b => b.Name).NotEmpty().WithMessage("El nombre de la marca es obligatorio.");
            RuleFor(b => b.Name).MaximumLength(50).WithMessage("El nombre de la marca no puede exceder los 50 caracteres.");
        }
    }
}
