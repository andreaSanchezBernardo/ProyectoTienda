using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Categorias
    {
        public List<Categoria> Listar()
        {
            List<CATEGORIA> listaEntity = null;
            List<Categoria> listDatos = new List<Categoria>();
            try
            {
                using (DBCARRITOEntities db = new DBCARRITOEntities())
                {
                    listaEntity = db.CATEGORIA.ToList();
                    if (listaEntity != null)
                    {
                        listaEntity.ForEach(categoria => listDatos.Add(new Categoria()
                        {
                            IdCategoria = categoria.IdCategoria,
                            Descripcion = categoria.Descripcion,
                            FechaRegistro = (DateTime)categoria.FechaRegistro,
                            Activo = (bool)categoria.Activo
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return listDatos;

        }

        public Response Agregar(Categoria categoria)
        {
            Response response = new Response();

            try
            {
                using (DBCARRITOEntities db = new DBCARRITOEntities())
                {
                    // Verificar si la categoria ya existe
                    bool categoriaExiste = db.CATEGORIA.Any(c => c.Descripcion == categoria.Descripcion);

                    if (categoriaExiste)
                    {
                        // Manejar el caso en que la descripcion ya existe
                        Console.WriteLine("El producto ya está registrado.");
                        response.success = false;
                        response.mensaje = "El producto ya está registrado.";

                    }
                    else
                    {
                        CATEGORIA nuevaCategoria = new CATEGORIA
                        {
                            Descripcion = categoria.Descripcion,
                            FechaRegistro = DateTime.Now,
                            Activo = categoria.Activo
                        };
                        db.CATEGORIA.Add(nuevaCategoria);
                        db.SaveChanges(); // Guarda los cambios en la base de datos

                        response.success = true;
                        response.mensaje = "Se ha creado la categoria correctamente";
                    }
                    return response; // La operación fue exitosa
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                response.success = false;
                response.mensaje = "Ha ocurrido un error: " + ex.Message;

                return response;
            }
        }

        public Response Editar(Categoria categoria)
        {
            Response response = new Response();

            try
            {
                using (DBCARRITOEntities db = new DBCARRITOEntities())
                {
                    var categoriaExistente = db.CATEGORIA.Find(categoria.IdCategoria);

                    if (categoriaExistente != null)
                    {

                        categoriaExistente.Descripcion = categoria.Descripcion;
                        categoriaExistente.Activo = categoria.Activo;

                        db.SaveChanges(); // Guarda los cambios en la base de datos
                        response.success = true;
                        response.mensaje = "Se ha actualizado correctamente";
                        return response; // La operación fue exitosa
                    }
                    else
                    {
                        response.success = false;
                        response.mensaje = "No se encontró el usuario";
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


        public Response Eliminar(int idCategoria)
        {
            Response response = new Response();
            try
            {
                using (DBCARRITOEntities db = new DBCARRITOEntities())
                {
                    var categoria = db.CATEGORIA.Find(idCategoria);
                    if (categoria != null)
                    {
                        db.CATEGORIA.Remove(categoria);
                        db.SaveChanges();
                        response.success = true;
                        response.mensaje = "Categoria eliminada correctamente.";
                    }
                    else
                    {
                        response.success = false;
                        response.mensaje = "No se encontró la categoria";

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
