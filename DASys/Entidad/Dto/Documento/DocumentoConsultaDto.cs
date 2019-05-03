using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
    public class DocumentoConsultaDto:UsuarioActualDto
    {
        public string IdDocumento { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool FechaVencimiento { get; set; }
        public bool Obligatorio { get; set; }
        public string IdCategoria { get; set; }
        public string Categoria { get; set; }
        public string IdTipo { get; set; }
        public string Tipo { get; set; }
        public string IdEstado { get; set; }
        public string Estado { get; set; }
        public string IdEntidad { get; set; }
    }
}
