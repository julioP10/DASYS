using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entidad;
using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WEB.Controllers;

namespace WEB.Areas.Mantenimiento.Controllers
{
    [Area("Mantenimiento")]
    public class MaquinariaController : BaseController
    {
        private readonly IMaquinaria _Maquinaria;
        public MaquinariaController(IServiceProvider serviceProvider, IMaquinaria Maquinaria) : base(serviceProvider)
        {
            _Maquinaria = Maquinaria;
        }
        #region Metodos publicos
        #region Vistas
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Registrar()
        {
            MaquinariaConsultaDto objMaquinariaConsultaDto = new MaquinariaConsultaDto();
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
            objMaquinariaConsultaDto.IdEmpresaPrincipal = GetEmpresaPadre();
            if (GetPerfil() == "SUPERUSUARIO")
            {
                objMaquinariaConsultaDto.IdEmpresaPrincipal = "";
            }
            objMaquinariaConsultaDto.Perfil = GetPerfil();

            return PartialView("Registrar", objMaquinariaConsultaDto);
        }


        [HttpPost]
        public IActionResult Eliminar([FromForm] MaquinariaConsultaDto objMaquinaria)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                var result = _Maquinaria.EliminarMaquinaria(objMaquinaria.IdMaquinaria, objMaquinaria.Accion);
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
        public IActionResult Actualizar([FromForm] MaquinariaConsultaDto objMaquinaria)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {

                objMaquinaria = _Maquinaria.ConsultaMaquinaria(objMaquinaria);
                objMaquinaria.IdEmpresaPrincipal = GetEmpresaPadre();
                if (objMaquinaria != null)
                {
                    if (GetPerfil() == "SUPERUSUARIO")
                    {
                        objMaquinaria.IdEmpresaPrincipal = "";
                    }
                    objMaquinaria.Perfil = GetPerfil();
                    return PartialView("Actualizar", objMaquinaria);
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
        public IActionResult Consultar([FromForm] MaquinariaConsultaDto objMaquinaria)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {

                objMaquinaria = _Maquinaria.ConsultaMaquinaria(objMaquinaria);
                objMaquinaria.IdEmpresaPrincipal = GetEmpresaPadre();

                if (objMaquinaria != null)
                {
                    objMaquinaria.Perfil = GetPerfil();
                    return PartialView("Consultar", objMaquinaria);
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
        public IActionResult Mantenimiento([FromForm] Maquinaria objMaquinaria, IFormFile file)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                var message = (objMaquinaria.IdMaquinaria == null) ? Constante.registroExitoso : Constante.actualizacionExitoso;
                objMaquinaria.IdMaquinaria = (objMaquinaria.IdMaquinaria == null) ? "" : objMaquinaria.IdMaquinaria;
                if (!string.IsNullOrWhiteSpace(objMaquinaria.FinContrato))
                {
                    objMaquinaria.FinContrato = Convert.ToDateTime(objMaquinaria.FinContrato).ToString("yyyyMMdd");
                }
                if (!string.IsNullOrWhiteSpace(objMaquinaria.InicioContrato))
                {
                    objMaquinaria.InicioContrato = Convert.ToDateTime(objMaquinaria.InicioContrato).ToString("yyyyMMdd");
                }

                var response = _Maquinaria.MantenimientoMaquinaria(objMaquinaria);

                if (response != "")
                {
                    var result = response.Split(":");
                    if (result.Count()>1)
                    {
                        jsonResponseDto.Type = Constante.Warning.ToLower();
                        jsonResponseDto.IsValid = false;
                        jsonResponseDto.Mensaje = result[1];
                        jsonResponseDto.data = result[1];
                    }
                    else
                    {
                        jsonResponseDto.Type = Constante.Success.ToLower();
                        jsonResponseDto.IsValid = true;
                        jsonResponseDto.Mensaje = message;
                        jsonResponseDto.data = response;
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
        public IActionResult Listar(DataTableModel<MaquinariaFilterDto> dataTableModel)
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

                List<MaquinariaPaginationDto> lstCampania = _Maquinaria.PaginadoMaquinaria(paginationParameter, dataTableModel.filter);
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


        #endregion
        #region metodos privados
        private void FormatDataTable(DataTableModel<MaquinariaFilterDto> dataTableModel)
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