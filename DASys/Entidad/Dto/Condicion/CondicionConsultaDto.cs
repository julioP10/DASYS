using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class CondicionConsultaDto:UsuarioActualDto
    {
        public string IdCondicion { get; set; }
        public string Nombre { get; set; }
        public string IdRegimen { get; set; }
        public string IdEstado { get; set; }
        public string Estado { get; set; }
    }
}
