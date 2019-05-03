using Entidad;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IProveedor
    {
        List<ProveedorPaginationDto> PaginadoProveedor(PaginationParameter objPaginationParameter);
        List<Proveedor> ListadoProveedor(string Proveedor);
        ProveedorConsultaDto ConsultaProveedor(ProveedorConsultaDto objProveedor);
        int MantenimientoProveedor(Proveedor objProveedor);
        string EliminarProveedor(string IdProveedor, int Accion);
    }
}
