using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class VehiculoFilterDto
    {
        //filtros
        public string Nombre { get; set; }
        public string Digitalizacion { get; set; }
        public string IdEmpresa { get; set; }
        public string IdPadre { get; set; }
        public string IdPadreSubcontratista { get; set; }
        public string DigitalizacionSearch { get; set; }
        public string NombreSearch { get; set; }
    }
}
