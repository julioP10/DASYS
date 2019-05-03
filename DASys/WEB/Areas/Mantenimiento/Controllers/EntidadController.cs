using System;
using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WEB.Controllers;
using Serilog;

namespace WEB.Areas.Mantenimiento.Controllers
{
    [Area("Mantenimiento")]
    public class EntidadController : BaseController
    {
        private readonly IEntidad _Entidad;
        public EntidadController(IServiceProvider serviceProvider, IEntidad Entidad):base(serviceProvider)
        {
            _Entidad = Entidad;
        }
        #region Metodos publicos
        #region Vistas
        public IActionResult Index()
        {
            EntidadFilterDto objEntidadFilterDto = new EntidadFilterDto();
            if (GetPerfil() == "SUPERUSUARIO")
                objEntidadFilterDto.Permiso = 1;
            else
                objEntidadFilterDto.Permiso = 0;

            return View(objEntidadFilterDto);
        }


        public IActionResult Registrar()
        {
            EntidadConsultaDto objEntidadConsultaDto = new EntidadConsultaDto();
            return PartialView("Registrar", objEntidadConsultaDto);
        }


        [HttpPost]
        public IActionResult Eliminar([FromForm] EntidadConsultaDto objEntidad)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                var result = _Entidad.EliminarEntidad(objEntidad.IdEntidad, objEntidad.Accion);
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
        public IActionResult Actualizar([FromForm] EntidadConsultaDto objEntidad)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {

                objEntidad = _Entidad.ConsultaEntidad(objEntidad);
                if (objEntidad != null)
                {
                    return PartialView("Actualizar", objEntidad);
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
        public IActionResult Consultar([FromForm] EntidadConsultaDto objEntidad)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {

                objEntidad = _Entidad.ConsultaEntidad(objEntidad);
                if (objEntidad != null)
                {
                    return PartialView("Consultar", objEntidad);
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
        public IActionResult Mantenimiento([FromForm] Entidades objEntidad)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                var message = (objEntidad.IdEntidad == null) ? Constante.registroExitoso : Constante.actualizacionExitoso;
                objEntidad.IdEntidad = (objEntidad.IdEntidad == null) ? "" : objEntidad.IdEntidad;
                var response = _Entidad.MantenimientoEntidad(objEntidad);
                if (response == 1)
                {
                    jsonResponseDto.Type = Constante.Success.ToLower();
                    jsonResponseDto.IsValid = true;
                    jsonResponseDto.Mensaje = message;
                }
                else
                {
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
        public IActionResult Listar(DataTableModel<EntidadFilterDto> dataTableModel)
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

                List<EntidadPaginationDto> lstCampania = _Entidad.PaginadoEntidad(paginationParameter);
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
        private void FormatDataTable(DataTableModel<EntidadFilterDto> dataTableModel)
        {
            for (int i = 0; i < dataTableModel.order.Count; i++)
            {
                var columnIndex = dataTableModel.order[0].column;
                var columnDir = dataTableModel.order[0].dir.ToUpper();
                var column = dataTableModel.columns[columnIndex].data;
                dataTableModel.orderBy = (" [" + column + "] " + columnDir + " ");
            }

            dataTableModel.whereFilter = "WHERE P.Estado != '' ";


            if (dataTableModel.filter != null)
            {
                if (!string.IsNullOrWhiteSpace(dataTableModel.filter.NombreSearch))
                    dataTableModel.whereFilter += (" AND UPPER(P.Nombre) LIKE '%'+'" + dataTableModel.filter.NombreSearch.ToUpper() + "'+'%'");
            }
        }
        #endregion
    }
}