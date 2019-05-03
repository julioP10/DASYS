using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
   public class OpcionConsultaDto
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
        public string Estado { get; set; }
        public bool EsInicio { get; set; }
        public string IdPadre { get; set; }
        public int Posicion { get; set; }
        public string Icono { get; set; }
        public int SubMenu { get; set; }
        public int Permitido { get; set; }
    }
}
