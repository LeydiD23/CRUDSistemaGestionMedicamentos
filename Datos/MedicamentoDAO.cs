using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using SistemaGestionMedicamentos.Entidades;

namespace SistemaGestionMedicamentos.Datos
{
    /// <summary>
    /// Capa de acceso a datos para la entidad Medicamento.
    /// Contiene operaciones CRUD con consultas parametrizadas.
    /// </summary>
    public class MedicamentoDAO
    {
        private Conexion conexion;

        public MedicamentoDAO()
        {
            conexion = new Conexion();
        }

        /// <summary>
        /// Inserta un nuevo medicamento en la base de datos.
        /// </summary>
        /// <param name="med">Objeto Medicamento con los datos a insertar.</param>
        /// <returns>True si se insert� correctamente.</returns>
        public bool Insertar(Medicamento med)
        {
            using (SqlConnection con = new SqlConnection(conexion.ObtenerCadenaConexion()))
            {
                try
                {
                    string query = @"INSERT INTO Medicamentos (Nombre, Categoria, Cantidad, FechaVencimiento, Descripcion)
                                     VALUES (@Nombre, @Categoria, @Cantidad, @FechaVencimiento, @Descripcion)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", med.Nombre);
                        cmd.Parameters.AddWithValue("@Categoria", med.Categoria);
                        cmd.Parameters.AddWithValue("@Cantidad", med.Cantidad);
                        cmd.Parameters.AddWithValue("@FechaVencimiento", med.FechaVencimiento);
                        cmd.Parameters.AddWithValue("@Descripcion", (object)med.Descripcion ?? DBNull.Value);

                        con.Open();
                        int filas = cmd.ExecuteNonQuery();
                        return filas > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al insertar medicamento: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        /// <summary>
        /// Obtiene todos los medicamentos como DataTable.
        /// </summary>
        public DataTable ObtenerTodos()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conexion.ObtenerCadenaConexion()))
            {
                try
                {
                    string query = @"SELECT IdMedicamento, Nombre, Categoria, Cantidad,
                                            FechaVencimiento, Descripcion
                                     FROM Medicamentos
                                     ORDER BY Nombre";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener medicamentos: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return dt;
        }

        /// <summary>
        /// Actualiza un medicamento existente.
        /// </summary>
        /// <param name="med">Objeto Medicamento con los datos actualizados.</param>
        /// <returns>True si se actualiz� correctamente.</returns>
        public bool Actualizar(Medicamento med)
        {
            using (SqlConnection con = new SqlConnection(conexion.ObtenerCadenaConexion()))
            {
                try
                {
                    string query = @"UPDATE Medicamentos
                                     SET Nombre = @Nombre,
                                         Categoria = @Categoria,
                                         Cantidad = @Cantidad,
                                         FechaVencimiento = @FechaVencimiento,
                                         Descripcion = @Descripcion
                                     WHERE IdMedicamento = @IdMedicamento";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@IdMedicamento", med.IdMedicamento);
                        cmd.Parameters.AddWithValue("@Nombre", med.Nombre);
                        cmd.Parameters.AddWithValue("@Categoria", med.Categoria);
                        cmd.Parameters.AddWithValue("@Cantidad", med.Cantidad);
                        cmd.Parameters.AddWithValue("@FechaVencimiento", med.FechaVencimiento);
                        cmd.Parameters.AddWithValue("@Descripcion", (object)med.Descripcion ?? DBNull.Value);

                        con.Open();
                        int filas = cmd.ExecuteNonQuery();
                        return filas > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar medicamento: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        /// <summary>
        /// Elimina un medicamento por su ID.
        /// </summary>
        /// <param name="id">ID del medicamento a eliminar.</param>
        /// <returns>True si se elimin� correctamente.</returns>
        public bool Eliminar(int id)
        {
            using (SqlConnection con = new SqlConnection(conexion.ObtenerCadenaConexion()))
            {
                try
                {
                    string query = "DELETE FROM Medicamentos WHERE IdMedicamento = @IdMedicamento";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@IdMedicamento", id);

                        con.Open();
                        int filas = cmd.ExecuteNonQuery();
                        return filas > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar medicamento: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        /// <summary>
        /// Busca medicamentos por nombre (b�squeda parcial).
        /// </summary>
        /// <param name="nombre">Texto a buscar en el nombre.</param>
        /// <returns>DataTable con los resultados.</returns>
        public DataTable BuscarPorNombre(string nombre)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conexion.ObtenerCadenaConexion()))
            {
                try
                {
                    string query = @"SELECT IdMedicamento, Nombre, Categoria, Cantidad,
                                            FechaVencimiento, Descripcion
                                     FROM Medicamentos
                                     WHERE Nombre LIKE @Nombre
                                     ORDER BY Nombre";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", "%" + nombre + "%");

                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar medicamentos: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return dt;
        }

        /// <summary>
        /// Obtiene el total de medicamentos registrados.
        /// </summary>
        public int ObtenerTotalMedicamentos()
        {
            using (SqlConnection con = new SqlConnection(conexion.ObtenerCadenaConexion()))
            {
                try
                {
                    string query = "SELECT COUNT(*) FROM Medicamentos";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        return (int)cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al contar medicamentos: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
            }
        }

        /// <summary>
        /// Obtiene el n�mero de medicamentos con cantidad menor a 10 (bajo stock).
        /// </summary>
        public int ObtenerBajoStock()
        {
            using (SqlConnection con = new SqlConnection(conexion.ObtenerCadenaConexion()))
            {
                try
                {
                    string query = "SELECT COUNT(*) FROM Medicamentos WHERE Cantidad < 10";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        return (int)cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener bajo stock: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
            }
        }

        /// <summary>
        /// Obtiene el n�mero de medicamentos que vencen en los pr�ximos 30 d�as.
        /// </summary>
        public int ObtenerProximosAVencer()
        {
            using (SqlConnection con = new SqlConnection(conexion.ObtenerCadenaConexion()))
            {
                try
                {
                    string query = @"SELECT COUNT(*)
                                     FROM Medicamentos
                                     WHERE FechaVencimiento BETWEEN CAST(GETDATE() AS DATE)
                                       AND DATEADD(DAY, 30, CAST(GETDATE() AS DATE))";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        return (int)cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener pr�ximos a vencer: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
            }
        }
    }
}
