using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaTienda.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Inicio()
        {
            return View();
        }
        public ActionResult Index()
        {
            CN_Productos cn_producto = new CN_Productos();
            List<Producto> productos = cn_producto.Listar();

            // Agrupación por marca
            var marcaAgrupada = productos
                .GroupBy(p => p.MarcaNombre)
                .Select(g => new
                {
                    Nombre = g.Key,
                    Cantidad = g.Count()
                })
                .ToList();

            var totalProductos = productos.Count;
            var nombresMarca = marcaAgrupada.Select(m => m.Nombre).ToList();
            var porcentajesMarca = marcaAgrupada.Select(m => (m.Cantidad / (double)totalProductos) * 100).ToList();

            // Agrupación por categoría
            var categoriaAgrupada = productos
                .GroupBy(p => p.CategoriaNombre)
                .Select(g => new
                {
                    Nombre = g.Key,
                    Cantidad = g.Count()
                })
                .ToList();

            var nombresCategoria = categoriaAgrupada.Select(m => m.Nombre).ToList();
            var porcentajesCategoria = categoriaAgrupada.Select(m => (m.Cantidad / (double)totalProductos) * 100).ToList();

            ViewBag.NombresMarca = nombresMarca;
            ViewBag.PorcentajesMarca = porcentajesMarca;
            ViewBag.NombresCategoria = nombresCategoria;
            ViewBag.PorcentajesCategoria = porcentajesCategoria;

            return View();
        }
    

        public ActionResult Usuarios()
        {
            return View();
        }


        public JsonResult ListarUsuarios()
        {
            List<Usuario> oList = new List<Usuario>();



            oList = new CN_Usuarios().Listar();


            return Json(new { data = oList }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AgregarUsuario(Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Clave))
            {
                return Json(new { success = false, message = "La clave no puede estar vacía." });
            }

            // Encripta la clave antes de pasarla a la capa de datos
            usuario.Clave = CN_Recursos.ConvertirSha256(usuario.Clave);

            CN_Usuarios cn_usuarios = new CN_Usuarios();

            Response response = cn_usuarios.Agregar(usuario);

            return Json(response);
        }

        [HttpPost]
        public JsonResult ActualizarUsuario(Usuario usuario)
        {
            if (!string.IsNullOrEmpty(usuario.Clave))
            {
                usuario.Clave = CN_Recursos.ConvertirSha256(usuario.Clave);
            }
            CN_Usuarios cN_Usuarios = new CN_Usuarios();

            Response response = cN_Usuarios.Actualizar(usuario);

            return Json(response);
        }

        public JsonResult EliminarUsuario(int idUsuario)
        {
            bool success = new CN_Usuarios().Eliminar(idUsuario);
            return Json(new { success = success });
        }
    }
}