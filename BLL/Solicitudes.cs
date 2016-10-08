using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class Solicitudes : ClaseMaestra
    {
        public int SolicitudlId { get; set; }
        public string Fecha { get; set; }
        public string Razon { get; set; }
        public int Total { get; set; }
        public List<SolicitudesDetalle> Detalle { get; set; }

        public Solicitudes()
        {
            this.SolicitudlId = 0;
            this.Fecha = "";
            this.Razon = "";
            this.Total = 0;
            this.Detalle = new List<SolicitudesDetalle>();
        }

        public Solicitudes(int solicitudid)
        {
            this.SolicitudlId = solicitudid;
        }

        public void AgregarSolicitud(int MaterialId, string Material, int Cantidad) 
        {
            this.Detalle.Add(new SolicitudesDetalle(MaterialId, Material, Cantidad)); 
        }

        public override bool Insertar()
        {
            ConexionDB Conexion = new ConexionDB();
            int Retorno = 0;
            object Identity;
            try
            {
                Identity = Conexion.ObtenerValor(String.Format("Insert Into Solicitudes(Fecha, Razon, Total) values('{0}','{1}', {2}) select @@IDENTITY", this.Fecha, this.Razon, this.Total));
                int.TryParse(Identity.ToString(), out Retorno);
                this.SolicitudlId = Retorno;
                if (Retorno > 0)
                {
                    foreach (SolicitudesDetalle var in Detalle)
                    {
                        Conexion.Ejecutar(String.Format("Insert into SolicitudesDetalle(SolicitudId, MaterialId, Material, Cantidad) Values ({0},{1},'{2}', {3})", this.SolicitudlId, var.MaterialId, var.Material, var.Cantidad));
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
                Retorno = Conexion.Ejecutar(String.Format("Update Solicitudes set Fecha='{0}', Razon='{1}', Total= {2} where MaterialId= {3}", this.Razon, this.SolicitudlId));
                if (Retorno)
                {
                    Conexion.Ejecutar(String.Format("Delete from SolicitudesDetalle Where SolicitudlId= {0}", this.SolicitudlId));
                    foreach (SolicitudesDetalle var in this.Detalle)
                    {
                        Conexion.Ejecutar(string.Format("Insert into SolicitudesDetalle(SolicitudId, MaterialId Material, Cantidad) Values ({0},{1},'{2}', {3})", this.SolicitudlId, var.MaterialId, var.Material, var.Cantidad));
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
                Retorno = Conexion.Ejecutar(String.Format("Delete from SolicitudesDetalle Where SolicitudId= {0};" + "Delete from Solicitudes where SolicitudId= {0}", this.SolicitudlId));
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
                dt = Conexion.ObtenerDatos(String.Format("select * from Solicitudes where SolicitudId=" + IdBuscado));
                if (dt.Rows.Count > 0)
                {
                    this.SolicitudlId = (int)dt.Rows[0]["SolicitudId"];
                    this.Fecha = dt.Rows[0]["Fecha"].ToString();
                    this.Razon = dt.Rows[0]["Razon"].ToString();
                    this.Total = (int)dt.Rows[0]["Total"];
                    dtEventDetalle = Conexion.ObtenerDatos(String.Format("select * from SolicitudesDetalle where SolicitudId=" + IdBuscado));

                    foreach (DataRow row in dtEventDetalle.Rows)
                    {
                        AgregarSolicitud((int)row["MaterialId"], row["Material"].ToString(), (int)row["Cantidad"]); 
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
            ConexionDB Conexion = new ConexionDB();
            string OrdenFinal = "";
            if (!Orden.Equals(""))
                OrdenFinal = " Order by " + Orden;
            return Conexion.ObtenerDatos("Select " + Campos + "From Solicitudes where " + Condicion + " " + OrdenFinal);
        }
    }
}
