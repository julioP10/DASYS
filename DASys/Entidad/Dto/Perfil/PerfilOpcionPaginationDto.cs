using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
    public class PerfilOpcionPaginationDto
    {
        public string IdPerfil { get; set; }
        public string IdOpcion { get; set; }
        public string IdEstado { get; set; }
        public string Ejecutar { get; set; }
        public string Consultar { get; set; }
        public string Registrar { get; set; }
        public string Actualizar { get; set; }
        public string Eliminar { get; set; }
        public string Imprimir { get; set; }
    }
}
