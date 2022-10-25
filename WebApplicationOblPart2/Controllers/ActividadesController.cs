using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Http;

namespace WebApplicationOblPart2.Controllers
{
    public class ActividadesController : Controller
    {
        Sistema unS = Sistema.Instancia;

        public IActionResult ListaActividades()
        {
            ViewBag.Actividades = unS.Actividades;
            ViewBag.FiltroCategoria = unS.ListaCategorias();
            ViewBag.ListaLugares = unS.Lugares;
            return View();
        }

        public IActionResult ActividadesPorLugar(string nombreLugar)
        {
            ViewBag.Actividades = unS.ActividadesSegunNombreLugar(nombreLugar);
            if(ViewBag.Actividades != null)
            {
                ViewBag.FiltroCategoria = unS.ListaCategorias();
                ViewBag.ListaLugares = unS.Lugares;
                return View("ListaActividades");
            } 
            else
            {
                return View("ListaActividades");
            }
        }

        [HttpPost]
        public IActionResult FiltroCategoria(string categoria, DateTime fechaDesde, DateTime fechaHasta)
        {


            ViewBag.ListaLugares = unS.Lugares;
            ViewBag.FiltroCategoria = unS.ListaCategorias();
            if(ViewBag.FiltroCategoria != null && fechaDesde != null && fechaHasta != null)
            {
                ViewBag.Actividades = unS.ListarActividadesPorCategoria(categoria, fechaDesde, fechaHasta);
                return View("ListaActividades");
            }
            else
            {
                return View("ListaActividades");
            }
        }

        public IActionResult FiltroPorPublico()
        {
            ViewBag.FiltroCategoria = unS.ListaCategorias();
            ViewBag.Actividades = unS.ListarActividadesParaTodoPublico();
            ViewBag.ListaLugares = unS.Lugares;

            return View("ListaActividades");
        }
    }
}
