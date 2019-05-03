using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
   public class EntidadConsultaDto:UsuarioActualDto
    {
        public string IdEntidad { get; set; }
        public string Nombre { get; set; }
        public string IdPrincipal { get; set; }
        public string IdEstado { get; set; }
        public string Estado { get; set; }
    }
}
