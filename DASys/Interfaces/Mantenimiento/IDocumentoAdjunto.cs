using Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IDocumentoAdjunto
    {
        List<DocumentoAdjuntoPaginationDto> PaginadoDocumentoAdjunto(PaginationParameter objPaginationParameter);
        List<DocumentoAdjuntoConsultaDto> ListadoDocumentoAdjunto(string DocumentoAdjunto);
        DocumentoAdjuntoConsultaDto ConsultaDocumentoAdjunto(DocumentoAdjuntoConsultaDto objDocumentoAdjunto);
        int MantenimientoDocumentoAdjunto(DocumentoAdjunto objDocumentoAdjunto);
        int EliminarDocumentoAdjunto(string IdDocumentoAdjunto);
    }
}
