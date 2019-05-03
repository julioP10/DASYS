using Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IModulo
    {
        List<ModuloPaginationDto> PaginadoModulo(PaginationParameter objPaginationParameter);
        List<ModuloConsultaDto> ListadoModulo(UsuarioPermisoDto objUsuarioPermisoDto);
        List<ModuloConsultaDto> ListadoModuloXPerfil(UsuarioPermisoDto objUsuarioPermisoDto);
        ModuloConsultaDto ConsultaModulo(ModuloConsultaDto objModulo);
        int MantenimientoModulo(Modulo objModulo);
        string EliminarModulo(string IdModulo,int Accion);
    }
}
