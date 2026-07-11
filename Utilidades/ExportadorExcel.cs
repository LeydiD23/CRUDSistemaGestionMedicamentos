using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace SistemaGestionMedicamentos.Utilidades
{
    /// <summary>
    /// Exporta un DataTable a un archivo Excel usando EPPlus.
    /// </summary>
    public static class ExportadorExcel
    {
        public static void Exportar(DataTable datos)
        {
            if (datos == null || datos.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Exportar Excel",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog dialogo = new SaveFileDialog
            {
                Filter = "Archivos Excel (*.xlsx)|*.xlsx",
                FileName = "Medicamentos_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx",
                Title = "Guardar archivo Excel"
            };

            if (dialogo.ShowDialog() != DialogResult.OK) return;

            try
            {
                using (OfficeOpenXml.ExcelPackage paquete = new OfficeOpenXml.ExcelPackage())
                {
                    OfficeOpenXml.ExcelWorksheet hoja = paquete.Workbook.Worksheets.Add("Medicamentos");

                    // Encabezados
                    for (int i = 0; i < datos.Columns.Count; i++)
                    {
                        hoja.Cells[1, i + 1].Value = datos.Columns[i].ColumnName;
                        hoja.Cells[1, i + 1].Style.Font.Bold = true;
                        hoja.Cells[1, i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        hoja.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(44, 62, 80));
                        hoja.Cells[1, i + 1].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    }

                    // Datos
                    for (int i = 0; i < datos.Rows.Count; i++)
                    {
                        for (int j = 0; j < datos.Columns.Count; j++)
                        {
                            hoja.Cells[i + 2, j + 1].Value = datos.Rows[i][j]?.ToString() ?? "";
                        }
                    }

                    // Autoajustar columnas
                    hoja.Cells.AutoFitColumns();

                    // Guardar archivo
                    FileInfo archivo = new FileInfo(dialogo.FileName);
                    paquete.SaveAs(archivo);
                }

                MessageBox.Show("Excel exportado correctamente:\n" + dialogo.FileName, "Exportaci�n exitosa",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar Excel: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
