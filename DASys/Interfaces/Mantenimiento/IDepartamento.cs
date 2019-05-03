using Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IDepartamento
    {
        List<DepartamentoPaginationDto> PaginadoDepartamento(PaginationParameter objPaginationParameter);
        List<DepartamentoConsultaDto> ListadoDepartamento(string Departamento);
        DepartamentoConsultaDto ConsultaDepartamento(DepartamentoConsultaDto objDepartamento);
        int MantenimientoDepartamento(Departamento objDepartamento);
        string EliminarDepartamento(string IdDepartamento,int Accion);
    }
}
