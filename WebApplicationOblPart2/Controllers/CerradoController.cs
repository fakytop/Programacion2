using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Http;

namespace WebApplicationOblPart2.Controllers
{
    public class CerradoController : Controller
    {
        Sistema unS = Sistema.Instancia;
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("rol") == "OPERADOR")
            {
                return View();
            }
            else
            {
                return Redirect("/Home/Error");
            }
        }

        public IActionResult CambiarAforo(string aforo)
        {
            if (HttpContext.Session.GetString("rol") == "OPERADOR")
            {
                if (unS.AdmCambiarAforo(aforo))
                {
                    ViewBag.Aforo = true;
                }
                else
                {
                    ViewBag.Aforo = false;
                }
                return View("Index");

            } else
            {
                return Redirect("/Home/Error");
            }
        }
    }
}
