using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Tienda
    {
        private CD_Tienda CD_Tienda = new CD_Tienda();
        public Response Acceso(string correo, string clave)
        {
            Response response = new Response();

            try
            {
                // Encripta la contraseña proporcionada por el usuario en la capa de negocio
                string claveEncriptada = CN_Recursos.ConvertirSha256(clave);

                // Llama al método Acceso en la capa de datos y pasa la contraseña encriptada
                CD_Tienda cd_tienda = new CD_Tienda();
                response = cd_tienda.Acceso(correo, claveEncriptada);

                // Verifica la respuesta de la capa de datos
                if (response.success && response.data is Usuario usuario)
                {
                    response.data = usuario; // Asigna el objeto USUARIO a response.data
                    response.mensaje = "Usuario autenticado exitosamente";
                }
                else
                {
                    response.success = false;
                    response.mensaje = "Usuario o contraseña inválidos";
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.mensaje = "Ha ocurrido un error: " + ex.Message;
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return response;


        }

        public List<Producto> MostrarProducto()
        {

            return CD_Tienda.MostrarProductos();
        }


        public CategoriaConProductos MostrarCategorias(int idCategoria)
        {
            CD_Tienda cD_Tienda = new CD_Tienda();
            return cD_Tienda.MostrarCategorias(idCategoria);
        }

        public MarcaConProductos MostrarMarcas(int idMarca)
        {
            CD_Tienda cD_Tienda = new CD_Tienda();
            return cD_Tienda.MostrarMarcas(idMarca);
        }

        public Response AnadirDeseo(int IdProducto, int IdUsuario)
        {
            CD_Tienda cd_tienda = new CD_Tienda();
            return cd_tienda.ListaDeseos(IdProducto, IdUsuario);
        }

        public ListaDeseos ListaDeseos(int IdUsuario)
        {
            CD_Tienda cd_tienda = new CD_Tienda();
            return cd_tienda.ListadeDeseos(IdUsuario);
        }
        public List<Marca> MarcasPagina()
        { 
            return CD_Tienda.PaginaMarca();
        }
        public CategoriasYproductosYmarcas CategoriaAcceso()
        {
            CD_Tienda cd_tienda = new CD_Tienda();
            return cd_tienda.CategoriasAcceso();
        }
    }
}
