using Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IModelo
    {
        List<ModeloPaginationDto> PaginadoModelo(PaginationParameter objPaginationParameter);
        List<ModeloConsultaDto> ListadoModelo(string Modelo);
        ModeloConsultaDto ConsultaModelo(ModeloConsultaDto objModelo);
        int MantenimientoModelo(Modelo objModelo);
        string EliminarModelo(string IdModelo,int Accion);
    }
}
