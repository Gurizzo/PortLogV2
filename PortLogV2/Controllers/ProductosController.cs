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
using PortLogV2.ViewModel.Producto;

namespace PortLogV2.Controllers
{
    public class ProductosController : Controller
    {
        private RepositorioProducto db = new RepositorioProducto();

        // GET: Productos
        public ActionResult Index()
        {
            List<VMProductosList> vms = new List<VMProductosList>();
            VMProductosList vm = new VMProductosList();
            var productos = db.FindAll();
            
            foreach (Producto p in productos)
            {
                vm.Nombre = p.Nombre;
                vm.Codigo = p.Codigo;
                vm.Cliente = p.Cliente.Nombre;
                vm.Rut = p.Cliente.Rut;
                vm.Peso = p.Peso;
                vms.Add(vm);

            }


            return View(vms);
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


