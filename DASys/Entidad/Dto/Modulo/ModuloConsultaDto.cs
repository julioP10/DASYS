using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
    public class ModuloConsultaDto:UsuarioActualDto
    {

        public string IdModulo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string IdEstado { get; set; }
        public string Estado { get; set; }
        public string Icono { get; set; }
        public int Posicion { get; set; }
    }
}
