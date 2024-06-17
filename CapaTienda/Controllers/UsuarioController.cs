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

   
    }
}