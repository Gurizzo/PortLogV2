using Dominio.Clases;
using Dominio.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    class RepositorioCliente : IRepositorio<Cliente>
    {
        private PortLogContext db = new PortLogContext();

        public bool Add(Cliente obj)
        {
            
            var validar = db.Clientes.Where(c => c.Rut == obj.Rut).FirstOrDefault<Cliente>();
            if (validar == null)
            {
                if (obj.Validar())
                {
                    db.Clientes.Add(obj);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            else
            {
                return false;

            }
        }

        public IEnumerable<Cliente> FindAll()
        {
            throw new NotImplementedException();
        }

        public Cliente FindById(int clave)
        {
            throw new NotImplementedException();
        }

        public bool Remove(object clave)
        {
            throw new NotImplementedException();
        }

        public bool Update(Cliente obj)
        {
            throw new NotImplementedException();
        }
    }
}
