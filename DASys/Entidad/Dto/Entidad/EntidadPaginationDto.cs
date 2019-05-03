using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
    public class EntidadPaginationDto
    {
        public string IdEntidad { get; set; }
        public string Nombre { get; set; }
        public string IdPrincipal { get; set; }
        public string IdEstado { get; set; }
        public string Estado { get; set; }
        public int Cantidad { get; set; }
    }
}
