using System;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaGestionMedicamentos.Presentacion
{
    /// <summary>
    /// Formulario de inicio de sesi�n del sistema.
    /// Valida credenciales fijas (admin / 1234).
    /// </summary>
    public class FrmLogin : Form
    {
        private TextBox txtUsuario;
        private TextBox txtContrasena;
        private Button btnIniciarSesion;
        private Label lblTitulo;
        private Label lblUsuario;
        private Label lblContrasena;
        private Panel panelLogin;

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Configuraci�n del formulario
            this.Text = "Sistema de Gesti�n de Medicamentos - Login";
            this.Size = new Size(420, 370);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(44, 62, 80);
            this.Icon = null;

            // Panel contenedor blanco
            panelLogin = new Panel
            {
                Size = new Size(340, 240),
                Location = new Point(40, 65),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None
            };

            // T�tulo
            lblTitulo = new Label
            {
                Text = "Iniciar Sesi\u00f3n",
                Location = new Point(0, 20),
                Size = new Size(340, 30),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Etiqueta Usuario
            lblUsuario = new Label
            {
                Text = "Usuario:",
                Location = new Point(40, 70),
                Size = new Size(80, 22),
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            // TextBox Usuario
            txtUsuario = new TextBox
            {
                Location = new Point(120, 68),
                Size = new Size(180, 25),
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Etiqueta Contrase�a
            lblContrasena = new Label
            {
                Text = "Contrase\u00f1a:",
                Location = new Point(40, 105),
                Size = new Size(80, 22),
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            // TextBox Contrase�a
            txtContrasena = new TextBox
            {
                Location = new Point(120, 103),
                Size = new Size(180, 25),
                Font = new Font("Segoe UI", 10),
                PasswordChar = '*',
                BorderStyle = BorderStyle.FixedSingle
            };

            // Bot�n Iniciar Sesi�n
            btnIniciarSesion = new Button
            {
                Text = "Iniciar Sesi\u00f3n",
                Location = new Point(100, 150),
                Size = new Size(140, 38),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Cursor = Cursors.Hand
            };
            btnIniciarSesion.Click += BtnIniciarSesion_Click;

            // Ensamblar controles
            panelLogin.Controls.Add(lblTitulo);
            panelLogin.Controls.Add(lblUsuario);
            panelLogin.Controls.Add(txtUsuario);
            panelLogin.Controls.Add(lblContrasena);
            panelLogin.Controls.Add(txtContrasena);
            panelLogin.Controls.Add(btnIniciarSesion);

            this.Controls.Add(panelLogin);

            // Enter ejecuta el login
            this.AcceptButton = btnIniciarSesion;
        }

        /// <summary>
        /// Valida las credenciales ingresadas.
        /// </summary>
        private void BtnIniciarSesion_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text.Trim() == "admin" && txtContrasena.Text == "1234")
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuario o contrase\u00f1a incorrectos.", "Error de autenticaci\u00f3n",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Clear();
                txtContrasena.Clear();
                txtUsuario.Focus();
            }
        }
    }
}
