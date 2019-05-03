using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class CategoriaConsultaDto:UsuarioActualDto
    {
        public string IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string IdEntidad { get; set; }
        public string IdEstado { get; set; }
    }
}
