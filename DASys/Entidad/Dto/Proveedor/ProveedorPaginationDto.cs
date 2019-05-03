using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class ProveedorPaginationDto
    {
        public string IdProveedor { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public string Empresa { get; set; }
        public string IdEstado { get; set; }
        public int Cantidad { get; set; }
    }
}
