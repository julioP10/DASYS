using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
   public  class EmpresaConsultaDto:UsuarioActualDto
    {
        public string RazonSocial { get; set; }
        public string RUC { get; set; }
        public string DireccionFiscal { get; set; }
        public string IdPadre { get; set; }
        public string IdEntidad { get; set; }
        public string Entidad { get; set; }
        public string IdEstado { get; set; }
        public string IdCategoria { get; set; }
        public string Estado { get; set; }
        public int EsPrincipal { get; set; }
        public int EsContratista { get; set; }
        public int EsSubContratista{ get; set; }
        public string Mensaje { get; set; }
        public int Digitalizado { get; set; }
        public int Enviado { get; set; }
        public string IdPersona { get; set; }
        public string Tipo { get; set; }
    }
}
