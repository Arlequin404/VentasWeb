using CapaDatos; // Importa el espacio de nombres CapaDatos para acceder a las clases relacionadas con el acceso a datos.
using CapaModelo; // Importa el espacio de nombres CapaModelo para acceder a las clases de modelo.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VentasWeb.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        // Método para mostrar la vista de creación de producto
        public ActionResult Crear()
        {
            return View();
        }

        // GET: Producto
        // Método para mostrar la vista de asignación de producto
        public ActionResult Asignar()
        {
            return View();
        }

        // Método para obtener la lista de productos
        public JsonResult Obtener()
        {
            // Obtiene la lista de productos desde la capa de datos
            List<Producto> lista = CD_Producto.Instancia.ObtenerProducto();
            // Devuelve la lista de productos en formato JSON
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        // Método para obtener la lista de productos por tienda
        public JsonResult ObtenerPorTienda(int IdTienda)
        {
            // Obtiene la lista de productos y la lista de productos en tienda desde la capa de datos
            List<Producto> oListaProducto = CD_Producto.Instancia.ObtenerProducto();
            List<ProductoTienda> oListaProductoTienda = CD_ProductoTienda.Instancia.ObtenerProductoTienda();

            // Filtra los productos activos
            oListaProducto = oListaProducto.Where(x => x.Activo == true).ToList();

            // Si se proporciona un IdTienda, filtra los productos por esa tienda
            if (IdTienda != 0)
            {
                oListaProductoTienda = oListaProductoTienda.Where(x => x.oTienda.IdTienda == IdTienda).ToList();
                oListaProducto = (from producto in oListaProducto
                                  join productotienda in oListaProductoTienda on producto.IdProducto equals productotienda.oProducto.IdProducto
                                  where productotienda.oTienda.IdTienda == IdTienda
                                  select producto).ToList();
            }

            // Devuelve la lista de productos en formato JSON
            return Json(new { data = oListaProducto }, JsonRequestBehavior.AllowGet);
        }

        // Método para guardar un producto
        [HttpPost]
        public JsonResult Guardar(Producto objeto)
        {
            bool respuesta = false;

            if (objeto.IdProducto == 0)
            {
                // Si el IdProducto es 0, registra el nuevo producto
                respuesta = CD_Producto.Instancia.RegistrarProducto(objeto);
            }
            else
            {
                // Si el IdProducto no es 0, modifica el producto existente
                respuesta = CD_Producto.Instancia.ModificarProducto(objeto);
            }

            // Devuelve el resultado de la operación de guardado en formato JSON
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        // Método para eliminar un producto
        [HttpGet]
        public JsonResult Eliminar(int id = 0)
        {
            // Elimina el producto con el Id especificado desde la capa de datos
            bool respuesta = CD_Producto.Instancia.EliminarProducto(id);

            // Devuelve el resultado de la operación de eliminación en formato JSON
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        // Método para registrar una asignación de producto a tienda
        [HttpPost]
        public JsonResult RegistrarProductoTienda(ProductoTienda objeto)
        {
            // Registra la asignación de producto a tienda en la capa de datos y obtiene la respuesta
            bool respuesta = CD_ProductoTienda.Instancia.RegistrarProductoTienda(objeto);
            // Devuelve el resultado de la operación en formato JSON
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        // Método para modificar una asignación de producto a tienda
        [HttpPost]
        public JsonResult ModificarProductoTienda(ProductoTienda objeto)
        {
            // Modifica la asignación de producto a tienda en la capa de datos y obtiene la respuesta
            bool respuesta = CD_ProductoTienda.Instancia.ModificarProductoTienda(objeto);
            // Devuelve el resultado de la operación en formato JSON
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        // Método para eliminar una asignación de producto a tienda
        [HttpGet]
        public JsonResult EliminarProductoTienda(int id)
        {
            // Elimina la asignación de producto a tienda con el Id especificado desde la capa de datos
            bool respuesta = CD_ProductoTienda.Instancia.EliminarProductoTienda(id);
            // Devuelve el resultado de la operación en formato JSON
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        // Método para obtener todas las asignaciones de productos a tiendas
        [HttpGet]
        public JsonResult ObtenerAsignaciones()
        {
            // Obtiene la lista de asignaciones de productos a tiendas desde la capa de datos
            List<ProductoTienda> lista = CD_ProductoTienda.Instancia.ObtenerProductoTienda();
            // Devuelve la lista de asignaciones de productos a tiendas en formato JSON
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }
    }
}
