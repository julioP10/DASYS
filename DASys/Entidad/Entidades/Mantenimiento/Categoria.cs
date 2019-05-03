using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
    public class Categoria:UsuarioActualDto
    {
        public string IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string IdEntidad { get; set; }
        public string IdEstado { get; set; }

    }
}
