# Pantalla de Creación de Proyectos

## Componentes
- `ProjectCreation`: Componente principal que orquesta la creación de proyectos.
- `ClientForm`: Formulario para crear clientes nuevos con validación y estados de carga.
- `CitySelector`: Selector y creador de ciudades basado en `CatalogoDetalle (CIUDAD)`.
- `UnitSelector`: Selector y creador de unidades de medida basado en `CatalogoDetalle (UN_ELEMENTO)`.

## Servicios
- `ClientService`: CRUD de clientes (`api/v1/clientes`).
- `CatalogService`: Detalles y creación en catálogos (`api/v1/catalogos/{id}/detalles`).
- `ProjectService`: CRUD de proyectos (`api/v1/proyectos`).

## Rutas
- Página: `@page "/proyectos/nuevo"` en `src/Spherical/Pages/ProjectCreation.razor`.

## Validación y UX
- Validación con `MudForm` y campos `Required`.
- Mensajes de error y éxito (`MudAlert`).
- Estados de carga en botones (`Loading`).

## API
- Controladores en `Spherical.Api`: `ClientesController`, `ProyectosController`, `CatalogosController (POST)`.

