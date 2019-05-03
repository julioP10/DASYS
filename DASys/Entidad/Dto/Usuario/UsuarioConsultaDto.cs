using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
    public class UsuarioConsultaDto:UsuarioActualDto
    {

        public string IdUsuario { get; set; }
        public string IdPersona { get; set; }
        public string Usuarios { get; set; }
        public string Correo { get; set; }
        public string Foto { get; set; }
        public string IdTipo { get; set; }
        public string Tipo { get; set; }
        public string Password { get; set; }
        public string Estado { get; set; }
        public string IdEstado { get; set; }
        public string ModoAutenticacion { get; set; }
        public string Cargo { get; set; }
        public string IdCargo { get; set; }
        public string IdPerfil { get; set; }
        public string IdOpcion { get; set; }
        public List<ModuloConsultaDto> lstModulo { get; set; }
        public List<OpcionConsultaDto> lstOpcion { get; set; }
        public string IdEmpresaPadre { get; set; }
        public string EmpresaPertence { get; set; }
        public string EmpresaNombre { get; set; }
    }
}
