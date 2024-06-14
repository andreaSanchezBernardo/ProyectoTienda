using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Marcas
    {
        public List<Marca> Listar()
        {
            List<MARCA> listaEntity = null;
            List<Marca> listDatos = new List<Marca>();

            try
            {
                using (DBCARRITOEntities db = new DBCARRITOEntities())
                {

                    listaEntity = db.MARCA.ToList();
                    if (listaEntity != null)
                    {
                        listaEntity.ForEach(marca => listDatos.Add(new Marca()
                        {
                            IdMarca = marca.IdMarca,
                            Descripcion = marca.Descripcion,
                            FechaRegistro = (DateTime)marca.FechaRegistro,
                            Activo = (bool)marca.Activo

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

        public Response Agregar(Marca marca)
        {
            Response response = new Response();

            try
            {
                using (DBCARRITOEntities db = new DBCARRITOEntities())
                {
                    // Verificar si la marca ya existe
                    bool marcaExiste = db.MARCA.Any(c => c.Descripcion == marca.Descripcion);

                    if (marcaExiste)
                    {
                        // Manejar el caso en que la descripcion ya existe
                        Console.WriteLine("La marca ya está registrada.");
                        response.success = false;
                        response.mensaje = "La marca ya está registrada.";

                    }
                    else
                    {
                        MARCA nuevaMarca = new MARCA
                        {
                            Descripcion = marca.Descripcion,
                            FechaRegistro = DateTime.Now,
                            Activo = marca.Activo
                        };
                        db.MARCA.Add(nuevaMarca);
                        db.SaveChanges(); // Guarda los cambios en la base de datos

                        response.success = true;
                        response.mensaje = "Se ha creado la marca correctamente";
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

        public Response Editar(Marca marca)
        {
            Response response = new Response();

            try
            {
                using (DBCARRITOEntities db = new DBCARRITOEntities())
                {
                    var marcaExistente = db.MARCA.Find(marca.IdMarca);

                    if (marcaExistente != null)
                    {

                        marcaExistente.Descripcion = marca.Descripcion;
                        marcaExistente.Activo = marca.Activo;

                        db.SaveChanges(); // Guarda los cambios en la base de datos
                        response.success = true;
                        response.mensaje = "Se ha actualizado correctamente";
                        return response; // La operación fue exitosa
                    }
                    else
                    {
                        response.success = false;
                        response.mensaje = "No se encontró la marca";
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


        public Response Eliminar(int idMarca)
        {
            Response response = new Response();
            try
            {
                using (DBCARRITOEntities db = new DBCARRITOEntities())
                {
                    var marca = db.MARCA.Find(idMarca);
                    if (marca != null)
                    {
                        db.MARCA.Remove(marca);
                        db.SaveChanges();
                        response.success = true;
                        response.mensaje = "Marca eliminada correctamente.";
                    }
                    else
                    {
                        response.success = false;
                        response.mensaje = "No se encontró la marca";

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
