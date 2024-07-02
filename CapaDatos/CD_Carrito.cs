using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Carrito
    {

        public Response AnadirCarrito(int IdProducto, int idUsuario)
        {
            Response response = new Response();
            try
            {
                using(DBCARRITOEntities db = new DBCARRITOEntities())
                {
                    bool productoExistente = db.CARRITO.Any(c => c.IdUsuario == idUsuario && c.IdProducto == IdProducto);

                    if (productoExistente)
                    {

                        Console.WriteLine("El producto ya está registrado la cesta.");
                        response.success = false;
                        response.message = "El producto ya está en la cesta.";
                    }
                    else
                    {
                        CARRITO nuevoCarrito = new CARRITO
                        {
                            IdProducto = IdProducto,
                            IdUsuario = idUsuario,
                        };

                        db.CARRITO.Add(nuevoCarrito);
                        db.SaveChanges();

                        response.success = true;
                        response.message = "Producto añadido a la cesta";
                    }
                    }
                }
                catch (Exception ex)
                {
                Console.WriteLine(ex.Message);
            }
            return response;

        }

        public ListaCarrito ListaCarrito(int idUsuario)
        {
            ListaCarrito resultado = new ListaCarrito();
            List<PRODUCTO> productos = null;

            try
            {
                using (DBCARRITOEntities db = new DBCARRITOEntities())
                {
                    var carritoUsuario = db.CARRITO.Where(d => d.IdUsuario == idUsuario).ToList();

                    if (carritoUsuario != null && carritoUsuario.Count > 0)
                    {
                        var idsProductoscARRITO = carritoUsuario.Select(d => d.IdProducto).ToList();
                        productos = db.PRODUCTO.Where(p => (bool)p.Activo && idsProductoscARRITO.Contains(p.IdProducto)).ToList();
                        resultado.usuarioId = idUsuario;

                        if (productos != null && productos.Count > 0)
                        {
                            resultado.Productos = productos.Select(entity => new Producto
                            {
                                IdProducto = entity.IdProducto,
                                Nombre = entity.Nombre,
                                Precio = (decimal)entity.Precio,
                                RutaImagen = entity.RutaImagen,
                                Descripcion = entity.Descripcion
                            }).ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return resultado;
        }
    }

}

