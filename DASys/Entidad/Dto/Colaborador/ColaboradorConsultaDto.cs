using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class ColaboradorConsultaDto:UsuarioActualDto
    {
        public string IdColaborador { get; set; }
        public string IdTipoLugar { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Sexo { get; set; }
        public string Direccion { get; set; }
        public string NumeroContrado { get; set; }
        public string NumeroDocumento { get; set; }
        public string FechaNacimiento { get; set; }
        public string FechaContrato { get; set; }
        public string Ubicacion { get; set; }
        public string IdUbicacion { get; set; }
        public string Departamento { get; set; }
        public string IdDepartamento { get; set; }
        public string IdCategoria { get; set; }
        public string IdCondicion { get; set; }
        public string IdRegimen { get; set; }
        public string IdDistrito { get; set; }
        public string IdProvincia { get; set; }
        public string IdUDepartamento { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string TipoTelefono { get; set; }
        public string TipoCorreo { get; set; }
        public string Tarjeta { get; set; }
        public string IdEstado { get; set; }
        public string Alias { get; set; }
        public string IdArea { get; set; }
        public string IdPersona { get; set; }
        public string Descripcion { get; set; }
        public string IdEmpresaPadre { get; set; }
        public string Foto { get; set; }
    }
}
