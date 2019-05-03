using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
   public  class AreaConsultaDto:UsuarioActualDto
    {
        public string IdArea { get; set; }
        public string Nombre { get; set; }
        public int Nivel { get; set; }
        public string IdPrincipal { get; set; }
        public string IdEstado { get; set; }
    }
}
