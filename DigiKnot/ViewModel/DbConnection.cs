using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DigiKnot.ViewModel
{
    static class DbConnection
    {
        static public SqlConnection DbConnect()
        {
            // Write all these elements to db
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\atassera\source\repos\DigiKnot\DigiKnot\Assets.mdf;Integrated Security=True";
            //string connString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={dbConnect};Integrated Security=True";

            return new SqlConnection(connString);
        }

    }
}
