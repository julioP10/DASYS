using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
    public class Terminal:UsuarioActualDto
    {
        public string IdTerminal { get; set; }
        public string Nombre { get; set; }
        public string IP { get; set; }
        public string Puerto { get; set; }
        public string Foto { get; set; }
        public string IdModelo { get; set; }
        public string IdMarca { get; set; }
        public string IdConfiguracion { get; set; }
        public string IdArea { get; set; }
        public string IdEstado { get; set; }
    }
}
