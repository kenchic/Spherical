using ClosedXML.Excel;
using Spherical.Api.Models;
using Spherical.Core.Creative;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Spherical.Api.Tests
{
    public class ExcelMovimientoInventarioExporterTests
    {
        private static string? TryFindTemplatePath()
        {
            var candidates = new List<string>
            {
                Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "src", "Spherical", "wwwroot", "plantillas", "MvtoInterno.xlsx"),
                Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "src", "Spherical", "wwwroot", "plantillas", "MvtoInterno.xlsx"),
                Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "..", "src", "Spherical", "wwwroot", "plantillas", "MvtoInterno.xlsx")
            };

            // Buscar la raíz que contiene el solution file
            var dir = new DirectoryInfo(AppContext.BaseDirectory);
            for (int i = 0; i < 8 && dir != null; i++)
            {
                var sln = Path.Combine(dir.FullName, "src", "Spherical.sln");
                if (File.Exists(sln))
                {
                    candidates.Add(Path.Combine(dir.FullName, "src", "Spherical", "wwwroot", "plantillas", "MvtoInterno.xlsx"));
                    break;
                }
                dir = dir.Parent;
            }

            foreach (var p in candidates)
            {
                var full = Path.GetFullPath(p);
                if (File.Exists(full)) return full;
            }
            return null;
        }

        [Fact(Skip = "Se omite en CI: requiere plantilla física MvtoInterno.xlsx")]
        public void Export_InsertaCeldasYDetalles_PreservaFormato()
        {
            var path = TryFindTemplatePath();
            Assert.True(path != null && File.Exists(path!), "No se encontró la plantilla MvtoInterno.xlsx para la prueba");

            var templateBytes = File.ReadAllBytes(path!);
            var exporter = new ExcelPlantillaExporter();

            var detalles = new List<ExcelPlantillaExporter.ModeloMovimientoDetalle>
            {
                new("REF-001 - Elemento A", 3),
                new("REF-002 - Elemento B", 5)
            };

            var encabezados = new Dictionary<string, object>
            {
                ["K2"] = 12345,
                ["D4"] = string.Empty,
                ["K4"] = DateTime.Now.ToShortDateString(),
                ["B28"] = "Observación de prueba"
            };

            byte[] result = exporter.Export(templateBytes, encabezados, detalles);

            using var ms = new MemoryStream(result);
            using var wb = new XLWorkbook(ms);
            var ws = wb.Worksheet(1);

            // Verificar inserción en celdas específicas
            Assert.Equal(12345, ws.Cell("E1").GetValue<int>());
            Assert.Equal("Observación de prueba", ws.Cell("A6").GetString());

            // Verificar detalles
            Assert.Equal("REF-001 - Elemento A", ws.Cell(11, 1).GetString());
            Assert.Equal(3, ws.Cell(11, 2).GetValue<int>());
            Assert.Equal("REF-002 - Elemento B", ws.Cell(12, 1).GetString());
            Assert.Equal(5, ws.Cell(12, 2).GetValue<int>());

            // Preservación de estilo: comparar estilo de fila 11 con 12
            var style11 = ws.Row(11).Style;
            var style12 = ws.Row(12).Style;
            Assert.Equal(style11.Font.Bold, style12.Font.Bold);
            Assert.Equal(style11.Font.FontName, style12.Font.FontName);
            Assert.Equal(style11.Font.FontSize, style12.Font.FontSize);
            Assert.Equal(style11.Fill.BackgroundColor, style12.Fill.BackgroundColor);
            Assert.Equal(style11.Border.LeftBorder, style12.Border.LeftBorder);
            Assert.Equal(style11.Border.RightBorder, style12.Border.RightBorder);
            Assert.Equal(style11.Border.TopBorder, style12.Border.TopBorder);
            Assert.Equal(style11.Border.BottomBorder, style12.Border.BottomBorder);
        }

        [Fact]
        public void Export_PlantillaInvalida_LanzaExcepcion()
        {
            var exporter = new ExcelPlantillaExporter();
            var detalles = new List<ExcelPlantillaExporter.ModeloMovimientoDetalle>();
            var encabezados = new Dictionary<string, object>();

            Assert.ThrowsAny<Exception>(() => exporter.Export(new byte[] { 1, 2, 3 }, encabezados, detalles));
        }
    }
}
