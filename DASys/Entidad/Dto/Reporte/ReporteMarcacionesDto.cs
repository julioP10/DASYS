using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class ReporteMarcacionesDto
    {
        public string Fecha { get; set; }
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Numero { get; set; }
        public string Terminal { get; set; }
        public string IdTerminal { get; set; }
        public string Empresa { get; set; }
        public string IdEmpresa { get; set; }
        public string Condicion { get; set; }
        public string Marcacion { get; set; }
        public string TipoMarcacion { get; set; }
        public string Estado { get; set; }
        public int Cantidad { get; set; }
        public string Categoria { get; set; }
        public string Departamento { get; set; }
        public string Ubicacion { get; set; }
        public string Genero { get; set; }
    }
}
