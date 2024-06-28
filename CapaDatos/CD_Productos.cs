using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Productos
    {
        public List<Producto> Listar()
        {
            List<PRODUCTO> listaEntity = null;
            List<Producto> listaDatos = new List<Producto>();

            try
            {
                using (DBCARRITOEntities db = new DBCARRITOEntities())
                {
                    listaEntity = db.PRODUCTO.ToList();

                    if (listaEntity != null)
                    {
                        listaEntity.ForEach(producto => listaDatos.Add(new Producto()
                        {
                            IdProducto = producto.IdProducto,
                            Nombre = producto.Nombre,
                            Descripcion = producto.Descripcion,
                            IdMarca = (int)producto.IdMarca, // Usar identificador de la marca
                            IdCategoria = (int)producto.IdCategoria,
                            MarcaNombre = producto.MARCA.Descripcion,
                            CategoriaNombre = producto.CATEGORIA.Descripcion,
                            oMarca = new Marca()
                            {
                                IdMarca = producto.MARCA.IdMarca,
                                Descripcion = producto.MARCA.Descripcion
                            },
                            oCategoria = new Categoria()
                            {
                                IdCategoria = producto.CATEGORIA.IdCategoria,
                                Descripcion = producto.CATEGORIA.Descripcion,
                            },
                            FechaRegistro = producto.FechaRegistro.HasValue ? producto.FechaRegistro.Value : DateTime.MinValue,
                            Precio = (decimal)producto.Precio,
                            Activo = (bool)producto.Activo,
                            Stock = (int)producto.Stock,
                            RutaImagen = producto.RutaImagen
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return listaDatos;
        }

        public Response Agregar(Producto producto)
        {
            Response response = new Response();

            try
            {
                using (DBCARRITOEntities db = new DBCARRITOEntities())
                {
                    // Verificar si el producto ya existe
                    bool productoExiste = db.PRODUCTO.Any(c => c.Nombre == producto.Nombre);

                    if (productoExiste)
                    {
                        // Manejar el caso en que la descripcion ya existe
                        Console.WriteLine("El producto ya está registrado.");
                        response.success = false;
                        response.mensaje = "El producto ya está registrado.";

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(producto.PrecioString))
                        {
                            producto.Precio = decimal.Parse(producto.PrecioString.Replace(".", ","));
                        }
                        // Obtener la marca y la categoría
                        MARCA marca = db.MARCA.FirstOrDefault(m => m.IdMarca == producto.IdMarca);
                        CATEGORIA categoria = db.CATEGORIA.FirstOrDefault(c => c.IdCategoria == producto.IdCategoria);

                        if (marca == null || categoria == null)
                        {
                            response.success = false;
                            response.mensaje = "La marca o la categoría no existen.";
                        }
                        else
                        {

                            PRODUCTO nuevoProducto = new PRODUCTO
                            {
                                Nombre = producto.Nombre,
                                Descripcion = producto.Descripcion,
                                Precio = producto.Precio,
                                Stock = producto.Stock,
                                IdMarca = producto.IdMarca,
                                IdCategoria = producto.IdCategoria,
                                FechaRegistro = DateTime.Now,
                                Activo = producto.Activo,
                                RutaImagen = producto.RutaImagen // Guardar la ruta de la imagen
                            };
                            db.PRODUCTO.Add(nuevoProducto);
                            db.SaveChanges(); // Guarda los cambios en la base de datos

                            response.success = true;
                            response.mensaje = "Se ha creado el producto correctamente";
                        }
                    }


                }

            }
            catch (Exception ex)
            {


                Console.WriteLine(ex.Message);
            }
            return response;
        }

        public Response Editar(Producto producto)
        {
            Response response = new Response();

            try
            {
                using (DBCARRITOEntities db = new DBCARRITOEntities())
                {
                    var productoExistente = db.PRODUCTO.Find(producto.IdProducto);

                    if (!string.IsNullOrEmpty(producto.PrecioString))
                    {
                        producto.Precio = decimal.Parse(producto.PrecioString.Replace(".", ","));
                    }

                    if (productoExistente != null)
                    {
                        productoExistente.Nombre = producto.Nombre;
                        productoExistente.Descripcion = producto.Descripcion;
                        productoExistente.Stock = producto.Stock;
                        productoExistente.Precio = producto.Precio;
                        productoExistente.IdMarca = producto.IdMarca;
                        productoExistente.IdCategoria = producto.IdCategoria;
                        productoExistente.Activo = producto.Activo;
                        productoExistente.RutaImagen = producto.RutaImagen;

                        db.SaveChanges(); // Guarda los cambios en la base de datos
                        response.success = true;
                        response.mensaje = "Se ha actualizado correctamente";
                        return response; // La operación fue exitosa
                    }
                    else
                    {
                        response.success = false;
                        response.mensaje = "No se encontró el producto";
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                response.mensaje = "Ha ocurrido un error " + ex.Message;
                return response; // Ocurrió un error
            }

        }




        public Response Elimnar(int IdProducto)
        {
            Response response = new Response();

            try
            {
                using (DBCARRITOEntities db = new DBCARRITOEntities())
                {
                    var producto = db.PRODUCTO.Find(IdProducto);
                    if (producto != null)
                    {
                        db.PRODUCTO.Remove(producto);
                        db.SaveChanges();
                        response.success = true;
                        response.mensaje = "Producto eliminado correctamente.";
                    }
                    else
                    {
                        response.success = false;
                        response.mensaje = "No se encontró el producto";

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                response.mensaje = "Ha ocurrido un error " + ex.Message;
                return response; // Ocurrió un error
            }
            return response;

        }
    }
}