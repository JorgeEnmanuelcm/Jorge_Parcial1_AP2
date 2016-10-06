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
        Material mat = new Material();
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[2] { new DataColumn("Material"), new DataColumn("Cantidad") });
            ViewState["Material"] = dt;
            
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
        }

        public void DevolverDatos()
        {
            //DataTable dt = new DataTable();
            IdTextBox.Text = mat.MaterialId.ToString();
            RazonTextBox.Text = mat.Razon.ToString();
            foreach (var item in mat.Detalle)
            {
                dt = (DataTable)ViewState["Material"];
                dt.Rows.Add(item.Material, item.Cantidad);
                ViewState["Material"] = dt;
                MaterialGridView.DataSource = (DataTable)ViewState["Material"];
                MaterialGridView.DataBind();
            }

        }

        private bool ObtenerDatos()
        {
            bool Retorno = true;
            int id;
            int.TryParse(IdTextBox.Text, out id);
            if (id > 0)
            {
                mat.MaterialId = id;
            }
            else
            {
                Retorno = false;
            }
            if (RazonTextBox.Text.Length > 0)
            {
                mat.Razon = RazonTextBox.Text;
            }
            else
            {
                Retorno = false;
            }
            if (MaterialGridView.Rows.Count > 0)
            {
                foreach (GridViewRow var in MaterialGridView.Rows)
                {
                    mat.AgregarMaterial(var.Cells[0].Text, var.Cells[1].Text);
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
           // DataTable dt = new DataTable();
            DataTable dt = (DataTable)ViewState["Material"];
            dt.Rows.Add(MaterialTextBox.Text, CantidadTextBox.Text);
            ViewState["Material"] = dt;
            MaterialGridView.DataSource = (DataTable)ViewState["Material"];
            MaterialGridView.DataBind();

        }

        protected void GuardarButton_Click(object sender, EventArgs e)
        {

            ObtenerDatos();
            if (mat.Insertar())
            {
                Limpiar();
                Response.Write("<script>alert('Inserto')</script>");
            }
            else
            {
                Response.Write("<script>alert('error')</script>");
            }
            if (IdTextBox.Text.Length > 0)
            {
                ObtenerDatos();
                if (mat.Editar())
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
                if (mat.Buscar(mat.MaterialId))
                {
                    if (mat.Eliminar())
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
                if (mat.Buscar(id))
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