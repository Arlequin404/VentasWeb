using CapaDatos; // Importa el espacio de nombres CapaDatos para acceder a las clases relacionadas con el acceso a datos.
using CapaModelo; // Importa el espacio de nombres CapaModelo para acceder a las clases de modelo.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VentasWeb.Utilidades; // Importa el espacio de nombres VentasWeb.Utilidades para acceder a las utilidades de la aplicación.

namespace VentasWeb.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        // Método para mostrar la vista de inicio de sesión
        public ActionResult Index()
        {
            return View();
        }

        // Método para manejar el inicio de sesión después de enviar el formulario de inicio de sesión
        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            // Obtiene el usuario de la base de datos que coincide con el correo y la clave proporcionados
            Usuario ousuario = CD_Usuario.Instancia.ObtenerUsuarios().Where(u => u.Correo == correo && u.Clave == Encriptar.GetSHA256(clave)).FirstOrDefault();

            // Si no se encuentra el usuario, muestra un mensaje de error y vuelve a mostrar la vista de inicio de sesión
            if (ousuario == null)
            {
                ViewBag.Error = "Usuario o contraseña no correcta";
                return View();
            }

            // Si se encuentra el usuario, guarda la información del usuario en sesión y redirige al usuario a la página de inicio
            Session["Usuario"] = ousuario;
            return RedirectToAction("Index", "Home");
        }
    }
}
