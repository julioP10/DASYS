using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Entidad;
using Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB.Models;
using Utilis;
using Serilog;

namespace WEB.Controllers
{
    public class LoginController : BaseController
    {
        private readonly IUsuario _Usuario;
        private readonly IModulo _Modulo;
        private readonly IOpcion _Opcion;
        private readonly IEmpresa _Empresa;
        private readonly IServiceProvider _serviceProvider;
        public LoginController(IUsuario Usuario, IServiceProvider serviceProvider, IModulo Modulo, IOpcion Opcion,IEmpresa Empresa) : base(serviceProvider)
        {
            _Usuario = Usuario;
            _serviceProvider = serviceProvider;
            _Modulo = Modulo;
            _Opcion = Opcion;
            _Empresa = Empresa;
        }

        
        public IActionResult Index()
        {
            //Comprobar si hay registros de los principales en el sistema
            var ok = Utils.IniciarSistema();
            Log.Information("ok");
            if (string.IsNullOrWhiteSpace(GetUsuarioActual()))
            {
                var modelo = new UsuarioLoginDto
                {
                    Usuarios = "",
                    Password = ""
                };
                return View(modelo);
            }
            else
            {
                return Redirect("/Home");
            }
        }

        public IActionResult LogOut()
        {
            LimpiarSesion();
            return Redirect("/Login");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult UserLogin([FromForm] UsuarioConsultaDto objUsuarioConsultaDto)
        {
            objUsuarioConsultaDto.Password = Encriptador.Encriptar(objUsuarioConsultaDto.Password);
            if (string.IsNullOrWhiteSpace(objUsuarioConsultaDto.Password))
            {
                ViewBag.Error = "Ingrese Usuario o Contraseña";
                return View("Index");
            }
            HttpContext.Session.SetString("Usu",objUsuarioConsultaDto.Usuarios);
            HttpContext.Session.SetString("Pas", objUsuarioConsultaDto.Password);
            try
            {
                objUsuarioConsultaDto = _Usuario.UsuarioLogin(objUsuarioConsultaDto);
                if (string.IsNullOrEmpty(objUsuarioConsultaDto.Correo)) {
                    ViewBag.Error = "Usuario o Contraseña Incorrecto";
                    UsuarioLoginDto objUsuarioLoginDto = new UsuarioLoginDto();
                    objUsuarioLoginDto.Tipo = "Login";
                    return View("Index", objUsuarioLoginDto);
                }
     

                GenerateTicketAuthentication(objUsuarioConsultaDto);
                Log.Information("INICIO");
                HttpContext.Session.SetString("_TipoEmpresa", GetPerfil());
                // HttpContext.Session.SetString("_TipoEmpresa",objUsuarioConsultaDto.TipoEmpresa);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                ViewBag.Error = "Usuario o Contraseña Incorrecto";
                return View("Index");
            }

        }
        [HttpPost]
        public IActionResult RecuperarUsuario([FromForm]UsuarioConsultaDto objUsuarioConsultaDto)
        {
            if (string.IsNullOrWhiteSpace(objUsuarioConsultaDto.Usuarios))
            {
                ViewBag.Error = "Ingrese Usuario o correo";
                return View("Index");
            }
            try
            {
                objUsuarioConsultaDto = _Usuario.UsuarioRecuperar(objUsuarioConsultaDto);
                if (string.IsNullOrEmpty(objUsuarioConsultaDto.Correo))
                {
                
                    ViewBag.Error = "Usuario o correo no registrado";
                    return View("Index");
                }
                ParametrosCorreoDto parametrosCorreoDto = new ParametrosCorreoDto();
                parametrosCorreoDto.IdEmpresa = objUsuarioConsultaDto.IdEmpresa;
                parametrosCorreoDto = _Empresa.ConsultaParametrosCorreo(parametrosCorreoDto);
                if (string.IsNullOrEmpty(parametrosCorreoDto.Correo))
                {
                    ViewBag.Error = "Falta de configuracion en el emsior de correos, comuniquece con el administrador";
                    UsuarioLoginDto objUsuarioLoginDto = new UsuarioLoginDto();
                    objUsuarioLoginDto.Tipo = "Recuperar";
                    objUsuarioLoginDto.Password = "   ";
                    objUsuarioLoginDto.Usuarios = "   ";
                    return View("Index", objUsuarioLoginDto);
                }
                objUsuarioConsultaDto.Password = Encriptador.Desencriptar(objUsuarioConsultaDto.Password);
                EnviarCorreoUsuarioRecuperado(parametrosCorreoDto,objUsuarioConsultaDto);
                ViewBag.Success = "Se ha recuperado su usuario, revise su correo personal";
                UsuarioLoginDto objUsuarioLoginDtos = new UsuarioLoginDto();
                objUsuarioLoginDtos.Tipo = "Recuperar";
                objUsuarioLoginDtos.Password = "   ";
                objUsuarioLoginDtos.Usuarios = "   ";
                return View("Index", objUsuarioLoginDtos);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                ViewBag.Error = "Usuario o Contraseña Incorrecto";
                return View("Index");
            }


        }
        [HttpPost]
        public IActionResult RenovarLogin(UsuarioConsultaDto objUsuarioConsultaDto)
        {
            try
            {
                objUsuarioConsultaDto.Usuarios = HttpContext.Session.GetString("Usu");
                objUsuarioConsultaDto.Password = HttpContext.Session.GetString("Pas");
                objUsuarioConsultaDto = _Usuario.UsuarioLogin(objUsuarioConsultaDto);
                if (string.IsNullOrEmpty(objUsuarioConsultaDto.Correo))
                {
                    return Json(new { ok = false });
                }
                GenerateTicketAuthentication(objUsuarioConsultaDto);
                ViewBag.Menu = ModulosActuales();
                ViewBag.Opciones = OpcionesActuales();
                ViewBag.OpcionesHijo = OpcionesHijoActuales();
                ViewBag.UsuarioActual = UsuarioActual();
                return Json(new { ok = true });
            }
            catch (Exception ex)
            {

                return Json(new {ok=false });
            }

        }
        [HttpPost]
        public JsonResult VerifySession()
        {
            //var jsonResponse = new JsonResponseDto { Type = Constante.Success };

            //try
            //{
            //    if (HttpContext.Session.GetString(Constante.UsuarioSesionKey) == null)
            //    {
            //        jsonResponse.Type = Constante.Error;
            //        jsonResponse.Mensaje = Constante.SeTerminoLaSession;
            //        LimpiarSesion();
            //    }
            //}
            //catch (Exception)
            //{
            //    jsonResponse.Type = Constante.Error;
            //    jsonResponse.Mensaje = Constante.IntenteloMasTarde;
            //}

            return Json(new{ });
        }


        #region Metodos Privados
        private void GenerateTicketAuthentication(UsuarioConsultaDto  usuarioDto)

        {
            HttpContext.Session.SetObject(Constante.UsuarioSesionKey, usuarioDto);
            SessionWebApp.SesionUsuario = HttpContext.Session.GetObject<UsuarioDto>(Constante.UsuarioSesionKey);
            UsuarioPermisoDto objUsuarioPermisoDto = new UsuarioPermisoDto();
            objUsuarioPermisoDto.IdPerfil = usuarioDto.IdPerfil;
            objUsuarioPermisoDto.IdUsuario = usuarioDto.IdUsuario;
            MenuActual(objUsuarioPermisoDto);
            var claims = new List<Claim> { };
            claims.Add(new Claim(ClaimTypes.Name, usuarioDto.Usuarios));
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();
             HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        }

        public void MenuActual(UsuarioPermisoDto objUsuarioPermisoDto)
        {
            List<ModuloConsultaDto> lista = _Modulo.ListadoModuloXPerfil(objUsuarioPermisoDto);
            List<OpcionConsultaDto> lista_ = _Opcion.ListadoOpcionXPerfil(objUsuarioPermisoDto);
            List<OpcionConsultaDto> lista_hijo = _Opcion.ListadoOpcionHijo(objUsuarioPermisoDto);

            HttpContext.Session.SetObject(Constante.ModuloSesion, lista);
            HttpContext.Session.SetObject(Constante.OpcionSesion, lista_);
            HttpContext.Session.SetObject(Constante.OpcionHijoSesion, lista_hijo);
            SessionWebApp.SessionAcciones = HttpContext.Session.GetObject<List<UsuarioAccionDto>>(Constante.AccionSesion);

        }

        public void LimpiarSesion()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove(Constante.ModuloSesion);
            HttpContext.Session.Remove(Constante.UsuarioSesion);
            HttpContext.Session.Remove(Constante.UsuarioSesionKey);
            HttpContext.Session.Remove(Constante.ModuloSesion);
            HttpContext.Session.Remove(Constante.AccionSesion);
            HttpContext.Session.Remove(Constante.ProspectoSesion);
            HttpContext.Session.Remove(Constante.SesionCore);
            HttpContext.Session.Remove(Constante.JwtToken);
            //SessionWebApp.SesionUsuario = null;
            //SessionWebApp.SessionAcciones.Clear();
            //SessionWebApp.SesionJwt = null;

            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }

        }
        #endregion
    }
}