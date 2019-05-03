using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
    public class Constante
    {
        #region Tipo de Respuesta
        public const string Error = "error";
        public const string Success = "success";
        public const string Warning = "warning";
        public const string Danger = "danger";
        public const string Information = "information";
        public const string Question = "question";
        #endregion
        #region Label JsTree
        public const string JstreeUsuario = "Usuarios";
        public const string JstreeModulo = "Modulos";
        #endregion
        #region ruta de ico 
        public const string rutaIco = "../../../jsTree/Folder.ico";
        #endregion
        #region Mensajes
        public static string IntenteloMasTarde = "Hubo un error, inténtelo más tarde";
        public static string SeTerminoLaSession = "Se terminó la sesión";
        public static string err_Credenciales = "Credenciales incorrectas, por favor vuelva a intentar.";
        public static string sol_Credenciales = "Por favor ingrese credenciales";
        public static string error_permisos = "No tiene permisos para realizar esta operacion, comunicate con el el administador.";
        public static string registroExitoso = "Se registro correctamente";
        public static string registroError = "Hubo un error al registrar";
        public static string actualizacionExitoso = "Se actualizo correctamente";
        public static string actualizacionError = "Hubo un error al actualizar";
        public static string eliminacionExitoso = "Se Elimino correctamente";
        public static string eliminacionError = "Hubo un error al eliminar";

        #endregion

        #region KeyString
        public const string UsuarioSesionKey = "UsuarioSesionKey";
        public const string UsuarioSesion = ".Columbia.Session";
        public const string SesionCore = ".AspNetCore.Cookies";
        public const string ModuloSesion = "ModuloSesion";
        public const string JwtToken = "JwtToken";
        public const string OpcionSesion = "OpcionSesion";
        public const string OpcionHijoSesion = "OpcionHijoSesion";
        public const string AccionSesion = "AccionSesion";
        public const string ProspectoSesion = "ProspectoSesion";


        #endregion

        #region Prospecto
        public static readonly string[] Separador = { ",", ".", "!", "?", ";", ":", " " };
        #endregion
    }
}
