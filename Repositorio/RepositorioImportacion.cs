using Dominio.Clases;
using Dominio.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class RepositorioImportacion : IRepositorio<Importacion>
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
            try
            {
                using (db)
                {

                    IEnumerable<Importacion> importaciones = db.Importaciones.Select(I => I).Include(P => P.Producto.Cliente).ToList();
                    
                    return importaciones;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public Importacion FindById(object clave)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Importacion> Find(string dato)
        {
            IEnumerable<Importacion> importaciones = null;
            //todo Filtro
            if (dato == null)
            {
                return this.FindAll();
            }
            bool validarFecha = DateTime.TryParse(dato,out DateTime p);

            if (validarFecha)
            {

               return BuscarPorFecha(DateTime.Parse(dato));

            }
            else if(dato.Length==12 && int.TryParse(dato,out int result))//Valido que sea 12 caracteres y solo numeros.
            {
                return BuscarPorRut(dato);
                //No es fecha
                //Busco Por rut
            }
            else
            {
                importaciones = BuscarPorCodigo(dato);
                //Busco Por codigo
                if (importaciones == null)
                {//Busco por coincidencia en descripcion.
                    importaciones = BuscarPorCoincidencia(dato);
                }
                return importaciones;

            }


            
        }

        private IEnumerable<Importacion> BuscarPorCoincidencia(string dato)
        {
            return db.Importaciones.Where(I => I.Producto.Nombre.Contains(dato)).ToList();
        }

        private IEnumerable<Importacion> BuscarPorCodigo(string dato)
        {
            return db.Importaciones.Where(i => i.Producto.Codigo == dato).ToList();
        }

        private IEnumerable<Importacion> BuscarPorRut(string dato)//Busco por rut.
        {
            return db.Importaciones.Where(I => I.Producto.Cliente.Rut == dato).ToList();
        }

        private IEnumerable<Importacion> BuscarPorFecha(DateTime fecha)//Busco por Fecha mayor y almacenado
        {
            return db.Importaciones.Where(I => I.FchSalida > fecha && I.Almacenado == true).ToList();
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
