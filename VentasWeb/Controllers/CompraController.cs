using CapaDatos; // Importa el espacio de nombres CapaDatos para acceder a las clases relacionadas con el acceso a datos.
using CapaModelo; // Importa el espacio de nombres CapaModelo para acceder a las clases de modelo.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VentasWeb.Controllers
{
    public class CompraController : Controller
    {
        private static Usuario SesionUsuario; // Declara una variable estática para almacenar la información del usuario en sesión.

        // GET: Compra
        // Método para mostrar la vista de creación de compra
        public ActionResult Crear()
        {
            // Obtiene la información del usuario en sesión y la asigna a la variable estática
            SesionUsuario = (Usuario)Session["Usuario"];
            return View();
        }

        // GET: Compra
        // Método para mostrar la vista de consulta de compras
        public ActionResult Consultar()
        {
            return View();
        }

        // Método para mostrar la vista de documento de compra con el detalle de la compra
        public ActionResult Documento(int idcompra = 0)
        {
            // Obtiene el detalle de la compra con el id especificado desde la capa de datos
            Compra oCompra = CD_Compra.Instancia.ObtenerDetalleCompra(idcompra);

            // Si no se encuentra la compra, se crea una nueva instancia
            if (oCompra == null)
            {
                oCompra = new Compra();
            }

            return View(oCompra);
        }

        // Método para obtener la lista de compras según los parámetros especificados
        public JsonResult Obtener(string fechainicio, string fechafin, int idproveedor, int idtienda)
        {
            // Obtiene la lista de compras desde la capa de datos según los parámetros
            List<Compra> lista = CD_Compra.Instancia.ObtenerListaCompra(Convert.ToDateTime(fechainicio), Convert.ToDateTime(fechafin), idproveedor, idtienda);
            // Devuelve la lista de compras en formato JSON
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        // Método para guardar una compra recibiendo datos en formato XML
        [HttpPost]
        public JsonResult Guardar(string xml)
        {
            // Reemplaza el marcador de posición "!idusuario¡" en el XML con el Id del usuario en sesión
            xml = xml.Replace("!idusuario¡", SesionUsuario.IdUsuario.ToString());

            // Registra la compra en la capa de datos y obtiene la respuesta
            bool respuesta = CD_Compra.Instancia.RegistrarCompra(xml);

            // Devuelve el resultado de la operación de guardado en formato JSON
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}
