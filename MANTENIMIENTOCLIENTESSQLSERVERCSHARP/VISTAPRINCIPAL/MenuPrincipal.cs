using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MANTENIMIENTOCLIENTESSQLSERVERCSHARP.VISTAPRINCIPAL
{
    public partial class MenuPrincipal : Form
    {
        MODELODEDATOS.ModeloNiveles mn = new MODELODEDATOS.ModeloNiveles();

        public MenuPrincipal()
        {
            InitializeComponent();            
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            //Aquí asigna a los campos textos del Menú las variables globales
            //de todo el proyecto
            textBox1.Text = MODELODEDATOS.ModeloUsuarios.ploginusuario;
            textBox2.Text = MODELODEDATOS.ModeloUsuarios.pnivelusuario;
            //Aquí asignamos a los campos textos en el Form del Menú, el
            //login del usuario y el nivel del usuario
            aplicarbloqueoniveles(); 
            //Acá llama al procedimiento aplicarbloqueoniveles
        }

        public void aplicarbloqueoniveles()
        {
            //Aquí llama al método AplicarNivel del Modelo Niveles, pasando
            //por parámetro el ploginusuario y el pnivelusuario que son las
            //variables globales del proyecto y dependiendo del codigo de nivel
            //así bloquea las opciones en el Menú Principal
            mn.AplicarNivel(MODELODEDATOS.ModeloUsuarios.ploginusuario,
                MODELODEDATOS.ModeloUsuarios.pnivelusuario, 
                registrarClientesToolStripMenuItem,
                consultarClientesToolStripMenuItem,
                eliminarClientesToolStripMenuItem,
                modificarClientesToolStripMenuItem1,
                registrarNivelesToolStripMenuItem,
                registrarFuncionesToolStripMenuItem,
                registrarFuncionesNivelToolStripMenuItem,
                todosLosClientesToolStripMenuItem,
                porNombreClienteToolStripMenuItem,
                registrarUsuariosToolStripMenuItem,
                consultarUsuariosToolStripMenuItem,
                eliminarUsuariosToolStripMenuItem);
        }

        //Cerrar Sesion
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VISTAUSUARIOS.IniciarSesion i = new
                VISTAUSUARIOS.IniciarSesion();
            i.Show();
            this.Hide();
        }

        //Registrar Clientes
        private void registrarClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VISTACLIENTES.RegistrarClientes r = new 
                VISTACLIENTES.RegistrarClientes();
            r.Show();
            this.Hide();
        }

        //Consultar Clientes
        private void consultarClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VISTACLIENTES.ConsultarClientes r = new
                VISTACLIENTES.ConsultarClientes();
            r.Show();
            this.Hide();
        }

        //Opción Modificar Clientes
        private void modificarClientesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            VISTACLIENTES.ModificarClientes r = new
                VISTACLIENTES.ModificarClientes();
            r.Show();
            this.Hide();
        }

        //Opción Eliminar Clientes
        private void modificarClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VISTACLIENTES.EliminarClientes r = new
                VISTACLIENTES.EliminarClientes();
            r.Show();
            this.Hide();
        }

        //Opción de Reporte Todos los Clientes
        private void todosLosClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            REPORTES.ReportarClientes r = new
                REPORTES.ReportarClientes();
            r.Show();
            this.Hide();
        }

        //Opción de Reporte Clientes por Nombre        
        private void porNombreClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            REPORTES.ReportarClientesNombre r = new
                REPORTES.ReportarClientesNombre();
            r.Show();
            this.Hide();
        }

        //Opción Registrar Niveles
        private void registrarNivelesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VISTANIVELES.RegistrarNiveles r = new
                VISTANIVELES.RegistrarNiveles();
            r.Show();
            this.Hide();
        }

        private void registrarFuncionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VISTAFUNCIONES.RegistrarFunciones r = new
                VISTAFUNCIONES.RegistrarFunciones();
            r.Show();
            this.Hide();
        }

        private void registrarFuncionesNivelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VISTAFUNCIONNIVEL.RegistrarFuncionNivel r = new
                VISTAFUNCIONNIVEL.RegistrarFuncionNivel();
            r.Show();
            this.Hide();
        }

        //Registrar Usuarios
        private void registrarUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VISTAUSUARIOS.RegistrarUsuarios r = new
                VISTAUSUARIOS.RegistrarUsuarios();
            r.Show();
            this.Hide();
        }

        //Cerrar Sesión
        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //Opción Consulta usuarios
        private void consultarUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VISTAUSUARIOS.ConsultarUsuarios cu = new
                VISTAUSUARIOS.ConsultarUsuarios();
            cu.Show();
            this.Hide();
        }

        //Opción eliminar usuarios
        private void eliminarUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VISTAUSUARIOS.EliminarUsuarios eu = new
                VISTAUSUARIOS.EliminarUsuarios();
            eu.Show();
            this.Hide();
        }
    }
}
