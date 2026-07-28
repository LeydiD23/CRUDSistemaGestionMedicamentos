using System;
using System.Windows.Forms;
using SistemaGestionMedicamentos.Presentacion;

// Mejora realizada para la práctica de Git Flow

namespace SistemaGestionMedicamentos
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FrmLogin login = new FrmLogin();
            if (login.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new FrmMenuPrincipal());
            }
        }
    }
}
