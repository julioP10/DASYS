using Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IEmpresa
    {
        List<EmpresaPaginationDto> PaginadoEmpresa(PaginationParameter objPaginationParameter);
        List<EmpresaDigitalizacionPaginationDto> PaginadoEmpresaDigitalizacion(PaginationParameter objPaginationParameter);
        List<DigitalizacionExcelDto> ListaEmpresaDigitalizacionExcel(string IdPersona);
        List<EmpresaConsultaDto> ListadoEmpresa(string Empresa);
        List<ParametrosCorreoDto> ListadoParametrosCorreo(ParametrosCorreoDto objParametrosCorreoDto);
        EmpresaConsultaDto ConsultaEmpresa(EmpresaConsultaDto objEmpresa);
        ParametrosCorreoDto ConsultaParametrosCorreo(ParametrosCorreoDto objParametrosCorreoDto);
        string MantenimientoEmpresa(Empresa objEmpresa);
        int MantenimientoParametrosCorreo(ParametrosCorreoDto objParametrosCorreoDto);
        string EliminarEmpresa(string IdEmpresa,int Accion);
    }
}
