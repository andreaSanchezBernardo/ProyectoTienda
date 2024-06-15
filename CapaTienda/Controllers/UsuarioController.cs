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
            List<Usuario> oUsuario = new List<Usuario>();
            oUsuario = new CN_Perfil().MostrarUsuario(IdUsuario);
            
            return View(oUsuario);
        }

   
    }
}