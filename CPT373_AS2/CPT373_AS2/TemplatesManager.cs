using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CPT373_AS2
{
    public class TemplatesManager
    {

        public DataTable GetTemplates()
        {




            using (var connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\GOLDB.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework"))
            {
                var command = connection.CreateCommand();
                command.CommandText = "select [UserTemplateID],[Name],[User].[Email],[Height],[Width],[Cells] from [UserTemplate] inner join [User] on [UserTemplate].[UserID]=[User].[UserID]";

                var dataTable = new DataTable();

                var adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);

                return dataTable;
            }

        }


        public void DeleteTemplate(int? id)
        {
            using (var connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\GOLDB.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework"))
            {
                connection.Open();
                string deleteTemplateStatement = "delete from [UserTemplate] where [UserTemplateID]=" + id;

                SqlCommand deleteTemplateQuery = new SqlCommand(deleteTemplateStatement, connection);

                var update = deleteTemplateQuery.ExecuteNonQuery();
            }
        }
    }
}