using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Perfil
    {
        private CD_Perfil CD_Perfil = new CD_Perfil();

        public UsuarioConDeseos MostrarUsuario(int IdUsuario)
        {
            return CD_Perfil.MostrarUsuario(IdUsuario);
        }

    }
}
