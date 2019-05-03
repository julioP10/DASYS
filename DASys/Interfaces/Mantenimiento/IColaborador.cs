using Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IColaborador
    {
        List<ColaboradorPaginationDto> PaginadoColaborador(PaginationParameter objPaginationParameter, ColaboradorFilterDto colaboradorFilterDto);
        List<ColaboradorDigitalizacionPaginationDto> PaginadoColaboradorDigitalizacion(PaginationParameter objPaginationParameter);
        List<ColaboradorDigitalizacionPaginationDto> ListaColaboradorDigitalizacion(string IdPersona);
        List<DigitalizacionExcelDto> ListaColaboradorDigitalizacionExcel(string IdUsuario);
        List<ColaboradorTiposDto> ListadoColaboradorTelefono(string Colaborador);
        List<ColaboradorTiposDto> ListadoColaboradorCorreo(string Colaborador);
        List<ColaboradorTiposDto> ListadoColaboradorTarjeta(string Colaborador);
        List<Colaborador> ListadoColaborador(string Colaborador);
        ColaboradorConsultaDto ConsultaColaborador(ColaboradorConsultaDto objColaborador);
        string MantenimientoColaborador(Colaborador objColaborador);
        string EliminarColaborador(string IdColaborador, int Accion);

        string MantenimientoTarjetaColaborador(ColaboradorTiposDto objColaboradorTiposDto);
        string EliminarTarjetaColaborador(ColaboradorTiposDto objColaboradorTiposDto);
        string MantenimientoCorreoColaborador(ColaboradorTiposDto objColaboradorTiposDto);
        string EliminarCorreoColaborador(ColaboradorTiposDto objColaboradorTiposDto);
        string MantenimientoTelefonoColaborador(ColaboradorTiposDto objColaboradorTiposDto);
        string EliminarTelefonoColaborador(ColaboradorTiposDto objColaboradorTiposDto);
    }
}
