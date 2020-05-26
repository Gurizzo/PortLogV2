using Dominio.Clases;
using Dominio.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class RepositorioProducto : IRepositorio<Producto>
    {
        private PortLogContext db = new PortLogContext();

        public bool Add(Producto obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Producto> FindAll()
        {

            return db.Productos.ToList();
        }

        public Producto FindById(object clave)
        {
            throw new NotImplementedException();
        }

        public bool Remove(object clave)
        {
            throw new NotImplementedException();
        }

        public bool Update(Producto obj)
        {
            throw new NotImplementedException();
        }
    }
}
