using CapaModelo; // Importa el espacio de nombres CapaModelo para acceder a las clases de modelo.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VentasWeb.Controllers
{
    public class HomeController : Controller
    {
        private static Usuario SesionUsuario; // Declara una variable estática para almacenar la información del usuario en sesión.

        // Método para mostrar la página de inicio
        public ActionResult Index()
        {
            // Verifica si hay un usuario en sesión
            if (Session["Usuario"] != null)
                // Si hay un usuario en sesión, asigna la información del usuario a la variable estática
                SesionUsuario = (Usuario)Session["Usuario"];
            else
            {
                // Si no hay un usuario en sesión, crea una nueva instancia de Usuario
                SesionUsuario = new Usuario();
            }

            try
            {
                // Intenta asignar el nombre y rol del usuario a las ViewBag para mostrarlos en la vista
                ViewBag.NombreUsuario = SesionUsuario.Nombres + " " + SesionUsuario.Apellidos;
                ViewBag.RolUsuario = SesionUsuario.oRol.Descripcion;
            }
            catch
            {
                // Si ocurre un error al obtener la información del usuario, no hace nada
            }

            return View();
        }

        // Método para cerrar la sesión del usuario
        public ActionResult Salir()
        {
            // Elimina la información del usuario de la sesión
            Session["Usuario"] = null;
            // Redirige al usuario a la página de inicio de sesión
            return RedirectToAction("Index", "Login");
        }

    }
}
