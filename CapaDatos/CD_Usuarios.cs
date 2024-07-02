using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Usuarios
    {
        public List<Usuario> Listar()
        {
            List<USUARIO> listaEntity = null;
            List<Usuario> listaDTO = new List<Usuario>();

            try
            {

                using (DBCARRITOEntities db = new DBCARRITOEntities())
                {
                    listaEntity = db.USUARIO.ToList();

                    if (listaEntity != null)
                    {
                        listaEntity.ForEach(usuario => listaDTO.Add(new Usuario()
                        {
                            IdUsuario = usuario.IdUsuario,
                            Nombres = usuario.Nombres,
                            Apellidos = usuario.Apellidos,
                            Activo = usuario.Activo,
                            Clave = usuario.Clave,
                            Correo = usuario.Correo,
                            FechaRegistro = usuario.FechaRegistro.HasValue ? usuario.FechaRegistro.Value : DateTime.MinValue,
                            Reestablecer = usuario.Reestablecer

                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return listaDTO;
        }

        public Response Agregar(Usuario usuario)
        {
            Response response = new Response();
            try
            {
                using (DBCARRITOEntities db = new DBCARRITOEntities())
                {
                    // Verificar si el correo ya existe
                    bool correoExiste = db.USUARIO.Any(u => u.Correo == usuario.Correo);

                    if (correoExiste)
                    {
                        // Manejar el caso en que el correo ya existe
                        Console.WriteLine("El correo ya está registrado.");
                        response.success = false;
                        response.message = "El correo ya está registrado.";

                    }
                    else
                    {
                        USUARIO nuevoUsuario = new USUARIO
                        {
                            Nombres = usuario.Nombres,
                            Apellidos = usuario.Apellidos,
                            Correo = usuario.Correo,
                            Activo = usuario.Activo,
                            Clave = usuario.Clave, 
                            Reestablecer = usuario.Reestablecer,
                            FechaRegistro = DateTime.Now
                        };

                        db.USUARIO.Add(nuevoUsuario);
                        db.SaveChanges(); // Guarda los cambios en la base de datos

                        response.success = true;
                        response.message = "Se ha creado el usuario correctamente";
                    }
                }
                return response; // La operación fue exitosa
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                response.success = false;
                response.message = "Ha ocurrido un error: " + ex.Message;

                return response;
            }
        }

        public Response Actualizar(Usuario usuario)
        {
            Response response = new Response();
            try
            {
                using (DBCARRITOEntities db = new DBCARRITOEntities())
                {
                    var usuarioExistente = db.USUARIO.Find(usuario.IdUsuario);
                    if (usuarioExistente != null)
                    {
                        usuarioExistente.Nombres = usuario.Nombres;
                        usuarioExistente.Apellidos = usuario.Apellidos;
                        usuarioExistente.Correo = usuario.Correo;
                        usuarioExistente.Activo = usuario.Activo;
                        usuarioExistente.Clave = usuario.Clave; // Asegúrate de manejar la clave adecuadamente
                        usuarioExistente.Reestablecer = usuario.Reestablecer;

                        db.SaveChanges(); // Guarda los cambios en la base de datos
                        response.success = true;
                        response.message = "Se ha actualizado correctamente";
                        return response; // La operación fue exitosa
                    }
                    else
                    {
                        response.success = false;
                        response.message = "No se encontró el usuario";
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                response.message = "Ha ocurrido un error " + ex.Message;
                return response; // Ocurrió un error
            }
        }

        public bool Eliminar(int idUsuario)
        {
            try
            {
                using (DBCARRITOEntities db = new DBCARRITOEntities())
                {
                    var usuario = db.USUARIO.Find(idUsuario);
                    if (usuario != null)
                    {
                        db.USUARIO.Remove(usuario);
                        db.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }

        public Response EliminarDeseo(int idUsuario, int idProducto)
        {
            Response response = new Response();


            try
            {
                using (DBCARRITOEntities db = new DBCARRITOEntities())
                {
                    
                    var usuario = db.USUARIO.Find(idUsuario);

                    if (usuario == null)
                    {
                        response.success = false;
                        response.message = "Usuario no encontrado.";
                        return response;
                    }

                    // Encuentra el producto en la lista de deseos del usuario
                    var deseo = db.DESEOS.FirstOrDefault(d => d.usuarioID == idUsuario && d.productoID == idProducto);

                    if (deseo == null)
                    {
                        response.success = false;
                        response.message = "Producto no encontrado en la lista de deseos.";
                        return response;
                    }

                    
                    db.DESEOS.Remove(deseo);
                    db.SaveChanges();

                    response.success = true;
                    response.message = "Producto eliminado correctamente.";
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = $"Error al eliminar el producto: {ex.Message}";
            }

            return response;
        }
    }
}
