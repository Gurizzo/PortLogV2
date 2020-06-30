using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortLogV2.ViewModel.Importacion
{
    public class VMImportacionFiltro
    {
        public int Id { get; set; }

        //[Display(Name = "Fecha de entrada")]
        public DateTime FchIngreso { get; set; }

        //[Display(Name = "Fecha de salida")]
        public DateTime FchSalida { get; set; }

        //[Display(Name = "Nombre del Producto")]
        public  string Producto { get; set; }

        //[Display(Name = "RUT del cliente")]
        public string Cliente { get; set; }

        //[Display(Name = "Cantidad a importar")]
        public int Cantidad { get; set; }

        public decimal Precio { get; set; }

        public bool Almacenado { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }


    }
}