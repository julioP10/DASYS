using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class ReporteDigitalizacionDto
    {
        public string IdPersona { get; set; }
        public string Nombres { get; set; }
        public string Categoria { get; set; }
        public string Documento { get; set; }
        public string Estado { get; set; }
        public string EstadoAdjunto { get; set; }
        public string FechaVencimiento { get; set; }
        public string Obligatorio { get; set; }
        public int Adjuntado { get; set; }
        public string Observacion { get; set; }
        public string ObservacionAdjunto { get; set; }
        public string Fecha { get; set; }
        public string Id { get; set; }
        public int Cantidad { get; set; }
        public string Entidad { get; set; }
    }
}
