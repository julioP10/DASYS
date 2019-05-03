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
    public class ProveedorController : BaseController
    {
        private readonly IProveedor _Proveedor;
        public ProveedorController(IServiceProvider serviceProvider,IProveedor Proveedor):base(serviceProvider)
        {
            _Proveedor = Proveedor;
        }
        #region Metodos publicos
        #region Vistas
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Registrar()
        {
            ProveedorConsultaDto objProveedorConsultaDto = new ProveedorConsultaDto();
            objProveedorConsultaDto.IdEmpresa = GetEmpresaPadre();
            objProveedorConsultaDto.IdEmpresaPrincipal = GetEmpresaPadre();
            objProveedorConsultaDto.Perfil = GetPerfil();
            return PartialView("Registrar", objProveedorConsultaDto);
        }


        [HttpPost]
        public IActionResult Eliminar([FromForm] ProveedorConsultaDto objProveedor)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                var result = _Proveedor.EliminarProveedor(objProveedor.IdProveedor, objProveedor.Accion);
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
        public IActionResult Actualizar([FromForm] ProveedorConsultaDto objProveedor)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {

                objProveedor = _Proveedor.ConsultaProveedor(objProveedor);
                objProveedor.IdEmpresaPrincipal = GetEmpresaPadre();
                objProveedor.Perfil = GetPerfil();
                if (objProveedor != null)
                {
                    return PartialView("Actualizar", objProveedor);
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
        public IActionResult Consultar([FromForm] ProveedorConsultaDto objProveedor)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {

                objProveedor = _Proveedor.ConsultaProveedor(objProveedor);
                objProveedor.IdEmpresaPrincipal = GetEmpresaPadre();
                objProveedor.Perfil = GetPerfil();
                if (objProveedor != null)
                {
                    return PartialView("Consultar", objProveedor);
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
        public IActionResult Mantenimiento([FromForm] Proveedor objProveedor)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                var message = (objProveedor.IdProveedor == null) ? Constante.registroExitoso : Constante.actualizacionExitoso;
                objProveedor.IdProveedor = (objProveedor.IdProveedor == null) ? "" : objProveedor.IdProveedor;
                var response = _Proveedor.MantenimientoProveedor(objProveedor);
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
        public IActionResult Listar(DataTableModel<ProveedorFilterDto> dataTableModel)
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

                List<ProveedorPaginationDto> lstCampania = _Proveedor.PaginadoProveedor(paginationParameter);
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
        private void FormatDataTable(DataTableModel<ProveedorFilterDto> dataTableModel)
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
            if (tipo == "SUPERUSUARIO")
            {
                dataTableModel.filter.IdEmpresaSearch = "";
            }
            else
            {
                dataTableModel.filter.IdEmpresaSearch = GetEmpresaPadre();
            }

            if (dataTableModel.filter != null)
            {
                if (!string.IsNullOrWhiteSpace(dataTableModel.filter.NombreSearch))
                    dataTableModel.whereFilter += (" AND UPPER(P.Nombre)LIKE '%'+'" + dataTableModel.filter.NombreSearch.ToUpper() + "'+'%'");
                if (!string.IsNullOrWhiteSpace(dataTableModel.filter.IdEmpresaSearch))
                    dataTableModel.whereFilter += (" AND P.IdEmpresa ='" + dataTableModel.filter.IdEmpresaSearch + "'");
            }
        }
        #endregion
    }
}