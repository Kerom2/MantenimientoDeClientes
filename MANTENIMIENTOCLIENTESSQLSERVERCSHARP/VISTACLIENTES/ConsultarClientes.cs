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
    public partial class ConsultarClientes : Form
    {
        MODELODEDATOS.ModeloDatos m = new MODELODEDATOS.ModeloDatos();
        CONTROLOBJETOS.ManipulaciondeObjetos mo = new CONTROLOBJETOS.ManipulaciondeObjetos();

        public ConsultarClientes()
        {
            InitializeComponent();
            mo.bloquearobjetos2(
                textBox1,textBox2,textBox3,textBox4,dateTimePicker1,button1);
            
            m.cargarcomboidentificacion(comboBox1);
            //Aquí invoca el método cargarcomboidentificacion del modelo de
            //datos con el nombre del objeto comboBox1 del Formulario
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
                    m.mostrardatosclientes(Convert.ToString(textBox1.Text),textBox2,
                        textBox3,textBox4,dateTimePicker1);
                }
                else
                {
                    MessageBox.Show("CLIENTE NO ESTA REGISTRADO., Debe Registrarlo..",
                        "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Focus();                    
                }
            }
        }

        //Botón Limpiar
        private void button3_Click(object sender, EventArgs e)
        {
            mo.limpiarcampostextos(textBox1,textBox2,textBox3,textBox4);
            comboBox1.Text="Seleccione ID";
            //Aquí limpia el campo texto y le asigna el texto especificado
            textBox1.Focus();
        }

        //Botón Regresar
        private void button4_Click(object sender, EventArgs e)
        {
            VISTAPRINCIPAL.MenuPrincipal m = new
                VISTAPRINCIPAL.MenuPrincipal();
            m.Show();
            this.Hide();
        }

        //Programación del evento Clic del Combo, para que cuando el usuario
        //haga Clic en algún elemento del Combo, se asigne al campo texto de
        //la Identificación
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = Convert.ToString(comboBox1.SelectedItem);
            //Asigna al campo TextBox el elemento seleccionado del Combo
        }
    }
}
