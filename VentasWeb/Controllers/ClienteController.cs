using CapaDatos; // Importa el espacio de nombres CapaDatos para acceder a las clases relacionadas con el acceso a datos.
using CapaModelo; // Importa el espacio de nombres CapaModelo para acceder a las clases de modelo.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VentasWeb.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        // Método para mostrar la vista de creación de cliente
        public ActionResult Crear()
        {
            return View();
        }

        // Método para obtener todos los clientes
        public JsonResult Obtener()
        {
            // Obtiene la lista de clientes desde la capa de datos
            List<Cliente> oListaCliente = CD_Cliente.Instancia.ObtenerClientes();
            // Devuelve la lista de clientes en formato JSON
            return Json(new { data = oListaCliente }, JsonRequestBehavior.AllowGet);
        }

        // Método para guardar un cliente
        [HttpPost]
        public JsonResult Guardar(Cliente objeto)
        {
            bool respuesta = false;

            if (objeto.IdCliente == 0)
            {
                // Si el IdCliente es 0, registra el nuevo cliente
                respuesta = CD_Cliente.Instancia.RegistrarCliente(objeto);
            }
            else
            {
                // Si el IdCliente no es 0, modifica el cliente existente
                respuesta = CD_Cliente.Instancia.ModificarCliente(objeto);
            }

            // Devuelve el resultado de la operación de guardado en formato JSON
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        // Método para eliminar un cliente
        [HttpGet]
        public JsonResult Eliminar(int id = 0)
        {
            // Elimina el cliente con el Id especificado desde la capa de datos
            bool respuesta = CD_Cliente.Instancia.EliminarCliente(id);

            // Devuelve el resultado de la operación de eliminación en formato JSON
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}
