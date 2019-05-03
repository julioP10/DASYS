using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class DigitalizacionCorreoDto
    {
        public string Nombre {get;set;}
        public string Numero {get;set;}
        public string Empresa {get;set;}
        public int Enviado { get; set;}
        public string Categoria {get;set;}
        public string IdEmpresa {get;set;}
        public string IdEmpresaGeneral {get;set;}
        public string IdEmpresaPadre {get;set;}
        public string TipoEmpresa {get;set;}
        public string IdPersona { get; set; }
        public string EstadoDigitalizacion { get; set; }
    }
}
