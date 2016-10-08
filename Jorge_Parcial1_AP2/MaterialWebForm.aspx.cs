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
    public partial class MaterialWebForm : System.Web.UI.Page
    {
        Material mat = new Material();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void Limpiar()
        {
            IdTextBox.Text = string.Empty;
            DescripcionTextBox.Text = string.Empty;
            PrecioTextBox.Text = string.Empty;
        }

        private bool ObtenerDatos()
        {
            bool Retorno = true;
            int id;
            int precio;
            int.TryParse(IdTextBox.Text, out id);
            int.TryParse(PrecioTextBox.Text, out precio);
            if (id > 0)
            {
                mat.MaterialId = id;
            }
            else
            {
                Retorno = false;
            }
            if(DescripcionTextBox.Text.Length > 0)
            {
                mat.Descripcion = DescripcionTextBox.Text;
            }
            else
            {
                Retorno = false;
            }
            if(PrecioTextBox.Text.Length > 0)
            {
                mat.Precio = precio;
            }
            else
            {
                Retorno = false;
            }
            return Retorno;
        }

        public void DevolverDatos()
        {
            IdTextBox.Text = mat.MaterialId.ToString();
            DescripcionTextBox.Text = mat.Descripcion.ToString();
            PrecioTextBox.Text = mat.Precio.ToString();
        }

        protected void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
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

        protected void GuardarButton_Click(object sender, EventArgs e)
        {
           if (IdTextBox.Text.Length == 0)
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
    }
}