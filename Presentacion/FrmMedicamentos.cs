using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SistemaGestionMedicamentos.Datos;
using SistemaGestionMedicamentos.Entidades;

namespace SistemaGestionMedicamentos.Presentacion
{
    /// <summary>
    /// Formulario para la gesti�n completa de medicamentos (CRUD).
    /// </summary>
    public class FrmMedicamentos : Form
    {
        // Controles de entrada
        private TextBox txtNombre;
        private TextBox txtCategoria;
        private NumericUpDown nudCantidad;
        private DateTimePicker dtpFechaVencimiento;
        private TextBox txtDescripcion;
        private TextBox txtBuscar;

        // Botones
        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnActualizar;
        private Button btnEliminar;
        private Button btnLimpiar;

        // DataGridView
        private DataGridView dgvMedicamentos;

        // Capa de datos
        private MedicamentoDAO dao;
        private DataTable dtMedicamentos;

        // Control de estado
        private int idSeleccionado = 0;

        public FrmMedicamentos()
        {
            dao = new MedicamentoDAO();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Configuraci�n del formulario
            this.Text = "Gesti\u00f3n de Medicamentos";
            this.Size = new Size(1100, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(236, 240, 241);

            // Panel superior (t�tulo)
            Panel panelTitulo = new Panel
            {
                Size = new Size(1100, 50),
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(44, 62, 80)
            };
            Label lblTituloForm = new Label
            {
                Text = "Gesti\u00f3n de Medicamentos",
                Location = new Point(20, 10),
                Size = new Size(400, 30),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White
            };
            panelTitulo.Controls.Add(lblTituloForm);

            // Panel de entrada de datos
            Panel panelInput = new Panel
            {
                Location = new Point(10, 60),
                Size = new Size(1080, 140),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None
            };

            // Fila 1: Nombre y Categor�a
            Label lblNombre = new Label { Text = "Nombre:", Location = new Point(20, 15), Size = new Size(100, 20),
                                          Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            txtNombre = new TextBox { Location = new Point(120, 12), Size = new Size(250, 22),
                                      Font = new Font("Segoe UI", 9), MaxLength = 100 };

            Label lblCategoria = new Label { Text = "Categor\u00eda:", Location = new Point(400, 15), Size = new Size(100, 20),
                                             Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            txtCategoria = new TextBox { Location = new Point(500, 12), Size = new Size(250, 22),
                                         Font = new Font("Segoe UI", 9), MaxLength = 100 };

            // Fila 2: Cantidad y Fecha
            Label lblCantidad = new Label { Text = "Cantidad:", Location = new Point(20, 50), Size = new Size(100, 20),
                                            Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            nudCantidad = new NumericUpDown
            {
                Location = new Point(120, 48),
                Size = new Size(100, 22),
                Minimum = 0,
                Maximum = 99999,
                Font = new Font("Segoe UI", 9)
            };

            Label lblFecha = new Label { Text = "Fecha Venc.:", Location = new Point(240, 50), Size = new Size(100, 20),
                                         Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            dtpFechaVencimiento = new DateTimePicker
            {
                Location = new Point(340, 48),
                Size = new Size(150, 22),
                Format = DateTimePickerFormat.Short,
                Font = new Font("Segoe UI", 9)
            };

            // Fila 3: Descripci�n
            Label lblDescripcion = new Label { Text = "Descripci\u00f3n:", Location = new Point(20, 85), Size = new Size(100, 20),
                                               Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            txtDescripcion = new TextBox { Location = new Point(120, 82), Size = new Size(550, 22),
                                           Font = new Font("Segoe UI", 9), MaxLength = 250 };

            panelInput.Controls.AddRange(new Control[] {
                lblNombre, txtNombre, lblCategoria, txtCategoria,
                lblCantidad, nudCantidad, lblFecha, dtpFechaVencimiento,
                lblDescripcion, txtDescripcion
            });

            // Panel de botones
            Panel panelBotones = new Panel
            {
                Location = new Point(10, 210),
                Size = new Size(1080, 50),
                BackColor = Color.FromArgb(236, 240, 241)
            };

            btnNuevo = CrearBoton("Nuevo", Color.FromArgb(52, 152, 219), 10);
            btnNuevo.Click += BtnNuevo_Click;

            btnGuardar = CrearBoton("Guardar", Color.FromArgb(46, 204, 113), 120);
            btnGuardar.Click += BtnGuardar_Click;

            btnActualizar = CrearBoton("Actualizar", Color.FromArgb(243, 156, 18), 230);
            btnActualizar.Click += BtnActualizar_Click;

            btnEliminar = CrearBoton("Eliminar", Color.FromArgb(231, 76, 60), 340);
            btnEliminar.Click += BtnEliminar_Click;

            btnLimpiar = CrearBoton("Limpiar", Color.FromArgb(149, 165, 166), 450);
            btnLimpiar.Click += BtnLimpiar_Click;

            panelBotones.Controls.AddRange(new Control[] {
                btnNuevo, btnGuardar, btnActualizar, btnEliminar, btnLimpiar
            });

            // Buscador
            Label lblBuscar = new Label
            {
                Text = "Buscar:",
                Location = new Point(10, 270),
                Size = new Size(60, 25),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };

            txtBuscar = new TextBox
            {
                Location = new Point(70, 272),
                Size = new Size(300, 22),
                Font = new Font("Segoe UI", 9)
            };
            txtBuscar.TextChanged += TxtBuscar_TextChanged;

            // DataGridView
            dgvMedicamentos = new DataGridView
            {
                Location = new Point(10, 305),
                Size = new Size(1070, 300),
                BackgroundColor = Color.White,
                AllowUserToOrderColumns = true,
                AllowUserToResizeRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 9)
            };

            // Estilo del DataGridView
            dgvMedicamentos.EnableHeadersVisualStyles = false;
            dgvMedicamentos.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(44, 62, 80),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            dgvMedicamentos.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(242, 245, 247)
            };
            dgvMedicamentos.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(52, 73, 94),
                SelectionBackColor = Color.FromArgb(52, 152, 219),
                SelectionForeColor = Color.White
            };
            dgvMedicamentos.SelectionChanged += DgvMedicamentos_SelectionChanged;

            // Agregar controles al formulario
            this.Controls.Add(panelTitulo);
            this.Controls.Add(panelInput);
            this.Controls.Add(panelBotones);
            this.Controls.Add(lblBuscar);
            this.Controls.Add(txtBuscar);
            this.Controls.Add(dgvMedicamentos);
        }

        /// <summary>
        /// Al cargar el formulario, carga todos los medicamentos.
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CargarMedicamentos();
            EstadoInicial();
        }

        // ===================== M�todos auxiliares =====================

        private Button CrearBoton(string texto, Color color, int x)
        {
            return new Button
            {
                Text = texto,
                Location = new Point(x, 8),
                Size = new Size(100, 35),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Cursor = Cursors.Hand
            };
        }

        /// <summary>
        /// Carga todos los medicamentos en el DataGridView.
        /// </summary>
        private void CargarMedicamentos()
        {
            dtMedicamentos = dao.ObtenerTodos();
            dgvMedicamentos.DataSource = dtMedicamentos;

            // Renombrar columnas para mostrar en espa�ol
            if (dgvMedicamentos.Columns.Count > 0)
            {
                dgvMedicamentos.Columns["IdMedicamento"].HeaderText = "ID";
                dgvMedicamentos.Columns["Nombre"].HeaderText = "Nombre";
                dgvMedicamentos.Columns["Categoria"].HeaderText = "Categor\u00eda";
                dgvMedicamentos.Columns["Cantidad"].HeaderText = "Cantidad";
                dgvMedicamentos.Columns["FechaVencimiento"].HeaderText = "Fecha Venc.";
                dgvMedicamentos.Columns["Descripcion"].HeaderText = "Descripci\u00f3n";
            }
        }

        /// <summary>
        /// Estado inicial del formulario (controles vac�os y deshabilitados).
        /// </summary>
        private void EstadoInicial()
        {
            LimpiarCampos();
            HabilitarControles(false);
            btnGuardar.Enabled = false;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;

            idSeleccionado = 0;
        }

        /// <summary>
        /// Habilita o deshabilita los controles de entrada.
        /// </summary>
        private void HabilitarControles(bool habilitar)
        {
            txtNombre.Enabled = habilitar;
            txtCategoria.Enabled = habilitar;
            nudCantidad.Enabled = habilitar;
            dtpFechaVencimiento.Enabled = habilitar;
            txtDescripcion.Enabled = habilitar;
        }

        /// <summary>
        /// Limpia todos los campos del formulario.
        /// </summary>
        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtCategoria.Clear();
            nudCantidad.Value = 0;
            dtpFechaVencimiento.Value = DateTime.Now;
            txtDescripcion.Clear();
        }

        /// <summary>
        /// Valida que los campos obligatorios no est�n vac�os y cumplan las reglas.
        /// </summary>
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El campo Nombre es obligatorio.", "Validaci\u00f3n",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCategoria.Text))
            {
                MessageBox.Show("El campo Categor\u00eda es obligatorio.", "Validaci\u00f3n",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCategoria.Focus();
                return false;
            }

            if (nudCantidad.Value <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor que cero.", "Validaci\u00f3n",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudCantidad.Focus();
                return false;
            }

            if (txtNombre.Text.Length > 100)
            {
                MessageBox.Show("El nombre no puede tener m\u00e1s de 100 caracteres.", "Validaci\u00f3n",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (txtCategoria.Text.Length > 100)
            {
                MessageBox.Show("La categor\u00eda no puede tener m\u00e1s de 100 caracteres.", "Validaci\u00f3n",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (txtDescripcion.Text.Length > 250)
            {
                MessageBox.Show("La descripci\u00f3n no puede tener m\u00e1s de 250 caracteres.", "Validaci\u00f3n",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Construye un objeto Medicamento con los datos de los controles.
        /// </summary>
        private Medicamento ObtenerMedicamentoDeFormulario()
        {
            return new Medicamento
            {
                IdMedicamento = idSeleccionado,
                Nombre = txtNombre.Text.Trim(),
                Categoria = txtCategoria.Text.Trim(),
                Cantidad = (int)nudCantidad.Value,
                FechaVencimiento = dtpFechaVencimiento.Value,
                Descripcion = txtDescripcion.Text.Trim()
            };
        }

        // ===================== Eventos =====================

        /// <summary>
        /// Prepara el formulario para crear un nuevo medicamento.
        /// </summary>
        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            HabilitarControles(true);
            btnGuardar.Enabled = true;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;

            idSeleccionado = 0;
            txtNombre.Focus();
        }

        /// <summary>
        /// Guarda un nuevo medicamento en la base de datos.
        /// </summary>
        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            Medicamento med = ObtenerMedicamentoDeFormulario();

            if (dao.Insertar(med))
            {
                MessageBox.Show("Medicamento registrado correctamente.", "Operaci\u00f3n exitosa",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarMedicamentos();
                EstadoInicial();
            }
        }

        /// <summary>
        /// Actualiza el medicamento seleccionado.
        /// </summary>
        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un medicamento para actualizar.", "Validaci\u00f3n",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidarCampos()) return;

            Medicamento med = ObtenerMedicamentoDeFormulario();

            if (dao.Actualizar(med))
            {
                MessageBox.Show("Medicamento actualizado correctamente.", "Operaci\u00f3n exitosa",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarMedicamentos();
                EstadoInicial();
            }
        }

        /// <summary>
        /// Elimina el medicamento seleccionado previa confirmaci�n.
        /// </summary>
        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un medicamento para eliminar.", "Validaci\u00f3n",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "\u00bfEst\u00e1 seguro de eliminar este medicamento?",
                "Confirmar eliminaci\u00f3n",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (dao.Eliminar(idSeleccionado))
                {
                    MessageBox.Show("Medicamento eliminado correctamente.", "Operaci\u00f3n exitosa",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarMedicamentos();
                    EstadoInicial();
                }
            }
        }

        /// <summary>
        /// Limpia todos los campos y resetea el formulario.
        /// </summary>
        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            EstadoInicial();
        }

        /// <summary>
        /// Cuando se selecciona una fila, carga los datos en los controles.
        /// </summary>
        private void DgvMedicamentos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMedicamentos.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dgvMedicamentos.SelectedRows[0];

                if (fila.Cells["IdMedicamento"].Value != null)
                {
                    idSeleccionado = Convert.ToInt32(fila.Cells["IdMedicamento"].Value);
                    txtNombre.Text = fila.Cells["Nombre"].Value?.ToString() ?? "";
                    txtCategoria.Text = fila.Cells["Categoria"].Value?.ToString() ?? "";
                    nudCantidad.Value = Convert.ToInt32(fila.Cells["Cantidad"].Value ?? 0);
                    dtpFechaVencimiento.Value = Convert.ToDateTime(fila.Cells["FechaVencimiento"].Value ?? DateTime.Now);
                    txtDescripcion.Text = fila.Cells["Descripcion"].Value?.ToString() ?? "";

                    HabilitarControles(true);
                    btnGuardar.Enabled = false;
                    btnActualizar.Enabled = true;
                    btnEliminar.Enabled = true;

                }
            }
        }

        /// <summary>
        /// Filtra el DataGridView mientras el usuario escribe en el buscador.
        /// </summary>
        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.Trim();

            if (string.IsNullOrEmpty(filtro))
            {
                CargarMedicamentos();
            }
            else
            {
                dtMedicamentos = dao.BuscarPorNombre(filtro);
                dgvMedicamentos.DataSource = dtMedicamentos;
            }
        }
    }
}
