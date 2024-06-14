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
        public ActionResult Index()
        {
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