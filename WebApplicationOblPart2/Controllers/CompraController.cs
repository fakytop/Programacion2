using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Http;

namespace WebApplicationOblPart2.Controllers
{
    public class CompraController : Controller
    {
        Sistema unS = Sistema.Instancia;

        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("rol") == "OPERADOR")
            {
                ViewBag.Compras = unS.Compras;

            } 
            else if(HttpContext.Session.GetString("rol") == "CLIENTE")
            {
                ViewBag.Compras = unS.ComprasPorUsuario(HttpContext.Session.GetString("nombreUsuario"));
            } else
            {
                return Redirect("/Usuario/Registro");
            }

            ViewBag.SumaTotal = unS.SumaTotalCompras(ViewBag.Compras);
            return View();
        }

        public IActionResult CompraEntradas(int id)
        {
            Actividad unA = unS.ObtenerActividad(id);
            Usuario unU = unS.ObtenerUsuario(HttpContext.Session.GetString("nombreUsuario"));

            if(unA != null && unU != null && HttpContext.Session.GetString("rol") == "CLIENTE")
            {
                int anios = (DateTime.Now - unU.FechaNacimiento).Days;
                if (unA.TipoPublico == "P" || unA.TipoPublico == "C13" && anios > 4745 || unA.TipoPublico == "C16" && anios > 5840 || unA.TipoPublico == "C18" && anios > 6570)
                {
                    ViewBag.IdActividad = id;
                    return View(new Compra());
                } 
                else
                {
                    return Redirect("/Actividades/ListaActividades");
                }
                
            } else
            {
                return Redirect("/Actividades/ListaActividades");
            }
   
        }
    
        [HttpPost]

        public IActionResult FiltroCompra(string fechaDesde, string fechaHasta)
        {
            if(HttpContext.Session.GetString("rol") == "CLIENTE" || HttpContext.Session.GetString("rol") == "OPERADOR")
            {
                if(fechaDesde != null && fechaHasta != null)
                {
                    DateTime nFechaD = unS.DevolverFecha(fechaDesde);
                    DateTime nFechaH = unS.DevolverFecha(fechaHasta);

                    ViewBag.Compras = unS.ComprasPorFecha(nFechaD, nFechaH);
                    ViewBag.SumaTotal = unS.SumaTotalCompras(nFechaD, nFechaH);

                    return View("Index");
                }
                else
                {
                    if(HttpContext.Session.GetString("rol") == "CLIENTE")
                    {
                        ViewBag.Compras = unS.ComprasPorUsuario(HttpContext.Session.GetString("nombreUsuario"));
                        ViewBag.SumaTotal = unS.SumaTotalCompras(ViewBag.Compras);
                    } else if(HttpContext.Session.GetString("rol") == "OPERADOR")
                    {
                        ViewBag.Compras = unS.Compras;
                        ViewBag.SumaTotal = unS.SumaTotalCompras(ViewBag.Compras);
                    }
                        
                    return View("Index");
                }
                    
            } 
            else
            {
                return Redirect("/Home/Error");
            }
            
        }

        [HttpPost]

        public IActionResult CompraEntradas(Compra compra, int idActividad, int qEntradas)
        
        {
            if(HttpContext.Session.GetString("rol") == "CLIENTE")
            {
                Actividad unA = unS.ObtenerActividad(idActividad);
                string usuarioLoggeado = HttpContext.Session.GetString("nombreUsuario");

                Usuario unU = unS.ObtenerUsuario(usuarioLoggeado);
                DateTime fechaCompra = DateTime.Now;

                if (unA != null && unU != null && qEntradas > 0)
                {
                    Compra unaC = new Compra(unA, qEntradas, unU, fechaCompra, true);
                    unS.Compras.Add(unaC);
                    return Redirect("/Actividades/ListaActividades");
                }
                else
                {
                    return RedirectToAction("CompraEntradas", idActividad);
                }
            } else
            {
                return Redirect("/Home/Error");
            }
            
        }

        public IActionResult DarMeGusta(int idCompra)
        {
            Compra unaC = unS.ObtenerCompra(idCompra);

            if (unaC == null || HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") == "OPERADOR" || unaC.UsuarioCompra.NombreUsuario != HttpContext.Session.GetString("nombreUsuario"))
            {
                return Redirect("/Home/Error");
            }


            unS.DarMeGustaAlaActividad(unaC.Actividad.Id);

            return Redirect("/Compra/Index");
        }

        public IActionResult CancelarCompra(int idCompra)
        {
            Compra unaC = unS.ObtenerCompra(idCompra);

            if (unaC == null || unaC.UsuarioCompra.NombreUsuario != HttpContext.Session.GetString("nombreUsuario") || HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") == "OPERADOR")
            {
                return Redirect("/Home/Error");
            }


            if ((unaC.FechayHoraCompra - DateTime.Now).Days >= 1)
            {
                unS.Compras.Remove(unaC);
            }

            return Redirect("/Compra/Index");
        }

        public IActionResult MasCaras()
        {
            ViewBag.Compras = unS.ComprasDeMayorCosto();
            ViewBag.SumaTotal = unS.SumaTotalCompras(ViewBag.Compras);

            if (ViewBag.Compras != null && HttpContext.Session.GetString("rol") == "OPERADOR")
            {
                return View("Index");
            }
            else
            {
                return Redirect("/Compra/Index");
            }
        }
    }

}