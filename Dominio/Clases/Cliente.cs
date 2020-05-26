using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        public string Rut { get; set; }

        public string Nombre { get; set; }


        public List<Producto> Productos { get; set; }




        public Cliente()
        {

        }




        
    }
}
