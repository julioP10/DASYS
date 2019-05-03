using Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IConfiguracion
    {
        List<ConfiguracionPaginationDto> PaginadoConfiguracion(PaginationParameter objPaginationParameter);
        List<ConfiguracionConsultaDto> ListadoConfiguracion(string Configuracion);
        ConfiguracionConsultaDto ConsultaConfiguracion(ConfiguracionConsultaDto objConfiguracion);
        int MantenimientoConfiguracion(Configuracion objConfiguracion);
        string EliminarConfiguracion(string IdConfiguracion,int Accion);
    }
}
