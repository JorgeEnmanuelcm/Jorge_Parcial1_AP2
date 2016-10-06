using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MaterialesDetalle
    {
        public int Id { get; set; }
        public int MaterialId { get; set; }

        public string Material { get; set; }
        public string Cantidad { get; set; } // no va string pero tube que ponerlo asi por algo que daba error


        public MaterialesDetalle()
        {
            this.Id = 0;
            this.MaterialId = 0;
            this.Material = "";
            this.Cantidad = "";
        }

        public MaterialesDetalle(string material, string cantidad)
        {
            this.Material = material;
            this.Cantidad = cantidad;
        }
    }
}
