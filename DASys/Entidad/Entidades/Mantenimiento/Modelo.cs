using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
    public class Modelo:UsuarioActualDto
    {
        public string IdModelo { get; set; }
        public string Nombre { get; set; }
        public string SDK { get; set; }
        public string IdMarca { get; set; }
        public string IdConfiguracion { get; set; }
        public string IdEstado { get; set; }

    }
}
