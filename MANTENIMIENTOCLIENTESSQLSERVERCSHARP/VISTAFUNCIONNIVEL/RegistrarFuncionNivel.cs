using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MANTENIMIENTOCLIENTESSQLSERVERCSHARP.VISTAFUNCIONNIVEL
{
    public partial class RegistrarFuncionNivel : Form
    {
        //Instancia las clases a utilizar
        MODELODEDATOS.ModeloNiveles mn = new MODELODEDATOS.ModeloNiveles();
        CONTROLOBJETOS.ManipulaciondeObjetos mo = new CONTROLOBJETOS.ManipulaciondeObjetos();
        public string nivelfuncion = "";

        public RegistrarFuncionNivel()
        {
            InitializeComponent();
            mo.bloquearobjetosfuncionnivel(textBox2,textBox3,textBox4);
            mn.cargarcomboniveles(comboBox1);
            mn.cargarcombofunciones(comboBox2);
            cargarcombocondicionfuncion();
        }

        //Método para cargar en el Combo las condiciones
        public void cargarcombocondicionfuncion()
        {
            comboBox3.Items.Add("ACTIVA");
            comboBox3.Items.Add("DESACTIVA");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            VISTAPRINCIPAL.MenuPrincipal m = new
                VISTAPRINCIPAL.MenuPrincipal();
            m.Show();
            this.Hide();
        }

        //Evento Cuando se da Clic en el Combo Niveles
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
             nivelfuncion = "";
            //Aquí asigna al campo textBox2 el código de nivel, ya que en el combo
            //se selecciona el Nombre del Nivel
            textBox2.Text = (""+mn.devuelvecodigonivel(""+comboBox1.SelectedItem));
            //textBox2.Text = (""+comboBox1.SelectedItem);
            nivelfuncion += "" + textBox2.Text;
            comboBox1.Enabled = false;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Aquí asigna al campo textBox3 el código de nivel, ya que en el combo
            //se selecciona el Nombre de la Función
            textBox3.Text = ("" + mn.devuelvecodigofuncion("" + comboBox2.SelectedItem));
            //textBox3.Text = ("" + comboBox2.SelectedItem);
            nivelfuncion += "" + textBox3.Text;
            textBox4.Text = "" + nivelfuncion; 
            //Asigna a textBox4 la variable string nivelfuncion con la concatenación
            //del código de nivel + código de función
            comboBox2.Enabled = false;
        }

        //Botón Registrar
        private void button2_Click(object sender, EventArgs e)
        {
            if ((textBox2.Text == "") || (textBox3.Text == "") || 
                (textBox4.Text == ""))
            {
                MessageBox.Show("FALTAN DATOS POR COMPLETAR..", "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                mo.limpiarcampostextosfuncionniveles(textBox2, textBox3, textBox4);
                mo.bloquearobjetosfuncionnivel(textBox2, textBox3, textBox4);                
            }
            else
            {
                //Aquí llama a la función para ver si la función nivel no se encuentra
                if (mn.buscarfuncionnivel("" + textBox4.Text) == 0)
                {
                    //Aquí invoca el método insertarnivel del Modelo Niveles
                    //con cada uno de los nombres de los objetos del formulario
                    mn.insertarfuncionnivel(this.textBox4.Text, this.textBox2.Text,
                        this.textBox3.Text, Convert.ToString(comboBox3.SelectedItem));

                    //Aquí le pasa cada uno de los campos textos a los parámetros del procedimiento ingresar en el 
                    //modelo de datos y los que son double y dateTimePicker se convierten al tipo de dato
                    mn.oDataAdapter.InsertCommand.Parameters["@codifunniv"].Value =
                        this.textBox4.Text;
                    mn.oDataAdapter.InsertCommand.Parameters["@codigoniv"].Value =
                        this.textBox2.Text;
                    mn.oDataAdapter.InsertCommand.Parameters["@codigofun"].Value =
                        this.textBox3.Text;
                    mn.oDataAdapter.InsertCommand.Parameters["@estado"].Value =
                        this.comboBox3.SelectedItem;

                    //Abre la conexión
                    mn.oConexion.Open();

                    //Aquí ejecuta la inserción del cliente
                    mn.oDataAdapter.InsertCommand.ExecuteNonQuery();

                    MessageBox.Show("Datos Almacenados Correctamente..", "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    mn.oConexion.Close(); //Cierra la conexión

                    mo.limpiarcampostextosfuncionniveles(textBox2, textBox3, textBox4);
                    mo.bloquearobjetosfuncionnivel(textBox2, textBox3, textBox4);
                    //textBox1.Focus();
                    comboBox1.Enabled = true;
                    comboBox2.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Función Ya Existe en Nivel..", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    mo.limpiarcampostextosfuncionniveles(textBox2, textBox3, textBox4);
                    mo.bloquearobjetosfuncionnivel(textBox2, textBox3, textBox4);                    
                    comboBox1.Enabled = true;
                    comboBox2.Enabled = true;
                }
            }
        }

        //Botón limpiar
        private void button3_Click(object sender, EventArgs e)
        {
            mo.limpiarcampostextosfuncionniveles(textBox2, textBox3, textBox4);
            mo.bloquearobjetosfuncionnivel(textBox2, textBox3, textBox4);
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
        }
    }
}
