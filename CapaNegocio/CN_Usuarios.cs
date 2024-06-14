using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Usuarios
    {
        private CD_Usuarios objCapaDato = new CD_Usuarios();

        public List<Usuario> Listar()
        {
            return objCapaDato.Listar();
        }
        public Response Agregar(Usuario usuario)
        {
            CD_Usuarios cd_usuarios = new CD_Usuarios();

            return cd_usuarios.Agregar(usuario);
        }

        public Response Actualizar(Usuario usuario)
        {
            CD_Usuarios cd_usuarios = new CD_Usuarios();

            return cd_usuarios.Actualizar(usuario);
        }
        public bool Eliminar(int idUsuario)
        {
            return new CD_Usuarios().Eliminar(idUsuario);
        }

    }
}
