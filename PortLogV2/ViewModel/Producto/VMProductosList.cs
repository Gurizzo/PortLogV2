using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortLogV2.ViewModel.Producto
{
    public class VMProductosList
    {
        [Display(Name = "Codigo del Producto")]
        public string Codigo { get; set; }

        [Display(Name = "Nombre del Producto")]
        public string Nombre { get; set; }

        public decimal Peso { get; set; }

        [Display(Name = "Nombre del cliente")]
        public string Cliente { get; set; }

        public string Rut { get; set; }
    }
}