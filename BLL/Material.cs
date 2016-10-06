using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class Material : ClaseMaestra
    {
        public int MaterialId { get; set; }
        public string Razon { get; set; }
        public List<MaterialesDetalle> Detalle { get; set; }

        public Material()
        {
            this.MaterialId = 0;
            this.Razon = "";
            this.Detalle = new List<MaterialesDetalle>();
        }

        public Material(int materialid)
        {
            this.MaterialId = materialid;
        }

        public void AgregarMaterial(string Material, string Cantidad)
        {
            this.Detalle.Add(new MaterialesDetalle(Material, Cantidad));
        }

        public override bool Insertar()
        {
            ConexionDB Conexion = new ConexionDB();
            int Retorno = 0;
            object Identity;
            try
            {
                Identity = Conexion.ObtenerValor(String.Format("Insert Into Materiales(Razon) values('{0}') select @@IDENTITY", this.Razon));
                int.TryParse(Identity.ToString(), out Retorno);
                this.MaterialId = Retorno;
                if (Retorno > 0)
                {
                    foreach (MaterialesDetalle var in Detalle)
                    {
                        Conexion.Ejecutar(String.Format("Insert into MaterialesDetalle(MaterialId, Material, Cantidad) Values ({0}, '{1}', '{2}')", this.MaterialId, var.Material, var.Cantidad));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno > 0;

        }

        public override bool Editar()
        {
            ConexionDB Conexion = new ConexionDB();
            bool Retorno = false;
            try
            {
                Retorno = Conexion.Ejecutar(String.Format("Update Materiales set Razon='{0}' where MaterialId= {1}", this.Razon, this.MaterialId ));
                if (Retorno)
                {
                    Conexion.Ejecutar(String.Format("Delete from MaterialesDetalle Where MaterialId= {0}", this.MaterialId));
                    foreach (MaterialesDetalle var in this.Detalle)
                    {
                        Conexion.Ejecutar(string.Format("Insert into MaterialesDetalle(MaterialId, Material, Cantidad) Values ({0},'{1}','{2}')", this.MaterialId, var.Material, var.Cantidad));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;
        }

        public override bool Eliminar()
        {
            ConexionDB Conexion = new ConexionDB();
            bool Retorno = false;
            try
            {
                Retorno = Conexion.Ejecutar(String.Format("Delete from MaterialesDetalle Where MaterialId= {0};" + "Delete from Materiales where MaterialId= {0}", this.MaterialId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;

        }

        public override bool Buscar(int IdBuscado)
        {
            ConexionDB Conexion = new ConexionDB();
            DataTable dt = new DataTable();
            DataTable dtEventDetalle = new DataTable();
            try
            {
                dt = Conexion.ObtenerDatos(String.Format("select * from Materiales where MaterialId=" + IdBuscado));
                if (dt.Rows.Count > 0)
                {
                    this.MaterialId = (int)dt.Rows[0]["MaterialId"];
                    this.Razon = dt.Rows[0]["Razon"].ToString();
                    dtEventDetalle = Conexion.ObtenerDatos(String.Format("select * from MaterialesDetalle where MaterialId=" + IdBuscado));

                    foreach (DataRow row in dtEventDetalle.Rows)
                    {
                        AgregarMaterial(row["Material"].ToString(), row["Cantidad"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt.Rows.Count > 0;
        }

        public override DataTable Listado(string Campos, string Condicion, string Orden)
        {
            throw new NotImplementedException();
        }
    }
}
