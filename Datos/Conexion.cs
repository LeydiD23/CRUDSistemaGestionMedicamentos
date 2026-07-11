using System;
using System.Data.SqlClient;

namespace SistemaGestionMedicamentos.Datos
{
    /// <summary>
    /// Gestiona la conexi�n a la base de datos SQL Server.
    /// Cambia la constante CADENA_CONEXION seg�n tu configuraci�n.
    /// </summary>
    public class Conexion
    {
        // Cadena de conexi�n - modificar seg�n entorno
        private const string CADENA_CONEXION =
            "Server=leydi;Database=DB_Medicamentos;Integrated Security=True;";

        private SqlConnection conexion;

        public Conexion()
        {
            conexion = new SqlConnection(CADENA_CONEXION);
        }

        /// <summary>
        /// Abre y retorna la conexi�n a la base de datos.
        /// </summary>
        public SqlConnection AbrirConexion()
        {
            if (conexion.State == System.Data.ConnectionState.Closed)
            {
                conexion.Open();
            }
            return conexion;
        }

        /// <summary>
        /// Cierra la conexi�n a la base de datos si est� abierta.
        /// </summary>
        public void CerrarConexion()
        {
            if (conexion != null && conexion.State == System.Data.ConnectionState.Open)
            {
                conexion.Close();
            }
        }

        /// <summary>
        /// Retorna la cadena de conexi�n actual.
        /// </summary>
        public string ObtenerCadenaConexion()
        {
            return CADENA_CONEXION;
        }

        // Liberar recursos
        public void Dispose()
        {
            CerrarConexion();
            conexion?.Dispose();
        }
    }
}
