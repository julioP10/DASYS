using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
    public class Empresa
    {
        public string IdEmpresa { get; set; }
        public string RazonSocial { get; set; }
        public string RUC { get; set; }
        public string DireccionFiscal { get; set; }
        public string IdPadre { get; set; }
        public string IdEntidad { get; set; }
        public string IdEstado { get; set; }
        public string IdCategoria{ get; set; }
        public int EsPrincipal { get; set; }
        public int EsContratista { get; set; }
        public int EsSubContratista { get; set; }
    }
}
