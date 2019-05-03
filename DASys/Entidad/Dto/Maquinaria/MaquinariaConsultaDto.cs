using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
   public class MaquinariaConsultaDto : UsuarioActualDto
    {
        public string IdMaquinaria { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Documento { get; set; }
        public string InicioContrato { get; set; }
        public string FinContrato { get; set; }
        public string Observacion { get; set; }
        public string Tipo { get; set; }
        public string IdTipo { get; set; }
        public string Modelo { get; set; }
        public string IdModelo { get; set; }
        public string Marca { get; set; }
        public string IdMarca { get; set; }
        public string Proveedor { get; set; }
        public string IdProveedor { get; set; }
        public string Estado { get; set; }
        public string IdEstado { get; set; }
        public string IdPersona { get; set; }
        public string Categoria { get; set; }
        public string IdCategoria { get; set; }
    }
}
