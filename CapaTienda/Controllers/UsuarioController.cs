using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaTienda.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Perfil(int IdUsuario)
        {
            UsuarioConDeseos usuarioConDeseos = new CN_Perfil().MostrarUsuario(IdUsuario);

            return View(usuarioConDeseos);
        }

      public JsonResult AgregarImagenPerfil(int usuarioId, string rutaImagen)
        {
            CN_Perfil cd_perfil = new CN_Perfil();
            Response response = cd_perfil.CambiarImagenPerfil(usuarioId, rutaImagen);
            return Json(response);
        }

        [HttpPost]
        public JsonResult EliminarDeseo(int idUsuario, int idProducto)
        {
            CN_Usuarios cN_Usuarios = new CN_Usuarios();
            Response response = cN_Usuarios.EliminarDeseo(idUsuario, idProducto);
            return Json(response);
        }
    }
} 