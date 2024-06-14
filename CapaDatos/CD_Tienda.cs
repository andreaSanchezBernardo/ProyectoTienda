﻿using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Tienda
    {
        public Response Acceso(string correo, string clave)
        {
            Response response = new Response();
            try
            {
                using (DBCARRITOEntities db = new DBCARRITOEntities())
                {

                    // Verifica si existe un usuario con el correo y la clave proporcionados
                    var usuario = db.USUARIO.FirstOrDefault(d => d.Correo == correo && d.Clave == clave);
                    Usuario usuario1 = new Usuario()
                    {
                        IdUsuario = usuario.IdUsuario,
                    };

                    if (usuario != null)
                    {
                        // El usuario fue encontrado, realiza las acciones necesarias aquí
                        response.success = true;
                        response.mensaje = "Usuario autenticado exitosamente";
                        response.data = usuario1;
                    }
                    else
                    {
                        // El usuario no fue encontrado
                        response.success = false;
                        response.mensaje = "Usuario o contraseña inválidos";
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                response.success = false;
                response.mensaje = "Ha ocurrido un error: " + ex.Message;
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return response;
        }

        public List<Producto> MostrarProductos()
        {
            List<PRODUCTO> listEntity = null;
            List<Producto> list = new List<Producto>();

            try
            {
                using (DBCARRITOEntities db = new DBCARRITOEntities())
                {
                    listEntity = db.PRODUCTO.Where(p => (bool)p.Activo).ToList();

                    if (listEntity != null)
                    {
                        listEntity.ForEach(producto => list.Add(new Producto()
                        {
                            IdProducto = producto.IdProducto,
                            Nombre = producto.Nombre,
                            Descripcion = producto.Descripcion,
                            Precio = (decimal)producto.Precio,
                            RutaImagen = producto.RutaImagen,
                            Activo = (bool)producto.Activo,
                        }));
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return list;
        }



        public CategoriaConProductos MostrarCategorias(int idCategoria)
        {
            CategoriaConProductos resultado = new CategoriaConProductos();
            List<PRODUCTO> listEntity = null;

            try
            {
                using (DBCARRITOEntities db = new DBCARRITOEntities())
                {
                    var categoria = db.CATEGORIA.FirstOrDefault(c => c.IdCategoria == idCategoria);
                    if (categoria != null)
                    {
                        resultado.NombreCategoria = categoria.Descripcion;
                        listEntity = db.PRODUCTO.Where(p => (bool)p.Activo && p.IdCategoria == idCategoria).ToList();

                        if (listEntity != null)
                        {
                            resultado.Productos = listEntity.Select(entity => new Producto
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

        public MarcaConProductos MostrarMarcas(int idMarca)
        {
            MarcaConProductos resultado = new MarcaConProductos();
            List<PRODUCTO> listEntity = null;

            try
            {
                using (DBCARRITOEntities db = new DBCARRITOEntities())
                {
                    var marca = db.MARCA.FirstOrDefault(c => c.IdMarca == idMarca);
                    if (marca != null)
                    {
                        resultado.NombreMarca = marca.Descripcion;
                        listEntity = db.PRODUCTO.Where(p => (bool)p.Activo && p.IdMarca == idMarca).ToList();

                        if (listEntity != null)
                        {
                            resultado.Productos = listEntity.Select(entity => new Producto
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

        public Response ListaDeseos(int IdProducto, int IdUsuario)
        {
            Response response = new Response();

            try
            {
                using (DBCARRITOEntities db = new DBCARRITOEntities())
                {
                    bool productoExiste = db.DESEOS.Any(d => d.productoID == IdProducto && d.usuarioID == IdUsuario);


                    if (productoExiste)
                    {
                        Console.WriteLine("El producto ya está registrado.");
                        response.success = false;
                        response.mensaje = "El producto ya está registrado.";
                    }
                    else
                    {
                        DESEOS nuevoDeseo = new DESEOS
                        {
                            productoID = IdProducto,
                            usuarioID = IdUsuario,
                        };

                        db.DESEOS.Add(nuevoDeseo);
                        db.SaveChanges();

                        response.success = true;
                        response.mensaje = "Producto añadido a la lista de deseos correctamente";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return response;

        }


        public ListaDeseos ListadeDeseos(int IdUsuario)
        {
            ListaDeseos resultado = new ListaDeseos();
            List<PRODUCTO> productos = null;

            try
            {
                using (DBCARRITOEntities db = new DBCARRITOEntities())
                {
                    var deseoUsuario = db.DESEOS.Where(d => d.usuarioID == IdUsuario).ToList();

                    if (deseoUsuario != null && deseoUsuario.Count > 0)
                    {
                        var idsProductosDeseados = deseoUsuario.Select(d => d.productoID).ToList();
                        productos = db.PRODUCTO.Where(p => (bool)p.Activo && idsProductosDeseados.Contains(p.IdProducto)).ToList();
                        resultado.usuarioId = IdUsuario;

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