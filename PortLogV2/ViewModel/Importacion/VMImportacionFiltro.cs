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

        [Display(Name = "Fecha de entrada")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime FchIngreso { get; set; }

        [Display(Name = "Fecha de salida prevista")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime FchSalidaPrevista { get; set; }

        [Display(Name = "Fecha de salida")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? FchSalida { get; set; }

        [Display(Name = "Nombre del Producto")]
        public  string Producto { get; set; }

        [Display(Name = "RUT del cliente")]
        public string Cliente { get; set; }

        [Display(Name = "Cantidad a importar")]
        public int Cantidad { get; set; }

        public decimal Precio { get; set; }

        public bool Almacenado { get; set; }

        [Display(Name = "Cedula del encargado")]
        public string CedulaEncargado { get; set; }

        [Display(Name = "Matricula del camion")]
        public string Matricula { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }


    }
}