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
    public partial class RegistrarUsuarios : Form
    {
        //Instancia las clases a utilizar
        MODELODEDATOS.ModeloNiveles mn = new MODELODEDATOS.ModeloNiveles();
        CONTROLOBJETOS.ManipulaciondeObjetos mo = new CONTROLOBJETOS.ManipulaciondeObjetos();
        MODELODEDATOS.ModeloUsuarios mu = new MODELODEDATOS.ModeloUsuarios();

        public RegistrarUsuarios()
        {
            InitializeComponent();
            mo.bloquearobjetosusuarios(textBox1, textBox2, textBox3, textBox4,
                dateTimePicker1, button1, button2);
            agregarcondicioncombo();
            mn.cargarcomboniveles(comboBox1);
            //Se cargan los niveles en el Combo
        }

        //Agrega al Combo las condiciones del Usuario
        public void agregarcondicioncombo()
        {
            comboBox2.Items.Add("ACTIVO");
            comboBox2.Items.Add("DESACTIVO");
        }

        //Botón buscar
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
                //Aquí busca si el login está registrado con la función
                if (mu.buscarlogin(textBox1.Text) == 1)
                {
                    MessageBox.Show("USUARIO YA ESTÁ REGISTRADO..", "ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    mo.limpiarcampostextos(textBox1, textBox2, textBox3, textBox4);
                    textBox1.Focus();
                }
                else
                {
                    MessageBox.Show(
                        "USUARIO NO ESTÁ REGISTRADO.., Puede Registrarlo..",
                        "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mo.desbloquearobjetosusuarios(
                        textBox1, textBox2, textBox3, textBox4, dateTimePicker1, button1, button2);
                    textBox2.Focus();
                }
            }
        }

        //Botón registrar
        private void button2_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "") || (textBox2.Text == "") ||
                (textBox3.Text == "") || (textBox4.Text == ""))
            {
                MessageBox.Show("FALTAN DATOS POR COMPLETAR..", "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                mo.limpiarcampostextos(textBox1, textBox2, textBox3, textBox4);
                mo.bloquearobjetosusuarios(textBox1, textBox2, textBox3, textBox4,
                    dateTimePicker1, button1, button2);
                //textBox1.Focus();
            }
            else
            {
                //Aquí llama al ingresarusuario para que se registre un usuario
                mu.ingresarusuario(this.textBox1.Text, this.textBox4.Text, 
                    Convert.ToDateTime(dateTimePicker1.Text),
                    this.textBox2.Text,
                    this.textBox3.Text,
                    mn.devuelvecodigonivel(Convert.ToString(this.comboBox1.SelectedItem)),
                    Convert.ToString(this.comboBox2.SelectedItem));
                
                //Aquí le pasa cada uno de los campos textos a los parámetros del procedimiento ingresar en el 
                //modelo de datos y los que son double y dateTimePicker se convierten al tipo de dato
                mu.oDataAdapter.InsertCommand.Parameters["@loginusuario"].Value =
                    this.textBox1.Text;
                mu.oDataAdapter.InsertCommand.Parameters["@nombre"].Value =
                    this.textBox4.Text;
                mu.oDataAdapter.InsertCommand.Parameters["@fecha"].Value =
                    Convert.ToDateTime(this.dateTimePicker1.Text);
                mu.oDataAdapter.InsertCommand.Parameters["@password"].Value =
                    this.textBox2.Text;
                mu.oDataAdapter.InsertCommand.Parameters["@identificacion"].Value =
                    this.textBox3.Text;
                mu.oDataAdapter.InsertCommand.Parameters["@codnivel"].Value =
                    mn.devuelvecodigonivel(Convert.ToString(this.comboBox1.SelectedItem));
                mu.oDataAdapter.InsertCommand.Parameters["@condicion"].Value =
                    this.comboBox2.SelectedItem;

                //Abre la conexión
                mu.oConexion.Open();

                //Aquí ejecuta la inserción del cliente
                mu.oDataAdapter.InsertCommand.ExecuteNonQuery();

                MessageBox.Show("Datos Almacenados Correctamente..", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                mu.oConexion.Close(); //Cierra la conexión

                mo.limpiarcampostextosusuarios(textBox1, textBox2, textBox3, textBox4,
                comboBox1, comboBox2);
                
                mo.bloquearobjetosusuarios(textBox1, textBox2, textBox3, textBox4, dateTimePicker1, button1, button2);
                textBox1.Focus();
            }
        }

        //Botón regresar
        private void button4_Click(object sender, EventArgs e)
        {
            VISTAPRINCIPAL.MenuPrincipal m = new
                VISTAPRINCIPAL.MenuPrincipal();
            m.Show();
            this.Hide();
        }

        //Botón limpiar
        private void button3_Click(object sender, EventArgs e)
        {
            mo.limpiarcampostextosusuarios(textBox1, textBox2, textBox3, textBox4, 
                comboBox1, comboBox2); 
        }
    }
}
