using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jorge_Parcial1_AP2
{
    public partial class Principal : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void MaterialButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Material.aspx");
        }
    }
}