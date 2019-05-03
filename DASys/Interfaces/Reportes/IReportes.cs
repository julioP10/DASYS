using Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IReportes
    {
        List<ReporteColaboradorDto> ReporteColaborador(PaginationParameter objPaginationParameter, ReporteFilterDto objReporteFilterDto);
        List<ReporteEmpresaDto> ReporteEmpresa(PaginationParameter objPaginationParameter);
         List<ReporteVehiculoDto> ReporteVehiculo(PaginationParameter objPaginationParameter, ReporteFilterDto objReporteFilterDto);
        List<ReporteMaquinariaDto> ReporteMaquinaria(PaginationParameter objPaginationParameter, ReporteFilterDto objReporteFilterDto);
        List<ReporteDocumentosDto> ReporteDocumentos(PaginationParameter objPaginationParameter);
        List<ReporteDigitalizacionDto> ReporteDigitalizacion(PaginationParameter objPaginationParameter);
        List<ReporteOchoMarcacionesDto> ReporteOchoMarcaciones(PaginationParameter objPaginationParameter);
        List<ReporteMarcacionesDto> ReporteMarcaciones(PaginationParameter objPaginationParameter);
        List<ReporteAsistenciaDto> ReporteAsistencia(PaginationParameter objPaginationParameter);
    }


}
