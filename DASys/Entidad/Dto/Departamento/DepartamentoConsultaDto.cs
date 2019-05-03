using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
    public class DepartamentoConsultaDto:UsuarioActualDto
    {
        public string IdDepartamento { get; set; }
        public string Nombre { get; set; }
        public int Nivel { get; set; }
        public string Estado { get; set; }
        public string IdEstado { get; set; }
    }
}
