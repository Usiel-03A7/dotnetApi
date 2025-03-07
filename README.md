# Vehicle Catalog API

Este repositorio contiene la API para un sistema de catálogo de vehículos, que permite realizar operaciones de Altas, Bajas y Cambios (ABC) sobre vehículos y marcas. La API está desarrollada en .NET y utiliza SQLite como base de datos.

## Requerimientos del Cliente

El sistema permite:
- Crear, eliminar y modificar vehículos.
- Ver los vehículos en una lista paginada, ya sea en forma de tabla o tarjetas.
- Ver detalles de un vehículo (marca, modelo, año, color y fotos).
- Editar vehículos a través de un formulario.
- Gestionar marcas de vehículos, que se utilizan en un campo de selección al crear o editar un vehículo.

## Estructura del Proyecto

### Backend (.NET)
- **Entidades**: 
  - `Vehicle`: Representa un vehículo con propiedades como marca, modelo, año y fotos.
  - `Brand`: Representa una marca de vehículos.
  - `Image`: Representa una imagen asociada a un vehículo.
  
- **Controladores**:
  - `VehiclesController`: Maneja las operaciones CRUD para vehículos.
    - `GET /vehicles`: Obtiene una lista paginada de vehículos.
    - `GET /vehicles/{id}`: Obtiene un vehículo por su ID.
    - `POST /vehicles`: Crea un nuevo vehículo.
    - `PUT /vehicles/{id}`: Edita un vehículo existente.
    - `DELETE /vehicles/{id}`: Elimina un vehículo.
  
  - `BrandsController`: Maneja la obtención de marcas.
    - `GET /brands`: Obtiene todas las marcas disponibles.

- **Base de Datos**: 
  - Se utiliza SQLite para almacenar la información de vehículos, marcas e imágenes.
  - Se aplica un "Seeding" inicial para cargar las 10 marcas de vehículos más famosas.

### Frontend (Angular)
- **Rutas**:
  - `/vehicles`: Muestra el catálogo de vehículos.
  - `/vehicles/create`: Permite crear un nuevo vehículo.
  - `/vehicles/{id}`: Muestra los detalles de un vehículo.
  - `/vehicles/{id}/edit`: Permite editar un vehículo existente.
  - `/brands`: Muestra el catálogo de marcas.

- **Componentes**:
  - `VehicleListComponent`: Muestra la lista de vehículos.
  - `VehicleDetailComponent`: Muestra los detalles de un vehículo.
  - `VehicleFormComponent`: Maneja la creación y edición de vehículos.
  - `BrandListComponent`: Muestra la lista de marcas.

## Configuración del Proyecto

### Backend (.NET)
1. **Instalación**:
   - Clona el repositorio.
   - Abre la solución `.sln` en Visual Studio o tu IDE preferido.
   - Restaura los paquetes NuGet.

2. **Base de Datos**:
   - Asegúrate de que SQLite esté configurado en el archivo `appsettings.json`.
   - Ejecuta las migraciones para crear la base de datos:
     ```bash
     dotnet ef database update
     ```
   - Ejecuta el "Seeding" para cargar las marcas iniciales.

3. **Ejecución**:
   - Ejecuta la API en el puerto `7500`:
     ```bash
     dotnet run
     ```

