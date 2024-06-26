﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public string ConfirmarClave { get; set; }
        public bool? Reestablecer { get; set; }
        public bool? Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string RutaImagen { get; set; }  

    }
    public class UsuarioConDeseos
    {
       
        public Usuario Usuario { get; set; }

        public ListaDeseos ListaDeseos { get; set; }
       
    }
}
