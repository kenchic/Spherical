# Gestión de Elementos - Spherical

## Descripción
Se ha implementado una nueva funcionalidad completa para la gestión de elementos (items) de la empresa, que incluye:

- **Grid de elementos** con funcionalidades de búsqueda, filtrado y paginación
- **Modal de edición/creación** con pestañas para información básica y listas de precios
- **Operaciones CRUD completas** (Crear, Leer, Actualizar, Eliminar)
- **Integración con API** para persistencia de datos
- **Interfaz moderna** usando MudBlazor

## Archivos Creados

### 1. API Controller
- **`ElementosController.cs`** - Controlador API con endpoints CRUD para elementos
  - `GET /api/v1/elementos` - Obtener todos los elementos
  - `GET /api/v1/elementos/{id}` - Obtener elemento por ID
  - `POST /api/v1/elementos` - Crear nuevo elemento
  - `PUT /api/v1/elementos/{id}` - Actualizar elemento
  - `DELETE /api/v1/elementos/{id}` - Eliminar elemento
  - `GET /api/v1/elementos/{id}/precios` - Obtener precios del elemento

### 2. Servicio Cliente
- **`ElementoService.cs`** - Servicio para consumir la API desde el cliente Blazor
  - Implementa `IElementoService` con métodos async
  - Manejo de respuestas con `ApiResponse<T>`
  - Configuración de HttpClient

### 3. Componentes Blazor
- **`GestionElementos.razor`** - Componente principal con grid de elementos
  - MudDataGrid con columnas configurables
  - Botones de acción (Crear, Editar, Eliminar, Actualizar)
  - Manejo de estados de carga
  - Integración con snackbars para notificaciones

- **`ElementoModal.razor`** - Modal para crear/editar elementos
  - **Pestaña 1: Información Básica**
    - Referencia (código único)
    - Nombre descriptivo
    - Metros cuadrados (M²)
    - Peso en kilogramos
    - Valor de rotación
    - Grupo de elemento (dropdown)
    - Unidad de medida (dropdown)
    - Estado activo/inactivo
  - **Pestaña 2: Lista de Precios**
    - Tabla con precios de alquiler, venta y pérdida
    - Solo disponible para elementos existentes
    - Botón de actualización de precios
    - Carga asíncrona de datos

### 4. Página de Navegación
- **`GestionElementosPage.razor`** - Página principal accesible en `/elementos`
  - Breadcrumbs de navegación
  - Layout con tarjeta contenedora
  - Autorización requerida

### 5. Configuración
- **`Program.cs`** - Registro de servicios y configuración de HttpClient
- **`MenuElementos.sql`** - Script SQL para agregar menús y permisos

## Configuración Requerida

### 1. Base de Datos
Ejecutar el script SQL para agregar los menús:
```sql
-- Ejecutar el archivo MenuElementos.sql en la base de datos Spherical
```

### 2. Permisos de Usuario
Asignar los siguientes permisos a los grupos de usuarios:
- **`PLineupMenu`** - Para ver el menú de Lineup
- **`PElementosPlus`** - Para gestión completa de elementos

### 3. Configuración de API
Verificar que la URL de la API esté correctamente configurada en `appsettings.json`:
```json
{
  "AppSettings": {
    "ApiUrl": "https://localhost:7079"
  }
}
```

## Uso de la Funcionalidad

### Acceso
1. Iniciar sesión en la aplicación
2. Navegar al menú **Lineup > Elementos**
3. O acceder directamente a `/elementos`

### Crear Elemento
1. Hacer clic en **"Nuevo Elemento"**
2. Completar la información básica en la primera pestaña
3. Hacer clic en **"Crear"**
4. Los precios se pueden configurar después editando el elemento

### Editar Elemento
1. Hacer clic en el botón **"Editar"** en la fila del elemento
2. Modificar la información en la pestaña **"Información Básica"**
3. Ver/actualizar precios en la pestaña **"Lista de Precios"**
4. Hacer clic en **"Actualizar"**

### Eliminar Elemento
1. Hacer clic en el botón **"Eliminar"** en la fila del elemento
2. Confirmar la eliminación en el diálogo

### Funcionalidades del Grid
- **Búsqueda global** en la barra superior
- **Filtros por columna** haciendo clic en los encabezados
- **Ordenamiento** haciendo clic en los encabezados de columna
- **Paginación** en la parte inferior
- **Actualización manual** con el botón "Actualizar"

## Características Técnicas

### Validaciones
- Validación de campos requeridos usando Data Annotations
- Validación en tiempo real en el formulario
- Manejo de errores con mensajes descriptivos

### UI/UX
- Diseño responsivo que se adapta a diferentes tamaños de pantalla
- Indicadores de carga durante operaciones asíncronas
- Notificaciones toast para feedback del usuario
- Iconos intuitivos para acciones
- Estados visuales para elementos activos/inactivos

### Rendimiento
- Carga asíncrona de datos
- Paginación del lado del servidor (preparado para implementar)
- Reutilización de componentes
- Gestión eficiente del estado

### Seguridad
- Autorización requerida para acceder a las páginas
- Validación de permisos a nivel de menú
- Sanitización de entradas
- Manejo seguro de tokens de autenticación

## Próximas Mejoras Sugeridas

1. **Paginación del servidor** - Implementar paginación real en el API
2. **Filtros avanzados** - Agregar filtros por rango de fechas, categorías, etc.
3. **Exportación** - Permitir exportar la lista a Excel/PDF
4. **Importación masiva** - Cargar elementos desde archivos CSV/Excel
5. **Historial de cambios** - Auditoría de modificaciones
6. **Imágenes de elementos** - Subida y gestión de imágenes
7. **Códigos de barras** - Generación e impresión de códigos
8. **Integración con inventario** - Conexión con módulos de stock

## Soporte

Para reportar problemas o solicitar nuevas funcionalidades, contactar al equipo de desarrollo.

---

**Fecha de implementación:** $(Get-Date -Format "yyyy-MM-dd")
**Versión:** 1.0.0
**Desarrollado con:** ASP.NET Core, Blazor Server, MudBlazor, Entity Framework Core