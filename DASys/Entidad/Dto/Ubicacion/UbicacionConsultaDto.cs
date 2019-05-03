using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
    public class UbicacionConsultaDto:UsuarioActualDto
    {
        public string IdUbicacion { get; set; }
        public string Nombre { get; set; }
        public int Nivel { get; set; }
        public string IdPrincipal { get; set; }
        public string IdDepartamento { get; set; }
        public string Departamento { get; set; }
        public string IdEstado { get; set; }
    }
}
