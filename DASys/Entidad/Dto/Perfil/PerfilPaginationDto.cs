using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
   public  class PerfilPaginationDto
    {
        public string IdPerfil { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public int Cantidad { get; set; }
    }
}
