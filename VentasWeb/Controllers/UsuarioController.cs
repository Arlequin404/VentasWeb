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
    public class UsuarioController : Controller
    {
        // GET: Usuario
        // Método para mostrar la vista de creación de usuario
        public ActionResult Crear()
        {
            return View();
        }

        // Método para obtener la lista de usuarios
        public JsonResult Obtener()
        {
            // Obtiene la lista de usuarios desde la capa de datos
            List<Usuario> oListaUsuario = CD_Usuario.Instancia.ObtenerUsuarios();

            // Devuelve la lista de usuarios en formato JSON
            return Json(new { data = oListaUsuario }, JsonRequestBehavior.AllowGet);
        }

        // Método para guardar un usuario
        [HttpPost]
        public JsonResult Guardar(Usuario objeto)
        {
            bool respuesta = false;

            if (objeto.IdUsuario == 0)
            {
                // Si el IdUsuario es 0, encripta la contraseña y registra el nuevo usuario
                objeto.Clave = Encriptar.GetSHA256(objeto.Clave);
                respuesta = CD_Usuario.Instancia.RegistrarUsuario(objeto);
            }
            else
            {
                // Si el IdUsuario no es 0, modifica el usuario existente
                respuesta = CD_Usuario.Instancia.ModificarUsuario(objeto);
            }

            // Devuelve el resultado de la operación de guardado en formato JSON
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        // Método para eliminar un usuario
        [HttpGet]
        public JsonResult Eliminar(int id = 0)
        {
            // Elimina el usuario con el Id especificado desde la capa de datos
            bool respuesta = CD_Usuario.Instancia.EliminarUsuario(id);

            // Devuelve el resultado de la operación de eliminación en formato JSON
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}
