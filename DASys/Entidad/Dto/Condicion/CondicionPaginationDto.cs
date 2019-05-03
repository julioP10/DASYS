using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class CondicionPaginationDto
    {
        public string IdCondicion { get; set; }
        public string Nombre { get; set; }
        public string Regimen { get; set; }
        public string Estado { get; set; }
        public int Cantidad { get; set; }
    }
}
