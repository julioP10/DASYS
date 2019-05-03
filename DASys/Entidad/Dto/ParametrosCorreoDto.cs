using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class ParametrosCorreoDto
    {
        public string IdParametros { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public string IdEmpresa { get; set; }
        public string IdEmpresaPadre { get; set; }
        public string Empresa { get; set; }
        public string Ruc { get; set; }
    }
}
