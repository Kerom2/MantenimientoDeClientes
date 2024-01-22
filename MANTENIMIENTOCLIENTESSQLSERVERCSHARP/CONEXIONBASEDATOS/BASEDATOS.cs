using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;          //Para manejar conexión a SQL
using System.Data.SqlClient;    //Para ejecutar comandos SQL desde las clases
using System.Windows.Forms;
using System.Data;

namespace MANTENIMIENTOCLIENTESSQLSERVERCSHARP.CONEXIONBASEDATOS
{
    class BASEDATOS
    {
        //Define y construye una variable objeto de tipo SqlConnection que es donde
        //va a estar establecido la conexción con la base de datos
        
        public SqlConnection oConexion = new SqlConnection("Data Source=MARIOGOMEZ-PC;Initial Catalog=CLIENTES1242020;Integrated Security=True");        

        //Estos son ejemplos de cadenas de conexión
        //Se le aplica a oConexion el String para conectar con la Base de Datos
        //oConexion.ConnectionString = ("Data Source=LAB1-SOTFWARE10\\SQLEXPRESS;Initial Catalog=CLIENTES;Integrated Security=True");
        //oConexion.ConnectionString = ("Data Source=//192.168.56.1:8080;Initial Catalog=CLIENTES;Integrated Security=True");
        //oConexion.ConnectionString = ("Data Source=MARIOGOMEZ-PC\\SQLEXPRESS;Initial Catalog=CLIENTES;Integrated Security=True");
        //oConexion.ConnectionString = ("Data Source=//192.168.1.3:8080;Initial Catalog=CLIENTES;Integrated Security=True");       

        //Esta línea por ejemplo es si se construye la variable oConexion de tipo SqlConnection, entonces aquí nada mas sería
        //utilizar la propiedad ConnectionString
        //oConexion.ConnectionString = ("Data Source=MARIOGOMEZ-PC\\SQLEXPRESS;Initial Catalog=CLIENTES;Integrated Security=True");

        //Método para establecer conexión con la base de datos
        public void conectarbase()
        {
            try  //Intente hacer
            {
                oConexion.Open();
                Console.WriteLine("Conectado..");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void desconectarbase()
        {
            //Este if controla si la conexión está abierta y si está
            //abierta entonces la cierra
            if (oConexion.State == ConnectionState.Open)
                oConexion.Close();
        }
    }
}
