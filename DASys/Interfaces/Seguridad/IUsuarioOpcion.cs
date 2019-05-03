using Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IUsuarioOpcion
    {
        List<UsuarioOpcionPaginationDto> PaginadoUsuarioOpcion(PaginationParameter objPaginationParameter);
        List<UsuarioOpcionConsultaDto> ListadoUsuarioOpcion(string IdUsuario, string IdOpcion);
        UsuarioOpcionConsultaDto ConsultaUsuarioOpcion(UsuarioOpcionConsultaDto objUsuarioOpcion);
        int MantenimientoUsuarioOpcion(UsuarioOpcion objUsuarioOpcion);
        int EliminarUsuarioOpcion(string IdUsuario, string IdOpcion);
    }
}
