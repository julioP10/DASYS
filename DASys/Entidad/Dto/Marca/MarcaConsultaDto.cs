using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
    public class MarcaConsultaDto:UsuarioActualDto
    {
        public string IdMarca { get; set; }
        public string IdEntidad { get; set; }
        public string Entidad { get; set; }
        public string Nombre { get; set; }
        public string Estado { get; set; }
        public string IdEstado { get; set; }
    }
}
