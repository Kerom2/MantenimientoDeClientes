using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace MANTENIMIENTOCLIENTESSQLSERVERCSHARP.MODELODEDATOS
{
    class ModeloBitacora
    {

        #region "Objetos Base Datos"

        //Definiendo e instanciando en la variable cn la clase BASEDATOS
        //que pertenece a el paquete CONEXIONBASEDATOS
        public CONEXIONBASEDATOS.BASEDATOS cn =
            new CONEXIONBASEDATOS.BASEDATOS();

        //Define y construye una variable objeto de tipo SqlConnection que es donde
        //va a estar establecido la conexión con la base de datos de SQLServer

        public SqlConnection oConexion =
            new SqlConnection("Data Source=MARIOGOMEZ-PC;Initial Catalog=CLIENTES1242020;Integrated Security=True");

        //Este es un String de conexión ejemplo para los que utilizan SQLExpress
        //public SqlConnection oConexion = new SqlConnection(Data Source=MARIOGOMEZ-PC\\SQLEXPRESS;Initial Catalog=CLIENTES;Integrated Security=True");

        //Define e instancia la variable oDataSet del tipo de objeto DataSet
        //que funciona similar al ResultSet del entorno de Java
        public DataSet oDataSet = new DataSet();

        //Define e instancia la variable oDataAdapter del tipo de objeto
        //SqlDataAdapter. Este objeto permite utilizar y ejecutar las
        //sentencias e instrucciones de SQL en las tablas de la BD y 
        //funciona similar al objeto Statement de Java
        public SqlDataAdapter oDataAdapter = new SqlDataAdapter();

        #endregion


        #region "Métodos Bitacora"

        //Procedimiento que permite ingresar un cliente en la tablaclientes
        public void ingresarbitacora(DateTime fecha,
            string usuario, string detalle)
        {
            try
            {
                cn.conectarbase();
                //Aquí construye el objeto oCmdInsercion con la instrucción en SQL de insertar
                SqlCommand oCmdInsercion = new SqlCommand("INSERT INTO TABLABITACORA (FECHAMOVIMIENTO,LOGINUSUARIO,DESCRIPCION) VALUES (@fecha,@usuario,@detalle)", oConexion);
                
                oDataAdapter.InsertCommand = oCmdInsercion;
                //Aquí ejecuta la instrucción SQL que está en oCmdInsercion

                //Aquí se especifican los tipos de datos de cada uno de los parámetros que van en los values
                oDataAdapter.InsertCommand.Parameters.Add(
                    new SqlParameter("@fecha", SqlDbType.DateTime));
                oDataAdapter.InsertCommand.Parameters.Add(
                    new SqlParameter("@usuario", SqlDbType.VarChar));
                oDataAdapter.InsertCommand.Parameters.Add(
                    new SqlParameter("@detalle", SqlDbType.VarChar));                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                cn.desconectarbase();
            }
        }

        #endregion
    }
}
