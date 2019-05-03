using Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
   public interface IMaquinaria
    {
        List<MaquinariaPaginationDto> PaginadoMaquinaria(PaginationParameter objPaginationParameter, MaquinariaFilterDto MaquinariaFilterDto);
        List<MaquinariaDigitalizacionPaginationDto> PaginadoMaquinariaDigitalizacion(PaginationParameter objPaginationParameter);
        List<MaquinariaDigitalizacionPaginationDto> ListaMaquinariaDigitalizacion(string IdPersona);
        List<DigitalizacionExcelDto> ListaMaquinariaDigitalizacionExcel(string IdUsuario);
        List<Maquinaria> ListadoMaquinaria(string Maquinaria);
        MaquinariaConsultaDto ConsultaMaquinaria(MaquinariaConsultaDto objMaquinaria);
        string MantenimientoMaquinaria(Maquinaria objMaquinaria);
        string EliminarMaquinaria(string IdMaquinaria, int Accion);
    }
}
