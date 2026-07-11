using System;
using System.Drawing;
using System.Windows.Forms;
using SistemaGestionMedicamentos.Datos;
using SistemaGestionMedicamentos.Utilidades;

namespace SistemaGestionMedicamentos.Presentacion
{
    /// <summary>
    /// Formulario principal con dashboard y navegaci�n.
    /// </summary>
    public class FrmMenuPrincipal : Form
    {
        private Panel panelHeader;
        private Label lblTitulo;
        private FlowLayoutPanel panelDashboard;
        private Panel cardTotal;
        private Panel cardBajoStock;
        private Panel cardProximos;
        private Label lblTotalNumero;
        private Label lblBajoStockNumero;
        private Label lblProximosNumero;
        private Button btnGestionar;
        private Button btnExportarPDF;
        private Button btnExportarExcel;
        private Button btnSalir;
        private MedicamentoDAO dao;

        public FrmMenuPrincipal()
        {
            dao = new MedicamentoDAO();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Configuraci�n del formulario
            this.Text = "Sistema de Gesti\u00f3n de Medicamentos";
            this.Size = new Size(850, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(236, 240, 241);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.FormClosing += FrmMenuPrincipal_FormClosing;

            // Panel superior
            panelHeader = new Panel
            {
                Size = new Size(850, 90),
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(44, 62, 80)
            };

            lblTitulo = new Label
            {
                Text = "Sistema de Gesti\u00f3n de Medicamentos",
                Location = new Point(30, 25),
                Size = new Size(500, 40),
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White
            };
            panelHeader.Controls.Add(lblTitulo);

            // Panel Dashboard
            panelDashboard = new FlowLayoutPanel
            {
                Location = new Point(30, 110),
                Size = new Size(790, 170),
                BackColor = Color.Transparent,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            // --- Tarjeta 1: Total medicamentos ---
            cardTotal = CrearTarjeta("Total Medicamentos", Color.FromArgb(52, 152, 219));
            lblTotalNumero = CrearLabelNumero(cardTotal);
            cardTotal.Controls.Add(lblTotalNumero);
            cardTotal.Controls.Add(CrearLabelDescripcion("Medicamentos registrados"));
            panelDashboard.Controls.Add(cardTotal);

            // --- Tarjeta 2: Bajo stock ---
            cardBajoStock = CrearTarjeta("Bajo Stock", Color.FromArgb(231, 76, 60));
            lblBajoStockNumero = CrearLabelNumero(cardBajoStock);
            cardBajoStock.Controls.Add(lblBajoStockNumero);
            cardBajoStock.Controls.Add(CrearLabelDescripcion("Menos de 10 unidades"));
            panelDashboard.Controls.Add(cardBajoStock);

            // --- Tarjeta 3: Pr�ximos a vencer ---
            cardProximos = CrearTarjeta("Pr\u00f3ximos a Venir", Color.FromArgb(243, 156, 18));
            lblProximosNumero = CrearLabelNumero(cardProximos);
            cardProximos.Controls.Add(lblProximosNumero);
            cardProximos.Controls.Add(CrearLabelDescripcion("Vencen en 30 d\u00edas"));
            panelDashboard.Controls.Add(cardProximos);

            // --- Botones ---
            btnGestionar = CrearBoton("Gestionar Medicamentos", Color.FromArgb(46, 204, 113), new Point(30, 310));
            btnGestionar.Click += BtnGestionar_Click;

            btnExportarPDF = CrearBoton("Exportar a PDF", Color.FromArgb(155, 89, 182), new Point(250, 310));
            btnExportarPDF.Click += BtnExportarPDF_Click;

            btnExportarExcel = CrearBoton("Exportar a Excel", Color.FromArgb(39, 174, 96), new Point(470, 310));
            btnExportarExcel.Click += BtnExportarExcel_Click;

            btnSalir = new Button
            {
                Text = "Salir",
                Location = new Point(690, 310),
                Size = new Size(130, 50),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Cursor = Cursors.Hand
            };
            btnSalir.Click += BtnSalir_Click;

            // Agregar controles
            this.Controls.Add(panelHeader);
            this.Controls.Add(panelDashboard);
            this.Controls.Add(btnGestionar);
            this.Controls.Add(btnExportarPDF);
            this.Controls.Add(btnExportarExcel);
            this.Controls.Add(btnSalir);
        }

        /// <summary>
        /// Al cargar el formulario, actualiza los indicadores del dashboard.
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CargarDashboard();
        }

        /// <summary>
        /// Actualiza los n�meros del dashboard consultando la base de datos.
        /// </summary>
        private void CargarDashboard()
        {
            lblTotalNumero.Text = dao.ObtenerTotalMedicamentos().ToString();
            lblBajoStockNumero.Text = dao.ObtenerBajoStock().ToString();
            lblProximosNumero.Text = dao.ObtenerProximosAVencer().ToString();
        }

        // ===================== M�todos auxiliares =====================

        private Panel CrearTarjeta(string titulo, Color color)
        {
            Panel p = new Panel
            {
                Size = new Size(240, 150),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None
            };

            Label lblTituloTarjeta = new Label
            {
                Text = titulo,
                Location = new Point(0, 10),
                Size = new Size(240, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = color,
                TextAlign = ContentAlignment.MiddleCenter
            };
            p.Controls.Add(lblTituloTarjeta);

            Label linea = new Label
            {
                Location = new Point(20, 35),
                Size = new Size(200, 2),
                BackColor = Color.FromArgb(230, 230, 230),
                BorderStyle = BorderStyle.None
            };
            p.Controls.Add(linea);

            return p;
        }

        private Label CrearLabelNumero(Panel tarjeta)
        {
            Label lbl = new Label
            {
                Location = new Point(0, 45),
                Size = new Size(240, 50),
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "0"
            };
            return lbl;
        }

        private Label CrearLabelDescripcion(string texto)
        {
            return new Label
            {
                Text = texto,
                Location = new Point(0, 100),
                Size = new Size(240, 25),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(149, 165, 166),
                TextAlign = ContentAlignment.MiddleCenter
            };
        }

        private Button CrearBoton(string texto, Color color, Point location)
        {
            return new Button
            {
                Text = texto,
                Location = location,
                Size = new Size(200, 50),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Cursor = Cursors.Hand
            };
        }

        // ===================== Eventos =====================

        private void BtnGestionar_Click(object sender, EventArgs e)
        {
            FrmMedicamentos frm = new FrmMedicamentos();
            frm.ShowDialog();
            CargarDashboard();
        }

        private void BtnExportarPDF_Click(object sender, EventArgs e)
        {
            ExportadorPDF.Exportar(dao.ObtenerTodos());
        }

        private void BtnExportarExcel_Click(object sender, EventArgs e)
        {
            ExportadorExcel.Exportar(dao.ObtenerTodos());
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Confirma antes de cerrar la aplicaci�n.
        /// </summary>
        private void FrmMenuPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "\u00bfEst\u00e1 seguro de cerrar la aplicaci\u00f3n?",
                "Confirmar cierre",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
