using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    internal class Conexion
    {
        public static SqlConnection Conectar() {
            SqlConnection cn = new SqlConnection("SERVER=DESKTOP-MPPGADS;DATABASE=Prueba01;integrated security= true; ");
            cn.Open();  
            return cn;  

        }
    }
}
