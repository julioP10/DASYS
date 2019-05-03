using Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface ITerminal
    {
        List<TerminalPaginationDto> PaginadoTerminal(PaginationParameter objPaginationParameter);
        List<TerminalConsultaDto> ListadoTerminal(string Terminal);
        TerminalConsultaDto ConsultaTerminal(TerminalConsultaDto objTerminal);
        int MantenimientoTerminal(Terminal objTerminal);
        string EliminarTerminal(string IdTerminal, int Accion);
    }
}
