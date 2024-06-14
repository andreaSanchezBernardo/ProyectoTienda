using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Categorias
    {
        private CD_Categorias cn_categorias = new CD_Categorias();

        public List<Categoria> Listar()
        {
            return cn_categorias.Listar();
        }

        public Response Agregar(Categoria categoria)
        {
            CD_Categorias ocategoria = new CD_Categorias();
            return ocategoria.Agregar(categoria);
        }

        public Response Editar(Categoria categoria)
        {
            CD_Categorias ocategoria = new CD_Categorias();
            return ocategoria.Editar(categoria);
        }

        public Response Eliminar(int idCategoria)
        {
            CD_Categorias oCategoria = new CD_Categorias();
            return oCategoria.Eliminar(idCategoria);
        }
    }
}
