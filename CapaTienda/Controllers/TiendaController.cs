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
                response.message = "Autenticación exitosa";
            }
            else
            {
                response.success = false;
                response.message = "Usuario o contraseña inválidos.";
            }

            return Json(response);
        }

        public ActionResult CerrarSesion()
        {
            Session["UserId"] = null;
            Session.Clear();
            return RedirectToAction("Inicio", "Tienda");
        }


        public ActionResult Index()
        {

            List<Producto> oProducto = new List<Producto>();
            oProducto = new CN_Tienda().MostrarProducto();
            return View(oProducto);
        }

        public ActionResult PaginaCategoria()
        {
            CN_Tienda cN_tienda = new CN_Tienda();
            CategoriasYproductosYmarcas resultado = cN_tienda.CategoriaAcceso();
            return View(resultado);
        }
        public ActionResult PaginaMarca()
        {
            List<Marca> oMarca = new List<Marca>();
            oMarca = new CN_Tienda().MarcasPagina();
            return View(oMarca);
        }

        public ActionResult Cesta()
        {
            ListaCarrito carrito = new CN_Carrito().ListaCarrito((int)Session["UserId"]);
            return View(carrito);
        }

        [HttpPost]
        public JsonResult AnadirCarrito(int IdProducto)
        {

            int idUsuario = (int)Session["UserId"];

            CN_Carrito cN_Carrito = new CN_Carrito();
            cN_Carrito.AnadirCarrito(IdProducto, idUsuario);
            return Json(cN_Carrito);
        }
        [HttpPost]
        public JsonResult ListaDeseos(int IdProducto)
        {

            int IdUsuario = (int)Session["UserId"];

            CN_Tienda cN_Tienda = new CN_Tienda();
            cN_Tienda.AnadirDeseo(IdProducto, IdUsuario);
            return Json(cN_Tienda);


        }

        public ActionResult MarcasAcceso()
        {
            List<Marca> oMarca = new List<Marca>();
            oMarca = new CN_Tienda().MarcasPagina();
            return View(oMarca);
        }

        public ActionResult DentroMarca(int IdMarca)
        {
            CN_Tienda cN_Tienda = new CN_Tienda();
            var resultado = cN_Tienda.DentroMarca(IdMarca);
            return View(resultado);
        }

        public ActionResult Marcas(int IdMarca)
        {
            CN_Tienda cN_Tienda = new CN_Tienda();
            var resultado = cN_Tienda.DentroMarca(IdMarca);
            return View(resultado);
        }

        public ActionResult CategoriasAcceso()
        {
            CN_Tienda cN_tienda= new CN_Tienda();
            CategoriasYproductosYmarcas resultado = cN_tienda.CategoriaAcceso();
            return View(resultado);

        }
    }
}