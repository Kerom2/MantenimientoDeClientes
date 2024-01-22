//Esta es la clase ModeloDatos que es de donde vamos a ejecutar los diversos
//métodos que van a realizar funciones sobre las tablas de la base de datos
//y estos métodos se van a invocar o llamar desde los diversos formularios del
//proyecto
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using MANTENIMIENTOCLIENTESSQLSERVERCSHARP.CONEXIONBASEDATOS;

namespace MANTENIMIENTOCLIENTESSQLSERVERCSHARP.MODELODEDATOS
{
    class ModeloDatos
    {
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

        //Esta función permite buscar una identifcación en la tablaclientes de la BD
        public int buscaridentificacion(string ide)
        {
            int enco = 0;
            try
            {
                cn.conectarbase(); //Conectar con la BD
                SqlCommand oCmdConsulta = new SqlCommand("SELECT * FROM TABLACLIENTES WHERE IDENTIFICACION = '"+ide+"'",oConexion);
                //Plantear la instrucción en SQL que se va a ejecutar
                oDataAdapter.SelectCommand = oCmdConsulta;
                //Ejecuta por medio del oDataAdapter la instrucción que está almacenada
                //en oCmdConsulta
                oDataSet.Clear();
                oConexion.Open(); //Abre la conexión
                oDataAdapter.Fill(oDataSet, "TABLACLIENTES");
                //Rellena el DataSet con los datos que obtiene de la tablaclientes
                //cuando se hace el Select
                oConexion.Close();

                //Si el DataSet es mayor que 0 quiere decir que hubo datos encontrados
                //cuando realizó la consulta o select
                if (oDataAdapter.Fill(oDataSet) > 0)
                    enco = 1;
            }
            catch(Exception ex)
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

        //Procedimiento que permite ingresar un cliente en la tablaclientes
        public void ingresarcliente(String identificacion, String nombre,
            String telefono, double monto, DateTime fecha)
        {
            try
            {
                cn.conectarbase();
                //Aquí construye el objeto oCmdInsercion con la instrucción en SQL de insertar
                SqlCommand oCmdInsercion = new SqlCommand("INSERT INTO TABLACLIENTES (IDENTIFICACION,NOMBRE,TELEFONO,MONTO,FECHA) VALUES (@identificacion,@nombre,@telefono,@monto,@fecha)", oConexion);
                oDataAdapter.InsertCommand = oCmdInsercion;
                //Aquí ejecuta la instrucción SQL que está en oCmdInsercion

                //Aquí se especifican los tipos de datos de cada uno de los parámetros que van en los values
                oDataAdapter.InsertCommand.Parameters.Add(
                    new SqlParameter("@identificacion", SqlDbType.VarChar));
                oDataAdapter.InsertCommand.Parameters.Add(
                    new SqlParameter("@nombre", SqlDbType.VarChar));
                oDataAdapter.InsertCommand.Parameters.Add(
                    new SqlParameter("@telefono", SqlDbType.VarChar));
                oDataAdapter.InsertCommand.Parameters.Add(
                    new SqlParameter("@monto", SqlDbType.Decimal));
                oDataAdapter.InsertCommand.Parameters.Add(
                    new SqlParameter("@fecha", SqlDbType.DateTime));
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

        //Método que permite modificar los datos de un cliente en la tabla de la 
        //BD. Si les aclaro que este no es el método que invoco en el formulario
        //porque el quedó oficial para modificar es el modificarcliente
        //que es el que trabaja con la fecha DateTime
        //public Boolean modificarcliente2(string ide, string nombre,
        //    string telefono, double monto, DateTime fecha)
        //{            
        //    oConexion.Open();
        //    SqlCommand oCmdModificar = new SqlCommand(
        //       "UPDATE TABLACLIENTES SET NOMBRE = '" + nombre + "', TELEFONO = '" + telefono + "', MONTO = " + monto + ", FECHA = '" + fecha + "' WHERE IDENTIFICACION = '" + ide + "'", oConexion);
            //Aquí se escribe la instrucción Update para actualizar los campos de la tablaclientes
        //    int i = oCmdModificar.ExecuteNonQuery();
            //Aquí ejecuta la sentencia SQL que está en el comando oCmdModificar
        //    oConexion.Close();
        //    if (i > 0)
        //       return true;
        //    else
        //       return false;
        //}

        //Método que permite modificar los datos de un cliente en la 
        //tabla de BD, utilizando parámetros y trabajando con la fecha
        //de tipo DateTime
        //Este es el que estoy utilizando en el Form ModificarClientes
        public void modificarcliente(string ide, string nombre,
            string telefono, double monto, DateTime fecha)
        {
            try
            {
                oConexion.Open();
                SqlCommand oCmdModificar = new SqlCommand(
                    "UPDATE TABLACLIENTES SET NOMBRE = @nombre, TELEFONO =  @telefono, MONTO = @monto , FECHA = @fecha  WHERE IDENTIFICACION = '" + ide + "'", oConexion);
                //Aquí se escribe la instrucción Update para actualizar los campos de la tablaclientes

                oDataAdapter.UpdateCommand = oCmdModificar;
                //Aquí actualiza la sentencia SQL que está en oCmdModificar

                //Aquí especifica el tipo de dato de cada uno de los parámetros
                oDataAdapter.UpdateCommand.Parameters.Add(
                    new SqlParameter("@identificacion", SqlDbType.VarChar));
                oDataAdapter.UpdateCommand.Parameters.Add(
                    new SqlParameter("@nombre", SqlDbType.VarChar));
                oDataAdapter.UpdateCommand.Parameters.Add(
                    new SqlParameter("@telefono", SqlDbType.VarChar));
                oDataAdapter.UpdateCommand.Parameters.Add(
                    new SqlParameter("@monto", SqlDbType.Decimal));
                oDataAdapter.UpdateCommand.Parameters.Add(
                    new SqlParameter("@fecha", SqlDbType.DateTime));

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

        //Método que nos permite eliminar un cliente de la tablaclientes
        public void eliminarcliente(string ide)
        {
            try
            {
                cn.conectarbase();
                //Esta es la instrucción en SQL que está en un objeto SqlCommand la cual va a ejecutarse 
                //sobre la tabla de la BD
                SqlCommand oCmdElimina = new SqlCommand("DELETE FROM TABLACLIENTES WHERE IDENTIFICACION = '" + ide + "'", oConexion);
                oDataAdapter.SelectCommand = oCmdElimina;
                //Ejecutar por medio del oDataAdapter el comando oCmdElimina
                oDataSet.Clear();
                oConexion.Open();
                oDataAdapter.Fill(oDataSet, "tablaclientes");
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

        //Método que permite mostrar en los objetos TexField los campos de
        //la tabla de la base de datos
        public void mostrardatosclientes(string ide,
            TextBox textbox2, TextBox textbox3, TextBox textbox4,
            DateTimePicker datetime1)
        {
            try
            {
                cn.conectarbase();
                SqlCommand oCmdConsulta = new SqlCommand(
                    "SELECT * FROM TABLACLIENTES WHERE IDENTIFICACION = '" + ide + "'", oConexion);
                oDataAdapter.SelectCommand = oCmdConsulta;
                //Aquí ejecuta la instrucción SELECT en SQL que está en oCmdConsulta
                oDataSet.Clear();
                oConexion.Open();
                //Rellena el DataSet con los registros de la tablaclientes que se han encontrado a la
                //hora que se ejecutó el SELECT
                oDataAdapter.Fill(oDataSet, "tablaclientes");
                oConexion.Close();
                //Si el DataSet es mayor que 0 es pq hay datos al hacer el Select
                if (oDataAdapter.Fill(oDataSet) > 0)
                {
                    //Acá se agregan los campos textos que son los parámetros que recibe
                    //el procedimiento y se vinculan con cada uno de los campos de la tabla
                    //de la BD, porque son los datos que se van a mostrar en los campos textos
                    textbox2.DataBindings.Add("text", oDataSet, "tablaclientes.nombre");
                    textbox3.DataBindings.Add("text", oDataSet, "tablaclientes.telefono");
                    textbox4.DataBindings.Add("text", oDataSet, "tablaclientes.monto");
                    datetime1.DataBindings.Add("text", oDataSet, "tablaclientes.fecha");
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

        //Procedimiento que permite cargar en un combo las identificaciones que 
        //están registradas en la tablaclientes para que el usuario seleccioné una
        //identificación cuando le da Clic y se muestre en el campo texto cuando se
        //invoca por ejemplo en el ConsultarClientes
        public void cargarcomboidentificacion(ComboBox combo)
        {
            //El DataReader es como el DataAdapter con la diferencia que los datos
            //los guarda como un conjunto de datos
            SqlDataReader dr = null;
            oConexion.Open();
            SqlCommand oCmdConsulta = new SqlCommand(
                "SELECT * FROM TABLACLIENTES", oConexion);
            dr = oCmdConsulta.ExecuteReader();
            if (dr.Read() == true) //Si es verdadero es pq hay datos en el dr
            {
                //Ciclo para recorrer el DataReader y cargar los datos en el combo
                do
                {
                    //Agrega al combo el campo (identificacion) 
                    combo.Items.Add(dr["identificacion"]).ToString();
                } while (dr.Read() == true);
                //Este ciclo se va a ejecutar mientras el dr tenga datos almacenados en él
                //ya que si es true es porque hay datos
            }
            oConexion.Close();
        }

        //Este procedimiento permite cargar los datos de acuerdo a la consulta
        //dada en el grid correspondiente
        public void cargardatos()
        {
            try
            {
                cn.conectarbase();
                SqlCommand oCmdConsulta = new SqlCommand(
                    "SELECT * FROM TABLACLIENTES", oConexion);
                oDataAdapter.SelectCommand = oCmdConsulta;
                oDataSet.Clear();
                oConexion.Open();
                oDataAdapter.Fill(oDataSet, "tablaclientes");
                oConexion.Close();
                oDataAdapter.SelectCommand = oCmdConsulta;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                if (oConexion.State == ConnectionState.Open)
                    oConexion.Close();
            }
        }

        //Este procedimiento carga los datos en el Grid, una vez que se 
        //han guardado los datos en el DataSet por medio de la instrucción en 
        //SQL que está en el procedimiento anterior que se llama cargardatos
        public void cargardatosengrid(DataGridView grid)
        {
            oDataSet.Clear();
            oConexion.Open();
            oDataAdapter.Fill(oDataSet,"tablaclientes");
            oConexion.Close();
            grid.DataSource = oDataSet; 
            //Aquí carga al parámetro grid por medio de la fuente de datos que
            //se obtienen del DataSet
            grid.DataMember = "tablaclientes";
            //Aquí al grid se le indica que los datos miembros los obtenga de
            //toda la tablaclientes
        }

        //Este procedimiento permite cargar los datos de acuerdo a la consulta
        //dada en el grid correspondiente        
        public void cargardatospornombre(String nom)
        {
            try
            {
                cn.conectarbase();
                //Esta instrucción permite seleccionar todos los registros donde el 
                //nombre del cliente que está en la tablaclientes sea parecido o como (LIKE) al
                //parámetro nom
                SqlCommand oCmdConsulta = new SqlCommand(
                    "SELECT * FROM TABLACLIENTES WHERE NOMBRE LIKE '" + nom + "%'", oConexion);
                oDataAdapter.SelectCommand = oCmdConsulta;
                oDataSet.Clear();
                oConexion.Open();
                oDataAdapter.Fill(oDataSet, "tablaclientes");
                oConexion.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                if (oConexion.State == ConnectionState.Open)
                    oConexion.Close();
            }
        }

        //Este procedimiento carga los datos en el Grid, una vez que se 
        //han guardado los datos en el DataSet por medio de la instrucción en 
        //SQL que está en el procedimiento anterior que se llama cargardatos
        public void cargardatosengridpornombre(DataGridView grid)
        {
            oDataSet.Clear();
            oConexion.Open();
            oDataAdapter.Fill(oDataSet, "tablaclientes");
            oConexion.Close();
            grid.DataSource = oDataSet;
            //Aquí carga al parámetro grid por medio de la fuente de datos que
            //se obtienen del DataSet
            grid.DataMember = "tablaclientes";
            //Aquí al grid se le indica que los datos miembros los obtenga de
            //toda la tablaclientes            
        }
    }

} //Fin de clase Modelo datos