using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MANTENIMIENTOCLIENTESSQLSERVERCSHARP.REPORTES
{
    public partial class ReportarClientes : Form
    {
        MODELODEDATOS.ModeloDatos m = new MODELODEDATOS.ModeloDatos();

        //Constructor del Formulario
        public ReportarClientes()
        {
            InitializeComponent();
            m.cargardatos(); 
            //Acá se llama al procedimiento cargardatos del modelo datos, para
            //que se carguen los datos en el dataSet en el momento que se
            //ejecuta la instrucción SELECT en SQL
            m.cargardatosengrid(dataGridView1);
            //Aquí se llama al procedimiento cargardatosengrid para que los
            //datos que están en el DataSet se carguen o se muestren en el
            //DataGrid
        }

        //Botón Regresar
        private void button1_Click(object sender, EventArgs e)
        {
            VISTAPRINCIPAL.MenuPrincipal 
                r = new VISTAPRINCIPAL.MenuPrincipal();
            r.Show();
            this.Hide();
        }
    }
}
