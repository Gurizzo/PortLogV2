using Dominio.Clases;
using Dominio.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    class RepositorioImportacion : IRepositorio<Importacion>
    {
        private PortLogContext db = new PortLogContext();

        public bool Add(Importacion obj)
        {
            
            if (obj.Validar())
            {
                obj.Producto= db.Productos.Find(obj.Producto.Codigo);
                db.Importaciones.Add(obj);
                db.SaveChanges();
                return true;


            }
            else
            {
                return false;

            }
        }

        public IEnumerable<Importacion> FindAll()
        {
            throw new NotImplementedException();
        }

        public Importacion FindById(object clave)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Importacion> Find(string dato)
        {
            //todo Filtro
            if(dato == null)
            {
                return this.FindAll();
            }
            bool validarFecha = DateTime.TryParse(dato,out DateTime p);
            if (validarFecha)
            {
                //todo si es una fecha buscar por fecha
            }
            else
            {
                //No es fecha
                //Busco Por codigo
                //Busco por RUT
                //Busco por coincidencia en descripcion.
            }


            throw new NotImplementedException();
        }

        public bool Remove(object clave)
        {
            throw new NotImplementedException();
        }

        public bool Update(Importacion obj)
        {
            throw new NotImplementedException();
        }
    }
}
