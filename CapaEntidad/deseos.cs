using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Deseos
    {
        public int Id { get; set; }
        public int usuarioId { get; set; }
        public int productoId { get; set; }
    }

    public class ListaDeseos
    {
        public int usuarioId { get; set; }
        public List<Producto> Productos { get; set; }
    }
}