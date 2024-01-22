using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;

using System.Windows.Forms;

namespace MANTENIMIENTOCLIENTESSQLSERVERCSHARP.VISTACLIENTES
{
    public partial class RegistrarClientes : Form
    {
        //Define e instancia la clase ManipulaciondeObjetos en mo
        CONTROLOBJETOS.ManipulaciondeObjetos mo = new 
            CONTROLOBJETOS.ManipulaciondeObjetos();
        
        //Instancia la clase ModeloDAtos
        MODELODEDATOS.ModeloDatos m = new MODELODEDATOS.ModeloDatos();

        //Instancia la clase Validaciones
        UTILITARIOS.Validaciones v = new UTILITARIOS.Validaciones();

        //Constructor
        public RegistrarClientes()
        {
            InitializeComponent();
            mo.bloquearobjetos(
                textBox1,textBox2,textBox3,textBox4,dateTimePicker1,button1,button2);
        }

        //Botón limpiar del Form
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
                if (m.buscaridentificacion(textBox1.Text) == 1)
                {
                    MessageBox.Show("CLIENTE YA ESTÁ REGISTRADO..", "ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    mo.limpiarcampostextos(textBox1, textBox2, textBox3, textBox4);
                    textBox1.Focus();
                }
                else
                {
                    MessageBox.Show(
                        "CLIENTE NO ESTÁ REGISTRADO.., Puede Registrarlo..",
                        "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mo.desbloquearobjetos(
                        textBox1, textBox2, textBox3, textBox4, dateTimePicker1, button1, button2);
                    textBox2.Focus();
                }
            }
        }

        //Botón Registrar
        private void button2_Click(object sender, EventArgs e)
        {
            //int iResultado;

            if ((textBox1.Text == "") || (textBox2.Text == "") ||
                (textBox3.Text == "") || (textBox4.Text == ""))
            {
                MessageBox.Show("FALTAN DATOS POR COMPLETAR..", "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                mo.limpiarcampostextos(textBox1, textBox2, textBox3, textBox4);
                mo.bloquearobjetos(textBox1, textBox2, textBox3, textBox4, 
                    dateTimePicker1, button1, button2);
                //textBox1.Focus();
            }
            else
            {
                //Aquí invoca el método ingresarcliente del Modelo de datos
                //con cada uno de los nombres de los objetos del formulario
                m.ingresarcliente(this.textBox1.Text,this.textBox2.Text,this.textBox3.Text,
                    Double.Parse(this.textBox4.Text),
                    Convert.ToDateTime(dateTimePicker1.Text));
                
                //Aquí le pasa cada uno de los campos textos a los parámetros del procedimiento ingresar en el 
                //modelo de datos y los que son double y dateTimePicker se convierten al tipo de dato
                m.oDataAdapter.InsertCommand.Parameters["@identificacion"].Value = 
                    this.textBox1.Text;
                m.oDataAdapter.InsertCommand.Parameters["@nombre"].Value = 
                    this.textBox2.Text;
                m.oDataAdapter.InsertCommand.Parameters["@telefono"].Value = 
                    this.textBox3.Text;
                m.oDataAdapter.InsertCommand.Parameters["@monto"].Value = 
                    Double.Parse(this.textBox4.Text);
                m.oDataAdapter.InsertCommand.Parameters["@fecha"].Value = 
                    Convert.ToDateTime(this.dateTimePicker1.Text);

                //Abre la conexión
                m.oConexion.Open();
                
                //Aquí ejecuta la inserción del cliente
                m.oDataAdapter.InsertCommand.ExecuteNonQuery();
                
                MessageBox.Show("Datos Almacenados Correctamente..", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                m.oConexion.Close(); //Cierra la conexión

                mo.limpiarcampostextos(textBox1, textBox2, textBox3, textBox4);
                mo.bloquearobjetos(textBox1, textBox2, textBox3, textBox4, dateTimePicker1, button1, button2);
                textBox1.Focus();                
            }
        }

        //Botón Regresar
        private void button4_Click(object sender, EventArgs e)
        {
            VISTAPRINCIPAL.MenuPrincipal m = new
                VISTAPRINCIPAL.MenuPrincipal();
            m.Show();
            this.Hide();
        }

        //Aquí se programa el llamado del procedimiento de la clase validaciones
        //para que solamente acepte letras en el campo texto
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Acá se llama al método Solotras de la clase Validaciones
            //para que en este campo texto solamente acepte caracteres alfabéticos
            v.SoloLetras(e);
        }

        //Aquí se programa el llamado del procedimiento de la clase validaciones
        //para que solamente acepte Números en el campo texto
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Acá se llama al método SoloNumeros de la clase Validaciones
            //para que en este campo texto solamente acepte números
            v.SoloNumeros(e);
        }
    }
}
