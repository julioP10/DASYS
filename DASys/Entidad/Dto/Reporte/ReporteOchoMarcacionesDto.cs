using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class ReporteOchoMarcacionesDto
    {
        public int Marcaciones {get;set;}
        public string Id {get;set;}
        public string Nombre {get;set;}
        public string Numero {get;set;}
        public string Terminal {get;set;}
        public string Empresa {get;set;}
        public string Fecha {get;set;}
        public string PrimeraMarcacion {get;set;}
        public string SegundaMarcacion {get;set;}
        public string TerceraMarcacion {get;set;}
        public string CuartaMarcacion {get;set;}
        public string QuintaMarcacion {get;set;}
        public string SextaMarcacion {get;set;}
        public string SeptimaMarcacion {get;set;}
        public string OctavaMarcacion {get;set;}
        public string Departamento { get; set; }
        public string Genero { get; set; }
        public string Ubicacion { get; set; }
        public string Categoria { get; set; }
        public int Cantidad { get; set; }
    }
}
