using CapaDatos; // Importa el espacio de nombres CapaDatos para acceder a las clases relacionadas con el acceso a datos.
using CapaModelo; // Importa el espacio de nombres CapaModelo para acceder a las clases de modelo.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VentasWeb.Controllers
{
    public class PermisosController : Controller
    {
        // GET: Permisos
        // Método para mostrar la vista de creación de permisos
        public ActionResult Crear()
        {
            return View();
        }

        // Método para obtener los permisos de un usuario específico
        [HttpGet]
        public JsonResult Obtener(int id)
        {
            // Obtiene la lista de permisos del usuario con el id especificado desde la capa de datos
            List<Permisos> olista = CD_Permisos.Instancia.ObtenerPermisos(id);

            // Devuelve la lista de permisos en formato JSON
            return Json(olista, JsonRequestBehavior.AllowGet);
        }

        // Método para guardar los permisos actualizados
        [HttpPost]
        public JsonResult Guardar(string xml)
        {
            // Actualiza los permisos en la base de datos con el XML proporcionado y obtiene la respuesta
            bool Respuesta = CD_Permisos.Instancia.ActualizarPermisos(xml);

            // Devuelve el resultado de la operación de guardado en formato JSON
            return Json(new { resultado = Respuesta }, JsonRequestBehavior.AllowGet);
        }

    }
}
