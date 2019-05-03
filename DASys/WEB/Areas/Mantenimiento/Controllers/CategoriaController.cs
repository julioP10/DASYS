﻿using System;
using Entidad;
using Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WEB.Controllers;
using Serilog;
using Microsoft.Extensions.Configuration;

namespace WEB.Categorias.Mantenimiento.Controllers
{
    [Area("Mantenimiento")]
    public class CategoriaController : BaseController
    {
        private readonly ICategoria _Categoria;
        private readonly IConfiguration _configuration;
        public CategoriaController(IConfiguration configuration,IServiceProvider serviceProvider, ICategoria Categoria):base(serviceProvider)
        {
            _Categoria = Categoria;
            this._configuration = configuration;
        }
        #region Metodos publicos
        #region Vistas
        public IActionResult Index()
        {
            var d_configuration = _configuration.GetConnectionString("DefaultConnection");
            return View();
        }


        public IActionResult Registrar()
        {
            CategoriaConsultaDto objCategoriaConsultaDto = new CategoriaConsultaDto();
            objCategoriaConsultaDto.IdEmpresa = GetEmpresaPadre();
            return PartialView("Registrar", objCategoriaConsultaDto);
        }


        [HttpPost]
        public IActionResult Eliminar([FromForm] CategoriaConsultaDto objCategoria)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                var result = _Categoria.EliminarCategoria(objCategoria.IdCategoria, objCategoria.Accion);
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
        public IActionResult Actualizar([FromForm] CategoriaConsultaDto objCategoria)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                objCategoria.IdEmpresa = GetEmpresaPadre();
                objCategoria = _Categoria.ConsultaCategoria(objCategoria);
                if (objCategoria != null)
                {
                    return PartialView("Actualizar", objCategoria);
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
        public IActionResult Consultar([FromForm] CategoriaConsultaDto objCategoria)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                objCategoria = _Categoria.ConsultaCategoria(objCategoria);
                if (objCategoria != null)
                {
                    return PartialView("Consultar", objCategoria);
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
        public IActionResult Mantenimiento([FromForm] Categoria objCategoria)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                var message = (objCategoria.IdCategoria == null) ? Constante.registroExitoso : Constante.actualizacionExitoso;
                objCategoria.IdCategoria = (objCategoria.IdCategoria == null) ? "" : objCategoria.IdCategoria;
                objCategoria.IdEmpresa = GetEmpresaPadre();
                var response = _Categoria.MantenimientoCategoria(objCategoria);
                if (response == -2)
                {
                    jsonResponseDto.Type = Constante.Error.ToLower();
                    jsonResponseDto.IsValid = false;
                    jsonResponseDto.Mensaje = Constante.registroError;
                }
                else if (response>0) {
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
        public IActionResult Listar(DataTableModel<CategoriaFilterDto> dataTableModel)
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

                List<CategoriaPaginationDto> lstCampania = _Categoria.PaginadoCategoria(paginationParameter);
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
        private void FormatDataTable(DataTableModel<CategoriaFilterDto> dataTableModel)
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
                    dataTableModel.whereFilter += (" AND UPPER(P.Nombre) LIKE '%'+'" + dataTableModel.filter.NombreSearch.ToUpper() + "'+'%'");
                if (!string.IsNullOrWhiteSpace(dataTableModel.filter.IdEmpresaSearch))
                    dataTableModel.whereFilter += (" AND P.IdEmpresa ='" + dataTableModel.filter.IdEmpresaSearch + "'");
            }
        }
        #endregion
    }
}