using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
    public class Vehiculo : UsuarioActualDto
    {
        public string IdVehiculo { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Documento { get; set; }
        public string InicioContrato { get; set; }
        public string FinContrato { get; set; }
        public string Observacion { get; set; }
        public string Tipo { get; set; }
        public string IdTipo { get; set; }
        public string Modelo { get; set; }
        public string IdModelo { get; set; }
        public string Marca { get; set; }
        public string IdMarca { get; set; }
        public string Proveedor { get; set; }
        public string IdProveedor { get; set; }
        public string Estado { get; set; }
        public string IdEstado { get; set; }
        public string IdPersona { get; set; }
        public string Categoria { get; set; }
        public string IdCategoria { get; set; }
        public int Enviado { get; set; }
        public int Digitalizado { get; set; }
        public object Mensaje { get; set; }
    }
}
