using Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IEntidad
    {
        List<EntidadPaginationDto> PaginadoEntidad(PaginationParameter objPaginationParameter);
        List<EntidadConsultaDto> ListadoEntidad(string Entidad);
        EntidadConsultaDto ConsultaEntidad(EntidadConsultaDto objEntidad);
        int MantenimientoEntidad(Entidades objEntidad);
        string EliminarEntidad(string IdEntidad, int Accion);
    }
}
