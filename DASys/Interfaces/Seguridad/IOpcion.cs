using Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IOpcion
    {
        List<OpcionPaginationDto> PaginadoOpcion(PaginationParameter objPaginationParameter);
        List<OpcionConsultaDto> ListadoOpcion(UsuarioPermisoDto objUsuarioPermisoDto);
        List<OpcionConsultaDto> ListadoOpcionXPerfil(UsuarioPermisoDto objUsuarioPermisoDto);
        List<OpcionConsultaDto> ListadoOpcionHijo(UsuarioPermisoDto objUsuarioPermisoDto);
        OpcionConsultaDto ConsultaOpcion(OpcionConsultaDto objOpcion);
        int MantenimientoOpcion(Opcion objOpcion);
        string EliminarOpcion(string IdOpcion, int Accion);
    }
}
