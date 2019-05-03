using Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IRegimen
    {
        List<RegimenPaginationDto> PaginadoRegimen(PaginationParameter objPaginationParameter);
        List<RegimenConsultaDto> ListadoRegimen(string Regimen);
        RegimenConsultaDto ConsultaRegimen(RegimenConsultaDto objRegimen);
        int MantenimientoRegimen(Regimen objRegimen);
        string EliminarRegimen(string IdRegimen,int Accion);
    }
}
