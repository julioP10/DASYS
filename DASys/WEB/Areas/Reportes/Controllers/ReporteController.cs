using System;
using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WEB.Controllers;
using OfficeOpenXml;

namespace WEB.Areas.Reportes.Controllers
{
    [Area("Reportes")]
    public class ReporteController : BaseController
    {
        private readonly IEmpresa _Empresa;
        private readonly IColaborador _colaborador;
        private readonly IDigitalizacion _digitalizacion;
        private readonly IReportes _reportes;
        public ReporteController(IServiceProvider serviceProvider, IReportes reportes, IColaborador colaborador, IDigitalizacion digitalizacion, IEmpresa Empresa) : base(serviceProvider)
        {
            _digitalizacion = digitalizacion;
            _Empresa = Empresa;
            _colaborador = colaborador;
            _reportes = reportes;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ReporteColaborador()
        {
            ReporteFilterDto objReporteFilterDto = new ReporteFilterDto();
            if (GetPerfil().ToUpper() != "SUPERUSUARIO")
            {
                objReporteFilterDto.IdEmpresaPrincipal = GetEmpresaPadre();
            }
            else
            {
                objReporteFilterDto.IdEmpresaPrincipal = "";
            }

            return View(objReporteFilterDto);
        }
        public IActionResult ReporteMaquinaria()
        {
            ReporteFilterDto objReporteFilterDto = new ReporteFilterDto();
            if (GetPerfil().ToUpper() != "SUPERUSUARIO")
            {
                objReporteFilterDto.IdEmpresaPrincipal = GetEmpresaPadre();
            }
            else
            {
                objReporteFilterDto.IdEmpresaPrincipal = "";
            }
            objReporteFilterDto.Perfil = GetPerfil();
            return View(objReporteFilterDto);
        }
        public IActionResult ReporteVehiculo()
        {
            ReporteFilterDto objReporteFilterDto = new ReporteFilterDto();
            if (GetPerfil().ToUpper() != "SUPERUSUARIO")
            {
                objReporteFilterDto.IdEmpresaPrincipal = GetEmpresaPadre();
            }
            else
            {
                objReporteFilterDto.IdEmpresaPrincipal = "";
            }
            objReporteFilterDto.Perfil = GetPerfil();
            return View(objReporteFilterDto);
        }
        [HttpPost]
        public IActionResult ListarReporteColaborador(DataTableModel<ReporteFilterDto> dataTableModel)
        {
            var jsonResponse = new JsonResponseDto();

            try
            {
                FormatReporteColaboradorDataTable(dataTableModel);

                var paginationParameter = new PaginationParameter
                {
                    Start = dataTableModel.start,
                    AmountRows = dataTableModel.length,
                    WhereFilter = dataTableModel.whereFilter,
                    OrderBy = dataTableModel.orderBy
                };
                paginationParameter.Parameters = dataTableModel.filter.EntidadSearch;
                List<ReporteColaboradorDto> lstDocumentos = _reportes.ReporteColaborador(paginationParameter, dataTableModel.filter);

                if (GetPerfil().ToUpper()!="SUPERUSUARIO")
                {
                    var empresa =GetNombreEmpresa();
                    lstDocumentos = lstDocumentos.Where(p => p.PadreSubcontratista==GetNombreEmpresa()).ToList();
                }
                dataTableModel.data = lstDocumentos;
                if (lstDocumentos.Count > 0)
                {
                    dataTableModel.recordsTotal = lstDocumentos[0].Cantidad;
                    dataTableModel.recordsFiltered = dataTableModel.recordsTotal;
                }
            }


            catch (Exception ex)
            {
                jsonResponse.Mensaje = ex.Message;
                jsonResponse.data = "";
            }
            return Json(dataTableModel);
        }
        [HttpPost]
        public IActionResult ListarReporteVehiculo(DataTableModel<ReporteFilterDto> dataTableModel)
        {
            var jsonResponse = new JsonResponseDto();

            try
            {
                FormatReporteColaboradorDataTable(dataTableModel);

                var paginationParameter = new PaginationParameter
                {
                    Start = dataTableModel.start,
                    AmountRows = dataTableModel.length,
                    WhereFilter = dataTableModel.whereFilter,
                    OrderBy = dataTableModel.orderBy
                };
                paginationParameter.Parameters = dataTableModel.filter.EntidadSearch;
                List<ReporteVehiculoDto> lstDocumentos = _reportes.ReporteVehiculo(paginationParameter, dataTableModel.filter);

                dataTableModel.data = lstDocumentos;
                if (lstDocumentos.Count > 0)
                {
                    dataTableModel.recordsTotal = lstDocumentos[0].Cantidad;
                    dataTableModel.recordsFiltered = dataTableModel.recordsTotal;
                }
            }


            catch (Exception ex)
            {
                jsonResponse.Mensaje = ex.Message;
                jsonResponse.data = "";
            }
            return Json(dataTableModel);
        }
        [HttpPost]
        public IActionResult ListarReporteMaquinaria(DataTableModel<ReporteFilterDto> dataTableModel)
        {
            var jsonResponse = new JsonResponseDto();

            try
            {
                FormatReporteColaboradorDataTable(dataTableModel);

                var paginationParameter = new PaginationParameter
                {
                    Start = dataTableModel.start,
                    AmountRows = dataTableModel.length,
                    WhereFilter = dataTableModel.whereFilter,
                    OrderBy = dataTableModel.orderBy
                };
                paginationParameter.Parameters = dataTableModel.filter.EntidadSearch;
                List<ReporteMaquinariaDto> lstDocumentos = _reportes.ReporteMaquinaria(paginationParameter, dataTableModel.filter);

                dataTableModel.data = lstDocumentos;
                if (lstDocumentos.Count > 0)
                {
                    dataTableModel.recordsTotal = lstDocumentos[0].Cantidad;
                    dataTableModel.recordsFiltered = dataTableModel.recordsTotal;
                }
            }


            catch (Exception ex)
            {
                jsonResponse.Mensaje = ex.Message;
                jsonResponse.data = "";
            }
            return Json(dataTableModel);
        }
        public IActionResult ReporteDigitalizacion()
        {
            ReporteFilterDto objReporteFilterDto = new ReporteFilterDto();
            if (GetPerfil().ToUpper() != "SUPERUSUARIO")
            {
                objReporteFilterDto.IdEmpresaPrincipal = GetEmpresaPadre();
            }
            return View(objReporteFilterDto);
        }

        public IActionResult ListarReporteDigitalizacion(DataTableModel<ReporteFilterDto> dataTableModel)
        {
            var jsonResponse = new JsonResponseDto() { Type = Constante.Error };

            try
            {
                FormatReporteDigitalizacionDataTable(dataTableModel);
                var jsonResponseDto = new JsonResponseDto() { Type = Constante.Error };

                var paginationParameter = new PaginationParameter
                {
                    Start = dataTableModel.start,
                    AmountRows = dataTableModel.length,
                    WhereFilter = dataTableModel.whereFilter,
                    OrderBy = dataTableModel.orderBy
                };
                paginationParameter.Parameters = dataTableModel.filter.EntidadSearch;
                List<ReporteDigitalizacionDto> lstCampania = _reportes.ReporteDigitalizacion(paginationParameter);

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
        public IActionResult ListarReporteDigitalizacionExcel()
        {
            var jsonResponse = new JsonResponseDto() { Type = Constante.Error };

            try
            {
                var paginationParameter = new PaginationParameter
                {
                    Start = 0,
                    AmountRows = -1,
                    WhereFilter = "WHERE P.Estado != '' ",
                    OrderBy = "IdPersona",
                    Parameters = "COLABORADOR"

                };
                byte[] fileContents;
                List<ReporteDigitalizacionDto> lstCampania = _reportes.ReporteDigitalizacion(paginationParameter);
                var nuevaLista = from pe in lstCampania
                                 select new
                                 {

                                 };
                ExcelPackage excel = new ExcelPackage();
                string nameExcelContent = "Digitalizacion";
                string[] cabeceras = { "Codigo", "Ruc", "Empresa", "Categoria", "Documento", "Observacion", "Obligatorio", "Con Fecha Vencimiento", "Estado" };

                var workSheet = excel.Workbook.Worksheets.Add(nameExcelContent);
                workSheet.TabColor = System.Drawing.Color.Black;
                workSheet.DefaultRowHeight = 12;

                FormatExcel<ReporteDigitalizacionDto>(excel, cabeceras, lstCampania, 1, nameExcelContent, "", 12, "A", "F");
                fileContents = excel.GetAsByteArray();
                if (fileContents == null || fileContents.Length == 0)
                {
                    return NotFound();
                }

                return File(
                    fileContents: fileContents,
                    contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    fileDownloadName: "Reporte" + ".xlsx"
                );

            }


            catch (Exception ex)
            {
                jsonResponse.Mensaje = ex.Message;
            }
            return Json(jsonResponse);
        }
        public IActionResult ReporteDocumentos()
        {
            ReporteFilterDto objReporteFilterDto = new ReporteFilterDto();
            if (GetPerfil().ToUpper() != "SUPERUSUARIO")
            {
                objReporteFilterDto.IdEmpresaPrincipal = GetEmpresaPadre();
            }
            return View(objReporteFilterDto);
        }
        [HttpPost]
        public IActionResult ListarReporteDocumentos(DataTableModel<ReporteFilterDto> dataTableModel)
        {
            var jsonResponse = new JsonResponseDto() { Type = Constante.Error };

            try
            {
                FormatReporteDocumentoDataTable(dataTableModel);
                var jsonResponseDto = new JsonResponseDto() { Type = Constante.Error };

                var paginationParameter = new PaginationParameter
                {
                    Start = dataTableModel.start,
                    AmountRows = dataTableModel.length,
                    WhereFilter = dataTableModel.whereFilter,
                    OrderBy = dataTableModel.orderBy
                };
                paginationParameter.Parameters = dataTableModel.filter.EntidadSearch;
                List<ReporteDocumentosDto> lstDocumentos = _reportes.ReporteDocumentos(paginationParameter);

                dataTableModel.data = lstDocumentos;
                if (lstDocumentos.Count > 0)
                {
                    dataTableModel.recordsTotal = lstDocumentos[0].Cantidad;
                    dataTableModel.recordsFiltered = dataTableModel.recordsTotal;
                }
            }


            catch (Exception ex)
            {
                jsonResponse.Mensaje = ex.Message;
            }
            return Json(dataTableModel);
        }
        public IActionResult ReporteEmpresa()
        {
            ReporteFilterDto objReporteFilterDto = new ReporteFilterDto();
            if (GetPerfil().ToUpper() != "SUPERUSUARIO")
            {
                objReporteFilterDto.IdEmpresaPrincipal = GetEmpresaPadre();
            }
            return View(objReporteFilterDto);
        }
        [HttpPost]
        public IActionResult ListarReporteEmpresa(DataTableModel<ReporteFilterDto> dataTableModel)
        {
            var jsonResponse = new JsonResponseDto();

            try
            {
                FormatReporteEmpresaDataTable(dataTableModel);

                var paginationParameter = new PaginationParameter
                {
                    Start = dataTableModel.start,
                    AmountRows = dataTableModel.length,
                    WhereFilter = dataTableModel.whereFilter,
                    OrderBy = dataTableModel.orderBy
                };
                paginationParameter.Parameters = dataTableModel.filter.EntidadSearch;
                List<ReporteEmpresaDto> lstDocumentos = _reportes.ReporteEmpresa(paginationParameter);

                dataTableModel.data = lstDocumentos;
                if (lstDocumentos.Count > 0)
                {
                    dataTableModel.recordsTotal = lstDocumentos[0].Cantidad;
                    dataTableModel.recordsFiltered = dataTableModel.recordsTotal;
                }
            }


            catch (Exception ex)
            {
                jsonResponse.Mensaje = ex.Message;
            }
            return Json(dataTableModel);
        }
        public IActionResult ReporteMarcaciones()
        {
            ReporteFilterDto objReporteFilterDto = new ReporteFilterDto();
            if (GetPerfil().ToUpper() != "SUPERUSUARIO")
            {
                objReporteFilterDto.IdEmpresaPrincipal = GetEmpresaPadre();
            }
            return View(objReporteFilterDto);
        }
        [HttpPost]
        public IActionResult ListarReporteMarcaciones(DataTableModel<ReporteFilterDto> dataTableModel)
        {
            var jsonResponse = new JsonResponseDto();

            try
            {
                FormatReporteMarcacionesDataTable(dataTableModel);

                var paginationParameter = new PaginationParameter
                {
                    Start = dataTableModel.start,
                    AmountRows = dataTableModel.length,
                    WhereFilter = dataTableModel.whereFilter,
                    OrderBy = dataTableModel.orderBy
                };
                paginationParameter.Parameters = dataTableModel.filter.EntidadSearch;
                List<ReporteMarcacionesDto> lstDocumentos = _reportes.ReporteMarcaciones(paginationParameter);

                dataTableModel.data = lstDocumentos;
                if (lstDocumentos.Count > 0)
                {
                    dataTableModel.recordsTotal = lstDocumentos[0].Cantidad;
                    dataTableModel.recordsFiltered = dataTableModel.recordsTotal;
                }
            }


            catch (Exception ex)
            {
                jsonResponse.Mensaje = ex.Message;
            }
            return Json(dataTableModel);
        }
        public IActionResult ReporteOchoMarcaciones()
        {
            ReporteFilterDto objReporteFilterDto = new ReporteFilterDto();
            return View(objReporteFilterDto);
        }
        [HttpPost]
        public IActionResult ListarReporteOchoMarcaciones(DataTableModel<ReporteFilterDto> dataTableModel)
        {
            var jsonResponse = new JsonResponseDto();

            try
            {
                FormatReporteOchoMarcacionesDataTable(dataTableModel);

                var paginationParameter = new PaginationParameter
                {
                    Start = dataTableModel.start,
                    AmountRows = dataTableModel.length,
                    WhereFilter = dataTableModel.whereFilter,
                    OrderBy = dataTableModel.orderBy
                };
                paginationParameter.Parameters = dataTableModel.filter.EntidadSearch;
                List<ReporteOchoMarcacionesDto> lstDocumentos = _reportes.ReporteOchoMarcaciones(paginationParameter);

                dataTableModel.data = lstDocumentos;
                if (lstDocumentos.Count > 0)
                {
                    dataTableModel.recordsTotal = lstDocumentos[0].Cantidad;
                    dataTableModel.recordsFiltered = dataTableModel.recordsTotal;
                }
            }


            catch (Exception ex)
            {
                jsonResponse.Mensaje = ex.Message;
            }
            return Json(dataTableModel);
        }
        public IActionResult ReporteAsistencia()
        {
            ReporteFilterDto objReporteFilterDto = new ReporteFilterDto();
            if (GetPerfil().ToUpper() != "SUPERUSUARIO")
            {
                objReporteFilterDto.IdEmpresaPrincipal = GetEmpresaPadre();
            }
            return View(objReporteFilterDto);
        }
        [HttpPost]
        public IActionResult ListarReporteAsistencia(DataTableModel<ReporteFilterDto> dataTableModel)
        {
            var jsonResponse = new JsonResponseDto();

            try
            {
                FormatReporteAsistenciaDataTable(dataTableModel);

                var paginationParameter = new PaginationParameter
                {
                    Start = dataTableModel.start,
                    AmountRows = dataTableModel.length,
                    WhereFilter = dataTableModel.whereFilter,
                    OrderBy = dataTableModel.orderBy
                };
                paginationParameter.Parameters = dataTableModel.filter.EntidadSearch;
                List<ReporteAsistenciaDto> lstDocumentos = _reportes.ReporteAsistencia(paginationParameter);

                dataTableModel.data = lstDocumentos;
                if (lstDocumentos.Count > 0)
                {
                    dataTableModel.recordsTotal = lstDocumentos[0].Cantidad;
                    dataTableModel.recordsFiltered = dataTableModel.recordsTotal;
                }
            }


            catch (Exception ex)
            {
                jsonResponse.Mensaje = ex.Message;
            }
            return Json(dataTableModel);
        }
        #region void
        private void FormatReporteDigitalizacionDataTable(DataTableModel<ReporteFilterDto> dataTableModel)
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
                if (GetPerfil() != "SUPERUSUARIO")
                {
                    var IdEmpresa = GetEmpresaPadre();
                    dataTableModel.whereFilter += (" AND P.IdEmpresa ='" + IdEmpresa + "'");
                    dataTableModel.whereFilter += (" OR P.IdPadre ='" + IdEmpresa + "'");
                    dataTableModel.whereFilter += (" OR P.IdPadreSubcontratista ='" + IdEmpresa + "'");
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(dataTableModel.filter.IdEmpresaSearch))
                        dataTableModel.whereFilter += (" AND P.IdEmpresa ='" + dataTableModel.filter.IdEmpresaSearch + "'");
                }

                if (!string.IsNullOrWhiteSpace(dataTableModel.filter.NombreSearch))
                    dataTableModel.whereFilter += (" AND UPPER(P.Nombre) LIKE '%'+'" + dataTableModel.filter.NombreSearch.ToUpper() + "'+'%'");
                if (dataTableModel.filter.EntidadSearch != "0")
                    dataTableModel.whereFilter += (" AND P.Entidad ='" + dataTableModel.filter.EntidadSearch + "'");
                if (dataTableModel.filter.IdCategoriaSearch != "0")
                    dataTableModel.whereFilter += (" AND P.IdCategoria ='" + dataTableModel.filter.IdCategoriaSearch + "'");
            }
        }
        private void FormatReporteDocumentoDataTable(DataTableModel<ReporteFilterDto> dataTableModel)
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
                if (!string.IsNullOrWhiteSpace(dataTableModel.filter.IdPersonaSearch))
                    dataTableModel.whereFilter += (" AND P.IdPersona ='" + dataTableModel.filter.IdPersonaSearch + "'");
                if (GetPerfil() != "SUPERUSUARIO")
                {
                    var IdEmpresa = GetEmpresaPadre();
                    if (!string.IsNullOrWhiteSpace(dataTableModel.filter.IdEmpresaSearch))
                    {
                        dataTableModel.whereFilter += (" AND P.IdEmpresa ='" + dataTableModel.filter.IdEmpresaSearch + "'");
                    }
                    else
                    {
                        dataTableModel.whereFilter += (" OR P.IdPadre ='" + IdEmpresa + "'");
                        dataTableModel.whereFilter += (" OR P.IdPadreSubcontratista ='" + IdEmpresa + "'");
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(dataTableModel.filter.IdEmpresaSearch))
                        dataTableModel.whereFilter += (" AND P.IdEmpresa ='" + dataTableModel.filter.IdEmpresaSearch + "'");
                }
                if (dataTableModel.filter.IdCategoriaSearch != "0")
                    dataTableModel.whereFilter += (" AND P.IdCategoria ='" + dataTableModel.filter.IdCategoriaSearch + "'");
                if (dataTableModel.filter.IdDepartamentoSearch != "0")
                    dataTableModel.whereFilter += (" AND P.IdDepartamento ='" + dataTableModel.filter.IdDepartamentoSearch + "'");
                if (dataTableModel.filter.IdUbicacionSearch != "0")
                    dataTableModel.whereFilter += (" AND P.IdUbicacion ='" + dataTableModel.filter.IdUbicacionSearch + "'");
                if (!string.IsNullOrWhiteSpace(dataTableModel.filter.FechaFinSearch))
                {
                    var ini = Convert.ToDateTime(dataTableModel.filter.FechaInicioSearch);
                    var fin = Convert.ToDateTime(dataTableModel.filter.FechaFinSearch);
                    dataTableModel.filter.FechaInicioSearch = ini.ToString("yyyy-MM-dd");
                    dataTableModel.filter.FechaFinSearch = fin.ToString("yyyy-MM-dd");
                }
                if (!string.IsNullOrWhiteSpace(dataTableModel.filter.FechaFinSearch))
                    dataTableModel.whereFilter += (" AND CONVERT(VARCHAR(10),P.Fecha,120) between  '" + dataTableModel.filter.FechaInicioSearch + "' and '" + dataTableModel.filter.FechaFinSearch + "'");
            }
        }
        private void FormatReporteEmpresaDataTable(DataTableModel<ReporteFilterDto> dataTableModel)
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
                if (!string.IsNullOrWhiteSpace(dataTableModel.filter.IdPersonaSearch))
                    dataTableModel.whereFilter += (" AND P.IdPersona ='" + dataTableModel.filter.IdPersonaSearch + "'");
                if (dataTableModel.filter.IdCategoriaSearch != "0")
                    dataTableModel.whereFilter += (" AND P.IdCategoria ='" + dataTableModel.filter.IdCategoriaSearch + "'");
                if (GetPerfil() != "SUPERUSUARIO")
                {
                    var IdEmpresa = GetEmpresaPadre();
                    dataTableModel.whereFilter += (" AND P.IdEmpresa ='" + IdEmpresa + "'");
                    dataTableModel.whereFilter += (" OR P.IdPadre ='" + IdEmpresa + "'");
                    dataTableModel.whereFilter += (" OR P.IdPadreSubcontratista ='" + IdEmpresa + "'");
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(dataTableModel.filter.IdEmpresaSearch))
                        dataTableModel.whereFilter += (" AND P.IdEmpresa ='" + dataTableModel.filter.IdEmpresaSearch + "'");
                }
                var tipoEmpresa = dataTableModel.filter.TipoEmpresaSearch;
                if (tipoEmpresa == "PRINCIPAL")
                {
                    dataTableModel.whereFilter += (" AND P.EsPrincipal = 1");
                }
                if (tipoEmpresa == "CONTRATISTA")
                {
                    dataTableModel.whereFilter += (" AND P.EsContratista = 1");
                }
                if (tipoEmpresa == "SUBCONTRATISTA")
                {
                    dataTableModel.whereFilter += (" AND P.EsSubContratista = 1");
                }
            }
        }
        private void FormatReporteColaboradorDataTable(DataTableModel<ReporteFilterDto> dataTableModel)
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
                    dataTableModel.filter.NombreSearch = dataTableModel.filter.NombreSearch;
                else
                    dataTableModel.filter.NombreSearch = "";

                if (!string.IsNullOrWhiteSpace(dataTableModel.filter.IdPersonaSearch))
                    dataTableModel.filter.IdPersonaSearch = dataTableModel.filter.IdPersonaSearch;
                else
                    dataTableModel.filter.IdPersonaSearch = "";

                if (dataTableModel.filter.IdTipoLugarSearch != "0")
                    dataTableModel.filter.IdTipoLugarSearch = dataTableModel.filter.IdTipoLugarSearch;
                else
                    dataTableModel.filter.IdTipoLugarSearch = "";

                if (dataTableModel.filter.IdEmpresaSearch != "0")
                    dataTableModel.filter.IdEmpresaSearch = dataTableModel.filter.IdEmpresaSearch;
                else
                    dataTableModel.filter.IdEmpresaSearch = "";

                if (GetPerfil() != "SUPERUSUARIO")
                {
                    var IdEmpresa = GetEmpresaPadre();
                    dataTableModel.filter.IdEmpresaPrincipal = IdEmpresa;
                }
                else
                {
                    dataTableModel.filter.IdEmpresaPrincipal = "";
                }

                if (dataTableModel.filter.IdDepartamentoSearch != "0")
                    dataTableModel.filter.IdDepartamentoSearch = dataTableModel.filter.IdDepartamentoSearch;
                else
                    dataTableModel.filter.IdDepartamentoSearch = "";

                if (dataTableModel.filter.IdUbicacionSearch != "0")
                    dataTableModel.filter.IdUbicacionSearch = dataTableModel.filter.IdUbicacionSearch;
                else
                    dataTableModel.filter.IdUbicacionSearch = "";

            }
        }
        private void FormatReporteMarcacionesDataTable(DataTableModel<ReporteFilterDto> dataTableModel)
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
                if (GetPerfil() != "SUPERUSUARIO")
                {
                    var IdEmpresa = GetEmpresaPadre();
                    if (!string.IsNullOrWhiteSpace(dataTableModel.filter.IdEmpresaSearch))
                    {
                        dataTableModel.whereFilter += (" AND P.IdEmpresa ='" + dataTableModel.filter.IdEmpresaSearch + "'");
                    }
                    else
                    {
                        dataTableModel.whereFilter += (" OR P.IdPadre ='" + IdEmpresa + "'");
                        dataTableModel.whereFilter += (" OR P.IdPadreSubcontratista ='" + IdEmpresa + "'");
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(dataTableModel.filter.IdEmpresaSearch))
                        dataTableModel.whereFilter += (" AND P.IdEmpresa ='" + dataTableModel.filter.IdEmpresaSearch + "'");
                }
                if (dataTableModel.filter.IdCategoriaSearch != "0")
                    dataTableModel.whereFilter += (" AND P.IdCategoria ='" + dataTableModel.filter.IdCategoriaSearch + "'");
                if (dataTableModel.filter.IdDepartamentoSearch != "0")
                    dataTableModel.whereFilter += (" AND P.IdDepartamento ='" + dataTableModel.filter.IdDepartamentoSearch + "'");
                if (dataTableModel.filter.IdUbicacionSearch != "0")
                    dataTableModel.whereFilter += (" AND P.IdUbicacion ='" + dataTableModel.filter.IdUbicacionSearch + "'");
                if (dataTableModel.filter.IdTerminalSearch != "0")
                    dataTableModel.whereFilter += (" AND P.IdTerminal ='" + dataTableModel.filter.IdTerminalSearch + "'");
                if (!string.IsNullOrWhiteSpace(dataTableModel.filter.FechaFinSearch))
                {
                    var ini = Convert.ToDateTime(dataTableModel.filter.FechaInicioSearch);
                    var fin = Convert.ToDateTime(dataTableModel.filter.FechaFinSearch);
                    dataTableModel.filter.FechaInicioSearch = ini.ToString("yyyy-MM-dd");
                    dataTableModel.filter.FechaFinSearch = fin.ToString("yyyy-MM-dd");
                }
                if (!string.IsNullOrWhiteSpace(dataTableModel.filter.FechaFinSearch))
                    dataTableModel.whereFilter += (" AND CONVERT(VARCHAR(10),P.Fecha,120) between  '" + dataTableModel.filter.FechaInicioSearch + "' and '" + dataTableModel.filter.FechaFinSearch + "'");

            }
        }
        private void FormatReporteAsistenciaDataTable(DataTableModel<ReporteFilterDto> dataTableModel)
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

                if (GetPerfil() != "SUPERUSUARIO")
                {
                    var IdEmpresa = GetEmpresaPadre();
                    dataTableModel.whereFilter += (" AND P.IdEmpresa ='" + IdEmpresa + "'");
                    dataTableModel.whereFilter += (" OR P.IdPadre ='" + IdEmpresa + "'");
                    dataTableModel.whereFilter += (" OR P.IdPadreSubcontratista ='" + IdEmpresa + "'");
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(dataTableModel.filter.IdEmpresaSearch))
                        dataTableModel.whereFilter += (" AND P.IdEmpresa ='" + dataTableModel.filter.IdEmpresaSearch + "'");
                }
                if (dataTableModel.filter.IdCategoriaSearch != "0")
                    dataTableModel.whereFilter += (" AND P.IdCategoria ='" + dataTableModel.filter.IdCategoriaSearch + "'");
                if (dataTableModel.filter.IdDepartamentoSearch != "0")
                    dataTableModel.whereFilter += (" AND P.IdDepartamento ='" + dataTableModel.filter.IdDepartamentoSearch + "'");
                if (dataTableModel.filter.IdUbicacionSearch != "0")
                    dataTableModel.whereFilter += (" AND P.IdUbicacion ='" + dataTableModel.filter.IdUbicacionSearch + "'");
                if (dataTableModel.filter.IdTerminalSearch != "0")
                    dataTableModel.whereFilter += (" AND P.IdTerminal ='" + dataTableModel.filter.IdTerminalSearch + "'");
                if (!string.IsNullOrWhiteSpace(dataTableModel.filter.FechaFinSearch))
                {
                    var ini = Convert.ToDateTime(dataTableModel.filter.FechaInicioSearch);
                    var fin = Convert.ToDateTime(dataTableModel.filter.FechaFinSearch);
                    dataTableModel.filter.FechaInicioSearch = ini.ToString("yyyy-MM-dd");
                    dataTableModel.filter.FechaFinSearch = fin.ToString("yyyy-MM-dd");
                }
                if (!string.IsNullOrWhiteSpace(dataTableModel.filter.FechaFinSearch))
                    dataTableModel.whereFilter += (" AND CONVERT(VARCHAR(10),P.Fecha,120) between  '" + dataTableModel.filter.FechaInicioSearch + "' and '" + dataTableModel.filter.FechaFinSearch + "'");
            }
        }
        private void FormatReporteOchoMarcacionesDataTable(DataTableModel<ReporteFilterDto> dataTableModel)
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
                if (GetPerfil() != "SUPERUSUARIO")
                {
                    var IdEmpresa = GetEmpresaPadre();
                    dataTableModel.whereFilter += (" AND P.IdEmpresa ='" + IdEmpresa + "'");
                    dataTableModel.whereFilter += (" OR P.IdPadre ='" + IdEmpresa + "'");
                    dataTableModel.whereFilter += (" OR P.IdPadreSubcontratista ='" + IdEmpresa + "'");
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(dataTableModel.filter.IdEmpresaSearch))
                        dataTableModel.whereFilter += (" AND P.IdEmpresa ='" + dataTableModel.filter.IdEmpresaSearch + "'");
                }
                if (!string.IsNullOrWhiteSpace(dataTableModel.filter.IdPersonaSearch))
                    dataTableModel.whereFilter += (" AND P.IdEmpresa ='" + dataTableModel.filter.IdPersonaSearch + "'");
                if (dataTableModel.filter.IdCategoriaSearch != "0")
                    dataTableModel.whereFilter += (" AND P.IdCategoria ='" + dataTableModel.filter.IdCategoriaSearch + "'");
                if (dataTableModel.filter.IdDepartamentoSearch != "0")
                    dataTableModel.whereFilter += (" AND P.IdDepartamento ='" + dataTableModel.filter.IdDepartamentoSearch + "'");
                if (dataTableModel.filter.IdUbicacionSearch != "0")
                    dataTableModel.whereFilter += (" AND P.IdUbicacion ='" + dataTableModel.filter.IdUbicacionSearch + "'");
                if (dataTableModel.filter.IdTerminalSearch != "0")
                    dataTableModel.whereFilter += (" AND P.IdTerminal ='" + dataTableModel.filter.IdTerminalSearch + "'");
                if (!string.IsNullOrWhiteSpace(dataTableModel.filter.FechaFinSearch))
                {
                    var ini = Convert.ToDateTime(dataTableModel.filter.FechaInicioSearch);
                    var fin = Convert.ToDateTime(dataTableModel.filter.FechaFinSearch);
                    dataTableModel.filter.FechaInicioSearch = ini.ToString("yyyy-MM-dd");
                    dataTableModel.filter.FechaFinSearch = fin.ToString("yyyy-MM-dd");
                }
                if (!string.IsNullOrWhiteSpace(dataTableModel.filter.FechaFinSearch))
                    dataTableModel.whereFilter += (" AND CONVERT(VARCHAR(10),P.Fecha,120) between  '" + dataTableModel.filter.FechaInicioSearch + "' and '" + dataTableModel.filter.FechaFinSearch + "'");

            }
        }
        #endregion
    }
}