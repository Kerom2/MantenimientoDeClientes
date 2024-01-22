using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MANTENIMIENTOCLIENTESSQLSERVERCSHARP.VISTAFUNCIONES
{
    public partial class RegistrarFunciones : Form
    {
        //Instancia las clases a utilizar
        MODELODEDATOS.ModeloNiveles mn = new MODELODEDATOS.ModeloNiveles();
        CONTROLOBJETOS.ManipulaciondeObjetos mo = new CONTROLOBJETOS.ManipulaciondeObjetos();

        public RegistrarFunciones()
        {
            InitializeComponent();
            mo.bloquearobjetosniveles(textBox1,textBox2,button1,button2);
            mn.cargarcombofunciones(comboBox1);
        }

        //Regresar
        private void button4_Click(object sender, EventArgs e)
        {
            VISTAPRINCIPAL.MenuPrincipal m = new
                VISTAPRINCIPAL.MenuPrincipal();
            m.Show();
            this.Hide();
        }

        //Botón Buscar Función
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
                //Aquí busca el código de nivel en la tabla de la BD
                if (mn.buscarcodigofuncion(textBox1.Text) == 1)
                {
                    MessageBox.Show("FUNCIÓN YA ESTÁ REGISTRADA..", "ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = "";
                    textBox1.Focus();
                }
                else
                {
                    MessageBox.Show(
                        "FUNCIÓN NO ESTÁ REGISTRADA.., Puede Registrarla..",
                        "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mo.desbloquearobjetosniveles(textBox1, textBox2, button1, button2);
                    //Invocar al desbloquearobjetos para cambiar el estado a objetos del Form
                    textBox2.Focus();
                }
            }
        }

        //Botón Registrar Funciones
        private void button2_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "") || (textBox2.Text == ""))
            {
                MessageBox.Show("FALTAN DATOS POR COMPLETAR..", "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                mo.limpiarcampostextosniveles(textBox1, textBox2);
                mo.desbloquearobjetosniveles(textBox1, textBox2, button1, button2);
                //textBox1.Focus();
            }
            else
            {
                //Aquí invoca el método insertarnivel del Modelo Niveles
                //con cada uno de los nombres de los objetos del formulario
                mn.insertarfuncion(this.textBox1.Text, this.textBox2.Text);

                //Aquí le pasa cada uno de los campos textos a los parámetros del procedimiento ingresar en el 
                //modelo de datos y los que son double y dateTimePicker se convierten al tipo de dato
                mn.oDataAdapter.InsertCommand.Parameters["@codigofun"].Value =
                    this.textBox1.Text;
                mn.oDataAdapter.InsertCommand.Parameters["@nombrefun"].Value =
                    this.textBox2.Text;

                //Abre la conexión
                mn.oConexion.Open();

                //Aquí ejecuta la inserción del cliente
                mn.oDataAdapter.InsertCommand.ExecuteNonQuery();

                MessageBox.Show("Datos Almacenados Correctamente..", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                mn.oConexion.Close(); //Cierra la conexión

                mo.limpiarcampostextosniveles(textBox1, textBox2);
                mo.bloquearobjetosniveles(textBox1, textBox2, button1, button2);
                
                comboBox1.Items.Clear(); //Limpia combo
                mn.cargarcombofunciones(comboBox1); //Refresca combo

                textBox1.Focus();
            }
        }

        //botón Limpiar
        private void button3_Click(object sender, EventArgs e)
        {
            mo.limpiarcampostextosniveles(textBox1, textBox2);
        }

        //Clic Combo Funciones
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text =
                mn.devuelvecodigofuncion(Convert.ToString(comboBox1.SelectedItem));
        }
    }
}
