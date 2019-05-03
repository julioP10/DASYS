using Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface ICategoria
    {
        List<CategoriaPaginationDto> PaginadoCategoria(PaginationParameter objPaginationParameter);
        List<Categoria> ListadoCategoria(string Categoria);
        CategoriaConsultaDto ConsultaCategoria(CategoriaConsultaDto objCategoria);
        int MantenimientoCategoria(Categoria objCategoria);
        string EliminarCategoria(string IdCategoria, int Accion);
    }
}
