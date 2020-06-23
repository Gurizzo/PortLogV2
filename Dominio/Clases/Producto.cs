using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    [Table("Producto")]
    public class Producto
    {


        [Key]
        public string Codigo { get; set; }

        public string Nombre { get; set; }

        public decimal Peso { get; set; }

        public virtual Cliente Cliente { get; set; }




       

        public bool Validar()
        {
            if(this.Codigo!="" && this.Nombre!="" && this.Peso > 0 && this.Cliente.Validar())
            {
                return true;

            }
            return false;
        }

        public Producto()
        {

        }


    }
}
