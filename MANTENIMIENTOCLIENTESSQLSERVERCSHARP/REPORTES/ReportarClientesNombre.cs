using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MANTENIMIENTOCLIENTESSQLSERVERCSHARP.REPORTES
{
    public partial class ReportarClientesNombre : Form
    {

        MODELODEDATOS.ModeloDatos m = new MODELODEDATOS.ModeloDatos();

        public ReportarClientesNombre()
        {
            InitializeComponent();
            textBox1.Focus();
        }

        //Botón Regresar
        private void button1_Click(object sender, EventArgs e)
        {
            VISTAPRINCIPAL.MenuPrincipal
                r = new VISTAPRINCIPAL.MenuPrincipal();
            r.Show();
            this.Hide();
        }

        //Botón Mostrar
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("FALTAN DATOS POR COMPLETAR..", "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
            }
            else
            {
                m.cargardatospornombre(Convert.ToString(textBox1.Text));
                m.cargardatosengridpornombre(dataGridView1);
            }
        }

        //Botón Nuevo
        private void button3_Click(object sender, EventArgs e)
        {
            REPORTES.ReportarClientesNombre
                r = new REPORTES.ReportarClientesNombre();
            r.Show();
            this.Hide();
            textBox1.Focus();
        }
    }
}
