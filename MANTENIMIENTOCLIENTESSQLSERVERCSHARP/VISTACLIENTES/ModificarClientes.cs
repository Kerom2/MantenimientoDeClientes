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
    public partial class ModificarClientes : Form
    {
        MODELODEDATOS.ModeloDatos m = new MODELODEDATOS.ModeloDatos();
        CONTROLOBJETOS.ManipulaciondeObjetos mo = new CONTROLOBJETOS.ManipulaciondeObjetos();

        public ModificarClientes()
        {
            InitializeComponent();
            mo.bloquearobjetos(textBox1,textBox2,textBox3,textBox4,
                dateTimePicker1,button1,button2);
        }

        //Botón Regresar
        private void button4_Click(object sender, EventArgs e)
        {
            VISTAPRINCIPAL.MenuPrincipal m = new
                VISTAPRINCIPAL.MenuPrincipal();
            m.Show();
            this.Hide();
        }

        //Botón Limpiar
        private void button3_Click(object sender, EventArgs e)
        {
            mo.limpiarcampostextos(textBox1, textBox2, textBox3, textBox4);
            mo.bloquearobjetos(
                            textBox1, textBox2, textBox3, textBox4, dateTimePicker1, button1, button2);
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
                    mo.desbloquearobjetos(
                        textBox1, textBox2, textBox3, textBox4, dateTimePicker1, button1, button2);
                    textBox2.Focus();
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

        //Programación del botón Actualizar que llama al método ModificarCliente
        //del Modelo Datos
        private void button2_Click(object sender, EventArgs e)
        {   
            //Esta fechaformato es la que vamos a llamar cuando se llame el método
            //modificarcliente y lo que se hace acá es convertir lo que tenga guardado
            //dateTimePicker1 a formato DateTime
            DateTime fechaformato = Convert.ToDateTime(dateTimePicker1.Value.ToString("dd-MM-yyyy"));

            //DateTime fecha = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day);

            if ((textBox2.Text == "") || (textBox3.Text == "") || (textBox4.Text == ""))
            {
                MessageBox.Show("FALTAN DATOS POR COMPLETAR..", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus();
            }
            else
            {
                //Aquí se llama al método modificarcliente del modelo de datos y este
                //método ya está trabajando la fecha con el tipo de dato DateTime
                m.modificarcliente(this.textBox1.Text, this.textBox2.Text, this.textBox3.Text,
                    Double.Parse(this.textBox4.Text), fechaformato);
                //Aquí se utiliza la variable fechaformato que fue donde quedó convertido lo
                //del dateTimePicker1 a DateTime

                //Aquí se pasa cada uno de los objetos del formulario a cada uno de los 
                //parámetros del procedimiento modificar en el 
                //modelo de datos y los que son double y dateTimePicker 
                //se convierten al tipo de dato, pero utilizando el procedo UpdateCommand
                m.oDataAdapter.UpdateCommand.Parameters["@identificacion"].Value =
                  this.textBox1.Text;
                m.oDataAdapter.UpdateCommand.Parameters["@nombre"].Value =
                  this.textBox2.Text;
                m.oDataAdapter.UpdateCommand.Parameters["@telefono"].Value =
                  this.textBox3.Text;
                m.oDataAdapter.UpdateCommand.Parameters["@monto"].Value =
                  Double.Parse(this.textBox4.Text);
                m.oDataAdapter.UpdateCommand.Parameters["@fecha"].Value =
                  Convert.ToDateTime(this.dateTimePicker1.Text);
                
                m.oConexion.Open();

                //Aquí ejecuta la modificación del cliente
                m.oDataAdapter.UpdateCommand.ExecuteNonQuery();

                MessageBox.Show("Datos Modificados Correctamente..", "Información",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);

                m.oConexion.Close(); //Cierra la conexión

                mo.limpiarcampostextos(textBox1, textBox2, textBox3, textBox4);
                mo.bloquearobjetos(textBox1, textBox2, textBox3, textBox4, dateTimePicker1, button1, button2);
                textBox1.Focus();                
            }
        }
    }
}
