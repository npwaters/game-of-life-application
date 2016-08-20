using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CPT373_AS2
{
    public partial class TemplateList : System.Web.UI.Page
    {
        public TemplatesManager templatesManager = new TemplatesManager();
        public DataTable templatesTable = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            templatesTable = templatesManager.GetTemplates();

            TemplateListGridView.DataSource = templatesTable;
            TemplateListGridView.DataBind();
        }


        protected void TemplateListGridView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }




        protected void TempplateListGridView_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int? id = null;
            var v = sender;

            var keys = e.Keys.Values;
            //var userID = keys.
            foreach (var item in keys)
            {
                id = (int)item;
            }
            //templatesManager.DeleteUser(id);
            templatesTable = templatesManager.GetTemplates();
            TemplateListGridView.DataSource = templatesTable;
            TemplateListGridView.DataBind();
        }

        
    }
}