using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
   public  class DocumentoAdjuntoConsultaDto
    {
        public string IdDocumentoAdjunto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string IdPersona { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string IdEstado { get; set; }
        public string IdDigitalizacion { get; set; }
        public string Ruta { get; set; }
        public string FechaVencimiento { get; set; }
        public string Observacion { get; set; }
        public string Vencimiento { get; set; }
        public string Documento { get; set; }
        public string Obligatorio { get; set; }
        public string Estado { get; set; }
        public string Tipo { get; set; }
        public IList<DocumentoAdjuntoConsultaDto> listDocumentoAdjunto { get; set; }
    }
}
