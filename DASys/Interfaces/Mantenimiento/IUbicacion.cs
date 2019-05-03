using Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IUbicacion
    {
        List<UbicacionPaginationDto> PaginadoUbicacion(PaginationParameter objPaginationParameter);
        List<UbicacionConsultaDto> ListadoUbicacion(string Ubicacion);
        UbicacionConsultaDto ConsultaUbicacion(UbicacionConsultaDto objUbicacion);
        int MantenimientoUbicacion(Ubicacion objUbicacion);
        string EliminarUbicacion(string IdUbicacion,int Accion);
    }
}
