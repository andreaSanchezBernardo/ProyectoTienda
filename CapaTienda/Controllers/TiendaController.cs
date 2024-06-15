using CapaEntidad;
using CapaNegocio;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaTienda.Controllers
{
    public class TiendaController : Controller
    {
        // GET: Tienda
        public ActionResult Inicio()
        {
            List<Producto> oProducto = new List<Producto>();
            oProducto = new CN_Tienda().MostrarProducto();
            return View(oProducto);
        }

        public ActionResult NuevaSesion()
        {
            return View();
        }

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
        public ActionResult Acceso(string correo, string clave)
        {
            CN_Tienda cN_Tienda = new CN_Tienda();
            Response response = cN_Tienda.Acceso(correo, clave);

            if (response.success && response.data is Usuario usuario)
            {
                Session["UserId"] = usuario.IdUsuario;
                response.success = true;
                response.mensaje = "Autenticación exitosa";
            }
            else
            {
                response.success = false;
                response.mensaje = "Error en el tipo de datos retornado.";
            }

            return Json(response);
        }


        public ActionResult Index()
        {

            List<Producto> oProducto = new List<Producto>();
            oProducto = new CN_Tienda().MostrarProducto();
            return View(oProducto);
        }

        public ActionResult PaginaCategoria(int idCategoria)
        {
            CN_Tienda cN_Tienda = new CN_Tienda();
            CategoriaConProductos resultado = cN_Tienda.MostrarCategorias(idCategoria);
            ViewBag.NombreCategoria = resultado.NombreCategoria;
            return View(resultado.Productos);
        }
        public ActionResult PaginaMarca(int idMarca)
        {
            CN_Tienda cN_Tienda = new CN_Tienda();
            MarcaConProductos resultado = cN_Tienda.MostrarMarcas(idMarca);
            ViewBag.NombreMarca = resultado.NombreMarca;
            return View(resultado.Productos);
        }

        public ActionResult Cesta()
        {
            //int IdUsuario = (int)Session["UserId"];
            CN_Tienda cn_tienda = new CN_Tienda();
            ListaDeseos resultado = cn_tienda.ListaDeseos((int)Session["UserId"]);
            
            return View(resultado);

        }
        [HttpPost]
        public JsonResult ListaDeseos(int IdProducto)
        {

            int IdUsuario = (int)Session["UserId"];

            CN_Tienda cN_Tienda = new CN_Tienda();
            cN_Tienda.AnadirDeseo(IdProducto, IdUsuario);
            return Json(cN_Tienda);


        }
    }
}