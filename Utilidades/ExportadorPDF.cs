using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace SistemaGestionMedicamentos.Utilidades
{
    /// <summary>
    /// Exporta un DataTable a un archivo PDF usando iTextSharp.
    /// </summary>
    public static class ExportadorPDF
    {
        public static void Exportar(DataTable datos)
        {
            if (datos == null || datos.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Exportar PDF",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog dialogo = new SaveFileDialog
            {
                Filter = "Archivos PDF (*.pdf)|*.pdf",
                FileName = "Medicamentos_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf",
                Title = "Guardar archivo PDF"
            };

            if (dialogo.ShowDialog() != DialogResult.OK) return;

            try
            {
                // Usar iTextSharp para generar el PDF
                iTextSharp.text.Document documento = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate());
                iTextSharp.text.pdf.PdfWriter.GetInstance(documento, new FileStream(dialogo.FileName, FileMode.Create));
                documento.Open();

                // T�tulo
                iTextSharp.text.Font tituloFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.BOLD);
                documento.Add(new iTextSharp.text.Paragraph("Listado de Medicamentos", tituloFont));
                documento.Add(new iTextSharp.text.Paragraph(" "));

                // Tabla
                iTextSharp.text.pdf.PdfPTable tabla = new iTextSharp.text.pdf.PdfPTable(datos.Columns.Count);
                tabla.WidthPercentage = 100;

                // Encabezados
                iTextSharp.text.Font headerFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD);
                foreach (DataColumn columna in datos.Columns)
                {
                    tabla.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(columna.ColumnName, headerFont)));
                }

                // Datos
                iTextSharp.text.Font dataFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9);
                foreach (DataRow fila in datos.Rows)
                {
                    foreach (var valor in fila.ItemArray)
                    {
                        tabla.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(valor?.ToString() ?? "", dataFont)));
                    }
                }

                documento.Add(tabla);
                documento.Close();

                MessageBox.Show("PDF exportado correctamente:\n" + dialogo.FileName, "Exportaci�n exitosa",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar PDF: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
