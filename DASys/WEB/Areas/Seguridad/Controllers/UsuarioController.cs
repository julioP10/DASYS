using System;
using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WEB.Controllers;
using Serilog;

namespace WEB.Areas.Seguridad.Controllers
{
    [Area("Seguridad")]
    public class UsuarioController : BaseController
    {
        private readonly IUsuario _Usuario;
        private readonly IModulo _Modulo;
        private readonly IOpcion _Opcion;
        private readonly IEmpresa _Empresa;
        public UsuarioController(IServiceProvider serviceProvider, IUsuario Usuario, IModulo Modulo, IOpcion Opcion, IEmpresa Empresa) : base(serviceProvider)
        {
            _Usuario = Usuario;
            _Modulo = Modulo;
            _Opcion = Opcion;
            _Empresa = Empresa;
        }
        #region Metodos publicos
        #region Vistas
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Permiso([FromForm] UsuarioPermisoDto objUsuarioPermisoDto)
        {
            UsuarioConsultaDto objUsuarioConsultaDto = new UsuarioConsultaDto();
            objUsuarioPermisoDto.Ver = 1;
            objUsuarioConsultaDto.lstModulo = _Modulo.ListadoModuloXPerfil(objUsuarioPermisoDto).ToList();
            objUsuarioConsultaDto.lstOpcion = _Opcion.ListadoOpcionXPerfil(objUsuarioPermisoDto).ToList();

            return View(objUsuarioConsultaDto);
        }
        public IActionResult Registrar()
        {
            UsuarioConsultaDto objUsuarioConsultaDto = new UsuarioConsultaDto();
            objUsuarioConsultaDto.IdEmpresaPadre = GetEmpresaPadre();
            return PartialView("Registrar", objUsuarioConsultaDto);
        }


        [HttpPost]
        public IActionResult Eliminar([FromForm] UsuarioConsultaDto objUsuario)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                var result = _Usuario.EliminarUsuario(objUsuario.IdUsuario, objUsuario.Accion);
                var mensaje = result.Split(":");
                jsonResponseDto.Type = mensaje[0];
                jsonResponseDto.IsValid = true;
                jsonResponseDto.Mensaje = mensaje[1];
            }
            catch (Exception ex)
            {
                //Log.Error(ex.Message);
                jsonResponseDto.Type = Constante.Warning.ToLower();
                jsonResponseDto.IsValid = false;
                jsonResponseDto.Mensaje = ex.Message.ToString();
            }
            return Json(jsonResponseDto);
        }

        [HttpPost]
        public IActionResult Actualizar([FromForm] UsuarioConsultaDto objUsuario)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {

                objUsuario = _Usuario.ConsultaUsuario(objUsuario);
                if (objUsuario != null)
                {
                    return PartialView("Actualizar", objUsuario);
                }
                else
                {
                    //Log.Error(response.Content.ToString());
                    jsonResponseDto.Type = Constante.Warning.ToLower();
                    jsonResponseDto.IsValid = false;
                    jsonResponseDto.Mensaje = "Error en al consulta";
                }
            }
            catch (Exception ex)
            {
                //Log.Error(ex.Message);
                jsonResponseDto.Type = Constante.Warning.ToLower();
                jsonResponseDto.IsValid = false;
                jsonResponseDto.Mensaje = ex.Message.ToString();
            }
            return Json(jsonResponseDto);
        }
        [HttpPost]
        public IActionResult Consultar([FromForm] UsuarioConsultaDto objUsuario)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {

                objUsuario = _Usuario.ConsultaUsuario(objUsuario);
                if (objUsuario != null)
                {
                    return PartialView("Consultar", objUsuario);
                }
                else
                {
                    //Log.Error(response.Content.ToString());
                    jsonResponseDto.Type = Constante.Warning.ToLower();
                    jsonResponseDto.IsValid = false;
                    jsonResponseDto.Mensaje = "Error en al consulta";
                }
            }
            catch (Exception ex)
            {
                //Log.Error(ex.Message);
                jsonResponseDto.Type = Constante.Warning.ToLower();
                jsonResponseDto.IsValid = false;
                jsonResponseDto.Mensaje = ex.Message.ToString();
            }
            return Json(jsonResponseDto);
        }
        [HttpPost]
        public IActionResult Perfil()
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                UsuarioConsultaDto objUsuario = new UsuarioConsultaDto();
                objUsuario.IdUsuario = GetUsuarioActual();
                objUsuario = _Usuario.ConsultaUsuario(objUsuario);
                if (objUsuario != null)
                {
                    return PartialView("Perfil", objUsuario);
                }
                else
                {
                    //Log.Error(response.Content.ToString());
                    jsonResponseDto.Type = Constante.Warning.ToLower();
                    jsonResponseDto.IsValid = false;
                    jsonResponseDto.Mensaje = "Error en la consulta";
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                jsonResponseDto.Type = Constante.Warning.ToLower();
                jsonResponseDto.IsValid = false;
                jsonResponseDto.Mensaje = ex.Message.ToString();
            }
            return Json(jsonResponseDto);
        }
        #endregion
        [HttpPost]
        public IActionResult Mantenimiento([FromForm] Usuario objUsuario)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                //VERIFICAR SI EXISTE CORREO PARAMETRIZADO

                ParametrosCorreoDto parametrosCorreoDto = new ParametrosCorreoDto();
                parametrosCorreoDto.IdEmpresa = (GetEmpresaPadre() == "" || GetEmpresaPadre() == null) ? objUsuario.IdEmpresa : GetEmpresaPadre();

                if (string.IsNullOrWhiteSpace(objUsuario.IdUsuario))
                {
                    parametrosCorreoDto = _Empresa.ConsultaParametrosCorreo(parametrosCorreoDto);
                    if (string.IsNullOrWhiteSpace(parametrosCorreoDto.Correo))
                    {
                        jsonResponseDto.Type = Constante.Error.ToLower();
                        jsonResponseDto.IsValid = false;
                        jsonResponseDto.Mensaje = "Falta de configuracion en el emsior de correos, comuniquece con el administrador";
                        return Json(jsonResponseDto);
                    }
                    try
                    {
                        EnviarCorreoUsuarioCreado(parametrosCorreoDto, objUsuario);
                    }
                    catch (Exception ex)
                    {
                        jsonResponseDto.Type = Constante.Error.ToLower();
                        jsonResponseDto.IsValid = false;
                        if (ex.HResult == -2146233088)
                        {
                            jsonResponseDto.Mensaje = "Correo o Contraseña del emisor es incorrecta";
                        }
                        else
                        {
                            jsonResponseDto.Mensaje = "Error al enviar correo";
                        }
                        Log.Error("Envio de correo" + "\n" + ex.Message + "\n" + ex.StackTrace);
                        return Json(jsonResponseDto);
                    }
                }
                if (GetPerfil() != "SUPERUSUARIO")
                {

                    if (string.IsNullOrWhiteSpace(objUsuario.IdUsuario))
                    {
                        if (string.IsNullOrWhiteSpace(parametrosCorreoDto.Correo))
                        {
                            jsonResponseDto.Type = Constante.Error.ToLower();
                            jsonResponseDto.IsValid = false;
                            jsonResponseDto.Mensaje = "Falta de configuracion en el emsior de correos, comuniquece con el administrador";
                            return Json(jsonResponseDto);
                        }
                    }

                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(objUsuario.IdUsuario))
                    {
                        //objUsuario.IdEmpresa = null;
                        //objUsuario.IdPerfil = null;
                    }
                }
                if (!string.IsNullOrWhiteSpace(objUsuario.Password))
                {
                    objUsuario.Password = Encriptador.Encriptar(objUsuario.Password);
                }
                else
                {
                    objUsuario.Password = "";
                }
                var tipo = objUsuario.IdUsuario;
                var message = (objUsuario.IdUsuario == null) ? Constante.registroExitoso : Constante.actualizacionExitoso;
                objUsuario.IdUsuario = (objUsuario.IdUsuario == null) ? "" : objUsuario.IdUsuario;
                var response = _Usuario.MantenimientoUsuario(objUsuario);
                if (response == "-2")
                {
                    jsonResponseDto.Type = Constante.Error.ToLower();
                    jsonResponseDto.IsValid = false;
                    jsonResponseDto.Mensaje = Constante.registroError;
                }
                else if (response == "100")
                {
                    jsonResponseDto.Type = Constante.Error.ToLower();
                    jsonResponseDto.IsValid = false;
                    jsonResponseDto.Mensaje = "Elija otro nombre de usuario";
                }
                else if (response == "200")
                {
                    jsonResponseDto.Type = Constante.Error.ToLower();
                    jsonResponseDto.IsValid = false;
                    jsonResponseDto.Mensaje = "Elija otro correo";
                }
                else if (Convert.ToInt32(response) > 0)
                {
                    //if (tipo==null)
                    //{
                    //    objUsuario.Password = Encriptador.Desencriptar(objUsuario.Password);
                    //    //EmailTest s = new EmailTest();
                    //    //string[] correo = objUsuario.Correo.Split('@');
                    //    //bool ok;
                    //    //if (correo[1] == "gmail.com")
                    //    //    ok = s.ExisteCorreo(objUsuario.Correo);
                    //    //else
                    //    //    ok = true;

                    //    //if (ok)
                    //    //{
                    //        EnviarCorreoUsuarioCreado(parametrosCorreoDto, objUsuario);
                    //    //}
                    //}
                    jsonResponseDto.Type = Constante.Success.ToLower();
                    jsonResponseDto.IsValid = true;
                    jsonResponseDto.Mensaje = Constante.registroExitoso;
                }
                else
                {
                    // Log.Error(response.Content.ToString());
                    jsonResponseDto.Type = Constante.Warning.ToLower();
                    jsonResponseDto.IsValid = false;
                    jsonResponseDto.Mensaje = Constante.registroError;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                jsonResponseDto.Type = Constante.Warning.ToLower();
                jsonResponseDto.IsValid = false;
                jsonResponseDto.Mensaje = ex.Message.ToString();
            }
            return Json(jsonResponseDto);
        }
        [HttpPost]
        public IActionResult Listar(DataTableModel<UsuarioFilterDto> dataTableModel)
        {
            var jsonResponse = new JsonResponseDto() { Type = Constante.Error };

            try
            {
                FormatDataTable(dataTableModel);
                var jsonResponseDto = new JsonResponseDto() { Type = Constante.Error };

                var paginationParameter = new PaginationParameter
                {
                    Start = dataTableModel.start,
                    AmountRows = dataTableModel.length,
                    WhereFilter = dataTableModel.whereFilter,
                    OrderBy = dataTableModel.orderBy
                };

                List<UsuarioPaginationDto> lstCampania = _Usuario.PaginadoUsuario(paginationParameter);
                dataTableModel.data = lstCampania;
                if (lstCampania.Count > 0)
                {
                    dataTableModel.recordsTotal = lstCampania[0].Cantidad;
                    dataTableModel.recordsFiltered = dataTableModel.recordsTotal;
                }
            }


            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
                jsonResponse.Mensaje = ex.Message;
            }
            return Json(dataTableModel);
        }
        [HttpPost]
        public IActionResult PermisoPerfil([FromForm] Usuario objUsuario)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {

                var ok = _Usuario.PermisoUsuario(objUsuario);

                if (objUsuario.Check == 0)
                {
                    jsonResponseDto.Type = Constante.Success.ToLower();
                    jsonResponseDto.IsValid = true;
                    jsonResponseDto.Mensaje = Constante.eliminacionExitoso;
                }
                else
                {
                    //Log.Error(response.Content.ToString());
                    jsonResponseDto.Type = Constante.Success.ToLower();
                    jsonResponseDto.IsValid = true;
                    jsonResponseDto.Mensaje = Constante.registroExitoso;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                jsonResponseDto.Type = Constante.Warning.ToLower();
                jsonResponseDto.IsValid = false;
                jsonResponseDto.Mensaje = ex.Message.ToString();
            }
            return Json(jsonResponseDto);
        }
        #endregion
        #region metodos privados
        private void FormatDataTable(DataTableModel<UsuarioFilterDto> dataTableModel)
        {
            for (int i = 0; i < dataTableModel.order.Count; i++)
            {
                var columnIndex = dataTableModel.order[0].column;
                var columnDir = dataTableModel.order[0].dir.ToUpper();
                var column = dataTableModel.columns[columnIndex].data;
                dataTableModel.orderBy = (" [" + column + "] " + columnDir + " ");
            }

            dataTableModel.whereFilter = "WHERE P.Estado != '' ";
            var tipo = GetPerfil();
            var IdEmpresa = "";
            if (tipo == "SUPERUSUARIO")
            {
                IdEmpresa = "";
            }
            else
            {
                IdEmpresa = GetEmpresaPadre();
            }

            if (dataTableModel.filter != null)
            {
                if (!string.IsNullOrWhiteSpace(dataTableModel.filter.NombreSearch))
                    dataTableModel.whereFilter += (" AND UPPER(P.Usuario) LIKE '%'+'" + dataTableModel.filter.NombreSearch.ToUpper() + "'+'%'");
                if (GetTipoEmpresa().ToUpper() == "PRINCIPAL")
                {
                    if (!string.IsNullOrWhiteSpace(IdEmpresa))
                        dataTableModel.whereFilter += (" AND P.IdEmpresa ='" + IdEmpresa + "' OR  P.IdPadreSubcontratista='" + IdEmpresa + "' OR P.IdPadre = '" + IdEmpresa + "'");
                }
                else if (GetTipoEmpresa().ToUpper() == "CONTRATISTA")
                {
                    if (!string.IsNullOrWhiteSpace(IdEmpresa))
                        dataTableModel.whereFilter += (" AND P.IdEmpresa ='" + IdEmpresa + "' OR P.IdPadre = '" + IdEmpresa + "'");
                }
                else if (GetTipoEmpresa().ToUpper() == "SUBCONTRATISTA")
                {
                    if (!string.IsNullOrWhiteSpace(IdEmpresa))
                        dataTableModel.whereFilter += (" AND P.IdEmpresa ='" + IdEmpresa + "'");
                }
            }


        }
        #endregion
    }
}