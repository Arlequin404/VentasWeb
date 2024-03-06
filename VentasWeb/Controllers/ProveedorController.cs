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
    public class ProveedorController : Controller
    {
        // GET: Proveedor
        // Método para mostrar la vista de creación de proveedor
        public ActionResult Crear()
        {
            return View();
        }

        // Método para obtener la lista de proveedores
        public JsonResult Obtener()
        {
            // Obtiene la lista de proveedores desde la capa de datos
            List<Proveedor> olista = CD_Proveedor.Instancia.ObtenerProveedor();
            // Devuelve la lista de proveedores en formato JSON
            return Json(new { data = olista }, JsonRequestBehavior.AllowGet);
        }

        // Método para guardar un proveedor
        [HttpPost]
        public JsonResult Guardar(Proveedor objeto)
        {
            bool respuesta = false;

            if (objeto.IdProveedor == 0)
            {
                // Si el IdProveedor es 0, registra el nuevo proveedor
                respuesta = CD_Proveedor.Instancia.RegistrarProveedor(objeto);
            }
            else
            {
                // Si el IdProveedor no es 0, modifica el proveedor existente
                respuesta = CD_Proveedor.Instancia.ModificarProveedor(objeto);
            }

            // Devuelve el resultado de la operación de guardado en formato JSON
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        // Método para eliminar un proveedor
        [HttpGet]
        public JsonResult Eliminar(int id = 0)
        {
            // Elimina el proveedor con el Id especificado desde la capa de datos
            bool respuesta = CD_Proveedor.Instancia.EliminarProveedor(id);

            // Devuelve el resultado de la operación de eliminación en formato JSON
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

    }
}
