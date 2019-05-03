using Entidad;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IArea
    {
        List<AreaPaginationDto> PaginadoArea(PaginationParameter objPaginationParameter);
        List<Area> ListadoArea(string area);
        AreaConsultaDto ConsultaArea(AreaConsultaDto objArea);
        int MantenimientoArea(Area objArea);
        string EliminarArea(string IdArea, int Accion);
    }
}
