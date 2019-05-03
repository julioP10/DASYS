using Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IPerfilOpcion
    {
        List<PerfilOpcionPaginationDto> PaginadoPerfilOpcion(PaginationParameter objPaginationParameter);
        List<PerfilOpcionConsultaDto> ListadoPerfilOpcion(string IdPerfil, string IdOpcion);
        PerfilOpcionConsultaDto ConsultaPerfilOpcion(PerfilOpcionConsultaDto objPerfilOpcion);
        int MantenimientoPerfilOpcion(PerfilOpcion objPerfilOpcion);
        int EliminarPerfilOpcion(string IdPerfil, string IdOpcion);
    }
}
