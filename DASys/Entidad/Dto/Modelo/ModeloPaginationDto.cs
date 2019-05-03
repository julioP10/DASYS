using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
    public class ModeloPaginationDto
    {
        public string IdModelo { get; set; }
        public string Nombre { get; set; }
        public int SDK { get; set; }
        public string Marca { get; set; }
        public string Configuracion { get; set; }
        public string Estado { get; set; }
        public int Cantidad { get; set; }
    }
}
