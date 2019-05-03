//-- =============================================
//-- Author:  AYllus.com
//-- Create date: 22/01/19
//-- CREATE ENTIDAD  - 
//-- =============================================
namespace Entidad
{
    public class SalonConsultaDto : UsuarioActualDto
    {
        public int IdSalon { get; set; }
        public string Nombre { get; set; }
        public int Estado { get; set; }
        public int IdSucursal { get; set; }
        public string Sucursal { get; set; }
    }
}
