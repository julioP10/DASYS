using Datos;
using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilis
{
    public class Utils
    {
        public static int IniciarSistema()
        {
            return UtilsDAL.IniciarSistema();
        }
        public static List<DropDownDto> ListaArea(string Consulta, string Empresa)
        {
            return UtilsDAL.ListaArea(Consulta,Empresa);
        }
        public static List<DropDownDto> ListaCategoria(string Consulta, string empresa) {
            return UtilsDAL.ListaCategoria(Consulta,empresa);
        }
        public static List<DropDownDto> ListaConfiguracion(string Consulta) {
            return UtilsDAL.ListaConfiguracion(Consulta);
        }
        public static List<DropDownDto> ListaEntidad(string Consulta) {
            return UtilsDAL.ListaEntidad(Consulta);
        }
        public static List<DropDownDto> ListaMarcaEntidad(string Consulta)
        {
            return UtilsDAL.ListaMarcaEntidad(Consulta);
        }
        public static List<DropDownDto> ListaEmpresa(string Consulta) {
            return UtilsDAL.ListaEmpresa(Consulta);
        }
        public static List<DropDownDto> ListaEmpresaColaborador(string Consulta,string empresa)
        {
            return UtilsDAL.ListaEmpresaColaborador(Consulta,empresa);
        }
        
        public static List<DropDownDto> ListaEmpresaSuperUsuario(string Consulta)
        {
            return UtilsDAL.ListaEmpresaSuperUsuario(Consulta);
        }

        
        public static List<DropDownDto> ListaEmpresaUsuario(string Consulta, string _empresa)
        {
            return UtilsDAL.ListaEmpresaUsuario(Consulta, _empresa);
        }
        public static List<DropDownDto> ListaEmpresaXempresa(string Consulta)
        {
            return UtilsDAL.ListaEmpresaXempresa(Consulta);
        }
        public static List<DropDownDto> ListaEstado(string Consulta) {
            return UtilsDAL.ListaEstado("");
        }
        public static List<DropDownDto> ListaMarca(string codigo, string entidad) {
            return UtilsDAL.ListaMarca(codigo, entidad);

        }
        public static List<DropDownDto> ListaConfiguiracion(string codigo, string entidad)
        {
            return UtilsDAL.ListaConfiguiracion(codigo, entidad);

        }


        public static List<DropDownDto> ListaModelo(string Consulta, string IdEmpresa) {
            return UtilsDAL.ListaModelo(Consulta, IdEmpresa);
        }
        public static List<DropDownDto> ListaOperador(string Consulta) {
            return UtilsDAL.ListaOperador("");
        }
        public static List<DropDownDto> ListaTipo(string Consulta) {
            return UtilsDAL.ListaTipo(Consulta);
        }
        public static List<DropDownDto> ListaTerminal(string Consulta) {
            return UtilsDAL.ListaTerminal("");
        }
        public static List<DropDownDto> ListaUbicacion(string Consulta) {
            return UtilsDAL.ListaUbicacion(Consulta);
        }
        public static List<DropDownDto> ListaUDepartamento(string Consulta) {
            return UtilsDAL.ListaUDepartamento("");
        }
        public static List<DropDownDto> ListaDepartamento(string Consulta) {
            return UtilsDAL.ListaDepartamento(Consulta);
        }
        public static List<DropDownDto> ListaEmail(string Consulta) {
            return UtilsDAL.ListaEmail("");
        }
        public static List<DropDownDto> ListaRegimen(string Consulta)
        {
            return UtilsDAL.ListaRegimen(Consulta);
        }
        public static List<DropDownDto> ListaCondicion(string Consulta)
        {
            return UtilsDAL.ListaCondicion(Consulta);
        }
        public static List<DropDownDto> ListaProvincia(string Consulta)
        {
            return UtilsDAL.ListaProvincia(Consulta);
        }
        public static List<DropDownDto> ListaDistrito(string Consulta, string Consulta2)
        {
            return UtilsDAL.ListaDistrito(Consulta,Consulta2);
        }
        public static List<DropDownDto> ListaModulo(string Consulta)
        {
            return UtilsDAL.ListaModulo(Consulta);
        }
        public static List<DropDownDto> ListaPerfil(string Consulta)
        {
            return UtilsDAL.ListaPerfil(Consulta);
        }
        public static List<DropDownDto> ListaTipoLugar(string Consulta)
        {
            return UtilsDAL.ListaTipoLugar(Consulta);
        }
        public static List<DropDownDto> ListaProveedor(string Consulta)
        {
            return UtilsDAL.ListaProveedor(Consulta);
        }
        
    }
}
