using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class UsuarioActualDto
    {
        public string IdUsuario { get; set; }
        public string IdEmpresa { get; set; }
        public string Perfil { get; set; }
        public string TipoEmpresa { get; set; }
        public string DescripcionAdcional { get; set; }
        public string IdEmpresaPrincipal { get; set; }
        public int Accion { get; set; }

    }
}
