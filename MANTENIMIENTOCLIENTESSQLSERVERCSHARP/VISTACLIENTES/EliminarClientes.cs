using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MANTENIMIENTOCLIENTESSQLSERVERCSHARP.VISTACLIENTES
{
    public partial class EliminarClientes : Form
    {
        MODELODEDATOS.ModeloDatos m = new MODELODEDATOS.ModeloDatos();
        CONTROLOBJETOS.ManipulaciondeObjetos mo = new CONTROLOBJETOS.ManipulaciondeObjetos();

        public EliminarClientes()
        {
            InitializeComponent();
            mo.bloquearobjetos(textBox1, textBox2, textBox3, textBox4,
                dateTimePicker1, button1, button2);
        }

        //Botón Limpiar
        private void button3_Click(object sender, EventArgs e)
        {
            mo.limpiarcampostextos(textBox1, textBox2, textBox3, textBox4);
            mo.bloquearobjetos(
                            textBox1, textBox2, textBox3, textBox4, dateTimePicker1, button1, button2);
        }

        //Botón Regresar
        private void button4_Click(object sender, EventArgs e)
        {
            VISTAPRINCIPAL.MenuPrincipal m = new
                VISTAPRINCIPAL.MenuPrincipal();
            m.Show();
            this.Hide();
        }

        //Botón Buscar
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("FALTAN DATOS POR COMPLETAR..", "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
            }
            else
            {
                //Acá se busca el cliente por medio de la función buscaridentificación
                if (m.buscaridentificacion(textBox1.Text) == 1)
                {
                    MessageBox.Show("CLIENTE ESTA REGISTRADO., Se Mostrarán sus Datos..",
                        "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Focus();

                    //Aquí se llama al procedimiento mostrardatoscliente
                    //del modelodatos
                    m.mostrardatosclientes(Convert.ToString(textBox1.Text), textBox2,
                        textBox3, textBox4, dateTimePicker1);
                    mo.desbloquearobjetoseliminar(textBox1, textBox2, textBox3, textBox4, dateTimePicker1, button1, button2);
                }
                else
                {
                    MessageBox.Show("CLIENTE NO ESTA REGISTRADO., Debe Registrarlo..",
                        "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    mo.limpiarcampostextos(textBox1, textBox2, textBox3, textBox4);
                    textBox1.Focus();
                }
            }
        }

        //Botón eliminar
        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea Eliminar el Cliente (S/N) ?", "Eliminar",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == 
                DialogResult.Yes)
            {
                //Llama a eliminarcliente del modelo de datos
                m.eliminarcliente(textBox1.Text); 
                MessageBox.Show("CLIENTE HA SIDO ELIMINADO..",
                    "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                mo.limpiarcampostextos(textBox1, textBox2, textBox3, textBox4);
                mo.bloquearobjetos(textBox1, textBox2, textBox3, textBox4,
                    dateTimePicker1, button1, button2);
                textBox1.Focus();
            }
        }
    }
}
