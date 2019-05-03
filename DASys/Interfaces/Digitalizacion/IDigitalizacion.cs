using Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IDigitalizacion
    {
        string MantenimientoDigitalizacion(Digitalizacion objDigitalizacion);
        int EliminarDigitalizacion(string objDigitalizacion);
        List<DigitalizacionCorreoDto> ListaDigtalizados(UsuarioActualDto objUsuarioActualDto);
    }
}
