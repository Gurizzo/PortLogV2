using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public class Pais
    {
        public Pais()
        {
        }

        public Pais(int id, string codPais, string nombre)
        {
            Id = id;
            CodPais = codPais;
            Nombre = nombre;
        }

        public int Id { get; set; }

        public string CodPais { get; set; }

        public string Nombre { get; set; }


    }
}
