using System;
using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace WEB.Areas.Seguridad.Controllers
{
    [Area("Seguridad")]
    public class OpcionController : Controller
    {
        private readonly IOpcion _Opcion;
        public OpcionController(IOpcion Opcion)
        {
            _Opcion = Opcion;
        }
        #region Metodos publicos
        #region Vistas
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Registrar()
        {
            OpcionConsultaDto objOpcionConsultaDto = new OpcionConsultaDto();
            return PartialView("Registrar", objOpcionConsultaDto);
        }


        [HttpPost]
        public IActionResult Eliminar([FromForm] OpcionConsultaDto objOpcion)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                var result = _Opcion.EliminarOpcion(objOpcion.IdOpcion, Convert.ToInt32(objOpcion.Accion));
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
        public IActionResult Actualizar([FromForm] OpcionConsultaDto objOpcion)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {

                objOpcion = _Opcion.ConsultaOpcion(objOpcion);
                if (objOpcion != null)
                {
                    return PartialView("Actualizar", objOpcion);
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
        public IActionResult Consultar([FromForm] OpcionConsultaDto objOpcion)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {

                objOpcion = _Opcion.ConsultaOpcion(objOpcion);
                if (objOpcion != null)
                {
                    return PartialView("Consultar", objOpcion);
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
        public IActionResult Mantenimiento([FromForm] Entidad.Opcion objOpcion)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                var message = (objOpcion.IdOpcion == null) ? Constante.registroExitoso : Constante.actualizacionExitoso;
                objOpcion.IdOpcion = (objOpcion.IdOpcion == null) ? "" : objOpcion.IdOpcion;
                var response = _Opcion.MantenimientoOpcion(objOpcion);
                if (response == 2)
                {
                    jsonResponseDto.Type = Constante.Error.ToLower();
                    jsonResponseDto.IsValid = false;
                    jsonResponseDto.Mensaje = Constante.registroError;
                }
                else if (response > 0)
                {
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
        public IActionResult Listar(DataTableModel<OpcionFilterDto> dataTableModel)
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

                List<OpcionPaginationDto> lstCampania = _Opcion.PaginadoOpcion(paginationParameter);
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

        #endregion
        #region metodos privados
        private void FormatDataTable(DataTableModel<OpcionFilterDto> dataTableModel)
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