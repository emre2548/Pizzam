using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzam.Class
{
    public class c_db
    {
        public SqlConnection db_connect()
        {
            SqlConnection connect = new SqlConnection(@"Data Source = ASUS;Initial Catalog=OrderPizza;Integrated Security=True");
            connect.Open();
            return connect;
        }
    }
}
