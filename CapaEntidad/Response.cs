using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Response
    {
        public bool success { get; set; }
        public string message { get; set; }
        public object data { get; set; } // Agrega una propiedad 'data' para almacenar el usuario
    }
}

