using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
    public class Documento:UsuarioActualDto
    {
        public string IdDocumento { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool FechaVencimiento { get; set; }
        public bool Obligatorio { get; set; }
        public string IdEntidad { get; set; }
        public string IdTipo { get; set; }
        public string IdEstado { get; set; }
        public string IdCategoria { get; set; }
    }
}
