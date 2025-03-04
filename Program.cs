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
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Registrar FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<BrandValidator>();

// Registrar los servicios
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();

// Registrar los servicios necesarios para los controladores
builder.Services.AddControllers();

// Configurar Swagger (opcional)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar JsonSerializerOptions para manejar referencias circulares
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

var app = builder.Build();

// Configurar el pipeline de HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Mapear los controladores
app.MapControllers();

app.Run();
