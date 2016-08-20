using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CPT373_AS2
{
    public class UsersManager
    {
        public DataTable GetUsers()
        {




            using (var connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\GOLDB.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework"))
            {
                var command = connection.CreateCommand();
                command.CommandText = "select [UserID],[Email],[FirstName],[LastName] from [User]";

                var dataTable = new DataTable();

                var adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);

                return dataTable;
            }

        }

        public void DeleteUser(int? id)
        {
            using (var connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\GOLDB.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework"))
            {
                connection.Open();
                string deleteUserStatement = "delete from [User] where [UserID]=" + id;
                string deleteTemplateStatement = "delete from [UserTemplate] where [UserID]=" + id;
                string deleteGameStatement = "delete from [UserGame] where [UserID]=" + id;
                // check if the User has any Games or Templates
                string findGamesStatement = "select * from [UserGame] where [UserID]=" + id;

                SqlCommand deleteTemplateQuery = new SqlCommand(deleteTemplateStatement, connection);
                SqlCommand deleteGameQuery = new SqlCommand(deleteGameStatement, connection);
                SqlCommand deleteUserQuery = new SqlCommand(deleteUserStatement, connection);
                SqlCommand findGamesQuery = new SqlCommand(findGamesStatement, connection);
                //var numberOfGames = Convert.ToInt32(findGamesQuery.ExecuteScalar());
                //SqlDataReader reader = findGamesQuery.ExecuteReader();

                // TODO:
                // delete all the associated games and templates




                var update = deleteGameQuery.ExecuteNonQuery();
                update = deleteTemplateQuery.ExecuteNonQuery();
                update = deleteUserQuery.ExecuteNonQuery();

                //if (reader.HasRows)
                //{


                //    var update = deleteUserQuery.ExecuteNonQuery();
                //}








            }
        }

    }
}