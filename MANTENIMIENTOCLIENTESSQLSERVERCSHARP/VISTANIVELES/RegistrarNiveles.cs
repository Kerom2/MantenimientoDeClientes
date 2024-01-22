using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MANTENIMIENTOCLIENTESSQLSERVERCSHARP.VISTANIVELES
{
    public partial class RegistrarNiveles : Form
    {
        //Instancia las clases a utilizar
        MODELODEDATOS.ModeloNiveles mn = new MODELODEDATOS.ModeloNiveles();
        CONTROLOBJETOS.ManipulaciondeObjetos mo = new CONTROLOBJETOS.ManipulaciondeObjetos();

        public RegistrarNiveles()
        {
            InitializeComponent();
            mn.cargarcomboniveles(comboBox1);
            mo.bloquearobjetosniveles(textBox1, textBox2, button1, button2);
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
                //Aquí busca el código de nivel en la tabla de la BD
                if (mn.buscarcodigonivel(textBox1.Text) == 1)
                {
                    MessageBox.Show("NIVEL YA ESTÁ REGISTRADO..", "ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = "";
                    textBox1.Focus();
                }
                else
                {
                    MessageBox.Show(
                        "NIVEL NO ESTÁ REGISTRADO.., Puede Registrarlo..",
                        "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mo.desbloquearobjetosniveles(textBox1, textBox2, button1, button2);
                    //Invocar al desbloquearobjetos para cambiar el estado a objetos del Form
                    textBox2.Focus();
                }
            }
        }

        //Botón Registrar
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
                mn.insertarnivel(this.textBox1.Text, this.textBox2.Text);

                //Aquí le pasa cada uno de los campos textos a los parámetros del procedimiento ingresar en el 
                //modelo de datos y los que son double y dateTimePicker se convierten al tipo de dato
                mn.oDataAdapter.InsertCommand.Parameters["@codigoniv"].Value =
                    this.textBox1.Text;
                mn.oDataAdapter.InsertCommand.Parameters["@nombreniv"].Value =
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
                textBox1.Focus();
            }
        }

        //Botón Limpiar
        private void button3_Click(object sender, EventArgs e)
        {
            mo.limpiarcampostextosniveles(textBox1, textBox2);
        }

        //Clic del Combo Niveles
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = 
                mn.devuelvecodigonivel(Convert.ToString(comboBox1.SelectedItem));
        }
    }
}
