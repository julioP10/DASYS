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
    public class VehiculoController : BaseController
    {
        private readonly IVehiculo _Vehiculo;
        public VehiculoController(IServiceProvider serviceProvider, IVehiculo Vehiculo) : base(serviceProvider)
        {
            _Vehiculo = Vehiculo;
        }
        #region Metodos publicos
        #region Vistas
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Registrar()
        {
            VehiculoConsultaDto objVehiculoConsultaDto = new VehiculoConsultaDto();
            var _tipo = GetTipoEmpresa();
            if (!string.IsNullOrWhiteSpace(_tipo))
            {
                HttpContext.Session.SetString("_TipoEmpresa", _tipo);
                objVehiculoConsultaDto.Tipo = _tipo;
            }
            else
            {
                _tipo = GetPerfil();
                HttpContext.Session.SetString("_TipoEmpresa", _tipo);
                objVehiculoConsultaDto.Tipo = _tipo;
            }
            objVehiculoConsultaDto.IdEmpresaPrincipal = GetEmpresaPadre();
            if (GetPerfil() == "SUPERUSUARIO")
            {
                objVehiculoConsultaDto.IdEmpresaPrincipal = "";
            }
            objVehiculoConsultaDto.Perfil = GetPerfil();

            return PartialView("Registrar", objVehiculoConsultaDto);
        }


        [HttpPost]
        public IActionResult Eliminar([FromForm] VehiculoConsultaDto objVehiculo)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                var result = _Vehiculo.EliminarVehiculo(objVehiculo.IdVehiculo, objVehiculo.Accion);
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
        public IActionResult Actualizar([FromForm] VehiculoConsultaDto objVehiculo)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {

                objVehiculo = _Vehiculo.ConsultaVehiculo(objVehiculo);
                objVehiculo.IdEmpresaPrincipal = GetEmpresaPadre();
                objVehiculo.Tipo = GetTipoEmpresa();
                if (objVehiculo != null)
                {
                    if (GetPerfil() == "SUPERUSUARIO")
                    {
                        objVehiculo.IdEmpresaPrincipal = "";
                    }
                    objVehiculo.Perfil = GetPerfil();
                    return PartialView("Actualizar", objVehiculo);
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
        public IActionResult Consultar([FromForm] VehiculoConsultaDto objVehiculo)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {

                objVehiculo = _Vehiculo.ConsultaVehiculo(objVehiculo);
                objVehiculo.IdEmpresaPrincipal = GetEmpresaPadre();
                objVehiculo.Tipo = GetTipoEmpresa();
                if (objVehiculo != null)
                {
                    objVehiculo.Perfil = GetPerfil();
                    return PartialView("Consultar", objVehiculo);
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
        public IActionResult Mantenimiento([FromForm] Vehiculo objVehiculo, IFormFile file)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                var message = (objVehiculo.IdVehiculo == null) ? Constante.registroExitoso : Constante.actualizacionExitoso;
                objVehiculo.IdVehiculo = (objVehiculo.IdVehiculo == null) ? "" : objVehiculo.IdVehiculo;
                if (!string.IsNullOrWhiteSpace(objVehiculo.FinContrato))
                {
                    objVehiculo.FinContrato = Convert.ToDateTime(objVehiculo.FinContrato).ToString("yyyyMMdd");
                }
                if (!string.IsNullOrWhiteSpace(objVehiculo.InicioContrato))
                {
                    objVehiculo.InicioContrato = Convert.ToDateTime(objVehiculo.InicioContrato).ToString("yyyyMMdd");
                }

                var response = _Vehiculo.MantenimientoVehiculo(objVehiculo);

                if (response != "")
                {
                    var result = response.Split(":");
                    if (result.Count() > 1)
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
        public IActionResult Listar(DataTableModel<VehiculoFilterDto> dataTableModel)
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

                List<VehiculoPaginationDto> lstCampania = _Vehiculo.PaginadoVehiculo(paginationParameter, dataTableModel.filter);
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
        private void FormatDataTable(DataTableModel<VehiculoFilterDto> dataTableModel)
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