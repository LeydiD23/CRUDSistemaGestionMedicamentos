using System;

namespace SistemaGestionMedicamentos.Entidades
{
    /// <summary>
    /// Representa un medicamento en el sistema.
    /// </summary>
    public class Medicamento
    {
        // Propiedades
        public int IdMedicamento { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string Descripcion { get; set; }

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public Medicamento()
        {
        }

        /// <summary>
        /// Constructor con todos los campos.
        /// </summary>
        public Medicamento(int idMedicamento, string nombre, string categoria,
                           int cantidad, DateTime fechaVencimiento, string descripcion)
        {
            IdMedicamento = idMedicamento;
            Nombre = nombre;
            Categoria = categoria;
            Cantidad = cantidad;
            FechaVencimiento = fechaVencimiento;
            Descripcion = descripcion;
        }

        /// <summary>
        /// Constructor sin IdMedicamento (para nuevos registros).
        /// </summary>
        public Medicamento(string nombre, string categoria, int cantidad,
                           DateTime fechaVencimiento, string descripcion)
        {
            Nombre = nombre;
            Categoria = categoria;
            Cantidad = cantidad;
            FechaVencimiento = fechaVencimiento;
            Descripcion = descripcion;
        }

        public override string ToString()
        {
            return $"{IdMedicamento} - {Nombre} ({Categoria})";
        }
    }
}
