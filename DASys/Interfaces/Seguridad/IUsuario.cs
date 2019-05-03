using Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IUsuario
    {
        List<UsuarioPaginationDto> PaginadoUsuario(PaginationParameter objPaginationParameter);
        List<UsuarioConsultaDto> ListadoUsuario(UsuarioActualDto objUsuarioActualDto);
        UsuarioConsultaDto ConsultaUsuario(UsuarioConsultaDto objUsuario);
        UsuarioConsultaDto UsuarioLogin(UsuarioConsultaDto objUsuario);
        UsuarioConsultaDto UsuarioRecuperar(UsuarioConsultaDto objUsuario);
        string MantenimientoUsuario(Usuario objUsuario);
        int PermisoUsuario(Usuario objUsuario);
        string EliminarUsuario(string IdUsuario,int Accion);
    }
}
