using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
   public   class PerfilConsultaDto:UsuarioActualDto
    {
        public string IdPerfil { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string IdTipo { get; set; }
        public string Tipo { get; set; }
        public string IdEstado { get; set; }
        public string IdModulo { get; set; }
        public string Estado { get; set; }
    }
}
