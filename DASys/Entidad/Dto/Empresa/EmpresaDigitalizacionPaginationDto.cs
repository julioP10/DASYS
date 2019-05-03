using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class EmpresaDigitalizacionPaginationDto
    {
        public string IdEmpresa { get; set; }
        public string Alias { get; set; }
        public string IdCategoria { get; set; }
        public string Categoria { get; set; }
        public string IdDocumento { get; set; }
        public string Documento { get; set; }
        public string Observacion { get; set; }
        public int Adjuntado { get; set; }
        public string Obligatorio { get; set; }
        public string FechaVencimiento { get; set; }
        public string Estado { get; set; }
        public int Cantidad { get; set; }
        public string IdPersona { get; set; }
        public string IdDocumentoAdjunto { get; set; }
        public string IdDigitalizacion { get; set; }
        public string ObservacionAdjunto { get; set; }
        public string EstadoAdjunto { get; set; }
    }
}
