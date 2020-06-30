using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    [Table("Importacion")]
    public class Importacion
    {


        public int Id { get; set; }

        public DateTime FchIngreso { get; set; }

        public DateTime FchSalidaPrevista { get; set; }

        public virtual Producto Producto { get; set; }

        public int Cantidad { get; set; }

        public decimal Precio { get; set; }

        public bool Almacenado { get; set; }

        public string MatriculaCamion { get; set; }

        public string CedulaEncargado { get; set; }

        public DateTime? FechaSalidaFinal { get; set; }
       



        public Importacion()
        {
        }




        public int CalcularDias()
        {
            
                return (this.FchSalidaPrevista - this.FchIngreso ).Days;
            
        }

        public bool Validar()
        {
            return this.FchIngreso < this.FchSalidaPrevista && this.Producto != null  && this.Precio > 0;

        }
            
        

    }
}
