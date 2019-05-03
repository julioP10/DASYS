using System;
using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WEB.Controllers;
using Microsoft.AspNetCore.Http;
using WEB.Models;
using System.IO;
using Serilog;

namespace WEB.Areas.Mantenimiento.Controllers
{
    [Area("Mantenimiento")]
    public class ColaboradorController : BaseController
    {
        private readonly IColaborador _Colaborador;
        public ColaboradorController(IServiceProvider serviceProvider, IColaborador Colaborador) : base(serviceProvider)
        {
            _Colaborador = Colaborador;
        }
        #region Metodos publicos
        #region Vistas
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Registrar()
        {
            ColaboradorConsultaDto objColaboradorConsultaDto = new ColaboradorConsultaDto();
            var _tipo = GetTipoEmpresa();
            if (!string.IsNullOrWhiteSpace(_tipo))
            {
                HttpContext.Session.SetString("_TipoEmpresa", _tipo);
            }
            else
            {
                _tipo = GetPerfil();
                HttpContext.Session.SetString("_TipoEmpresa", _tipo);
            }
            objColaboradorConsultaDto.IdEmpresaPrincipal = GetEmpresaPadre();
            objColaboradorConsultaDto.IdEmpresa = GetEmpresa();
            if (GetPerfil() == "SUPERUSUARIO")
            {
                objColaboradorConsultaDto.IdEmpresaPrincipal = "";
                objColaboradorConsultaDto.IdEmpresa = "";
            }
            

            return PartialView("Registrar", objColaboradorConsultaDto);
        }


        [HttpPost]
        public IActionResult Eliminar([FromForm] ColaboradorConsultaDto objColaborador)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                var result = _Colaborador.EliminarColaborador(objColaborador.IdColaborador, objColaborador.Accion);
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
        public IActionResult Actualizar([FromForm] ColaboradorConsultaDto objColaborador)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {

                objColaborador = _Colaborador.ConsultaColaborador(objColaborador);
                objColaborador.IdEmpresaPrincipal = GetEmpresaPadre();
                objColaborador.IdEmpresaPadre = GetEmpresa();
                if (objColaborador != null)
                {
                    if (GetPerfil() == "SUPERUSUARIO")
                    {
                        objColaborador.IdEmpresaPrincipal = "";
                    }
                    var _tipo = GetTipoEmpresa();
                    HttpContext.Session.SetString("_TipoEmpresa", _tipo);

                    return PartialView("Actualizar", objColaborador);
                }
                else
                {
                    jsonResponseDto.Type = Constante.Warning.ToLower();
                    jsonResponseDto.IsValid = false;
                    jsonResponseDto.Mensaje = "Error en al consulta";
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
        public IActionResult Consultar([FromForm] ColaboradorConsultaDto objColaborador)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {

                objColaborador = _Colaborador.ConsultaColaborador(objColaborador);
                objColaborador.IdEmpresaPrincipal = GetEmpresaPadre();

                if (objColaborador != null)
                {
                    var _tipo = GetPerfil();
                    HttpContext.Session.SetString("_TipoEmpresa", _tipo);
                    return PartialView("Consultar", objColaborador);
                }
                else
                {
                    jsonResponseDto.Type = Constante.Warning.ToLower();
                    jsonResponseDto.IsValid = false;
                    jsonResponseDto.Mensaje = "Error en al consulta";
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
        public async Task<IActionResult> Mantenimiento([FromForm] Colaborador objColaborador, IFormFile file)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                var message = (objColaborador.IdColaborador == null) ? Constante.registroExitoso : Constante.actualizacionExitoso;
                objColaborador.IdColaborador = (objColaborador.IdColaborador == null) ? "" : objColaborador.IdColaborador;
                string _extension = "", pathDirectoryTemp = "";
                if (file != null)
                {
                    pathDirectoryTemp = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "FotosColaborador", file.GetFilename()).ToString();
                    _extension = Path.GetExtension(pathDirectoryTemp);
                }
                objColaborador.Foto = _extension;
                var _data = Convert.ToDateTime(objColaborador.FechaNacimiento);
                objColaborador.FechaNacimiento = _data.ToString("yyyyMMdd");
                var response = _Colaborador.MantenimientoColaborador(objColaborador);

                if (response != "")
                {
                    jsonResponseDto.Type = Constante.Success.ToLower();
                    jsonResponseDto.IsValid = true;
                    jsonResponseDto.Mensaje = message;
                    jsonResponseDto.data = response;
                    if (file != null)
                    {
                        var pathDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "FotosColaborador");
                        if (!Directory.Exists(pathDirectory))
                            Directory.CreateDirectory(pathDirectory);

                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "FotosColaborador", response + _extension);
                        if (System.IO.File.Exists(path))
                            System.IO.File.Delete(path);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
                }
                else
                {
                    Log.Error(response.ToString());

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
        public IActionResult MantenimientoTelefono([FromForm] ColaboradorTiposDto objColaboradorTiposDto)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                var message = Constante.registroExitoso;
                var response = _Colaborador.MantenimientoTelefonoColaborador(objColaboradorTiposDto);
                if (response != "")
                {
                    jsonResponseDto.Type = Constante.Success.ToLower();
                    jsonResponseDto.IsValid = true;
                    jsonResponseDto.Mensaje = message;
                }
                else
                {
                    Log.Error(response.ToString());
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
        public IActionResult EliminarTelefono([FromForm] ColaboradorTiposDto objColaboradorTiposDto)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                var message = Constante.eliminacionExitoso;
                var response = _Colaborador.EliminarTelefonoColaborador(objColaboradorTiposDto);
                if (response != "")
                {
                    jsonResponseDto.Type = Constante.Success.ToLower();
                    jsonResponseDto.IsValid = true;
                    jsonResponseDto.Mensaje = message;
                }
                else
                {
                    Log.Error(response.ToString());
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
        public IActionResult MantenimientoCorreo([FromForm] ColaboradorTiposDto objColaboradorTiposDto)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                var message = Constante.registroExitoso;
                var response = _Colaborador.MantenimientoCorreoColaborador(objColaboradorTiposDto);
                if (response != "")
                {
                    jsonResponseDto.Type = Constante.Success.ToLower();
                    jsonResponseDto.IsValid = true;
                    jsonResponseDto.Mensaje = message;
                }
                else
                {
                    Log.Error(response.ToString());
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
        public IActionResult EliminarCorreo([FromForm] ColaboradorTiposDto objColaboradorTiposDto)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                var message = Constante.eliminacionExitoso;
                var response = _Colaborador.EliminarCorreoColaborador(objColaboradorTiposDto);
                if (response != "")
                {
                    jsonResponseDto.Type = Constante.Success.ToLower();
                    jsonResponseDto.IsValid = true;
                    jsonResponseDto.Mensaje = message;
                }
                else
                {
                    Log.Error(response.ToString());
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
        public IActionResult MantenimientoTarjeta([FromForm] ColaboradorTiposDto objColaboradorTiposDto)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                var message = Constante.registroExitoso;
                var response = _Colaborador.MantenimientoTarjetaColaborador(objColaboradorTiposDto);
                if (response != "")
                {
                    jsonResponseDto.Type = Constante.Success.ToLower();
                    jsonResponseDto.IsValid = true;
                    jsonResponseDto.Mensaje = message;
                }
                else
                {
                    Log.Error(response.ToString());
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
        public IActionResult EliminarTarjeta([FromForm] ColaboradorTiposDto objColaboradorTiposDto)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                var message = Constante.eliminacionExitoso;
                var response = _Colaborador.EliminarTarjetaColaborador(objColaboradorTiposDto);
                if (response != "")
                {
                    jsonResponseDto.Type = Constante.Success.ToLower();
                    jsonResponseDto.IsValid = true;
                    jsonResponseDto.Mensaje = message;
                }
                else
                {
                    Log.Error(response.ToString());
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
        public IActionResult Listar(DataTableModel<ColaboradorFilterDto> dataTableModel)
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

                List<ColaboradorPaginationDto> lstCampania = _Colaborador.PaginadoColaborador(paginationParameter, dataTableModel.filter);
                if (!string.IsNullOrWhiteSpace(dataTableModel.filter.DigitalizacionSearch))
                {
                    lstCampania = lstCampania.Where(p => p.Digitalizacion == dataTableModel.filter.DigitalizacionSearch).ToList();
                }
                var tipo_empresa = GetTipoEmpresa();
                var idEmpresa = GetEmpresa();
                dataTableModel.data = lstCampania;
                if (lstCampania.Count > 0)
                {
                    dataTableModel.recordsTotal = lstCampania[0].Cantidad;
                    dataTableModel.recordsFiltered = dataTableModel.recordsTotal;
                }
            }


            catch (Exception ex)
            {
                jsonResponse.Mensaje = ex.Message;
            }
            return Json(dataTableModel);
        }

        [HttpPost]
        public IActionResult ListarCorreo([FromForm] ColaboradorTiposDto objColaboradorTiposDto)
        {
            var jsonResponse = new JsonResponseDto() { Type = Constante.Error };
            try
            {
                List<ColaboradorTiposDto> lstCampania = _Colaborador.ListadoColaboradorCorreo(objColaboradorTiposDto.IdPersona);
                jsonResponse.data = lstCampania;
            }


            catch (Exception ex)
            {
                jsonResponse.Mensaje = ex.Message;
            }
            return Json(jsonResponse);
        }
        [HttpPost]
        public IActionResult ListarTelefono([FromForm] ColaboradorTiposDto objColaboradorTiposDto)
        {
            var jsonResponse = new JsonResponseDto() { Type = Constante.Error };
            try
            {
                List<ColaboradorTiposDto> lstCampania = _Colaborador.ListadoColaboradorTelefono(objColaboradorTiposDto.IdPersona);
                jsonResponse.data = lstCampania;
            }


            catch (Exception ex)
            {
                jsonResponse.Mensaje = ex.Message;
            }
            return Json(jsonResponse);
        }

        [HttpPost]
        public IActionResult ListarTarjeta([FromForm] ColaboradorTiposDto objColaboradorTiposDto)
        {
            var jsonResponse = new JsonResponseDto() { Type = Constante.Error };
            try
            {
                List<ColaboradorTiposDto> lstCampania = _Colaborador.ListadoColaboradorTarjeta(objColaboradorTiposDto.IdPersona);
                jsonResponse.data = lstCampania;
            }


            catch (Exception ex)
            {
                jsonResponse.Mensaje = ex.Message;
            }
            return Json(jsonResponse);
        }

        #endregion
        #region metodos privados
        private void FormatDataTable(DataTableModel<ColaboradorFilterDto> dataTableModel)
        {
            for (int i = 0; i < dataTableModel.order.Count; i++)
            {
                var columnIndex = dataTableModel.order[0].column;
                var columnDir = dataTableModel.order[0].dir.ToUpper();
                var column = dataTableModel.columns[columnIndex].data;
                dataTableModel.orderBy = (" [" + column + "] " + columnDir + " ");
            }

            dataTableModel.whereFilter = "WHERE P.Estado != '' ";

            var tipo_empresa = GetTipoEmpresa();
            var idEmpresa = GetEmpresa();

            if (tipo_empresa == "PRINCIPAL")
            {
                if (dataTableModel.filter != null)
                {
                    //if (!string.IsNullOrWhiteSpace(dataTableModel.filter.DigitalizacionSearch))
                    //    dataTableModel.whereFilter += (" AND P.Digitalizacion ='" + dataTableModel.filter.DigitalizacionSearch + "' ");
                    //dataTableModel.whereFilter += (" AND P.IdEmpresa ='" + idEmpresa + "' OR P.IdPadre ='" + idEmpresa + "' OR P.IdPadreSubcontratista ='" + idEmpresa + "'");
                    dataTableModel.filter.IdEmpresa = idEmpresa;
                    dataTableModel.filter.IdPadre = idEmpresa;
                    dataTableModel.filter.IdPadreSubcontratista = idEmpresa;

                    //dataTableModel.whereFilter += (" AND P.EsPrincipal ='" + 1 + "' OR P.EsContratista ='" + 1 + "' OR P.EsSubContratista ='" + 1 + "'");

                }
            }
            else if (tipo_empresa == "CONTRATISTA")
            {
                if (dataTableModel.filter != null)
                {
                    //if (!string.IsNullOrWhiteSpace(dataTableModel.filter.NombreSearch))
                    //    dataTableModel.whereFilter += (" AND P.Nombre LIKE '%'+'" + dataTableModel.filter.NombreSearch + "'+'%'");
                    //dataTableModel.whereFilter += (" AND P.IdEmpresa ='" + idEmpresa + "' OR P.IdPadre ='" + idEmpresa + "'");
                    dataTableModel.filter.IdEmpresa = idEmpresa;
                    dataTableModel.filter.IdPadre = idEmpresa;
                    dataTableModel.filter.IdPadreSubcontratista = "";
                    //dataTableModel.whereFilter += (" AND P.EsContratista ='" + 1 + "' OR P.EsSubContratista ='" + 1 + "'");
                    //if (!string.IsNullOrWhiteSpace(dataTableModel.filter.DigitalizacionSearch))
                    //    dataTableModel.whereFilter += (" AND P.Digitalizacion ='" + dataTableModel.filter.DigitalizacionSearch + "' ");
                }
            }
            else if (tipo_empresa == "SUBCONTRATISTA")
            {
                if (dataTableModel.filter != null)
                {
                    //if (!string.IsNullOrWhiteSpace(dataTableModel.filter.NombreSearch))
                    //    dataTableModel.whereFilter += (" AND P.Nombre LIKE '%'+'" + dataTableModel.filter.NombreSearch + "'+'%'");
                    //dataTableModel.whereFilter += (" AND P.IdEmpresa ='" + idEmpresa + "'");
                    dataTableModel.filter.IdEmpresa = idEmpresa;
                    dataTableModel.filter.IdPadre = "";
                    dataTableModel.filter.IdPadreSubcontratista = "";
                    //dataTableModel.whereFilter += (" AND P.EsSubContratista ='" + 1 + "'");
                    //if (!string.IsNullOrWhiteSpace(dataTableModel.filter.NombreSearch))
                    //    dataTableModel.whereFilter += (" AND P.Nombre LIKE '%'+'" + dataTableModel.filter.NombreSearch + "'+'%'");
                }
            }
            else
            {
                //if (!string.IsNullOrWhiteSpace(dataTableModel.filter.DigitalizacionSearch))
                //    dataTableModel.whereFilter += (" AND P.Digitalizacion ='" + dataTableModel.filter.DigitalizacionSearch + "' ");
                if (!string.IsNullOrWhiteSpace(dataTableModel.filter.NombreSearch))
                    dataTableModel.filter.Nombre = dataTableModel.filter.NombreSearch;
                else
                    dataTableModel.filter.Nombre = "";


            }

            if (!string.IsNullOrWhiteSpace(dataTableModel.filter.NombreSearch))
                dataTableModel.filter.Nombre = dataTableModel.filter.NombreSearch;
            else
                dataTableModel.filter.Nombre = "";
        }
        #endregion
    }
}