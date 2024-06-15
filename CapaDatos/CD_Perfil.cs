using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Perfil
    {
        public List<Usuario> MostrarUsuario(int IdUsuario)
        {
            List<USUARIO> listDatos = null;
            List<Usuario> list = new List<Usuario>();

            try
            {
                using(DBCARRITOEntities db = new DBCARRITOEntities())
                {
                    listDatos = db.USUARIO.Where(u => u.IdUsuario == IdUsuario).ToList();

                    if(listDatos != null)
                    {
                        listDatos.ForEach(usuario => list.Add(new Usuario()
                        {
                            IdUsuario = usuario.IdUsuario,
                            Nombres = usuario.Nombres,
                            Apellidos = usuario.Apellidos,
                            Correo = usuario.Correo,
                            Clave = usuario.Clave,

                        }));
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return list;
        }

    }
}
