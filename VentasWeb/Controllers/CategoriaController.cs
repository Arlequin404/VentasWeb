using CapaDatos; // Importa el espacio de nombres CapaDatos para acceder a las clases relacionadas con el acceso a datos.
using CapaModelo; // Importa el espacio de nombres CapaModelo para acceder a las clases de modelo.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VentasWeb.Controllers
{
    public class CategoriaController : Controller
    {
        // GET: Categoria
        // Método para mostrar la vista de creación de categoría
        public ActionResult Crear()
        {
            return View();
        }

        // Método para obtener todas las categorías
        public JsonResult Obtener()
        {
            // Obtiene la lista de categorías desde la capa de datos
            List<Categoria> lista = CD_Categoria.Instancia.ObtenerCategoria();
            // Devuelve la lista de categorías en formato JSON
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        // Método para guardar una categoría
        [HttpPost]
        public JsonResult Guardar(Categoria objeto)
        {
            bool respuesta = false;

            if (objeto.IdCategoria == 0)
            {
                // Si el IdCategoria es 0, registra la nueva categoría
                respuesta = CD_Categoria.Instancia.RegistrarCategoria(objeto);
            }
            else
            {
                // Si el IdCategoria no es 0, modifica la categoría existente
                respuesta = CD_Categoria.Instancia.ModificarCategoria(objeto);
            }

            // Devuelve el resultado de la operación de guardado en formato JSON
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        // Método para eliminar una categoría
        [HttpGet]
        public JsonResult Eliminar(int id = 0)
        {
            // Elimina la categoría con el Id especificado desde la capa de datos
            bool respuesta = CD_Categoria.Instancia.EliminarCategoria(id);

            // Devuelve el resultado de la operación de eliminación en formato JSON
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

    }
}
