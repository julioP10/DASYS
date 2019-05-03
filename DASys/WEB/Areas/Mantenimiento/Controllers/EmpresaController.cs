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
    public class EmpresaController : BaseController
    {
        private readonly IEmpresa _Empresa;
        public EmpresaController(IServiceProvider serviceProvider, IEmpresa Empresa)
                   : base(serviceProvider)
        {
            _Empresa = Empresa;
        }
        #region Metodos publicos
        #region Vistas
        public IActionResult Index()
        {
            EmpresaFilterDto objEmpresaFilterDto = new EmpresaFilterDto();
            objEmpresaFilterDto.TipoEmpresa = GetTipoEmpresa();
            return View(objEmpresaFilterDto);
        }
        //public IActionResult Contratista()
        //{
        //    EmpresaFilterDto objEmpresaFilterDto = new EmpresaFilterDto();
        //    objEmpresaFilterDto.EsPrincipalSearch = 0;
        //    objEmpresaFilterDto.EsContratistaSearch = 1;
        //    objEmpresaFilterDto.EsSubContratistaSearch = 0;
        //    return View(objEmpresaFilterDto);
        //}
        //public IActionResult SubContratista()
        //{
        //    EmpresaFilterDto objEmpresaFilterDto = new EmpresaFilterDto();
        //    objEmpresaFilterDto.EsPrincipalSearch = 0;
        //    objEmpresaFilterDto.EsContratistaSearch = 0;
        //    objEmpresaFilterDto.EsSubContratistaSearch = 1;
        //    return View(objEmpresaFilterDto);
        //}

        public IActionResult Registrar([FromForm]  EmpresaConsultaDto objEmpresaConsultaDto)
        {

            objEmpresaConsultaDto.Tipo = GetTipoEmpresa();
            objEmpresaConsultaDto.Perfil = GetPerfil();
            if (GetPerfil() != "SUPERUSUARIO")
                objEmpresaConsultaDto.IdEmpresaPrincipal = GetEmpresaPadre();
            else
                objEmpresaConsultaDto.IdEmpresaPrincipal = "";

            return PartialView("Registrar", objEmpresaConsultaDto);
        }
        public IActionResult RegistrarParametros([FromForm]  ParametrosCorreoDto objParametrosCorreoDto)
        {
            objParametrosCorreoDto = _Empresa.ListadoParametrosCorreo(objParametrosCorreoDto).FirstOrDefault();
            if (objParametrosCorreoDto == null)
            {
                objParametrosCorreoDto = new ParametrosCorreoDto();
            }
            return PartialView("RegistrarParametros", objParametrosCorreoDto);
        }

        [HttpPost]
        public IActionResult Eliminar([FromForm] EmpresaConsultaDto objEmpresa)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                var result = _Empresa.EliminarEmpresa(objEmpresa.IdEmpresa, objEmpresa.Accion);
                var mensaje = result.Split("|");
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
        public IActionResult Actualizar([FromForm] EmpresaConsultaDto objEmpresa)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                objEmpresa.Perfil = GetPerfil();
                objEmpresa = _Empresa.ConsultaEmpresa(objEmpresa);
                objEmpresa.Tipo = GetTipoEmpresa();
                if (objEmpresa != null)
                {
                    objEmpresa.Tipo = GetTipoEmpresa();
                    if (GetPerfil() != "SUPERUSUARIO")
                        objEmpresa.IdEmpresaPrincipal = GetEmpresaPadre();

                    else
                        objEmpresa.IdEmpresaPrincipal = "";

                    return PartialView("Actualizar", objEmpresa);
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
        public IActionResult Consultar([FromForm] EmpresaConsultaDto objEmpresa)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                objEmpresa.TipoEmpresa = GetTipoEmpresa();
                objEmpresa.Perfil = GetPerfil();
                objEmpresa = _Empresa.ConsultaEmpresa(objEmpresa);
                if (objEmpresa != null)
                {
                    objEmpresa.Tipo = GetTipoEmpresa();
                    if (GetPerfil() != "SUPERUSUARIO")
                        objEmpresa.IdEmpresaPrincipal = GetEmpresaPadre();
                    else
                        objEmpresa.IdEmpresaPrincipal = "";

                    return PartialView("Consultar", objEmpresa);
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
        public IActionResult Mantenimiento([FromForm] Empresa objEmpresa)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                var message = (objEmpresa.IdEmpresa == null) ? Constante.registroExitoso : Constante.actualizacionExitoso;



                if (objEmpresa.EsPrincipal == 1)
                    objEmpresa.IdPadre = null;

                if (GetPerfil() != "SUPERUSUARIO")
                {
                    if (objEmpresa.EsContratista == 1)
                    {
                        objEmpresa.IdPadre = GetEmpresa();
                    }
                }
                if (GetTipoEmpresa().ToUpper() == "CONTRATISTA")
                {
                    objEmpresa.IdEmpresa = (objEmpresa.IdEmpresa == null) ? "" : objEmpresa.IdEmpresa;
                    objEmpresa.IdPadre = GetEmpresa();
                    objEmpresa.EsSubContratista = 1;
                }
                else
                {
                    objEmpresa.IdEmpresa = (objEmpresa.IdEmpresa == null) ? "" : objEmpresa.IdEmpresa;
                    objEmpresa.IdPadre = ((objEmpresa.IdPadre == null || objEmpresa.IdPadre == "" || objEmpresa.IdPadre == "0") && objEmpresa.EsPrincipal != 1) ? GetEmpresaPadre() : objEmpresa.IdPadre;
                }
                var response = _Empresa.MantenimientoEmpresa(objEmpresa);
                var mensaje = response.Split(":");

                if (mensaje[0].ToUpper()==Constante.Success.ToUpper())
                {
                    jsonResponseDto.Type = mensaje[0];
                    jsonResponseDto.IsValid = true;
                    jsonResponseDto.Mensaje = mensaje[1];
                }
                else
                {
                    jsonResponseDto.Type = mensaje[0];
                    jsonResponseDto.IsValid = false;
                    jsonResponseDto.Mensaje = mensaje[1];
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
        public IActionResult MantenimientoParametros([FromForm] ParametrosCorreoDto objParametrosCorreoDto)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                objParametrosCorreoDto.Password = Encriptador.Encriptar(objParametrosCorreoDto.Password);
                var message = (objParametrosCorreoDto.IdParametros == null) ? Constante.registroExitoso : Constante.actualizacionExitoso;
                objParametrosCorreoDto.IdParametros = (objParametrosCorreoDto.IdParametros == null) ? "" : objParametrosCorreoDto.IdParametros;
                objParametrosCorreoDto.Password = (objParametrosCorreoDto.Password == null) ? "" : objParametrosCorreoDto.Password;
                var response = _Empresa.MantenimientoParametrosCorreo(objParametrosCorreoDto);
                if (response >= 1)
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
        public IActionResult Listar(DataTableModel<EmpresaFilterDto> dataTableModel)
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

                List<EmpresaPaginationDto> lstCampania = _Empresa.PaginadoEmpresa(paginationParameter);
                if (!string.IsNullOrWhiteSpace(dataTableModel.filter.DigitalizacionSearch))
                {
                    lstCampania = lstCampania.Where(p => p.Digitalizacion == dataTableModel.filter.DigitalizacionSearch).ToList();
                }
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
        private void FormatDataTable(DataTableModel<EmpresaFilterDto> dataTableModel)
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
                    dataTableModel.whereFilter += (" AND P.IdEmpresa ='" + idEmpresa + "' OR P.IdPadre ='" + idEmpresa + "' OR P.IdPadreSubcontratista ='" + idEmpresa + "'");

                    //dataTableModel.whereFilter += (" AND P.EsPrincipal ='" + 1 + "' OR P.EsContratista ='" + 1 + "' OR P.EsSubContratista ='" + 1 + "'");
                    if (!string.IsNullOrWhiteSpace(dataTableModel.filter.NombreSearch))
                        dataTableModel.whereFilter += (" AND (P.RazonSocial) LIKE '%'+'" + dataTableModel.filter.NombreSearch.ToUpper() + "'+'%'");
                }
            }
            else if (tipo_empresa == "CONTRATISTA")
            {
                if (dataTableModel.filter != null)
                {
                    dataTableModel.whereFilter += (" AND P.IdEmpresa ='" + idEmpresa + "' OR P.IdPadre ='" + idEmpresa + "'");
                    //dataTableModel.whereFilter += (" AND P.EsContratista ='" + 1 + "' OR P.EsSubContratista ='" + 1 + "'");
                    if (!string.IsNullOrWhiteSpace(dataTableModel.filter.NombreSearch))
                        dataTableModel.whereFilter += (" AND (P.RazonSocial) LIKE '%'+'" + dataTableModel.filter.NombreSearch.ToUpper() + "'+'%'");
                }
            }
            else if (tipo_empresa == "SUBCONTRATISTA")
            {
                if (dataTableModel.filter != null)
                {
                    dataTableModel.whereFilter += (" AND P.IdEmpresa ='" + idEmpresa + "'");
                    //dataTableModel.whereFilter += (" AND P.EsSubContratista ='" + 1 + "'");
                    if (!string.IsNullOrWhiteSpace(dataTableModel.filter.NombreSearch))
                        dataTableModel.whereFilter += (" AND (P.RazonSocial) LIKE '%'+'" + dataTableModel.filter.NombreSearch.ToUpper() + "'+'%'");
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(dataTableModel.filter.NombreSearch))
                    dataTableModel.whereFilter += (" AND (P.RazonSocial) LIKE '%'+'" + dataTableModel.filter.NombreSearch.ToUpper() + "'+'%'");

            }

        }
        #endregion
    }
}