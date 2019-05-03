using Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IMarca
    {
        List<MarcaPaginationDto> PaginadoMarca(PaginationParameter objPaginationParameter);
        List<MarcaConsultaDto> ListadoMarca(string Marca);
        MarcaConsultaDto ConsultaMarca(MarcaConsultaDto objMarca);
        string MantenimientoMarca(Marca objMarca);
        string EliminarMarca(string IdMarca,int Accion);
    }
}
