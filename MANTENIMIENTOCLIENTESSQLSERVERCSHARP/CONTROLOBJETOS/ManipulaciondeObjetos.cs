//Esta clase es la que controla el comportamiento de los objetos en los
//diferente formularios, comportamientos como bloquearlosobjetos, desbloquearobjetos
//limpiarobjetos, etc.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MANTENIMIENTOCLIENTESSQLSERVERCSHARP.CONTROLOBJETOS
{
    class ManipulaciondeObjetos
    {
        //Método que bloquea los objetos en el formulario
        public void bloquearobjetos(System.Windows.Forms.TextBox textbox1,
            TextBox textbox2, TextBox textbox3,TextBox textbox4, 
            DateTimePicker fecha,Button boton1, Button boton2)
        {
            textbox1.Enabled = true;
            textbox2.Enabled = false;
            textbox3.Enabled = false;
            textbox4.Enabled = false;
            boton1.Enabled = true;
            boton2.Enabled = false;
            fecha.Enabled = false;
        }

        //Método que bloquea los objetos en el formulario consultar
        public void bloquearobjetos2(System.Windows.Forms.TextBox textbox1,
            TextBox textbox2, TextBox textbox3, TextBox textbox4,            
            DateTimePicker datetime1, Button boton1)
        {
            textbox1.Enabled = true;
            textbox2.Enabled = false;
            textbox3.Enabled = false;
            textbox4.Enabled = false;
            datetime1.Enabled = false;
            boton1.Enabled = true;            
        }

        //Procedimiento que permite desbloquear objetos en Forms
        public void desbloquearobjetos(System.Windows.Forms.TextBox textbox1,
            TextBox textbox2, TextBox textbox3, TextBox textbox4,
            DateTimePicker fecha, Button boton1, Button boton2)
        {
            textbox1.Enabled = false;
            textbox2.Enabled = true;
            textbox3.Enabled = true;
            textbox4.Enabled = true;
            boton1.Enabled = false;
            boton2.Enabled = true;
            fecha.Enabled = true;            
        }

        //Procedimiento que permite desbloquear objetos en Forms
        public void desbloquearobjetoseliminar(System.Windows.Forms.TextBox textbox1,
            TextBox textbox2, TextBox textbox3, TextBox textbox4,
            DateTimePicker fecha, Button boton1, Button boton2)
        {
            textbox1.Enabled = false;
            textbox2.Enabled = false;
            textbox3.Enabled = false;
            textbox4.Enabled = false;
            boton1.Enabled = false;
            boton2.Enabled = true;
            fecha.Enabled = false;
        }

        //Método para limpiar objetos textBox en el formulario
        public void limpiarcampostextos(TextBox campotexto1,
            TextBox campotexto2, TextBox campotexto3,
            TextBox campotexto4)
        {
            campotexto1.Text = "";
            campotexto2.Text = "";
            campotexto3.Text = "";
            campotexto4.Text = "";
            campotexto1.Focus();
        }

        //Bloquear objetos en Form Registrar Niveles
        public void bloquearobjetosniveles(TextBox textbox1, TextBox textbox2,
            Button boton1, Button boton2)
        {
            textbox1.Enabled = true;
            textbox2.Enabled = false;
            boton1.Enabled = true;
            boton2.Enabled = false;
        }

        //Desbloquear objetos en Form Registrar Niveles
        public void desbloquearobjetosniveles(TextBox textbox1, TextBox textbox2,
            Button boton1, Button boton2)
        {
            textbox1.Enabled = false;
            textbox2.Enabled = true;
            boton1.Enabled = false;
            boton2.Enabled = true;
        }

        //Limpiar campos textos de niveles
        public void limpiarcampostextosniveles(TextBox campotexto1,
            TextBox campotexto2)
        {
            campotexto1.Text = "";
            campotexto2.Text = "";
            campotexto1.Focus();
        }

        //Limpiar campos textos de funciones
        public void limpiarcampostextosfuncionniveles(TextBox texto1,
            TextBox texto2, TextBox texto3)
        {
            texto1.Text = "";
            texto2.Text = "";
            texto3.Text = "";
        }

        //Bloquear objetos funciones de nivel
        public void bloquearobjetosfuncionnivel(TextBox textbox1, 
            TextBox textbox2, TextBox textbox3)
        {
            textbox1.Enabled = false;
            textbox2.Enabled = false;
            textbox3.Enabled = false;
        }

        //Bloquear objetos de usuarios
        public void bloquearobjetosusuarios(TextBox textbox1, 
            TextBox textbox2, TextBox textbox3, TextBox textbox4,
            DateTimePicker fecha,
            Button boton1, Button boton2)
        {
            textbox1.Enabled = true;
            textbox2.Enabled = false;
            textbox3.Enabled = false;
            textbox4.Enabled = false;
            fecha.Enabled = false;
            boton1.Enabled = true;
            boton2.Enabled = false;
        }

        //Desbloquear objetos de usuarios
        public void desbloquearobjetosusuarios(TextBox textbox1,
            TextBox textbox2, TextBox textbox3, TextBox textbox4,
            DateTimePicker fecha,
            Button boton1, Button boton2)
        {
            textbox1.Enabled = false;
            textbox2.Enabled = true;
            textbox3.Enabled = true;
            textbox4.Enabled = true;
            fecha.Enabled = true;
            boton1.Enabled = false;
            boton2.Enabled = true;
        }

        public void limpiarcampostextosusuarios(TextBox textbox1,
            TextBox textbox2, TextBox textbox3, TextBox textbox4,
            ComboBox combobox1, ComboBox combobox2)
        {
            textbox1.Text = "";
            textbox2.Text = "";
            textbox3.Text = "";
            textbox4.Text = "";
            combobox1.Text = "Seleccione Nivel";
            combobox2.Text = "Seleccione Condición";
            textbox1.Focus();
        }

        public void bloquearobjetosusuariosconsulta(TextBox textbox1,
            TextBox textbox2, TextBox textbox3, TextBox textbox4,
            TextBox textbox5,
            DateTimePicker fecha)
        {
            textbox1.Enabled = true;
            textbox2.Enabled = false;
            textbox3.Enabled = false;
            textbox4.Enabled = false;
            textbox5.Enabled = false;
            fecha.Enabled = false;
        }

        public void limpiarcampostextosusuariosconsulta(
            TextBox textbox1, TextBox textbox2, TextBox textbox3, 
            TextBox textbox4, TextBox textbox5, ComboBox combobox1)
        {
            textbox1.Text = "";
            textbox2.Text = "";
            textbox3.Text = "";
            textbox4.Text = "";
            textbox5.Text = "";
            combobox1.Text = "Seleccione login";
            textbox1.Focus();
        }

        public void bloquearobjetosusuarioseliminar(
            TextBox textbox1,
            TextBox textbox2, TextBox textbox3, TextBox textbox4,
            TextBox textbox5,
            DateTimePicker fecha,
            Button boton1, Button boton2, ComboBox combobox1)
        {
            textbox1.Enabled = false;
            textbox2.Enabled = false;
            textbox3.Enabled = false;
            textbox4.Enabled = false;
            textbox5.Enabled = false;
            fecha.Enabled = false;
            boton1.Enabled = true;
            boton2.Enabled = false;
        }

        public void desbloquearobjetosusuarioseliminar(
            TextBox textbox1,
            TextBox textbox2, TextBox textbox3, TextBox textbox4,
            TextBox textbox5,
            DateTimePicker fecha,
            Button boton1, Button boton2, ComboBox combobox1)
        {
            textbox1.Enabled = false;
            textbox2.Enabled = false;
            textbox3.Enabled = false;
            textbox4.Enabled = false;
            textbox5.Enabled = false;
            fecha.Enabled = false;
            boton1.Enabled = false;
            boton2.Enabled = true;
        }
    }
}
