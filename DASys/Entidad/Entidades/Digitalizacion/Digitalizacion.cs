using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Digitalizacion:UsuarioActualDto
    {
        public string IdDigitalizacion { get; set; }
        public string IdDigitalizacionDesaprobado { get; set; }
        public string IdDocumento { get; set; }
        public string Observacion { get; set; }
        public string FechaVencimiento { get; set; }
        public string IdEstado { get; set; }
        public string IdPersona { get; set; }
        public string Opcion { get; set; }
    }
}
