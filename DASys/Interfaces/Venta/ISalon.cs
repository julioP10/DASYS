using Entidad;
using System.Collections.Generic;

namespace Interfaces
{
    public interface ISalon
    {
        List<SalonPaginationDto> PaginadoSalon(PaginationParameter objPaginationParameter);
        List<SalonConsultaDto> ListadoSalon(SalonConsultaDto objSalon);
        SalonConsultaDto ConsultaSalon(SalonConsultaDto objSalon);
        string MantenimientoSalon(Salon objSalon);
        string EliminarSalon(int IdSalon, int Accion);
    }
}
