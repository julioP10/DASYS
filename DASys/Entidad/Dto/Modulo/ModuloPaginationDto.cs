using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
   public  class ModuloPaginationDto
    {

        public string IdModulo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public int Cantidad { get; set; }
        public int Posicion { get; set; }
        public string Icono { get; set; }
    }
}
