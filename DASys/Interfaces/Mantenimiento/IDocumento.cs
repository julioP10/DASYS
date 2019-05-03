using Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IDocumento
    {

        List<DocumentoPaginationDto> PaginadoDocumento(PaginationParameter objPaginationParameter);
        List<DocumentoConsultaDto> ListadoDocumento(string Documento);
        DocumentoConsultaDto ConsultaDocumento(DocumentoConsultaDto objDocumento);
        int MantenimientoDocumento(Documento objDocumento);
        string EliminarDocumento(string IdDocumento,int Accion);
    }
}
