using System;
using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WEB.Controllers;
using Serilog;

namespace WEB.Areas.Equipo.Controllers
{
    [Area("Equipo")]
    public class TerminalController : BaseController
    {
        private readonly ITerminal _Terminal;
        public TerminalController(IServiceProvider serviceProvider,ITerminal Terminal):base(serviceProvider)
        {
            _Terminal = Terminal;
        }
        #region Metodos publicos
        #region Vistas
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Registrar()
        {
            TerminalConsultaDto objTerminalConsultaDto = new TerminalConsultaDto();
            objTerminalConsultaDto.IdEmpresa = GetEmpresaPadre();
            return PartialView("Registrar", objTerminalConsultaDto);
        }


        [HttpPost]
        public IActionResult Eliminar([FromForm] TerminalConsultaDto objTerminal)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                var result = _Terminal.EliminarTerminal(objTerminal.IdTerminal, objTerminal.Accion);
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
        public IActionResult Actualizar([FromForm] TerminalConsultaDto objTerminal)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {

                objTerminal = _Terminal.ConsultaTerminal(objTerminal);
                if (GetPerfil().ToUpper()!="SUPERUSUARIO")
                {
                    objTerminal.IdEmpresa = GetEmpresaPadre();
                }
                if (objTerminal != null)
                {
                    return PartialView("Actualizar", objTerminal);
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
        public IActionResult Consultar([FromForm] TerminalConsultaDto objTerminal)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {

                objTerminal = _Terminal.ConsultaTerminal(objTerminal);
                if (objTerminal != null)
                {
                    return PartialView("Consultar", objTerminal);
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
        public IActionResult Mantenimiento([FromForm] Terminal objTerminal)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                var message = (objTerminal.IdTerminal == null) ? Constante.registroExitoso : Constante.actualizacionExitoso;
                objTerminal.IdTerminal = (objTerminal.IdTerminal == null) ? "" : objTerminal.IdTerminal;
                if (!string.IsNullOrWhiteSpace(objTerminal.IdTerminal))
                {
                    if (GetPerfil().ToUpper()!="SUPERUSUARIO")
                    {
                        objTerminal.IdEmpresa = GetEmpresaPadre();
                    }
                }
                var response = _Terminal.MantenimientoTerminal(objTerminal);
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
        public IActionResult Listar(DataTableModel<TerminalFilterDto> dataTableModel)
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

                List<TerminalPaginationDto> lstCampania = _Terminal.PaginadoTerminal(paginationParameter);
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
        private void FormatDataTable(DataTableModel<TerminalFilterDto> dataTableModel)
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