using System;
using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WEB.Controllers;
using Serilog;

namespace WEB.Configuracions.Equipo.Controllers
{
    [Area("Equipo")]
    public class ConfiguracionController : BaseController
    {
        private readonly IConfiguracion _Configuracion;
        public ConfiguracionController(IServiceProvider serviceProvider,IConfiguracion Configuracion):base(serviceProvider)
        {
            _Configuracion = Configuracion;
        }
        #region Metodos publicos
        #region Vistas
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Registrar()
        {
            ConfiguracionConsultaDto objConfiguracionConsultaDto = new ConfiguracionConsultaDto();
            return PartialView("Registrar", objConfiguracionConsultaDto);
        }


        [HttpPost]
        public IActionResult Eliminar([FromForm] ConfiguracionConsultaDto objConfiguracion)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                var result = _Configuracion.EliminarConfiguracion(objConfiguracion.IdConfiguracion, objConfiguracion.Accion);
                var mensaje = result.Split(":");
                jsonResponseDto.Type = mensaje[0];
                jsonResponseDto.IsValid = true;
                jsonResponseDto.Mensaje = mensaje[1];
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
        public IActionResult Actualizar([FromForm] ConfiguracionConsultaDto objConfiguracion)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {

                objConfiguracion = _Configuracion.ConsultaConfiguracion(objConfiguracion);
                if (objConfiguracion != null)
                {
                    return PartialView("Actualizar", objConfiguracion);
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
                Log.Error(ex.Message);
                jsonResponseDto.Type = Constante.Warning.ToLower();
                jsonResponseDto.IsValid = false;
                jsonResponseDto.Mensaje = ex.Message.ToString();
            }
            return Json(jsonResponseDto);
        }
        [HttpPost]
        public IActionResult Consultar([FromForm] ConfiguracionConsultaDto objConfiguracion)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {

                objConfiguracion = _Configuracion.ConsultaConfiguracion(objConfiguracion);
                if (objConfiguracion != null)
                {
                    return PartialView("Consultar", objConfiguracion);
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
                Log.Error(ex.Message);
                jsonResponseDto.Type = Constante.Warning.ToLower();
                jsonResponseDto.IsValid = false;
                jsonResponseDto.Mensaje = ex.Message.ToString();
            }
            return Json(jsonResponseDto);
        }
        #endregion
        [HttpPost]
        public IActionResult Mantenimiento([FromForm] Configuracion objConfiguracion)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                var message = (objConfiguracion.IdConfiguracion == null) ? Constante.registroExitoso : Constante.actualizacionExitoso;
                objConfiguracion.IdConfiguracion = (objConfiguracion.IdConfiguracion == null) ? "" : objConfiguracion.IdConfiguracion;
                //objConfiguracion.IdEmpresa = GetEmpresaPadre();
                var response = _Configuracion.MantenimientoConfiguracion(objConfiguracion);
                if (response == 1)
                {
                    jsonResponseDto.Type = Constante.Success.ToLower();
                    jsonResponseDto.IsValid = true;
                    jsonResponseDto.Mensaje = message;
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
        public IActionResult Listar(DataTableModel<ConfiguracionFilterDto> dataTableModel)
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

                List<ConfiguracionPaginationDto> lstCampania = _Configuracion.PaginadoConfiguracion(paginationParameter);
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
        private void FormatDataTable(DataTableModel<ConfiguracionFilterDto> dataTableModel)
        {
            for (int i = 0; i < dataTableModel.order.Count; i++)
            {
                var columnIndex = dataTableModel.order[0].column;
                var columnDir = dataTableModel.order[0].dir.ToUpper();
                var column = dataTableModel.columns[columnIndex].data;
                dataTableModel.orderBy = (" [" + column + "] " + columnDir + " ");
            }


            var tipo = GetPerfil();
            if (tipo == "SUPERUSUARIO")
            {
                dataTableModel.filter.IdEmpresaSearch = "";
            }
            else
            {
                dataTableModel.filter.IdEmpresaSearch = GetEmpresaPadre();
            }
            dataTableModel.whereFilter = "WHERE P.Estado != '' ";


            if (dataTableModel.filter != null)
            {
                if (!string.IsNullOrWhiteSpace(dataTableModel.filter.NombreSearch))
                    dataTableModel.whereFilter += (" AND UPPER(P.Nombre) LIKE '%'+'" + dataTableModel.filter.NombreSearch.ToUpper() + "'+'%'");
                if (!string.IsNullOrWhiteSpace(dataTableModel.filter.IdEmpresaSearch))
                    dataTableModel.whereFilter += (" AND P.IdEmpresa ='" + dataTableModel.filter.IdEmpresaSearch + "'");
            }
        }
        #endregion
    }
}