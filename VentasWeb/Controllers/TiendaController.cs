using CapaDatos; // Importa el espacio de nombres CapaDatos para acceder a las clases relacionadas con el acceso a datos.
using CapaModelo; // Importa el espacio de nombres CapaModelo para acceder a las clases de modelo.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VentasWeb.Controllers
{
    public class TiendaController : Controller
    {
        // GET: Tienda
        // Método para mostrar la vista de creación de tienda
        public ActionResult Crear()
        {
            return View();
        }

        // Método para obtener la lista de tiendas
        public JsonResult Obtener()
        {
            // Obtiene la lista de tiendas desde la capa de datos
            List<Tienda> lista = CD_Tienda.Instancia.ObtenerTiendas();

            // Devuelve la lista de tiendas en formato JSON
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        // Método para guardar una tienda
        [HttpPost]
        public JsonResult Guardar(Tienda objeto)
        {
            bool respuesta = false;

            if (objeto.IdTienda == 0)
            {
                // Si el IdTienda es 0, registra la nueva tienda
                respuesta = CD_Tienda.Instancia.RegistrarTienda(objeto);
            }
            else
            {
                // Si el IdTienda no es 0, modifica la tienda existente
                respuesta = CD_Tienda.Instancia.ModificarTienda(objeto);
            }

            // Devuelve el resultado de la operación de guardado en formato JSON
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        // Método para eliminar una tienda
        [HttpGet]
        public JsonResult Eliminar(int id = 0)
        {
            // Elimina la tienda con el Id especificado desde la capa de datos
            bool respuesta = CD_Tienda.Instancia.EliminarTienda(id);

            // Devuelve el resultado de la operación de eliminación en formato JSON
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}
