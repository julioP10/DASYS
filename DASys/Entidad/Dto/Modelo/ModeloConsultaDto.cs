using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
    public class ModeloConsultaDto:UsuarioActualDto
    {
        public string IdModelo { get; set; }
        public string Nombre { get; set; }
        public int SDK { get; set; }
        public string IdMarca { get; set; }
        public string Marca { get; set; }
        public string IdConfiguracion { get; set; }
        public string Configuracion { get; set; }
        public string IdEstado { get; set; }
        public string Estado { get; set; }
    }
}
