using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Http;

namespace WebApplicationOblPart2.Controllers
{
    public class AbiertoController : Controller
    {
        Sistema unS = Sistema.Instancia;
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("rol") == "OPERADOR")
            {
                return View();
            } else
            {
                
                return Redirect("/Home/Error");
            }

        }

        public IActionResult CambiarPrecioButaca(string precioButaca)
        {
            if(HttpContext.Session.GetString("rol") == "OPERADOR")
            {
                if (unS.AdmCambiarPrecioButaca(precioButaca))
                {
                    ViewBag.PrecioPorButaca = true;

                }
                else
                {
                    ViewBag.PrecioPorButaca = false;
                }

                return View("Index");
            }
            else
            {
                return Redirect("/Home/Error");
            }
            
        }
    }
}