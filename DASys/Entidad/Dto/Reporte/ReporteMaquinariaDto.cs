using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class ReporteMaquinariaDto
    {
        public string IdMaquinaria { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string InicioContrato { get; set; }
        public string FinContrato { get; set; }
        public string Ubicacion { get; set; }
        public string Departamento { get; set; }
        public int Cantidad { get; set; }
        public string Entidad { get; set; }
        public string Categoria { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Empresa { get; set; }
        public string Digitalizacion { get; set; }
        public string PadreSubcontratista { get; set; }
        public string NumeroDocumento { get; set; }
    }
}
