using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MANTENIMIENTOCLIENTESSQLSERVERCSHARP.VISTAUSUARIOS
{
    public partial class IniciarSesion : Form
    {
        //Instancia los modelos
        MODELODEDATOS.ModeloUsuarios mu = new MODELODEDATOS.ModeloUsuarios();
        MODELODEDATOS.ModeloBitacora mb = new MODELODEDATOS.ModeloBitacora();

        public IniciarSesion()
        {
            InitializeComponent();
        }

        //Botón Salir
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Botón Iniciar
        private void button2_Click(object sender, EventArgs e)
        {
            //Aquí se instancia el formulario Iniciar Sesión para accesar
            //la propiedad Name del Formulario y agregarlo en el detalle 
            //de la bitácora
            VISTAUSUARIOS.IniciarSesion r = new
                        VISTAUSUARIOS.IniciarSesion();

            if ((textBox1.Text == "") || (textBox2.Text == ""))
            {
                MessageBox.Show("FALTAN DATOS POR COMPLETAR..", "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
            }
            else
            {
                //Aquí llama a la función buscarloginpassword para que busque
                //el login y el password
                if (mu.buscarloginpassword(textBox1.Text,textBox2.Text) == 1)
                {
                    MessageBox.Show("USUARIO ENCONTRADO..", "INFORMACIÓN",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Aquí llamamos al insertar en bitácora para que inserte 
                    //un nuevo movimiento en la tabla bitácora para que quede
                    //registrado los datos del usuario que inicia la sesión

                    //Obtiene la fecha de actual                    
                    DateTime fecha = DateTime.Now; 
                    mb.ingresarbitacora(Convert.ToDateTime(fecha), ""+textBox1.Text, " ");

                    //Especifica los tipos de datos de los parámetros para la bitácora
                    mb.oDataAdapter.InsertCommand.Parameters["@fecha"].Value =
                        fecha;
                    mb.oDataAdapter.InsertCommand.Parameters["@usuario"].Value =
                        this.textBox1.Text;
                    mb.oDataAdapter.InsertCommand.Parameters["@detalle"].Value = 
                        r.Name;
                    //La propiedad Name obtiene el nombre del formulario y nótese que arriba
                    //antes se instancia el formulario de iniciar sesión

                    //Abre la conexión
                    mb.oConexion.Open();
                    //Aquí ejecuta la inserción en la tabla bitácora
                    mb.oDataAdapter.InsertCommand.ExecuteNonQuery();
                    mb.oConexion.Close(); //Cierra la conexión

                    //Aquí se instancia y llama al Menú, 
                    VISTAPRINCIPAL.MenuPrincipal m = new
                        VISTAPRINCIPAL.MenuPrincipal();
                    m.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show(
                        "USUARIO NO REGISTRADO ó NO ESTÁ ACTIVO, Inténtelo Nuevamente..",
                        "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = ""; textBox2.Text = "";
                    textBox1.Focus();
                }
            }
        }
    }
}
