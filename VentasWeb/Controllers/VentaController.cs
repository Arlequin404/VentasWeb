using CapaDatos; // Importa el espacio de nombres CapaDatos para acceder a las clases relacionadas con el acceso a datos.
using CapaModelo; // Importa el espacio de nombres CapaModelo para acceder a las clases de modelo.
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VentasWeb.Controllers
{
    public class VentaController : Controller
    {
        private static Usuario SesionUsuario; // Variable estática para almacenar la sesión del usuario

        // GET: Venta
        // Método para mostrar la vista de creación de venta
        public ActionResult Crear()
        {
            // Obtiene la sesión del usuario actual
            SesionUsuario = (Usuario)Session["Usuario"];
            return View();
        }

        // GET: Venta
        // Método para mostrar la vista de consulta de ventas
        public ActionResult Consultar()
        {
            return View();
        }

        // Método para mostrar el documento de una venta específica
        public ActionResult Documento(int IdVenta = 0)
        {
            // Obtiene los detalles de la venta con el IdVenta especificado
            Venta oVenta = CD_Venta.Instancia.ObtenerDetalleVenta(IdVenta);

            // Configura el formato de número para la moneda peruana
            NumberFormatInfo formato = new CultureInfo("es-PE").NumberFormat;
            formato.CurrencyGroupSeparator = ".";

            // Si no se encontró la venta, crea una nueva venta vacía
            if (oVenta == null)
                oVenta = new Venta();
            else
            {
                // Formatea los números en los detalles de la venta para mostrarlos con separador de miles
                oVenta.oListaDetalleVenta = (from dv in oVenta.oListaDetalleVenta
                                             select new DetalleVenta()
                                             {
                                                 Cantidad = dv.Cantidad,
                                                 NombreProducto = dv.NombreProducto,
                                                 PrecioUnidad = dv.PrecioUnidad,
                                                 TextoPrecioUnidad = dv.PrecioUnidad.ToString("N", formato),
                                                 ImporteTotal = dv.ImporteTotal,
                                                 TextoImporteTotal = dv.ImporteTotal.ToString("N", formato)
                                             }).ToList();

                // Formatea los números en la venta para mostrarlos con separador de miles
                oVenta.TextoImporteRecibido = oVenta.ImporteRecibido.ToString("N", formato);
                oVenta.TextoImporteCambio = oVenta.ImporteCambio.ToString("N", formato);
                oVenta.TextoTotalCosto = oVenta.TotalCosto.ToString("N", formato);
            }

            // Devuelve la vista con los detalles de la venta
            return View(oVenta);
        }

        // Método para obtener la lista de ventas con filtros
        public JsonResult Obtener(string codigo, string fechainicio, string fechafin, string numerodocumento, string nombres)
        {
            // Obtiene la lista de ventas utilizando los filtros especificados
            List<Venta> lista = CD_Venta.Instancia.ObtenerListaVenta(codigo, Convert.ToDateTime(fechainicio), Convert.ToDateTime(fechafin), numerodocumento, nombres);

            // Si no se encontraron ventas, crea una lista vacía
            if (lista == null)
                lista = new List<Venta>();

            // Devuelve la lista de ventas en formato JSON
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        // Método para obtener el detalle del usuario actual
        public JsonResult ObtenerUsuario()
        {
            // Obtiene los detalles del usuario actual
            Usuario rptUsuario = CD_Usuario.Instancia.ObtenerDetalleUsuario(SesionUsuario.IdUsuario);

            // Devuelve los detalles del usuario en formato JSON
            return Json(rptUsuario, JsonRequestBehavior.AllowGet);
        }

        // Método para obtener la lista de productos disponibles en una tienda específica
        public JsonResult ObtenerProductoPorTienda(int IdTienda)
        {
            // Obtiene la lista de productos disponibles en la tienda especificada
            List<ProductoTienda> oListaProductoTienda = CD_ProductoTienda.Instancia.ObtenerProductoTienda();
            oListaProductoTienda = oListaProductoTienda.Where(x => x.oTienda.IdTienda == IdTienda && x.Stock > 0).ToList();

            // Devuelve la lista de productos disponibles en formato JSON
            return Json(new { data = oListaProductoTienda }, JsonRequestBehavior.AllowGet);
        }

        // Método para controlar el stock de un producto en una tienda
        [HttpPost]
        public JsonResult ControlarStock(int idproducto, int idtienda, int cantidad, bool restar)
        {
            // Controla el stock del producto en la tienda especificada
            bool respuesta = CD_ProductoTienda.Instancia.ControlarStock(idproducto, idtienda, cantidad, restar);

            // Devuelve el resultado de la operación en formato JSON
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        // Método para guardar una venta
        [HttpPost]
        public JsonResult Guardar(string xml)
        {
            // Reemplaza el marcador de posición "!idusuario¡" en el XML con el Id del usuario actual
            xml = xml.Replace("!idusuario¡", SesionUsuario.IdUsuario.ToString());
            int Respuesta = 0;

            // Registra la venta y obtiene el Id de la venta registrada
            Respuesta = CD_Venta.Instancia.RegistrarVenta(xml);

            // Si se registró la venta correctamente, devuelve el Id de la venta
            if (Respuesta != 0)
                return Json(new { estado = true, valor = Respuesta.ToString() }, JsonRequestBehavior.AllowGet);
            else // Si no, devuelve un estado de error
                return Json(new { estado = false, valor = "" }, JsonRequestBehavior.AllowGet);
        }
    }
}
