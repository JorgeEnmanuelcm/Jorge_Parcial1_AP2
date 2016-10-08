using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;

namespace Jorge_Parcial1_AP2
{
    public partial class Materiales : System.Web.UI.Page
    {
        Solicitudes sol = new Solicitudes();
        DataTable dt = new DataTable();
        Material mat = new Material();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                MaterialIdDropDownList.DataSource = mat.Listado(" * ", "1=1", "");
                MaterialIdDropDownList.DataTextField = "Descripcion";
                MaterialIdDropDownList.DataValueField = "MaterialId";
                MaterialIdDropDownList.DataBind();
                

                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[3] { new DataColumn("MaterialId"), new DataColumn("Material"), new DataColumn("Cantidad") });  
                ViewState["Solicitudes"] = dt;
            }
        }

        protected void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        public void Limpiar()
        {
            MaterialTextBox.Text = string.Empty;
            CantidadTextBox.Text = string.Empty;
            RazonTextBox.Text = string.Empty;
            IdTextBox.Text = string.Empty;
            fechaTextBox.Text = string.Empty;
            TotalTextBox.Text = string.Empty;
            MaterialGridView.DataSource = string.Empty;
            MaterialGridView.DataBind();
           // Response.Write("<script>alert('Todo Limpio')</script>");
        }

        public void DevolverDatos()
        {
            
            IdTextBox.Text = sol.SolicitudlId.ToString();
            fechaTextBox.Text = sol.Fecha.ToString();
            RazonTextBox.Text = sol.Razon.ToString();
            TotalTextBox.Text = sol.Total.ToString();
            foreach (var item in sol.Detalle)
            {
                dt = (DataTable)ViewState["Solicitudes"];
                dt.Rows.Add(item.MaterialId, item.Material, item.Cantidad); 
                ViewState["Solicitudes"] = dt;
                MaterialGridView.DataSource = (DataTable)ViewState["Solicitudes"];
                MaterialGridView.DataBind();
            }

        }

        private bool ObtenerDatos()
        {
            bool Retorno = true;
            int id;
            int total;
            int.TryParse(IdTextBox.Text, out id);
            int.TryParse(TotalTextBox.Text, out total);
            if (id > 0)
            {
                sol.SolicitudlId = id;
            }
            else
            {
                Retorno = false;
            }
            if (fechaTextBox.Text.Length > 0)
            {
                sol.Fecha = fechaTextBox.Text;
            }
            else
            {
                Retorno = false;
            }
            if (RazonTextBox.Text.Length > 0)
            {
                sol.Razon = RazonTextBox.Text;
            }
            else
            {
                Retorno = false;
            }
            if (total > 0)
            {
                sol.Total = total;
            }
            else
            {
                Retorno = false;
            }
            if (MaterialGridView.Rows.Count > 0)
            {
                foreach (GridViewRow var in MaterialGridView.Rows)
                {
                    sol.AgregarSolicitud(Convert.ToInt32(var.Cells[0].Text), var.Cells[1].Text, Convert.ToInt32(var.Cells[2].Text)); 
                }
            }
            else
            {
                Retorno = false;
            }
            return Retorno;
        }

        protected void AgregarButton_Click(object sender, EventArgs e)
        {
            int cantidad;
            int.TryParse(CantidadTextBox.Text, out cantidad);
            DataTable dt = (DataTable)ViewState["Solicitudes"];
            dt.Rows.Add(MaterialIdDropDownList.SelectedValue, MaterialTextBox.Text, CantidadTextBox.Text);
            ViewState["Solicitudes"] = dt;
            MaterialGridView.DataSource = dt; 
            MaterialGridView.DataBind();
            TotalTextBox.Text += (20 * cantidad).ToString();
        }

        protected void GuardarButton_Click(object sender, EventArgs e)
        {
            if (IdTextBox.Text.Length == 0)
            {
                ObtenerDatos();
            if (sol.Insertar())
            {
                Limpiar();
                Response.Write("<script>alert('Inserto')</script>");
            }
            else
            {
                Response.Write("<script>alert('error')</script>");
            }
            }
            if (IdTextBox.Text.Length > 0)
            {
                ObtenerDatos();
                if (sol.Editar())
                {
                    Response.Write("<script>alert('Modifico')</script>");
                }
                else
                {
                    Response.Write("<script>alert('error')</script>");
                }
            }
        }

        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            try
            {
                ObtenerDatos();
                if (sol.Buscar(sol.SolicitudlId))
                {
                    if (sol.Eliminar())
                    {
                        Limpiar();  
                    }
                    else
                    {
                        Response.Write("<script>alert('error')</script>");
                    }
                }
            }
            catch (Exception)
            {
                Response.Write("<script>alert('error')</script>");
            }
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            int id;
            int.TryParse(IdTextBox.Text, out id);
            if (id < 0)
            {
                Response.Write("<script>alert('error')</script>");
            }
            else
            {
                if (sol.Buscar(id))
                {
                    DevolverDatos();
                }
                else
                {
                    Response.Write("<script>alert('error')</script>");
                }
            }
        }
    }
}