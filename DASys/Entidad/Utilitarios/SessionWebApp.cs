
using System.Collections.Generic;

namespace Entidad
{
    public static class SessionWebApp
    {
        public static IList<ModuloDto> SeccionModulo{get; set; }
        public static IList<UsuarioOpcionDto> SessionOpciones { get; set; }
        public static IList<UsuarioAccionDto> SessionAcciones { get; set; }
        public static UsuarioDto SesionUsuario { get; set; }
        public static JwtTokenResponseDto SesionJwt { get; set; }
    }
}
