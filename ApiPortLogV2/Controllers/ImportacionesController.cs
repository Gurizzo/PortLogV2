using ApiPortLogV2.Models;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiPortLogV2.Controllers
{
    public class ImportacionesController : ApiController
    {
        RepositorioImportacion repo = new RepositorioImportacion();
        // GET: api/Importaciones
        public IHttpActionResult Get()
        {
            var importaciones = repo.FindAll();
            if (importaciones == null)
            {
                return NotFound();
            }
            return Ok(importaciones);
        }

        // GET: api/Importaciones/5
        public IHttpActionResult Get(int id)
        {
            var importacion = repo.FindById(id);
            if (importacion != null)
            {
                return Ok(new ImportacionesVM()
                {
                    Almacenado=importacion.Almacenado,
                    Cantidad=importacion.Cantidad,
                    Cliente=importacion.Producto.Cliente.Rut,
                    Producto=importacion.Producto.Nombre,
                    FchIngreso=importacion.FchIngreso,
                    FchSalidaPrevista = importacion.FchSalidaPrevista,
                    Id=importacion.Id,
                    Precio=importacion.Precio
                    
                });
            }
            return NotFound();
        }

        
        [Route("api/Importaciones/getFilter/{categoria}/{dato}", Name= "getFilter")]
        public IHttpActionResult GetFilter(string dato,string categoria)
        {
            
            var importaciones = repo.Find(dato,categoria);
            if (importaciones != null)
            {

                return Ok(importaciones.Select(i => new ImportacionesVM
                {
                    Almacenado = i.Almacenado,
                    Cantidad = i.Cantidad,
                    Cliente = i.Producto.Cliente.Rut,
                    Producto = i.Producto.Nombre,
                    FchIngreso = i.FchIngreso,
                    FchSalidaPrevista = i.FchSalidaPrevista,
                    FchSalida = i.FechaSalidaFinal,
                    Id = i.Id,
                    Precio = i.Precio
                }).ToList());
            }


            return NotFound();

        }

        [Route("api/Importaciones/getFilter/")]
        public IHttpActionResult GetFilter()
        {
            List<ImportacionesVM> importaciones = new List<ImportacionesVM>();
            var respuesta = repo.FindAll();
            if (respuesta != null)
            {
                
                return Ok(respuesta.Select(i => new ImportacionesVM
                {
                    Almacenado=i.Almacenado,
                    Cantidad=i.Cantidad,
                    Cliente=i.Producto.Cliente.Rut,
                    Producto=i.Producto.Nombre,
                    FchIngreso=i.FchIngreso,
                    FchSalidaPrevista=i.FchSalidaPrevista,
                    FchSalida=i.FechaSalidaFinal,
                    Id=i.Id,
                    Precio=i.Precio
                }).ToList());
            }
           
                
            return NotFound();

        }



        // POST: api/Importaciones
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Importaciones/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Importaciones/5
        public void Delete(int id)
        {
        }
    }
}
