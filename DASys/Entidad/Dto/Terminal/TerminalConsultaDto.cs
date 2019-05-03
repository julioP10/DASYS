using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
    public class TerminalConsultaDto:UsuarioActualDto
    {
        public string IdTerminal { get; set; }
        public string Nombre { get; set; }
        public string IP { get; set; }
        public string Puerto { get; set; }
        public string Foto { get; set; }
        public string IdModelo { get; set; }
        public string Modelo { get; set; }
        public string IdMarca { get; set; }
        public string Marca { get; set; }
        public string IdConfiguracion { get; set; }
        public string Configuracion { get; set; }
        public string IdArea { get; set; }
        public string Area { get; set; }
        public string IdEstado { get; set; }
        public string Estado { get; set; }
    }
}
