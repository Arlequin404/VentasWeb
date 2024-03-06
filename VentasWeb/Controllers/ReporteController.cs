using CapaDatos; // Importa el espacio de nombres CapaDatos para acceder a las clases relacionadas con el acceso a datos.
using CapaModelo; // Importa el espacio de nombres CapaModelo para acceder a las clases de modelo.
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VentasWeb.Controllers
{
    public class ReporteController : Controller
    {
        // GET: Reporte
        // Método para mostrar la vista de reporte de productos
        public ActionResult Producto()
        {
            return View();
        }

        // GET: Reporte
        // Método para mostrar la vista de reporte de ventas
        public ActionResult Ventas()
        {
            return View();
        }

        // Método para obtener el reporte de productos por tienda y código de producto
        public JsonResult ObtenerProducto(int idtienda, string codigoproducto)
        {
            // Obtiene el reporte de producto por tienda y código de producto desde la capa de datos
            List<ReporteProducto> lista = CD_Reportes.Instancia.ReporteProductoTienda(idtienda, codigoproducto);

            // Devuelve el reporte de producto en formato JSON
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        // Método para obtener el reporte de ventas por rango de fechas y tienda
        public JsonResult ObtenerVenta(string fechainicio, string fechafin, int idtienda)
        {
            // Convierte las cadenas de fecha en objetos DateTime
            List<ReporteVenta> lista = CD_Reportes.Instancia.ReporteVenta(Convert.ToDateTime(fechainicio), Convert.ToDateTime(fechafin), idtienda);

            // Devuelve el reporte de ventas en formato JSON
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
    }
}
