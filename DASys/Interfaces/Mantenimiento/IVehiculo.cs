using Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IVehiculo
    {
        List<VehiculoPaginationDto> PaginadoVehiculo(PaginationParameter objPaginationParameter, VehiculoFilterDto VehiculoFilterDto);
        List<VehiculoDigitalizacionPaginationDto> PaginadoVehiculoDigitalizacion(PaginationParameter objPaginationParameter);
        List<VehiculoDigitalizacionPaginationDto> ListaVehiculoDigitalizacion(string IdPersona);
        List<DigitalizacionExcelDto> ListaVehiculoDigitalizacionExcel(string IdUsuario);
        List<Vehiculo> ListadoVehiculo(string Vehiculo);
        VehiculoConsultaDto ConsultaVehiculo(VehiculoConsultaDto objVehiculo);
        string MantenimientoVehiculo(Vehiculo objVehiculo);
        string EliminarVehiculo(string IdVehiculo, int Accion);
    }
}
