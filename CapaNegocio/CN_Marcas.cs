using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Marcas
    {
        private CD_Marcas cn_marcas = new CD_Marcas();

        public List<Marca> Listar()
        {
            return cn_marcas.Listar();
        }

        public Response Agregar(Marca marca)
        {
            CD_Marcas omarca = new CD_Marcas();
            return omarca.Agregar(marca);
        }

        public Response Editar(Marca marca)
        {
            CD_Marcas omarca = new CD_Marcas();
            return omarca.Editar(marca);
        }

        public Response Eliminar(int idMarca)
        {
            CD_Marcas omarca = new CD_Marcas();
            return omarca.Eliminar(idMarca);
        }
    }
}
