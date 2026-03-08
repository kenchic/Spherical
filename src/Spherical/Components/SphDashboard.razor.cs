using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Spherical.Components;

public partial class SphDashboard : ComponentBase
{
    #region Propiedades de Métricas
    
    public int TotalFollowers { get; set; } = 121500;
    public int TotalLikes { get; set; } = 30400;
    public int TotalShares { get; set; } = 207100;
    public int TotalComments { get; set; } = 213400;
    
    #endregion

    #region Datos para Gráficos
    
    public List<ChartSeries> EngagementSeries { get; set; } = new();
    public string[] MonthLabels { get; set; } = { "Ene", "Feb", "Mar", "Abr", "May", "Jun" };
    public ChartOptions ChartOptions { get; set; } = new();
    
    public double[] SocialMediaData { get; set; } = { 45.2, 28.7, 15.3, 10.8 };
    public string[] SocialMediaLabels { get; set; } = { "Instagram", "Facebook", "Twitter", "LinkedIn" };
    
    #endregion

    #region Datos de Posts
    
    public List<PostData> BestPosts { get; set; } = new();
    
    #endregion

    #region Métodos del Ciclo de Vida
    
    protected override async Task OnInitializedAsync()
    {
        await LoadDashboardData();
        ConfigureChartOptions();
        await base.OnInitializedAsync();
    }
    
    #endregion

    #region Métodos Privados
    
    private async Task LoadDashboardData()
    {
        // Simular carga de datos - aquí conectarías con tu API o servicio
        await Task.Delay(100); // Simular latencia
        
        // Configurar datos del gráfico de engagement
        EngagementSeries = new List<ChartSeries>
        {
            new ChartSeries
            {
                Name = "Likes",
                Data = new double[] { 5200, 6100, 7300, 6800, 8200, 9100 }
            },
            new ChartSeries
            {
                Name = "Shares",
                Data = new double[] { 2100, 2800, 3200, 2900, 3600, 4100 }
            },
            new ChartSeries
            {
                Name = "Comments",
                Data = new double[] { 1800, 2200, 2600, 2400, 2900, 3200 }
            }
        };
        
        // Configurar datos de mejores posts
        BestPosts = new List<PostData>
        {
            new PostData
            {
                ImageUrl = "https://via.placeholder.com/50",
                Description = "Nuevo producto lanzado con gran éxito",
                SocialMedia = "Instagram",
                Engagement = 0.0962,
                Date = DateTime.Now.AddDays(-2)
            },
            new PostData
            {
                ImageUrl = "https://via.placeholder.com/50",
                Description = "Tutorial paso a paso muy popular",
                SocialMedia = "Facebook",
                Engagement = 0.0348,
                Date = DateTime.Now.AddDays(-5)
            },
            new PostData
            {
                ImageUrl = "https://via.placeholder.com/50",
                Description = "Detrás de cámaras del equipo",
                SocialMedia = "Twitter",
                Engagement = 0.0251,
                Date = DateTime.Now.AddDays(-7)
            },
            new PostData
            {
                ImageUrl = "https://via.placeholder.com/50",
                Description = "Artículo técnico especializado",
                SocialMedia = "LinkedIn",
                Engagement = 0.021,
                Date = DateTime.Now.AddDays(-10)
            },
            new PostData
            {
                ImageUrl = "https://via.placeholder.com/50",
                Description = "Evento en vivo transmitido",
                SocialMedia = "Instagram",
                Engagement = 0.0206,
                Date = DateTime.Now.AddDays(-12)
            }
        };
    }
    
    private void ConfigureChartOptions()
    {
        ChartOptions = new ChartOptions
        {
            YAxisTicks = 1000,
            MaxNumYAxisTicks = 6,
            YAxisLines = true,
            XAxisLines = false,
            LineStrokeWidth = 3,
            ChartPalette = new string[] { "#667eea", "#f093fb", "#4facfe" }
        };
    }
    
    private Color GetSocialMediaColor(string socialMedia)
    {
        return socialMedia switch
        {
            "Instagram" => Color.Secondary,
            "Facebook" => Color.Primary,
            "Twitter" => Color.Info,
            "LinkedIn" => Color.Success,
            _ => Color.Default
        };
    }
    
    #endregion

    #region Métodos Públicos
    
    public async Task RefreshData()
    {
        // Simular actualización de datos
        await LoadDashboardData();
        StateHasChanged();
    }
    
    #endregion
}

#region Modelos de Datos

public class PostData
{
    public string ImageUrl { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string SocialMedia { get; set; } = string.Empty;
    public double Engagement { get; set; }
    public DateTime Date { get; set; }
}

#endregion