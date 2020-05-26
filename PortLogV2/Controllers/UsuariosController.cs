using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dominio.Clases;
using PortLogV2.ViewModel.Usuario;
using Repositorio;

namespace PortLogV2.Controllers
{
    public class UsuariosController : Controller
    {
        private RepositorioUsuario db = new RepositorioUsuario();

        // GET: Usuarios
        public ActionResult Index()
        {
            return View();
        }

        //Get
        [HttpGet]
        public ActionResult Login()
        {
            VMUsuario vm = new VMUsuario();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Login(VMUsuario vm)
        {
            var rol = db.Login(vm.CI, vm.Password);
            if (rol!="")
            {
                TempData["Exito"] = "Si";
            }
            else
            {
                TempData["Fail"] = "No ";
            }
            //todo Login

            return RedirectToAction("Login", "Usuarios");
        }

        public ActionResult Precarga()
        {
            db.PreCarga();
            return View("Login");
        }



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
