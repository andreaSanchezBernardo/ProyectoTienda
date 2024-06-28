using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Productos
    {
        private CD_Productos CD_Productos = new CD_Productos();

        public List<Producto> Listar()
        {
            return CD_Productos.Listar();
        }

        public Response Agregar(Producto producto)
        {
            CD_Productos cD_Productos = new CD_Productos();
            Response response = cD_Productos.Agregar(producto);
            return response;
        }

        public Response Eliminar(int IdProducto)
        {
            CD_Productos cD_Productos = new CD_Productos();
            Response response = cD_Productos.Elimnar(IdProducto);
            return response;
        }


        public Response Editar(Producto producto)
        {
            CD_Productos cD_productos = new CD_Productos();
            Response response = cD_productos.Editar(producto);
            return response;
        }
    }
}
