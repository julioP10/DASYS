using Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IUsuarioPerfil
    {
        List<UsuarioPerfilPaginationDto> PaginadoUsuarioPerfil(PaginationParameter objPaginationParameter);
        List<UsuarioPerfilConsultaDto> ListadoUsuarioPerfil(string IdUsuario, string IdPerfil);
        UsuarioPerfilConsultaDto ConsultaUsuarioPerfil(UsuarioPerfilConsultaDto objUsuarioPerfil);
        int MantenimientoUsuarioPerfil(UsuarioPerfil objUsuarioPerfil);
        int EliminarUsuarioPerfil(string IdUsuario, string IdPerfil);
    }
}
