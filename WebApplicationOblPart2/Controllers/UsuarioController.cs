using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Http;

namespace WebApplicationOblPart2.Controllers
{
    public class UsuarioController : Controller
    {
        private Sistema sistema = Sistema.Instancia;
        public IActionResult Registro()
        {
            //if (HttpContext.Session.GetString("rol") == null)
            //{
            //    return Redirect("/Usuario/Registro");
            //}
            Usuario modelo = new Usuario();
            return View(modelo);
        }

        public IActionResult Login()
        {
            if(HttpContext.Session.GetString("rol") == null)
            {
                return View();

            } else
            {
                return Redirect("/Home/Index");
            }
        }


        //public IActionResult GuardarRegistro(string nombre, string apellido, DateTime fechaNacimiento, string eMail, string nombreUsuario, string contrasena)
        [HttpPost]
        public IActionResult GuardarRegistro(Usuario nuevoUsuario)
        {
            bool altaUsuario = false;

            if(nuevoUsuario.Nombre != null && nuevoUsuario.Apellido != null && nuevoUsuario.EMail != null && nuevoUsuario.Contrasena != null && nuevoUsuario.FechaNacimiento != null)
            {
                if (nuevoUsuario.ValidarNuevoUsuario(nuevoUsuario.Nombre, nuevoUsuario.Apellido, nuevoUsuario.EMail, nuevoUsuario.Contrasena))
                {
                    altaUsuario = sistema.registrarUsuario(nuevoUsuario); // dentro del sistema llama al metodo registrar usuario que recibe un objeto del tipo Usuario.

                }
                //Usuario usuarioRegistro = new Usuario(nombre, apellido, eMail, fechaNacimiento, contrasena, nombreUsuario);
                if (altaUsuario == true)
                {
                    return RedirectToAction("Registro", new { mensaje = "Se registro el usuario correctamente." });
                }
                else
                {
                    ViewBag.Mensaje = "ingrese los datos correctamente";
                    return View("Registro", nuevoUsuario);

                }

            }
            else
            {
                ViewBag.Mensaje = "ingrese los datos correctamente";
                return View("Registro", nuevoUsuario);

            }

        }

        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("rol") == "OPERADOR")
            {
                ViewBag.usuarios = sistema.ClientesOrdenados();
                return View();
            } else
            {
                return Redirect("/Home/Index");
            }
        }

       [HttpPost]
        public IActionResult Login(string nombreUsuario, string password)
        {

            Usuario unU = sistema.ObtenerUsuario(nombreUsuario,password);

            if(unU != null)
            {
                HttpContext.Session.SetString("nombreUsuario", unU.NombreUsuario);
                HttpContext.Session.SetString("rol", unU.Rol);
                return Redirect("/Home/Index");
            } else
            {
                ViewBag.Mensaje = "Usuario no encontrado, intente nuevamente.";
                return View("Login");

            }
            

          
        }

        public IActionResult Logout()
        {


            HttpContext.Session.Clear();
            return RedirectToAction("login");
        }

    }
    
}
