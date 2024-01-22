﻿using System;
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
    public partial class ConsultarUsuarios : Form
    {
        //Instancia las clases a utilizar
        MODELODEDATOS.ModeloNiveles mn = new MODELODEDATOS.ModeloNiveles();
        CONTROLOBJETOS.ManipulaciondeObjetos mo = new CONTROLOBJETOS.ManipulaciondeObjetos();
        MODELODEDATOS.ModeloUsuarios mu = new MODELODEDATOS.ModeloUsuarios();

        public ConsultarUsuarios()
        {
            InitializeComponent();
            mo.bloquearobjetosusuariosconsulta(
                textBox1, textBox2, textBox3, textBox4, textBox5,
                dateTimePicker1);
            mu.cargarcombousuarios(comboBox1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            VISTAPRINCIPAL.MenuPrincipal m = new
                VISTAPRINCIPAL.MenuPrincipal();
            m.Show();
            this.Hide();
        }

        //Clic del combo login
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = Convert.ToString(comboBox1.SelectedItem);
            //Asigna al campo TextBox el elemento seleccionado del Combo
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
                //Acá se busca el cliente por medio de la función buscaridentificación
                if (mu.buscarlogin(textBox1.Text) == 1)
                {
                    MessageBox.Show("USUARIO ESTA REGISTRADO., Se Mostrarán sus Datos..",
                        "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Focus();

                    //Aquí se llama al procedimiento mostrardatoscliente
                    //del modelodatos
                    mu.mostrardatosusuario(Convert.ToString(textBox1.Text), textBox2,
                        textBox3, textBox4, textBox5, dateTimePicker1);
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

        //Botón limpiar
        private void button3_Click(object sender, EventArgs e)
        {
            mo.limpiarcampostextosusuariosconsulta(
                textBox1, textBox2, 
                textBox3, textBox4,textBox5,
                comboBox1);
        }
    }
}
