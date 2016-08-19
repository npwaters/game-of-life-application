using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CPT373_AS2
{
    public class UsersManagerDisconnected
    {
        public DataTable GetUsers()
        {




            using (var connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\GOLDB.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework"))
            {
                var command = connection.CreateCommand();
                command.CommandText = "select [UserID],[Email],[Password],[FirstName],[LastName] from [User]";

                var dataTable = new DataTable();

                var adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);

                return dataTable;
            }

        }
    }


}