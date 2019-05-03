using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
    public  class Configuracion:UsuarioActualDto
    {
        public string IdConfiguracion { get; set; }
        public string Nombre { get; set; }
        public string TiempoColor { get; set; }
        public string TiempoEntreMarcaciones { get; set; }
        public string TiempoRELAY { get; set; }
        public string IdTipo { get; set; }
        public string IdEstado { get; set; }

    }
}
