using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Producto
    {

        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdMarca { get; set; } // Identificador de la marca
        public int IdCategoria { get; set; }
        public string MarcaNombre { get; set; }
        public string CategoriaNombre { get; set; }

        public Marca oMarca { get; set; }
        public Categoria oCategoria { get; set; }
        public decimal Precio { get; set; }
        public string PrecioString { get; set; }

        public int Stock { get; set; }
        public string RutaImagen { get; set; }
        public string NombreImagen { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }

    }
    public class CategoriaConProductos
    {
        public string NombreCategoria { get; set; }
        public List<Producto> Productos { get; set; }
    }
    public class MarcaConProductos
    {
        public string NombreMarca { get; set; }
        public List<Producto> Productos { get; set; }
    }

    public class MarcasYproductos
    {
        public List<Marca> ListaMarcas { get; set; }
        public List<Producto> Productos { get; set; }
    }

    public class CategoriasYproductos
    {

        public List<Categoria> ListaCategorias { get; set; }
        public List<Producto> Productos { get; set; }
    }
}
