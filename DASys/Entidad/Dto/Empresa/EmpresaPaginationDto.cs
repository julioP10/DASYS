using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
    public class EmpresaPaginationDto
    {
        public string IdEmpresa { get; set; }
        public string RazonSocial { get; set; }
        public string RUC { get; set; }
        public string DireccionFiscal { get; set; }
        public string Entidad { get; set; }
        public string Estado { get; set; }
        public int Cantidad { get; set; }
        public int EsPrincipal { get; set; }
        public int EsContratista { get; set; }
        public int EsSubContratista { get; set; }
        public string TipoEmpresa { get; set; }
        public string IdPadre { get; set; }
        public string IdPadreSubcontratista { get; set; }
        public string PadreSubcontratista { get; set; }
        public string Categoria { get; set; }
        public string Digitalizacion { get; set; }
    }
}
