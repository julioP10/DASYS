using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
    public class Departamento:UsuarioActualDto
    {
        public string IdDepartamento { get; set; }
        public string Nombre { get; set; }
        public string Nivel { get; set; }
        public string IdPrincipal { get; set; }
        public string IdEstado { get; set; }
    }
}
