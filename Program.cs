using Microsoft.EntityFrameworkCore;
using VehicleCatalog.API.Data;
using VehicleCatalog.API.Services;
using VehicleCatalog.API.Mappings;
using FluentValidation;
using VehicleCatalog.API.Validators;

var builder = WebApplication.CreateBuilder(args);

// Registrar el DbContext
builder.Services.AddDbContext<VehicleCatalogDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Registrar FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<BrandValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<VehicleValidator>();

// Registrar los servicios
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();

// Registrar los servicios necesarios para los controladores
builder.Services.AddControllers();

// Configurar Swagger (opcional)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar el pipeline de HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run("http://localhost:7500");
