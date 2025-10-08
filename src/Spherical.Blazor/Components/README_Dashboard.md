# Componente Dashboard - Spherical.Blazor

## Descripción
El componente `SphDashboard` es un dashboard completo desarrollado con MudBlazor que muestra métricas y estadísticas de la aplicación de manera visual e interactiva.

## Características

### 📊 Métricas Principales
- **Total Followers**: Tarjeta con gradiente azul-púrpura
- **Total Likes**: Tarjeta con gradiente rosa-rojo
- **Total Shares**: Tarjeta con gradiente azul-cian
- **Total Comments**: Tarjeta con gradiente rosa-amarillo

### 📈 Gráficos
- **Gráfico de Líneas**: Muestra el engagement por mes (Likes, Shares, Comments)
- **Gráfico de Dona**: Distribución por redes sociales

### 📋 Tabla de Datos
- Lista de los mejores posts con información detallada
- Incluye imagen, descripción, red social, engagement y fecha
- Chips de colores para identificar redes sociales

## Archivos Creados

1. **`SphDashboard.razor`**: Componente principal con la interfaz de usuario
2. **`SphDashboard.razor.cs`**: Lógica del componente y modelos de datos
3. **`DashboardPage.razor`**: Página de ejemplo que utiliza el componente

## Uso del Componente

### Uso Básico
```razor
<SphDashboard />
```

### Uso en una Página con Layout
```razor
@page "/dashboard"
@using Spherical.Blazor.Components

<SphLayout>
    <SphDashboard />
</SphLayout>
```

## Configuración del Menú

Para agregar el dashboard al menú de navegación, necesitas:

1. **Agregar entrada en la base de datos** (si usas menú dinámico):
```sql
INSERT INTO Menu (Id, Nombre, Url, Icono, Orden, Padre) 
VALUES ('dashboard', 'Dashboard', '/dashboard', 'dashboard', 1, NULL);
```

2. **O agregar manualmente en SphMenu.razor** (si usas menú estático):
```razor
<MudNavLink Href="/dashboard" Icon="@Icons.Material.Filled.Dashboard">
    Dashboard
</MudNavLink>
```

## Personalización

### Cambiar Datos
Modifica los valores en `SphDashboard.razor.cs`:
```csharp
public int TotalFollowers { get; set; } = 121500; // Cambia este valor
public int TotalLikes { get; set; } = 30400;     // Cambia este valor
// etc...
```

### Conectar con API Real
Reemplaza el método `LoadDashboardData()` para conectar con tu API:
```csharp
private async Task LoadDashboardData()
{
    // Reemplaza con llamadas a tu API
    var metrics = await ApiService.GetMetricsAsync();
    TotalFollowers = metrics.Followers;
    TotalLikes = metrics.Likes;
    // etc...
}
```

### Cambiar Colores
Modifica los gradientes en `SphDashboard.razor`:
```razor
Style="background: linear-gradient(135deg, #tu-color-1 0%, #tu-color-2 100%);"
```

## Dependencias

- **MudBlazor**: Framework de componentes UI
- **Microsoft.AspNetCore.Components**: Base de Blazor
- **System.Globalization**: Para formateo de números

## Estructura de Datos

### PostData
```csharp
public class PostData
{
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public string SocialMedia { get; set; }
    public double Engagement { get; set; }
    public DateTime Date { get; set; }
}
```

## Métodos Públicos

- **`RefreshData()`**: Actualiza los datos del dashboard
- **`GetSocialMediaColor(string)`**: Obtiene el color según la red social

## Responsive Design

El componente es completamente responsive:
- **xs=12**: Móviles (ancho completo)
- **sm=6**: Tablets (2 columnas)
- **md=3**: Desktop (4 columnas)

## Próximas Mejoras

1. Integración con servicios reales de datos
2. Filtros por fecha
3. Exportación de reportes
4. Notificaciones en tiempo real
5. Más tipos de gráficos

---

**Desarrollado para Spherical con MudBlazor y .NET 8**