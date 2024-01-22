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
    class ModeloNiveles
    {

      #region "Objetos Base Datos"

        //Definiendo e instanciando en la variable cn la clase BASEDATOS
        //que pertenece a el paquete CONEXIONBASEDATOS
        public CONEXIONBASEDATOS.BASEDATOS cn =
            new CONEXIONBASEDATOS.BASEDATOS();

        //Define y construye una variable objeto de tipo SqlConnection que es donde
        //va a estar establecido la conexión con la base de datos de SQLServer
        public SqlConnection oConexion =
           new SqlConnection(//colegioKarla
                             //"Data Source=LAPTOP-2IURRRLU;Initial Catalog=creditos;Integrated Security=True");
                             //casa
                             "Data Source=lapt_jero;Initial Catalog=CLIENTES1242020;Integrated Security=True");
        //colegioMArio
        // "Data Source=LAPTOP-L53GD9SR;Initial Catalog=Creditos;Integrated Security=True");


       

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

      #region "Tabla Niveles"

        //Esta función permite buscar una identifcación en la tablaclientes de la BD
        public int buscarcodigonivel(string cod)
        {
            int enco = 0;
            try
            {
                cn.conectarbase(); //Conectar con la BD
                SqlCommand oCmdConsulta = new SqlCommand("SELECT * FROM TABLANIVEL WHERE CODIGONIVEL = '" + cod + "'", oConexion);
                //Plantear la instrucción en SQL que se va a ejecutar
                oDataAdapter.SelectCommand = oCmdConsulta;
                //Ejecuta por medio del oDataAdapter la instrucción que está almacenada
                //en oCmdConsulta
                oDataSet.Clear();
                oConexion.Open(); //Abre la conexión
                oDataAdapter.Fill(oDataSet, "TABLANIVEL");
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
                //if (cn.oConexion.State == ConnectionState.Open)
                //    cn.oConexion.Close();
            }
            return enco;
        } //Cierra función

        //Procedimiento que permite ingresar un Nivel en la tablaniveles
        public void insertarnivel(String codigoniv, String nombreniv)
        {
            try
            {
                cn.conectarbase();
                //Aquí construye el objeto oCmdInsercion con la instrucción en SQL de insertar
                SqlCommand oCmdInsercion = new SqlCommand("INSERT INTO TABLANIVEL (CODIGONIVEL,NOMBRENIVEL) VALUES (@codigoniv,@nombreniv)", oConexion);
                oDataAdapter.InsertCommand = oCmdInsercion;
                //Aquí ejecuta la instrucción SQL que está en oCmdInsercion

                //Aquí se especifican los tipos de datos de cada uno de los parámetros que van en los values
                oDataAdapter.InsertCommand.Parameters.Add(
                    new SqlParameter("@codigoniv", SqlDbType.VarChar));
                oDataAdapter.InsertCommand.Parameters.Add(
                    new SqlParameter("@nombreniv", SqlDbType.VarChar));
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

        public void cargarcomboniveles(ComboBox combo)
        {
            //El DataReader es como el DataAdapter con la diferencia que los datos
            //los guarda como un conjunto de datos
            SqlDataReader dr = null;
            oConexion.Open();
            SqlCommand oCmdConsulta = new SqlCommand(
                "SELECT * FROM TABLANIVEL", oConexion);
            dr = oCmdConsulta.ExecuteReader();
            if (dr.Read() == true) //Si es verdadero es pq hay datos en el dr
            {
                //Ciclo para recorrer el DataReader y cargar los datos en el combo
                do
                {
                    //Agrega al combo el campo (identificacion) 
                    combo.Items.Add(dr["NOMBRENIVEL"]).ToString();
                } while (dr.Read() == true);
                //Este ciclo se va a ejecutar mientras el dr tenga datos almacenados en él
                //ya que si es true es porque hay datos
            }
            oConexion.Close();
        }
        
        //Función que devuelve el código de nivel
        public string devuelvecodigonivel(string nombre)
        {
            string dato = "";
            string num = "";
            //El DataReader es como el DataAdapter con la diferencia que los datos
            //los guarda como un conjunto de datos
            SqlDataReader dr = null;
            oConexion.Open();
            SqlCommand oCmdConsulta = new SqlCommand("SELECT * FROM TABLANIVEL WHERE NOMBRENIVEL = '" + nombre + "'", oConexion);
            dr = oCmdConsulta.ExecuteReader();
            if (dr.Read() == true) //Si es verdadero es pq hay datos en el dr
            {
                dato = (dr["nombrenivel"]).ToString();
                //combo.Items.Add(dr["nombrenivel"]).ToString();
                if (dato.Equals(nombre))
                    num = (dr["CODIGONIVEL"].ToString());
            }
            oConexion.Close();
            return num;   
        } //Cierra función

        #endregion

      #region "Tabla Funciones"

        //Esta función permite buscar una identifcación en la tablaclientes de la BD
        public int buscarcodigofuncion(string cod)
        {
            int enco = 0;
            try
            {
                cn.conectarbase(); //Conectar con la BD
                SqlCommand oCmdConsulta = new SqlCommand("SELECT * FROM TABLAFUNCION WHERE CODIGOFUNCION = '" + cod + "'", oConexion);
                //Plantear la instrucción en SQL que se va a ejecutar
                oDataAdapter.SelectCommand = oCmdConsulta;
                //Ejecuta por medio del oDataAdapter la instrucción que está almacenada
                //en oCmdConsulta
                oDataSet.Clear();
                oConexion.Open(); //Abre la conexión
                oDataAdapter.Fill(oDataSet, "TABLAFUNCION");
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
                //if (cn.oConexion.State == ConnectionState.Open)
                //    cn.oConexion.Close();
            }
            return enco;
        } //Cierra función

        //Procedimiento que permite insertar una Función en la tablafunciones
        public void insertarfuncion(String codigofun, String nombrefun)
        {
            try
            {
                cn.conectarbase();
                //Aquí construye el objeto oCmdInsercion con la instrucción en SQL de insertar
                SqlCommand oCmdInsercion = new SqlCommand("INSERT INTO TABLAFUNCION (CODIGOFUNCION,NOMBREFUNCION) VALUES (@codigofun,@nombrefun)", oConexion);
                oDataAdapter.InsertCommand = oCmdInsercion;
                //Aquí ejecuta la instrucción SQL que está en oCmdInsercion

                //Aquí se especifican los tipos de datos de cada uno de los parámetros que van en los values
                oDataAdapter.InsertCommand.Parameters.Add(
                    new SqlParameter("@codigofun", SqlDbType.VarChar));
                oDataAdapter.InsertCommand.Parameters.Add(
                    new SqlParameter("@nombrefun", SqlDbType.VarChar));
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

        public void cargarcombofunciones(ComboBox combo)
        {
            //El DataReader es como el DataAdapter con la diferencia que los datos
            //los guarda como un conjunto de datos
            SqlDataReader dr = null;
            oConexion.Open();
            SqlCommand oCmdConsulta = new SqlCommand(
                "SELECT * FROM TABLAFUNCION", oConexion);
            dr = oCmdConsulta.ExecuteReader();
            if (dr.Read() == true) //Si es verdadero es pq hay datos en el dr
            {
                //Ciclo para recorrer el DataReader y cargar los datos en el combo
                do
                {
                    //Agrega al combo el campo (identificacion) 
                    combo.Items.Add(dr["nombrefuncion"]).ToString();
                } while (dr.Read() == true);
                //Este ciclo se va a ejecutar mientras el dr tenga datos almacenados en él
                //ya que si es true es porque hay datos
            }
            oConexion.Close();
        }

        //Función que devuelve el código de nivel
        public string devuelvecodigofuncion(string nombre)
        {
            string dato = "";
            string num = "";
            //El DataReader es como el DataAdapter con la diferencia que los datos
            //los guarda como un conjunto de datos
            SqlDataReader dr = null;
            oConexion.Open();
            SqlCommand oCmdConsulta = new SqlCommand("SELECT * FROM TABLAFUNCION WHERE NOMBREFUNCION = '" + nombre + "'", oConexion);
            dr = oCmdConsulta.ExecuteReader();
            if (dr.Read() == true) //Si es verdadero es pq hay datos en el dr
            {
                dato = (dr["nombrefuncion"]).ToString();
                //combo.Items.Add(dr["nombrenivel"]).ToString();
                if (dato.Equals(nombre))
                    num = (dr["CODIGOFUNCION"].ToString());
            }
            oConexion.Close();
            return num;
        } //Cierra función


      #endregion

      #region "TablaFuncionesNivel"

        //Esta función permite buscar una identifcación en la tablaclientes de la BD
        public int buscarfuncionnivel(string codigo)
        {
            int enco = 0;
            try
            {
                cn.conectarbase(); //Conectar con la BD
                SqlCommand oCmdConsulta = new SqlCommand("SELECT * FROM TABLAFUNCIONNIVEL WHERE CODFUNCIONNIVEL = '" + codigo + "'", oConexion);
                //Plantear la instrucción en SQL que se va a ejecutar
                oDataAdapter.SelectCommand = oCmdConsulta;
                //Ejecuta por medio del oDataAdapter la instrucción que está almacenada
                //en oCmdConsulta
                oDataSet.Clear();
                oConexion.Open(); //Abre la conexión
                oDataAdapter.Fill(oDataSet, "TABLAFUNCIONNIVEL");
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
                //if (cn.oConexion.State == ConnectionState.Open)
                //    cn.oConexion.Close();
            }
            return enco;
        } //Cierra función


        //Procedimiento que permite ingresar un Nivel en la tablaniveles
        public void insertarfuncionnivel(String codifunniv, 
            String codigoniv, String codigofun, String estado)
        {
            try
            {
                cn.conectarbase();
                //Aquí construye el objeto oCmdInsercion con la instrucción en SQL de insertar
                SqlCommand oCmdInsercion = new SqlCommand("INSERT INTO TABLAFUNCIONNIVEL (CODFUNCIONNIVEL,CODIGONIVEL,CODIGOFUNCION,ESTADO) VALUES (@codifunniv,@codigoniv,@codigofun,@estado)", oConexion);
                oDataAdapter.InsertCommand = oCmdInsercion;
                //Aquí ejecuta la instrucción SQL que está en oCmdInsercion

                //Aquí se especifican los tipos de datos de cada uno de los parámetros que van en los values
                oDataAdapter.InsertCommand.Parameters.Add(
                    new SqlParameter("@codifunniv", SqlDbType.VarChar));
                oDataAdapter.InsertCommand.Parameters.Add(
                    new SqlParameter("@codigoniv", SqlDbType.VarChar));
                oDataAdapter.InsertCommand.Parameters.Add(
                    new SqlParameter("@codigofun", SqlDbType.VarChar));
                oDataAdapter.InsertCommand.Parameters.Add(
                    new SqlParameter("@estado", SqlDbType.VarChar));
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

        //Procedimiento que aplica el Nivel de acuerdo al usuario que se
        //loguea
        public void AplicarNivel(string login, string nivel,
            ToolStripMenuItem op1, ToolStripMenuItem op2,
            ToolStripMenuItem op3, ToolStripMenuItem op4,
            ToolStripMenuItem op5, ToolStripMenuItem op6,
            ToolStripMenuItem op7, ToolStripMenuItem op8,
            ToolStripMenuItem op9, ToolStripMenuItem op10,
            ToolStripMenuItem op11, ToolStripMenuItem op12)
        {
            //El DataReader es como el DataAdapter con la diferencia que los datos
            //los guarda como un conjunto de datos
            SqlDataReader dr = null;
            oConexion.Open();
            SqlCommand oCmdConsulta = new SqlCommand(
                "SELECT * FROM TABLAFUNCIONNIVEL INNER JOIN TABLAUSUARIOS ON TABLAFUNCIONNIVEL.CODIGONIVEL = TABLAUSUARIOS.CODIGONIVEL WHERE TABLAUSUARIOS.CODIGONIVEL = '" + nivel + "'", oConexion);
            dr = oCmdConsulta.ExecuteReader();
            if (dr.Read() == true) //Si es verdadero es pq hay datos en el dr
            {
                //Ciclo para recorrer el DataReader y cargar los datos en el combo
                do
                {
                    if ((dr["codigonivel"].Equals(nivel)) &&
                        (dr["codigofuncion"].Equals("01")) &&
                        (dr["estado"].Equals("DESACTIVA")))
                    {
                        op1.Enabled = false;
                    }

                    if ((dr["codigonivel"].Equals(nivel)) &&
                        (dr["codigofuncion"].Equals("02")) &&
                        (dr["estado"].Equals("DESACTIVA")))
                    {
                        op2.Enabled = false;
                    }

                    if ((dr["codigonivel"].Equals(nivel)) &&
                        (dr["codigofuncion"].Equals("03")) &&
                        (dr["estado"].Equals("DESACTIVA")))
                    {
                        op3.Enabled = false;
                    }

                    if ((dr["codigonivel"].Equals(nivel)) &&
                        (dr["codigofuncion"].Equals("04")) &&
                        (dr["estado"].Equals("DESACTIVA")))
                    {
                        op4.Enabled = false;
                    }

                    if ((dr["codigonivel"].Equals(nivel)) &&
                        (dr["codigofuncion"].Equals("05")) &&
                        (dr["estado"].Equals("DESACTIVA")))
                    {
                        op5.Enabled = false;
                    }

                    if ((dr["codigonivel"].Equals(nivel)) &&
                        (dr["codigofuncion"].Equals("06")) &&
                        (dr["estado"].Equals("DESACTIVA")))
                    {
                        op6.Enabled = false;
                    }

                    if ((dr["codigonivel"].Equals(nivel)) &&
                        (dr["codigofuncion"].Equals("07")) &&
                        (dr["estado"].Equals("DESACTIVA")))
                    {
                        op7.Enabled = false;
                    }

                    if ((dr["codigonivel"].Equals(nivel)) &&
                        (dr["codigofuncion"].Equals("08")) &&
                        (dr["estado"].Equals("DESACTIVA")))
                    {
                        op8.Enabled = false;
                    }

                    if ((dr["codigonivel"].Equals(nivel)) &&
                        (dr["codigofuncion"].Equals("09")) &&
                        (dr["estado"].Equals("DESACTIVA")))
                    {
                        op9.Enabled = false;
                    }

                    if ((dr["codigonivel"].Equals(nivel)) &&
                        (dr["codigofuncion"].Equals("10")) &&
                        (dr["estado"].Equals("DESACTIVA")))
                    {
                        op10.Enabled = false;
                    }

                    if ((dr["codigonivel"].Equals(nivel)) &&
                        (dr["codigofuncion"].Equals("11")) &&
                        (dr["estado"].Equals("DESACTIVA")))
                    {
                        op11.Enabled = false;
                    }

                    if ((dr["codigonivel"].Equals(nivel)) &&
                        (dr["codigofuncion"].Equals("12")) &&
                        (dr["estado"].Equals("DESACTIVA")))
                    {
                        op12.Enabled = false;
                    }

                    //if ((dr["tablanivelfuncionescodigonivel.codigonivel"]    dr.GetString("tablanivelfunciones.codigonivel").Equals(nivel)) &&
                    //   (dr.GetString("tablanivelfunciones.codigofuncion").Equals("01")) &&
                    //   (dr.GetString("tablanivelfunciones.estado").Equals("DESACTIVA"))

                } while (dr.Read() == true);
                //Este ciclo se va a ejecutar mientras el dr tenga datos almacenados en él
                //ya que si es true es porque hay datos
            }
            oConexion.Close();            
        }

#endregion



    } //Fin clase ModeloNiveles
}
