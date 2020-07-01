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




        // GET: Importaciones/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["Rol"] == null || Session["Rol"].ToString().ToUpper().Equals("ADMIN"))
            {
                return RedirectToAction("LogOut", "Usuarios");
            }

            try
            {
                response = cliente.GetAsync(ImportacionUri +"/"+id).Result;
                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsAsync<ImportacionesVM>();
                    readTask.Wait();
                    var Importacion = readTask.Result;

                    if (Importacion != null )
                    {
                        VMImportacionesEdit vm = new VMImportacionesEdit()
                        {
                            Cedula = Session["Cedula"].ToString(),
                            Almacenado = Importacion.Almacenado,
                            Cantidad = Importacion.Cantidad,
                            Cliente = Importacion.Cliente,
                            Producto = Importacion.Producto,
                            FchIngreso = Importacion.FchIngreso,
                            FchSalida = Importacion.FchSalida,
                            FchSalidaPrevista = Importacion.FchSalidaPrevista,
                            Id = Importacion.Id,
                            Precio = Importacion.Precio
                        };
                        
                        return View(vm);
                    }
                    else
                    {
                        return View("Filtro");
                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return View("Filtro");
        }
        [HttpPost]
        public ActionResult Edit(VMImportacionesEdit vM)
        {
            /*api / Importaciones /*/
            if (Session["Rol"] == null || Session["Rol"].ToString().ToUpper().Equals("ADMIN"))
            {
                return RedirectToAction("LogOut", "Usuarios");
            }
            
            try
            {
                if (ModelState.IsValid)
                {
                    var tarea = cliente.PutAsJsonAsync(ImportacionUri + "/" + vM.Id, vM);

                    var result = tarea.Result;


                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Exito"] = "Salida de importacion exitosa";
                        return RedirectToAction("Filtro");
                    }
                    TempData["Fail"] = "Error al realizar salida";
                    return RedirectToAction("Filtro");
                }
                

                //HTTP POST
               

            }
            catch (Exception ex)
            {

                TempData["Fail"] = "Error al realizar salida";
                return RedirectToAction("Filtro");
            }

            return View();
        }



        //Get: Importaciones/Filtro
        [HttpGet]
        public ActionResult Filtro()
        {
            if (Session["Rol"] == null)
            {
                return RedirectToAction("LogOut", "Usuarios");
            }

            IEnumerable<ImportacionesVM> Importaciones = null;
            
            ViewBag.Opciones = Opciones();

            try
            {
                response = cliente.GetAsync(ImportacionUri+ "/getFilter"+"/").Result;
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
                            FchSalidaPrevista=i.FchSalidaPrevista,
                            Id = i.Id,
                            Precio = i.Precio,
                            CedulaEncargado=i.Cedula,
                            Matricula=i.Matricula
                            
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
            if (Session["Rol"] == null)
            {
                return RedirectToAction("LogOut", "Usuarios");
            }
            if (Buscar == "")
            {
                return RedirectToAction("Filtro", "Importaciones");
            }
            if (Buscar.Contains("/"))
            {
                Buscar = Buscar.Replace("/", "-");
                //( •_•)>⌐■-■  
                //you shall not pass
                //(⌐■_■)
            }

            IEnumerable<ImportacionesVM> Importaciones = null;

            ViewBag.Opciones = Opciones();

            try
            {
                response = cliente.GetAsync(ImportacionUri + "/getFilter" + "/"+categoria+"/"+Buscar).Result;
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
                            FchSalidaPrevista = i.FchSalidaPrevista,
                            Id = i.Id,
                            Precio = i.Precio
                        });
                        return View("Filtro", lista.ToList());
                    }
                    else
                    {
                        TempData["Fail"] = "No se encontraron importaciones (╯°□°）╯︵ ┻━┻";
                        IEnumerable<VMImportacionFiltro> vacia = null;
                        return View(vacia);
                    }

                }
                else
                {
                    TempData["Fail"] = "No se encontraron importaciones (╯°□°）╯︵ ┻━┻";
                    IEnumerable<VMImportacionFiltro> vacia = null;
                    return View(vacia);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return View(Importaciones);
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
