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
        public byte[] Export(byte[] templateBytes, Dictionary<string, string> documento, IEnumerable<ModeloMovimientoDetalle> detalles)
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

                // Escribir encabezados en celdas específicas
                ws.Cell("K2").SetValue(numeroDocumento);
                ws.Cell("K2").SetValue(numeroDocumento);
                ws.Cell("B28").SetValue(observacion ?? string.Empty);

                // Escribir detalles desde la fila 11, columnas A y B
                var startRow = 11;

                var currentRow = startRow;
                foreach (var d in detalles)
                {
                    // Copiar estilo de la fila plantilla para mantener formato
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

                // Escribir encabezados en celdas específicas
                ws.Cell("E1").SetValue(numeroDocumento);
                ws.Cell("A6").SetValue(observacion ?? string.Empty);

                // Escribir detalles desde la fila 11, columnas A y B
                var startRow = 11;

                var currentRow = startRow;
                foreach (var d in detalles)
                {
                    // Copiar estilo de la fila plantilla para mantener formato
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
    }
}

