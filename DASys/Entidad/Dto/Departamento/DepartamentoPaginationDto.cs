using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
    public class DepartamentoPaginationDto
    {
        public string IdDepartamento { get; set; }
        public string Nombre { get; set; }
        public int Nivel { get; set; }
        public int Cantidad { get; set; }
        public string Estado { get; set; }
    }
}
