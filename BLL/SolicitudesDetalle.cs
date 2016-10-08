using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SolicitudesDetalle
    {
        public int Id { get; set; }
        public int SolicitudId { get; set; }

       public int MaterialId { get; set; }
        public string Material { get; set; }
        public int Cantidad { get; set; } 


        public SolicitudesDetalle()
        {
            this.Id = 0;
            this.SolicitudId = 0;
            this.MaterialId = 0;
            this.Material = "";
            this.Cantidad = 0;
        }

        public SolicitudesDetalle(int materialid, string material, int cantidad) 
        {
            this.MaterialId = materialid;
            this.Material = material;
            this.Cantidad = cantidad;
        }
    }
}
