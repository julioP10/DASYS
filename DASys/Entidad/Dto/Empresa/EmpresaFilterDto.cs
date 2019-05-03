using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class EmpresaFilterDto:UsuarioActualDto
    {
        public string NombreSearch { get; set; }
        public int EsPrincipalSearch { get; set; }
        public int EsContratistaSearch { get; set; }
        public int EsSubContratistaSearch { get; set; }
        public string DigitalizacionSearch { get; set; }
    }
}
