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

        

        public Importacion FindById(int clave)
        {

            return  db.Importaciones.Find(clave);             
            
        }

        

        public IEnumerable<Importacion> Find(string dato,string categoria)
        {

            //todo caso que llegue vacio
            if (dato == null )
            {
                return this.FindAll();
            }

            switch (categoria)
            {
                case "Fecha":
                    bool validarFecha = DateTime.TryParse(dato, out DateTime p);

                    if (validarFecha)
                    {

                        return BuscarPorFecha(DateTime.Parse(dato));

                    }
                    else
                    {
                         IEnumerable<Importacion> Importacion = null;
                        return Importacion;
                    }
                    

                case "Rut": return BuscarPorRut(dato);

                case "Codigo": return BuscarPorCodigo(dato);

                case "Nombre": return BuscarPorCoincidencia(dato);
            }

            return this.FindAll();
 
        }

        private IEnumerable<Importacion> BuscarPorCoincidencia(string dato)
        {
            try
            {
                return db.Importaciones.Where(I => I.Producto.Nombre.Contains(dato)).ToList();
            }
            catch (Exception ex)
            {

                return null;
            }
            
        }

        private IEnumerable<Importacion> BuscarPorCodigo(string dato)
        {
            try
            {
                return db.Importaciones.Where(i => i.Producto.Codigo == dato).ToList();
            }
            catch (Exception ex)
            {

                return null;
            }
           
        }

        private IEnumerable<Importacion> BuscarPorRut(string dato)//Busco por rut.
        {
            try
            {
                return db.Importaciones.Where(I => I.Producto.Cliente.Rut == dato).ToList();
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        private IEnumerable<Importacion> BuscarPorFecha(DateTime fecha)//Busco por Fecha mayor y almacenado
        {
            try
            {
                return db.Importaciones.Where(I => I.FchSalidaPrevista < fecha && I.Almacenado==true).ToList();
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public bool Remove(object clave)
        {
            throw new NotImplementedException();
        }

        public bool Update(Importacion obj)
        {
            try
            {
                var importacion = db.Importaciones.Find(obj.Id);

                importacion.MatriculaCamion = obj.MatriculaCamion;
                importacion.CedulaEncargado = obj.CedulaEncargado;
                importacion.FechaSalidaFinal = DateTime.Today;
                importacion.Almacenado = false;

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                
                return false;
            }

            
        }
    }
}
