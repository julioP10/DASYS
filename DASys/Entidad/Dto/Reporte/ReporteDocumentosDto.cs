using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class ReporteDocumentosDto
    {
        public string IdPersona { get; set; }
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public string Documento { get; set; }
        public string Estado { get; set; }
        public string EstadoAdjunto { get; set; }
        public string FechaVencimiento { get; set; }
        public string Obligatorio { get; set; }
        public string Adjuntado { get; set; }
        public string Observacion { get; set; }
        public string ObservacionAdjunto { get; set; }
        public string Fecha { get; set; }
        public string DiasRestante { get; set; }
        public string Genero { get; set; }
        public string Departamento { get; set; }
        public string Ubicacion { get; set; }
        public string Empresa { get; set; }
        public int Cantidad { get; set; }
    }
}
