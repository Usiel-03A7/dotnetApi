using FluentValidation;
using VehicleCatalog.API.DTOs;

namespace VehicleCatalog.API.Validators
{
    public class VehicleValidator : AbstractValidator<VehicleDto>
    {
        public VehicleValidator()
        {
            RuleFor(v => v.Model)
                .NotEmpty().WithMessage("El modelo del vehículo es obligatorio.")
                .MaximumLength(100).WithMessage("El modelo no puede exceder los 100 caracteres.");

            RuleFor(v => v.Year)
                .InclusiveBetween(1900, DateTime.Now.Year)
                .WithMessage($"El año debe estar entre 1900 y {DateTime.Now.Year}.");

            RuleFor(v => v.Images)
                .Must(images => images.Count <= 5)
                .WithMessage("Un vehículo no puede tener más de 5 imágenes.");
        }
    }
}
