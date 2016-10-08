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
        public string Descripcion { get; set; }
        public int Precio { get; set; }

        public Material()
        {
            this.MaterialId = 0;
            this.Descripcion = "";
            this.Precio = 0;
        }

        public Material(int materialid, string descripcion, int precio)
        {
            this.MaterialId = materialid;
            this.Descripcion = descripcion;
            this.Precio = precio;
        }

        public override bool Insertar()
        {
            ConexionDB Conexion = new ConexionDB();
            bool Retorno = false;
            try
            {
               Conexion.ObtenerValor(String.Format("Insert Into Materiales(Descripcion, Precio) values('{0}', {1})", this.Descripcion, this.Precio));
                Retorno = true;  
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Retorno;

        }

        public override bool Editar()
        {
            ConexionDB Conexion = new ConexionDB();
            bool Retorno = false;
            try
            {
                Conexion.Ejecutar(String.Format("Update Materiales set Descripcion='{0}', Precio={1} where MaterialId= {2}", this.Descripcion, this.Precio, this.MaterialId ));
                Retorno = true; 
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
                 Conexion.Ejecutar(String.Format("Delete from Materiales where MaterialId= {0}", this.MaterialId));
                Retorno = true;
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
            try
            {
                dt = Conexion.ObtenerDatos(String.Format("select * from Materiales where MaterialId=" + IdBuscado));
                if (dt.Rows.Count > 0)
                {
                    this.MaterialId = (int)dt.Rows[0]["MaterialId"];
                    this.Descripcion = dt.Rows[0]["Descripcion"].ToString();
                    this.Precio = (int)dt.Rows[0]["Precio"];
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
            ConexionDB Conexion = new ConexionDB();
            string OrdenFinal = "";
            if (!Orden.Equals(""))
                OrdenFinal = " Order by " + Orden;
            return Conexion.ObtenerDatos("Select " + Campos + "From Materiales where " + Condicion + " " + OrdenFinal);
        }
    }
}
