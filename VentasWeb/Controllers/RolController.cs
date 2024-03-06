using CapaDatos; // Importa el espacio de nombres CapaDatos para acceder a las clases relacionadas con el acceso a datos.
using CapaModelo; // Importa el espacio de nombres CapaModelo para acceder a las clases de modelo.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VentasWeb.Controllers
{
    public class RolController : Controller
    {
        // GET: Rol
        // Método para mostrar la vista de creación de rol
        public ActionResult Crear()
        {
            return View();
        }

        // Método para obtener la lista de roles
        [HttpGet]
        public JsonResult Obtener()
        {
            // Obtiene la lista de roles desde la capa de datos
            List<Rol> olista = CD_Rol.Instancia.ObtenerRol();

            // Devuelve la lista de roles en formato JSON
            return Json(new { data = olista }, JsonRequestBehavior.AllowGet);
        }

        // Método para guardar un rol
        [HttpPost]
        public JsonResult Guardar(Rol objeto)
        {
            bool respuesta = false;

            if (objeto.IdRol == 0)
            {
                // Si el IdRol es 0, registra el nuevo rol
                respuesta = CD_Rol.Instancia.RegistrarRol(objeto);
            }
            else
            {
                // Si el IdRol no es 0, modifica el rol existente
                respuesta = CD_Rol.Instancia.ModificarRol(objeto);
            }

            // Devuelve el resultado de la operación de guardado en formato JSON
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        // Método para eliminar un rol
        [HttpGet]
        public JsonResult Eliminar(int id = 0)
        {
            // Elimina el rol con el Id especificado desde la capa de datos
            bool respuesta = CD_Rol.Instancia.EliminarRol(id);

            // Devuelve el resultado de la operación de eliminación en formato JSON
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

    }
}
