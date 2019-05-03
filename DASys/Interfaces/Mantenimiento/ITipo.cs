using Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface ITipo
    {
        List<TipoPaginationDto> PaginadoTipo(PaginationParameter objPaginationParameter);
        List<TipoConsultaDto> ListadoTipo(string Tipo);
        TipoConsultaDto ConsultaTipo(TipoConsultaDto objTipo);
        int MantenimientoTipo(Tipo objTipo);
        string EliminarTipo(string IdTipo);
    }
}
