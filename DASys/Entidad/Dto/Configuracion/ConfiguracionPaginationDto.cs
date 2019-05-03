using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
    public class ConfiguracionPaginationDto
    {
        public string IdConfiguracion { get; set; }
        public string Nombre { get; set; }
        public byte TiempoColor { get; set; }
        public string TiempoEntreMarcaciones { get; set; }
        public byte TiempoRELAY { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public int Cantidad { get; set; }
    }
}
