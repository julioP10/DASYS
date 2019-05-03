using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
    public  class JsonResponseDto
    {
        //True/False => Validar accion
        public bool IsValid { get; set; }

        //Texto de la accion
        public string Mensaje { get; set; }

        //Tipos => success,error,danger,warning,question,information
        public string Type { get; set; }

        //Objecto para complementar la respuesta
        public object data { get; set; }
    }
}
