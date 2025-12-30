using ClosedXML.Excel;

namespace Spherical.Core.Creative
{
    /// <summary>
    /// Exporta un movimiento de inventario usando una plantilla XLSX, preservando estilos.
    /// </summary>
    public class ExcelPlantillaExporter
    {
        /// <summary>
        /// Representa un detalle de movimiento para exportación.
        /// </summary>
        public record ModeloMovimientoDetalle(string Elemento, int Cantidad);

        public record ModeloFacturaDetalle(string Elemento, int Cantidad);

        /// <summary>
        /// Aplica datos sobre una plantilla XLSX y devuelve el archivo resultante como bytes.
        /// </summary>
        /// <param name="templateBytes">Contenido de la plantilla XLSX.</param>
        /// <param name="documento">Modelo con la información del movimiento.</param>
        /// <param name="detalles">Listado de detalles a partir de la fila A11.</param>
        /// <returns>Bytes del archivo XLSX modificado.</returns>
        /// <exception cref="FileNotFoundException">Si la plantilla es nula o vacía.</exception>
        /// <exception cref="InvalidDataException">Si la plantilla no tiene hojas válidas.</exception>
        /// <exception cref="InvalidOperationException">Si ocurre un error de formato durante la exportación.</exception>
        public byte[] Export(byte[] templateBytes, IDictionary<string, object> documento, IEnumerable<ModeloMovimientoDetalle> detalles, string? worksheetName = null, int startRow = 7, int colElemento = 3, int colCantidad = 2)
        {
            if (templateBytes == null || templateBytes.Length == 0)
                throw new FileNotFoundException("La plantilla no existe o está vacía.");

            try
            {
                using var msIn = new MemoryStream(templateBytes);
                using var wb = new XLWorkbook(msIn);

                var ws = !string.IsNullOrWhiteSpace(worksheetName) ? wb.Worksheet(worksheetName) : (wb.Worksheets.Count > 0 ? wb.Worksheet(1) : null);
                if (ws == null)
                    throw new InvalidDataException("La plantilla no contiene hojas de cálculo.");

                foreach (var kv in documento)
                {
                    var key = kv.Key;
                    var value = kv.Value ?? string.Empty;

                    var isCellRef = System.Text.RegularExpressions.Regex.IsMatch(key, "^[A-Za-z]+[0-9]+$");
                    if (isCellRef)
                    {
                        ws.Cell(key).SetValue(value.ToString());
                        continue;
                    }

                    var nr = wb.NamedRanges.NamedRange(key);
                    if (nr != null)
                    {
                        var cell = nr.Ranges.FirstOrDefault()?.FirstCell();
                        if (cell != null)
                            cell.SetValue(value.ToString());
                    }
                }

                var currentRow = startRow;
                foreach (var d in detalles)
                {
                    ws.Cell(currentRow, colElemento).SetValue(d.Elemento ?? string.Empty);
                    ws.Cell(currentRow, colCantidad).SetValue(d.Cantidad);
                    currentRow++;
                }

                using var msOut = new MemoryStream();
                wb.SaveAs(msOut);
                return msOut.ToArray();
            }
            catch (ArgumentException ex)
            {
                throw new InvalidOperationException($"Error de formato en la plantilla: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al exportar XLSX: {ex.Message}", ex);
            }
        }


        public byte[] Export(byte[] templateBytes, int numeroDocumento, string? observacion, IEnumerable<ModeloFacturaDetalle> detalles)
        {
            if (templateBytes == null || templateBytes.Length == 0)
                throw new FileNotFoundException("La plantilla no existe o está vacía.");

            try
            {
                using var msIn = new MemoryStream(templateBytes);
                using var wb = new XLWorkbook(msIn);

                var ws = wb.Worksheets.Count > 0 ? wb.Worksheet(1) : null;
                if (ws == null)
                    throw new InvalidDataException("La plantilla no contiene hojas de cálculo.");

                ws.Cell("E1").SetValue(numeroDocumento);
                ws.Cell("A6").SetValue(observacion ?? string.Empty);

                var startRow = 11;

                var currentRow = startRow;
                foreach (var d in detalles)
                {
                    ws.Cell(currentRow, 1).SetValue(d.Elemento ?? string.Empty);
                    ws.Cell(currentRow, 2).SetValue(d.Cantidad);
                    currentRow++;
                }

                using var msOut = new MemoryStream();
                wb.SaveAs(msOut);
                return msOut.ToArray();
            }
            catch (ArgumentException ex)
            {
                throw new InvalidOperationException($"Error de formato en la plantilla: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al exportar XLSX: {ex.Message}", ex);
            }
        }

        public byte[] ExportFromJson(byte[] templateBytes, string encabezadosJson, IEnumerable<ModeloMovimientoDetalle> detalles, string? worksheetName = null, int startRow = 11, int colElemento = 1, int colCantidad = 2)
        {
            var dict = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(encabezadosJson) ?? new Dictionary<string, object>();
            return Export(templateBytes, dict, detalles, worksheetName, startRow, colElemento, colCantidad);
        }
    }
}
