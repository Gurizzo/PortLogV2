using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using ApiPortLogV2.Models;
using Dominio.Clases;
using PortLogV2.ViewModel.Importacion;
using Repositorio;


namespace PortLogV2.Controllers
{
    public class ImportacionesController : Controller
    {
        private PortLogContext db = new PortLogContext();
        private RepositorioImportacion dbEjemplo = new RepositorioImportacion();

        HttpClient cliente = new HttpClient();
        HttpResponseMessage response = new HttpResponseMessage();
        Uri ImportacionUri = null;

        public ImportacionesController()
        {
            cliente.BaseAddress = new Uri("http://localhost:58963");
            ImportacionUri = new Uri("http://localhost:58963/api/Importaciones");
            cliente.DefaultRequestHeaders.Accept.Clear();
            cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }


        // GET: Importaciones
        public ActionResult Index()
        {
            return View(db.Importaciones.ToList());
        }

        // GET: Importaciones/Details/5
        public ActionResult Details(int id)
        {
            
            Importacion importacion = dbEjemplo.FindById(id);
            if (importacion == null)
            {
                return HttpNotFound();
            }
            return View(importacion);
        }

        // GET: Importaciones/Create
        public ActionResult Create()
        {
            return View();
        }

        //Get: Importaciones/Filtro
        [HttpGet]
        public ActionResult Filtro()
        {
            IEnumerable<ImportacionesVM> Importaciones = null;
            
            ViewBag.Opciones = Opciones();

            try
            {
                response = cliente.GetAsync(ImportacionUri+ "/getFilter").Result;
                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsAsync<IEnumerable<ImportacionesVM>>();
                    readTask.Wait();
                    Importaciones = readTask.Result.ToList();

                    if (Importaciones != null && Importaciones.Count() > 0)
                    {
                        var lista = Importaciones.Select(i => new VMImportacionFiltro
                        {
                            Almacenado = i.Almacenado,
                            Cantidad = i.Cantidad,
                            Cliente = i.Cliente,
                            Producto = i.Producto,
                            FchIngreso = i.FchIngreso,
                            FchSalida = i.FchSalida,
                            Id = i.Id,
                            Precio = i.Precio
                        });
                        return View("Filtro", lista.ToList());
                    }
                    else
                    {
                        return View(Importaciones);
                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return View(Importaciones);
        }

        [HttpPost]
        public ActionResult Filtro(string Buscar,string categoria)
        {
            

            ViewBag.Opciones = Opciones();
            return View();
        }
        


        private List<SelectListItem> Opciones()
        {
            List<SelectListItem> Opciones = new List<SelectListItem>()
            {
                new SelectListItem{ Text = "Fecha", Value = "Fecha" },
                new SelectListItem { Text = "Rut cliente", Value = "Rut" },
                new SelectListItem { Text = "Codigo del producto", Value = "Codigo" },
                new SelectListItem { Text = "Nombre producto", Value = "Nombre" },

            };

            return Opciones;
        }

        // POST: Importaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FchIngreso,FchSalida,Cantidad,Precio,Almacenado,MatriculaCamion,CedulaEncargado")] Importacion importacion)
        {
            if (ModelState.IsValid)
            {
                db.Importaciones.Add(importacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(importacion);
        }

        // GET: Importaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Importacion importacion = db.Importaciones.Find(id);
            if (importacion == null)
            {
                return HttpNotFound();
            }
            return View(importacion);
        }

        // POST: Importaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FchIngreso,FchSalida,Cantidad,Precio,Almacenado,MatriculaCamion,CedulaEncargado")] Importacion importacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(importacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(importacion);
        }

        // GET: Importaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Importacion importacion = db.Importaciones.Find(id);
            if (importacion == null)
            {
                return HttpNotFound();
            }
            return View(importacion);
        }

        // POST: Importaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Importacion importacion = db.Importaciones.Find(id);
            db.Importaciones.Remove(importacion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
