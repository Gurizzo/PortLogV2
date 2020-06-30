using Dominio.Clases;
using Dominio.Interfaz;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var validar = db.Productos.Where(p => p.Codigo == obj.Codigo).FirstOrDefault<Producto>();
            if (validar == null && obj.Validar())
            {
                obj.Cliente = db.Clientes.Find(obj.Cliente.Rut);
                    db.Productos.Add(obj);
                    db.SaveChanges();
                    return true;
                
                
            }
            else
            {
                return false;

            }
        }

        public IEnumerable<Producto> FindAll()
        {
            
            
            return db.Productos.Include(c=> c.Cliente).ToList();
        }

        public Producto FindById(int clave)
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
