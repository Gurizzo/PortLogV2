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
        public string Get(int id)
        {
            return "value";
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
