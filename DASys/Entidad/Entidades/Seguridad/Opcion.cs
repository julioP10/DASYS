using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
    public class Opcion
    {
        public string IdOpcion { get; set; }
        public string IdModulo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Url { get; set; }
        public string Controlador { get; set; }
        public string Accion { get; set; }
        public string Area { get; set; }
        public string IdEstado { get; set; }
        public string Posicion { get; set; }
        public string Icono { get; set; }
        public bool EsInicio { get; set; }
    }
}
