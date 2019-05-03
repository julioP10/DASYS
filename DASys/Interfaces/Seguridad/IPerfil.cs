using Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IPerfil
    {
        List<PerfilPaginationDto> PaginadoPerfil(PaginationParameter objPaginationParameter);
        List<PerfilConsultaDto> ListadoPerfil(string Perfil);
        PerfilConsultaDto ConsultaPerfil(PerfilConsultaDto objPerfil);
        int MantenimientoPerfil(Perfil objPerfil);
        string EliminarPerfil(string IdPerfil, int Accion);
    }
}
