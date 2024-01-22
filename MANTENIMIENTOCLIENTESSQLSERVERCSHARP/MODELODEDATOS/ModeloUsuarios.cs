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
    class ModeloUsuarios
    {
        //Aquí declaramos estas 2 variables globales para todo el proyecto
        public static string ploginusuario;
        public static string pnivelusuario;
        MODELODEDATOS.ModeloNiveles mn = new MODELODEDATOS.ModeloNiveles();        

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

        #region "Metodos Usuarios"

        //Esta función permite buscar una identifcación en la tablaclientes de la BD
        public int buscarlogin(string login)
        {
            int enco = 0;
            try
            {
                cn.conectarbase(); //Conectar con la BD
                SqlCommand oCmdConsulta = new SqlCommand("SELECT * FROM TABLAUSUARIOS WHERE LOGINUSUARIO = '" + login + "'", oConexion);
                //Plantear la instrucción en SQL que se va a ejecutar
                oDataAdapter.SelectCommand = oCmdConsulta;
                //Ejecuta por medio del oDataAdapter la instrucción que está almacenada
                //en oCmdConsulta
                oDataSet.Clear();
                oConexion.Open(); //Abre la conexión
                oDataAdapter.Fill(oDataSet, "TABLAUSUARIOS");
                //Rellena el DataSet con los datos que obtiene de la tablaclientes
                //cuando se hace el Select
                oConexion.Close();

                //Si el DataSet es mayor que 0 quiere decir que hubo datos encontrados
                //cuando realizó la consulta o select
                if (oDataAdapter.Fill(oDataSet) > 0)
                    enco = 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                cn.desconectarbase();
            }
            return enco;
        } //Cierra función

        //Procedimiento que permite ingresar un cliente en la tablaclientes
        public void ingresarusuario(String loginusuario, 
            String nombre, DateTime fecha, String password,
            String identificacion, String codnivel, String condicion)
        {
            try
            {
                cn.conectarbase();
                //Aquí construye el objeto oCmdInsercion con la instrucción en SQL de insertar
                SqlCommand oCmdInsercion = new SqlCommand("INSERT INTO TABLAUSUARIOS (LOGINUSUARIO,NOMBREUSUARIO,FECHAREGISTRO,PASSWORD,IDENTIFICACION,CODIGONIVEL,CONDICIONUSUARIO) VALUES (@loginusuario,@nombre,@fecha,@password,@identificacion,@codnivel,@condicion)", oConexion);
                oDataAdapter.InsertCommand = oCmdInsercion;
                //Aquí ejecuta la instrucción SQL que está en oCmdInsercion

                //Aquí se especifican los tipos de datos de cada uno de los parámetros que van en los values
                oDataAdapter.InsertCommand.Parameters.Add(
                    new SqlParameter("@loginusuario", SqlDbType.VarChar));
                oDataAdapter.InsertCommand.Parameters.Add(
                    new SqlParameter("@nombre", SqlDbType.VarChar));
                oDataAdapter.InsertCommand.Parameters.Add(
                    new SqlParameter("@fecha", SqlDbType.DateTime));
                oDataAdapter.InsertCommand.Parameters.Add(
                    new SqlParameter("@password", SqlDbType.VarChar));
                oDataAdapter.InsertCommand.Parameters.Add(
                    new SqlParameter("@identificacion", SqlDbType.VarChar));
                oDataAdapter.InsertCommand.Parameters.Add(
                    new SqlParameter("@codnivel", SqlDbType.VarChar));
                oDataAdapter.InsertCommand.Parameters.Add(
                    new SqlParameter("@condicion", SqlDbType.VarChar));
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

        //Este es el método que se utiliza cuando se busca el usuario y
        //el Password y también asigna a las variables globales del proyecto
        //el login y el codigo de nivel
        public int buscarloginpassword(string login, string pass)
        {
            int enco = 0;
            //El DataReader es como el DataAdapter con la diferencia que los datos
            //los guarda como un conjunto de datos
            SqlDataReader dr = null;
            oConexion.Open();
            SqlCommand oCmdConsulta = new SqlCommand("SELECT * FROM TABLAUSUARIOS WHERE LOGINUSUARIO = '" + login + "' AND PASSWORD = '" + pass + "' AND CONDICIONUSUARIO = 'ACTIVO' ", oConexion);
            dr = oCmdConsulta.ExecuteReader();
            if (dr.Read() == true) //Si es verdadero es pq hay datos en el dr
            {
                enco = 1;
                //Asigna a las variables globales del proyecto el login
                //y el codigo de nivel del usuario para que cuando llega al Menú
                //se bloqueen las opciones según el código de nivel
                ploginusuario = login;
                pnivelusuario = Convert.ToString((dr["CODIGONIVEL"]));
            }
            oConexion.Close();
            return enco;
        }

        //Esta función no la vamos a utiizar
        //Esta función permite buscar una identifcación en la tablaclientes de la BD
        public int buscarloginpasword2(string login, string pass)
        {
            int enco = 0;
            try
            {
                cn.conectarbase(); //Conectar con la BD
                SqlCommand oCmdConsulta = new SqlCommand("SELECT * FROM TABLAUSUARIOS WHERE LOGINUSUARIO = '" + login + "' AND PASSWORD = '"+pass+"' AND CONDICIONUSUARIO = 'ACTIVO' ", oConexion);
                //Plantear la instrucción en SQL que se va a ejecutar
                oDataAdapter.SelectCommand = oCmdConsulta;
                //Ejecuta por medio del oDataAdapter la instrucción que está almacenada
                //en oCmdConsulta
                oDataSet.Clear();
                oConexion.Open(); //Abre la conexión
                oDataAdapter.Fill(oDataSet, "TABLAUSUARIOS");
                //Rellena el DataSet con los datos que obtiene de la tablaclientes
                //cuando se hace el Select
                oConexion.Close();

                //Si el DataSet es mayor que 0 quiere decir que hubo datos encontrados
                //cuando realizó la consulta o select
                if (oDataAdapter.Fill(oDataSet) > 0)
                {
                    enco = 1;
                    ploginusuario = login;
                    //pnivelusuario = Convert.ToString(mn.devuelvecodigonivel());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                cn.desconectarbase();
            }
            return enco;
        } //Cierra función

        #endregion

        public void cargarcombousuarios(ComboBox combo)
        {
            //El DataReader es como el DataAdapter con la diferencia que los datos
            //los guarda como un conjunto de datos
            SqlDataReader dr = null;
            oConexion.Open();
            SqlCommand oCmdConsulta = new SqlCommand(
                "SELECT * FROM TABLAUSUARIOS", oConexion);
            dr = oCmdConsulta.ExecuteReader();
            if (dr.Read() == true) //Si es verdadero es pq hay datos en el dr
            {
                //Ciclo para recorrer el DataReader y cargar los datos en el combo
                do
                {
                    //Agrega al combo el campo (identificacion) 
                    combo.Items.Add(dr["LOGINUSUARIO"]).ToString();
                } while (dr.Read() == true);
                //Este ciclo se va a ejecutar mientras el dr tenga datos almacenados en él
                //ya que si es true es porque hay datos
            }
            oConexion.Close();
        }

        //Método que permite mostrar en los objetos TexField los campos de
        //la tabla de la base de datos
        public void mostrardatosusuario(string login,
            TextBox textbox2, TextBox textbox3, TextBox textbox4,
            TextBox textbox5,
            DateTimePicker datetime1)
        {
            try
            {
                cn.conectarbase();
                SqlCommand oCmdConsulta = new SqlCommand(
                    "SELECT * FROM TABLAUSUARIOS WHERE LOGINUSUARIO = '" + login + "'", oConexion);
                oDataAdapter.SelectCommand = oCmdConsulta;
                //Aquí ejecuta la instrucción SELECT en SQL que está en oCmdConsulta
                oDataSet.Clear();
                oConexion.Open();
                //Rellena el DataSet con los registros de la tablaclientes que se han encontrado a la
                //hora que se ejecutó el SELECT
                oDataAdapter.Fill(oDataSet, "tablausuarios");
                oConexion.Close();
                //Si el DataSet es mayor que 0 es pq hay datos al hacer el Select
                if (oDataAdapter.Fill(oDataSet) > 0)
                {
                    //Acá se agregan los campos textos que son los parámetros que recibe
                    //el procedimiento y se vinculan con cada uno de los campos de la tabla
                    //de la BD, porque son los datos que se van a mostrar en los campos textos
                    textbox2.DataBindings.Add("text", oDataSet, "tablausuarios.nombreusuario");
                    textbox3.DataBindings.Add("text", oDataSet, "tablausuarios.identificacion");
                    textbox4.DataBindings.Add("text", oDataSet, "tablausuarios.codigonivel");
                    textbox5.DataBindings.Add("text", oDataSet, "tablausuarios.condicionusuario");
                    datetime1.DataBindings.Add("text", oDataSet, "tablausuarios.fecharegistro");
                }
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

        //Método que nos permite eliminar un cliente de la tablaclientes
        public void eliminarusuario(string login)
        {
            try
            {
                cn.conectarbase();
                //Esta es la instrucción en SQL que está en un objeto SqlCommand la cual va a ejecutarse 
                //sobre la tabla de la BD
                SqlCommand oCmdElimina = new SqlCommand("DELETE FROM TABLAUSUARIOS WHERE LOGINUSUARIO = '" + login + "'", oConexion);
                oDataAdapter.SelectCommand = oCmdElimina;
                //Ejecutar por medio del oDataAdapter el comando oCmdElimina
                oDataSet.Clear();
                oConexion.Open();
                oDataAdapter.Fill(oDataSet, "tablausuarios");
                //Rellena o actualiza el dataSet con los datos que se obtienen de la tabla de la BD
                oConexion.Close();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            finally
            {
                cn.desconectarbase();
            }
        }

        internal void ingresarusuario(string p1, string p2, string p3, double p4, DateTime dateTime)
        {
            throw new NotImplementedException();
        }
    }
}
