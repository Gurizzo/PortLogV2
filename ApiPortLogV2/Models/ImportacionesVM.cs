using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiPortLogV2.Models
{
    public class ImportacionesVM
    {
        public int Id { get; set; }

        
        public DateTime FchIngreso { get; set; }

        public DateTime FchSalida { get; set; }

        
        public string Producto { get; set; }

        
        public string Cliente { get; set; }

       
        public int Cantidad { get; set; }

        public decimal Precio { get; set; }

        public bool Almacenado { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }


    }
}