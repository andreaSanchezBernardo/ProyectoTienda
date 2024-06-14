using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaTienda.Controllers
{
    public class MantenimientoController : Controller
    {
        // GET: Mantenimiento
        public ActionResult Categoria()
        {
            return View();
        }

        public ActionResult Marca()
        {
            return View();
        }

        public ActionResult Producto()
        {
            return View();
        }

        public JsonResult ListarCategoria()
        {
            List<Categoria> oCategoria = new List<Categoria>();

            oCategoria = new CN_Categorias().Listar();

            return Json(new { data = oCategoria }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult AgregarCategoria(Categoria categoria)
        {

            CN_Categorias oCategoria = new CN_Categorias();
            Response response = oCategoria.Agregar(categoria);

            return Json(response);

        }

        [HttpPost]
        public JsonResult EditarCategoria(Categoria categoria)
        {
            CN_Categorias oCategoria = new CN_Categorias();
            Response response = oCategoria.Editar(categoria);

            return Json(response);
        }

        public JsonResult EliminarCategoria(int idCategoria)
        {
            CN_Categorias cN_Categorias = new CN_Categorias();
            Response response = cN_Categorias.Eliminar(idCategoria);

            return Json(response);
        }

        public JsonResult ListarMarca()
        {
            List<Marca> oMarca = new List<Marca>();

            oMarca = new CN_Marcas().Listar();

            return Json(new { data = oMarca }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AgregarMarca(Marca marca)
        {

            CN_Marcas oMarcas = new CN_Marcas();
            Response response = oMarcas.Agregar(marca);

            return Json(response);

        }

        [HttpPost]
        public JsonResult EditarMarca(Marca marca)
        {
            CN_Marcas oMarcas = new CN_Marcas();
            Response response = oMarcas.Editar(marca);

            return Json(response);
        }

        public JsonResult EliminarMarca(int idMarca)
        {
            CN_Marcas oMarcas = new CN_Marcas();
            Response response = oMarcas.Eliminar(idMarca);

            return Json(response);
        }
        public JsonResult ListarProducto()
        {
            List<Producto> oProducto = new List<Producto>();

            oProducto = new CN_Productos().Listar();

            return Json(new { data = oProducto }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult AgregarProducto(Producto producto, HttpPostedFileBase Imagen)
        {
            if (Imagen != null && Imagen.ContentLength > 0)
            {  // Ruta de la carpeta de destino
                string carpeta = Server.MapPath("~/wwwroot/imagenes");
                if (!Directory.Exists(carpeta))
                {
                    Directory.CreateDirectory(carpeta);
                }

                // Obtener el nombre del archivo de imagen
                string archivo = Path.GetFileName(Imagen.FileName);

                // Ruta completa del archivo de imagen
                string rutaCompleta = Path.Combine(carpeta, archivo);
                Imagen.SaveAs(rutaCompleta);

                // Guardar la ruta relativa
                producto.RutaImagen = "/wwwroot/imagenes/" + archivo;
            }

            CN_Productos cN_Productos = new CN_Productos();
            Response response = cN_Productos.Agregar(producto);
            return Json(response);
        }

        public JsonResult EditarProducto(Producto producto, HttpPostedFileBase Imagen)
        {
            if (Imagen != null && Imagen.ContentLength > 0)
            {  // Ruta de la carpeta de destino
                string carpeta = Server.MapPath("~/wwwroot/imagenes");
                if (!Directory.Exists(carpeta))
                {
                    Directory.CreateDirectory(carpeta);
                }

                // Obtener el nombre del archivo de imagen
                string archivo = Path.GetFileName(Imagen.FileName);

                // Ruta completa del archivo de imagen
                string rutaCompleta = Path.Combine(carpeta, archivo);
                Imagen.SaveAs(rutaCompleta);

                // Guardar la ruta relativa
                producto.RutaImagen = "/wwwroot/imagenes/" + archivo;
            }

            CN_Productos cN_Productos = new CN_Productos();
            Response response = cN_Productos.Editar(producto);
            return Json(response);
        }

        public JsonResult EliminarProducto(int IdProducto)
        {
            CN_Productos cN_Productos = new CN_Productos();
            Response response = cN_Productos.Eliminar(IdProducto);
            return Json(response);
        }
    }
}