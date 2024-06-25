using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Carrito
    {
        private CD_Carrito objCarrito = new CD_Carrito();
        public Response AnadirCarrito(int IdProducto, int idUsuario)
        {
            Response response = new Response();
            CD_Carrito cd_Carrito = new CD_Carrito();
            response = cd_Carrito.AnadirCarrito(IdProducto, idUsuario);
            return response;
        }

        public ListaCarrito ListaCarrito(int idUsuario)
        {
            CD_Carrito cd_carrito = new CD_Carrito();
            return cd_carrito.ListaCarrito(idUsuario);
        }
    }
}
