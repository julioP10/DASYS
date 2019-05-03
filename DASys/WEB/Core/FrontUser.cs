using WEB.Models;
using System.Linq;
using Entidad;

namespace WEB
{
    public class FrontUser
    {
        public static bool TienePermiso(Opcion opcion, int accion)
        {
            var listaAcciones = SessionWebApp.SessionAcciones;
            return listaAcciones.Where(p => p.IdOpcion ==(int)opcion && p.IdAccion == accion).Any();
        }
    }
}