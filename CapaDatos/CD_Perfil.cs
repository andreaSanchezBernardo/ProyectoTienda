﻿using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Perfil
    {
        public UsuarioConDeseos MostrarUsuario(int IdUsuario)
        {
            UsuarioConDeseos resultado = new UsuarioConDeseos();

            try
            {
                using (DBCARRITOEntities db = new DBCARRITOEntities())
                {
                    // Obtener información del usuario
                    var usuarioEntity = db.USUARIO.FirstOrDefault(u => u.IdUsuario == IdUsuario);

                    if (usuarioEntity != null)
                    {
                        resultado.Usuario = new Usuario
                        {
                            IdUsuario = usuarioEntity.IdUsuario,
                            Nombres = usuarioEntity.Nombres,
                            Apellidos = usuarioEntity.Apellidos,
                            Correo = usuarioEntity.Correo,
                            RutaImagen = usuarioEntity.RutaImagen,
                            Clave = usuarioEntity.Clave
                        };
                    }

                    // Obtener lista de deseos del usuario
                    resultado.ListaDeseos = ListadeDeseos(IdUsuario);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
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
