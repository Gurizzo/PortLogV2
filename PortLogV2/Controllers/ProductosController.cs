using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dominio.Clases;
using Repositorio;

namespace PortLogV2.Controllers
{
    public class ProductosController : Controller
    {
        private RepositorioProducto db = new RepositorioProducto();

        // GET: Productos
        public ActionResult Index()
        {
            return View(db.FindAll());
        }

        // GET: Productos/Details/5

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }

    }



        
}


