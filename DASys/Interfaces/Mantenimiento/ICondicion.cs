using Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface ICondicion
    {
        List<CondicionPaginationDto> PaginadoCondicion(PaginationParameter objPaginationParameter);
        List<CondicionConsultaDto> ListadoCondicion(string Condicion);
        CondicionConsultaDto ConsultaCondicion(CondicionConsultaDto objCondicion);
        int MantenimientoCondicion(Condicion objCondicion);
        string EliminarCondicion(string IdCondicion, int Accion);
    }
}
