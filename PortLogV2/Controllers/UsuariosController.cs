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
            var usr = db.Login(vm.CI, vm.Password);
            if (usr !=null)
            {//Entro
                Session["Rol"] = usr.Rol.ToUpper();
                Session["Cedula"] = usr.CI;
                TempData["Exito"] = "Conectado con exito.";
                return RedirectToAction("Index", "Productos");

            }
            else
            {//No entro.
                TempData["Fail"] = "Datos no validos. ";
            }
            //todo Login

            return RedirectToAction("Login", "Usuarios");
        }

        public ActionResult Precarga()
        {
            db.PreCarga();
            TempData["Exito"] = "Datos cargados (ﾉ◕ヮ◕)ﾉ*:･ﾟ✧";
            return RedirectToAction("Index", "Productos");
        }

        public ActionResult LogOut()
        {
            Session["Rol"] = null;
            Session["Cedula"] = null;
            return RedirectToAction("Login");
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
