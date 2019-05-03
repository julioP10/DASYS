using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Entidad;
using Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using WEB.Controllers;

namespace WEB.Areas.Digitalizacion.Controllers
{
    [Area("Digitalizacion")]
    public class DigitalizacionController : BaseController
    {

        private readonly IDigitalizacion _Digitalizacion;
        private readonly IDocumentoAdjunto _DocumentoAdjunto;
        private readonly IColaborador _Colaborador;
        private readonly IVehiculo _Vehiculo;
        private readonly IMaquinaria _Maquinaria;
        private readonly IEmpresa _Empresa;
        private readonly IUsuario _Usuario;
        public DigitalizacionController(IServiceProvider serviceProvider, IDigitalizacion Digitalizacion, IColaborador Colaborador, IVehiculo Vehiculo, IEmpresa Empresa, IDocumentoAdjunto DocumentoAdjunto, IUsuario Usuario, IMaquinaria Maquinaria) : base(serviceProvider)
        {
            _Digitalizacion = Digitalizacion;
            _Colaborador = Colaborador;
            _Vehiculo = Vehiculo;
            _Empresa = Empresa;
            _DocumentoAdjunto = DocumentoAdjunto;
            _Usuario = Usuario;
            _Maquinaria = Maquinaria;
        }
        //[HttpPost]
        #region Correo
        public IActionResult EnviarCorreo([FromForm] CorreoConsultaDto objCorreoConsultaDto)
        {
            var jsonResponse = new JsonResponseDto();
            var resultado = "";
            var empresaCorreo = GetEmpresaPadre();
            var usuarioActual = new UsuarioActualDto();
            var objParametros = new ParametrosCorreoDto();
            List<UsuarioConsultaDto> lstUsuarios = new List<UsuarioConsultaDto>();

            try
            {


                //Usuario actual
                objParametros.IdEmpresa = empresaCorreo;
                usuarioActual.IdEmpresa = empresaCorreo;
                //Parametros para el correo emisor
                objParametros = _Empresa.ConsultaParametrosCorreo(objParametros);
                if (string.IsNullOrWhiteSpace(objParametros.Correo))
                {
                    jsonResponse.Mensaje = "Notique a la empresa Principal que no cuenta ccon la configuracion de envio de correo";
                    jsonResponse.IsValid = false;
                    jsonResponse.Type = Constante.Warning;
                    return Json(jsonResponse);
                }
                //Lista de usuarios de la empresa actual
                lstUsuarios = _Usuario.ListadoUsuario(usuarioActual);
                if (lstUsuarios.Count == 0)
                {
                    jsonResponse.Mensaje = "No hay usuarios registrados";
                    jsonResponse.IsValid = false;
                    jsonResponse.Type = Constante.Warning;
                    return Json(jsonResponse);
                }
                objCorreoConsultaDto.Usuario = GetNombreUsuarioActual();
                //Lista de entidades
                if (objCorreoConsultaDto.TipoEntidad.ToUpper() == "COLABORADOR")
                {
                    var lstColaboradores = _Colaborador.ListadoColaborador(GetEmpresa());
                    var lstFilterEnviado = lstColaboradores.Where(p => p.Enviado == 1 && p.Digitalizado > 0).ToList();
                    var Perfil = GetPerfil();

                    if (Perfil != "SUPERUSUARIO")
                    {
                        lstFilterEnviado.Where(p => p.IdUsuario == GetUsuarioActual()).ToList();
                    }
                    if (lstFilterEnviado.Count == 0)
                    {
                        jsonResponse.Mensaje = "No hay correo pendiente a envia en el estado actual";
                        jsonResponse.IsValid = false;
                        jsonResponse.Type = Constante.Warning;
                        return Json(jsonResponse);
                    }
                    //Enviar correo
                    var excel = GenerarColaborador();
                    resultado = EnviarCorreoDigitalizacion(objParametros, lstUsuarios, objCorreoConsultaDto, excel);
                }
                if (objCorreoConsultaDto.TipoEntidad.ToUpper() == "EMPRESA")
                {
                    var lstEmpresa = _Empresa.ListadoEmpresa(GetEmpresaPadre());
                    var lstFilterEnviado = lstEmpresa.Where(p => p.Enviado == 1 && p.Digitalizado > 0).ToList();
                    var Perfil = GetPerfil();

                    if (Perfil != "SUPERUSUARIO")
                    {
                        lstFilterEnviado.Where(p => p.IdUsuario == GetUsuarioActual()).ToList();
                    }
                    if (lstFilterEnviado.Count == 0)
                    {
                        jsonResponse.Mensaje = "No hay correo pendiente a envia en el estado actual";
                        jsonResponse.IsValid = false;
                        jsonResponse.Type = Constante.Warning;
                        return Json(jsonResponse);
                    }
                    //Enviar correo
                    var excel = GenerarEmpresa();

                    resultado = EnviarCorreoDigitalizacion(objParametros, lstUsuarios, objCorreoConsultaDto, excel);
                }
                if (objCorreoConsultaDto.TipoEntidad.ToUpper() == "VEHICULO")
                {
                    var lstVehiculo = _Vehiculo.ListadoVehiculo(GetEmpresaPadre());
                    var lstFilterEnviado = lstVehiculo.Where(p => p.Enviado == 1 && p.Digitalizado > 0).ToList();
                    var Perfil = GetPerfil();

                    if (Perfil != "SUPERUSUARIO")
                    {
                        lstFilterEnviado.Where(p => p.IdUsuario == GetUsuarioActual()).ToList();
                    }
                    if (lstFilterEnviado.Count == 0)
                    {
                        jsonResponse.Mensaje = "No hay correo pendiente a envia en el estado actual";
                        jsonResponse.IsValid = false;
                        jsonResponse.Type = Constante.Warning;
                        return Json(jsonResponse);
                    }
                    //Enviar correo
                    var excel = GenerarVehiculo();

                    resultado = EnviarCorreoDigitalizacion(objParametros, lstUsuarios, objCorreoConsultaDto, excel);
                }

                if (objCorreoConsultaDto.TipoEntidad.ToUpper() == "MAQUINARIA")
                {
                    var lstMaquinaria = _Maquinaria.ListadoMaquinaria(GetEmpresaPadre());
                    var lstFilterEnviado = lstMaquinaria.Where(p => p.Enviado == 1 && p.Digitalizado > 0).ToList();
                    var Perfil = GetPerfil();

                    if (Perfil != "SUPERUSUARIO")
                    {
                        lstFilterEnviado.Where(p => p.IdUsuario == GetUsuarioActual()).ToList();
                    }
                    if (lstFilterEnviado.Count == 0)
                    {
                        jsonResponse.Mensaje = "No hay correo pendiente a envia en el estado actual";
                        jsonResponse.IsValid = false;
                        jsonResponse.Type = Constante.Warning;
                        return Json(jsonResponse);
                    }
                    //Enviar correo
                    var excel = GenerarMaquinaria();

                    resultado = EnviarCorreoDigitalizacion(objParametros, lstUsuarios, objCorreoConsultaDto, excel);
                }

                jsonResponse.Mensaje = resultado;
                jsonResponse.IsValid = true;
                jsonResponse.Type = Constante.Warning;
                return Json(jsonResponse);
            }
            catch (Exception ex)
            {
                jsonResponse.Mensaje = ex.Message.ToString();
                jsonResponse.IsValid = false;
                jsonResponse.Type = Constante.Warning;
                return Json(jsonResponse);
            }
        }
        #endregion
        public class Personas
        {
            public string CodigoPersona { get; set; }
            public string nombre { get; set; }
        }
        public class Documentos
        {
            public string Codigo { get; set; }
            public string CodigoPersona { get; set; }
            public string nombre { get; set; }
        }

        #region Empresa
        public IActionResult DigitalizacionEmpresa()
        {
            EmpresaDigitalizacionFilterDto obj = new EmpresaDigitalizacionFilterDto();
            return PartialView("DigitalizacionEmpresa", obj);
        }
        [HttpPost]
        public IActionResult ListarEmpresa(DataTableModel<EmpresaDigitalizacionFilterDto> dataTableModel)
        {
            var jsonResponse = new JsonResponseDto() { Type = Constante.Error };

            try
            {
                FormatDataTableEmpresa(dataTableModel);
                var jsonResponseDto = new JsonResponseDto() { Type = Constante.Error };

                var paginationParameter = new PaginationParameter
                {
                    Start = dataTableModel.start,
                    AmountRows = dataTableModel.length,
                    WhereFilter = dataTableModel.whereFilter,
                    OrderBy = dataTableModel.orderBy
                };

                List<EmpresaDigitalizacionPaginationDto> lstCampania = _Empresa.PaginadoEmpresaDigitalizacion(paginationParameter);
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


        [HttpPost]
        public IActionResult RegistrarEmpresa([FromForm] Entidad.Digitalizacion objDigitalizacion)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                objDigitalizacion.IdUsuario = GetUsuarioActual();
                var message = (objDigitalizacion.IdDigitalizacion == null) ? Constante.registroExitoso : Constante.actualizacionExitoso;
                if (!string.IsNullOrEmpty(objDigitalizacion.FechaVencimiento))
                {
                    DateTime _hoy = DateTime.Parse(objDigitalizacion.FechaVencimiento);
                    string _hoyFormato = _hoy.ToString("yyyy/MM/dd");
                    objDigitalizacion.FechaVencimiento = _hoyFormato;
                }
                if (objDigitalizacion.IdDigitalizacionDesaprobado != null)
                {
                    if (objDigitalizacion.IdDigitalizacionDesaprobado != "")
                    {
                        string[] separador = { ",", ".", "!", "?", ";", ":", " " };
                        string[] id = objDigitalizacion.IdDigitalizacionDesaprobado.Split(separador, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in id)
                        {
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Documentos", item);
                            if (Directory.Exists(path))
                                Directory.Delete(path, true);
                            var ok = _Digitalizacion.EliminarDigitalizacion(item);
                        }
                    }
                }
                objDigitalizacion.IdDigitalizacion = (objDigitalizacion.IdDigitalizacion == null) ? "" : objDigitalizacion.IdDigitalizacion;
                var response = _Digitalizacion.MantenimientoDigitalizacion(objDigitalizacion);
                if (response != "-1" || response != "-2")
                {
                    jsonResponseDto.Type = Constante.Success.ToLower();
                    jsonResponseDto.IsValid = true;
                    jsonResponseDto.Mensaje = message;
                    jsonResponseDto.data = response;
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
                // Log.Error(ex.Message);
                jsonResponseDto.Type = Constante.Warning.ToLower();
                jsonResponseDto.IsValid = false;
                jsonResponseDto.Mensaje = ex.Message.ToString();
            }
            return Json(jsonResponseDto);

        }

        public Byte[] GenerarEmpresa()
        {
            //lista de personas colaboradores
            var lstEmpresa = _Empresa.ListadoEmpresa(GetEmpresaPadre());
            var lstFilterEnviado = lstEmpresa.Where(p => p.Enviado == 1 && p.Digitalizado > 0).ToList();
            var Perfil = GetPerfil();

            if (Perfil != "SUPERUSUARIO")
            {
                lstFilterEnviado.Where(p => p.IdUsuario == GetUsuarioActual()).ToList();
            }

            DateTime hoy = DateTime.Now;
            var hoyFormater = hoy.ToString("dd-MM-yyyy");
            var NombreArchivo = "DigitalizacionEmpresa" + "-(" + hoyFormater + ")";
            byte[] fileContents;
            ExcelPackage excel = new ExcelPackage();
            string nameExcelContent = "Digitalizacion";
            string[] cabeceras = { "Codigo", "Ruc", "Empresa", "Categoria", "Documento", "Observacion", "Obligatorio", "Con Fecha Vencimiento", "Estado" };

            var workSheet = excel.Workbook.Worksheets.Add(nameExcelContent);
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;
            //Header of table

            workSheet.Cells["A1:I1"].Merge = true;
            workSheet.Cells[1, 1].Value = "Lista de estados de digitalizacion de empresas  - " + GetNombreEmpresa();
            workSheet.Cells["A1:I1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["A1:I1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            int PosicionCabecera = 0;
            int PosicionDetalle = 0;
            int PosicionFila = 2;
            int PositionFilaDetalle = 0;
            foreach (var iteml in lstFilterEnviado)
            {



                PosicionFila++;
                PosicionFila++;
                int rangoDerecha = 9;
                int rangoAbajo = PosicionFila; //rangoAbajo igual a rango rangoArriba
                int rangoIzquierda = 1;
                int rangoArriba = PosicionFila;
                int _rangoArriba = 0;
                int _rangoAbajo = 0;
                // Armar la cabecera
                int _detalleCabecara = rangoAbajo - 1;
                workSheet.Cells["A" + _detalleCabecara + ":I" + _detalleCabecara].Merge = true;
                workSheet.Cells[_detalleCabecara, 1].Value = iteml.Mensaje;
                workSheet.Cells["A" + _detalleCabecara + ":I" + _detalleCabecara].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells["A" + _detalleCabecara + ":I" + _detalleCabecara].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                if (cabeceras.Length > 0)
                {
                    foreach (string item in cabeceras)
                    {
                        PosicionCabecera++;
                        workSheet.Cells[PosicionFila, PosicionCabecera].Value = item;
                    }


                    using (ExcelRange range = workSheet.Cells[rangoArriba, rangoIzquierda, rangoAbajo, rangoDerecha])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.LightGreen);

                        range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Top.Color.SetColor(Color.Black);
                        range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Left.Color.SetColor(Color.Black);
                        range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Right.Color.SetColor(Color.Black);
                        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Bottom.Color.SetColor(Color.Black);
                    }
                    _rangoAbajo = rangoAbajo + 1;
                    _rangoArriba = rangoArriba + 1;
                }
                PosicionFila++;

                //Armar las columnas de los datos
                foreach (DigitalizacionExcelDto _object in _Empresa.ListaEmpresaDigitalizacionExcel(iteml.IdPersona).ToList())
                {
                    foreach (var oPropiedadValor in _object.GetType().GetProperties())
                    {
                        PositionFilaDetalle++;
                        var positionValue = oPropiedadValor.GetValue(_object);
                        var nombre = _object.GetType().GetProperty(oPropiedadValor.Name);
                        if (positionValue != null)
                        {
                            workSheet.Cells[PosicionFila, PositionFilaDetalle].Value = positionValue;
                        }
                        else
                        {
                            workSheet.Cells[PosicionFila, PositionFilaDetalle].Value = "";
                        }
                        workSheet.Column(PositionFilaDetalle).AutoFit();
                    }
                    //Styles cells
                    //rangoArriba  igual a  rangoAbajo

                    using (ExcelRange range = workSheet.Cells[_rangoArriba, rangoIzquierda, _rangoAbajo, rangoDerecha])
                    {
                        _rangoAbajo++;
                        _rangoArriba++;
                        range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Top.Color.SetColor(Color.Black);
                        range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Left.Color.SetColor(Color.Black);
                        range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Right.Color.SetColor(Color.Black);
                        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Bottom.Color.SetColor(Color.Black);
                    }
                    //workSheet.Row(PosicionDetalle).Height = sizeFont;
                    PositionFilaDetalle = 0;
                    PosicionDetalle++;
                    PosicionFila++;
                }

                PosicionDetalle = 0;
                PosicionCabecera = 0;
            }
            //
            //
            fileContents = excel.GetAsByteArray();
            return fileContents;
            //if (fileContents == null || fileContents.Length == 0)
            //{
            //    return NotFound();
            //}

            //return File(
            //    fileContents: fileContents,
            //    contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            //    fileDownloadName: NombreArchivo + ".xlsx"
            //);
        }
        #endregion

        #region Colaborador 
        public IActionResult DigitalizacionColaborador()
        {
            ColaboradorDigitalizacionFilterDto obj = new ColaboradorDigitalizacionFilterDto();
            return PartialView("DigitalizacionColaborador", obj);
        }
        [HttpPost]
        public IActionResult ListarColaborador(DataTableModel<ColaboradorDigitalizacionFilterDto> dataTableModel)
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

                List<ColaboradorDigitalizacionPaginationDto> lstColaborador = _Colaborador.PaginadoColaboradorDigitalizacion(paginationParameter);
                dataTableModel.data = lstColaborador;
                if (lstColaborador.Count > 0)
                {
                    dataTableModel.recordsTotal = lstColaborador[0].Cantidad;
                    dataTableModel.recordsFiltered = dataTableModel.recordsTotal;
                }
            }


            catch (Exception ex)
            {
                jsonResponse.Mensaje = ex.Message;
            }
            return Json(dataTableModel);
        }

        [HttpPost]
        public IActionResult RegistrarColaborador([FromForm] Entidad.Digitalizacion objDigitalizacion)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                objDigitalizacion.IdUsuario = GetUsuarioActual();
                var message = (objDigitalizacion.IdDigitalizacion == null) ? Constante.registroExitoso : Constante.actualizacionExitoso;
                if (!string.IsNullOrEmpty(objDigitalizacion.FechaVencimiento))
                {
                    DateTime _hoy = DateTime.Parse(objDigitalizacion.FechaVencimiento);
                    string _hoyFormato = _hoy.ToString("yyyy/MM/dd");
                    objDigitalizacion.FechaVencimiento = _hoyFormato;
                }
                if (objDigitalizacion.IdDigitalizacionDesaprobado != null)
                {
                    if (objDigitalizacion.IdDigitalizacionDesaprobado != "")
                    {
                        string[] separador = { ",", ".", "!", "?", ";", ":", " " };
                        string[] id = objDigitalizacion.IdDigitalizacionDesaprobado.Split(separador, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in id)
                        {
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Documentos", item);
                            if (Directory.Exists(path))
                                Directory.Delete(path, true);
                            var ok = _Digitalizacion.EliminarDigitalizacion(item);
                        }
                    }
                }
                objDigitalizacion.IdDigitalizacion = (objDigitalizacion.IdDigitalizacion == null) ? "" : objDigitalizacion.IdDigitalizacion;
                var response = _Digitalizacion.MantenimientoDigitalizacion(objDigitalizacion);
                if (response != "-1" || response != "-2")
                {
                    jsonResponseDto.Type = Constante.Success.ToLower();
                    jsonResponseDto.IsValid = true;
                    jsonResponseDto.Mensaje = message;
                    jsonResponseDto.data = response;
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
                // Log.Error(ex.Message);
                jsonResponseDto.Type = Constante.Warning.ToLower();
                jsonResponseDto.IsValid = false;
                jsonResponseDto.Mensaje = ex.Message.ToString();
            }
            return Json(jsonResponseDto);

        }

        public Byte[] GenerarColaborador()
        {
            //lista de personas colaboradores
            var _empresa = GetEmpresa();
            var lstColaboradores = _Colaborador.ListadoColaborador(GetEmpresa());
            var lstFilterEnviado = lstColaboradores.Where(p => p.Enviado == 1 && p.Digitalizado > 0).ToList();
            var Perfil = GetPerfil();

            if (Perfil != "SUPERUSUARIO")
            {
                lstFilterEnviado.Where(p => p.IdUsuario == GetUsuarioActual()).ToList();
            }

            DateTime hoy = DateTime.Now;
            var hoyFormater = hoy.ToString("dd-MM-yyyy");
            var NombreArchivo = "Digitalizacion" + "-(" + hoyFormater + ")";
            byte[] fileContents;
            ExcelPackage excel = new ExcelPackage();
            string nameExcelContent = "Digitalizacion";
            string[] cabeceras = { "Codigo", "Documento Identidad", "Empresa", "Categoria", "Documento", "Observacion", "Obligatorio", "Con Fecha Vencimiento", "Estado" };

            var workSheet = excel.Workbook.Worksheets.Add(nameExcelContent);
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;
            //Header of table

            workSheet.Cells["A1:I1"].Merge = true;
            workSheet.Cells[1, 1].Value = "Lista de estados de digitalizacion de colaboradores  - " + GetNombreEmpresa();
            workSheet.Cells["A1:I1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["A1:I1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            int PosicionCabecera = 0;
            int PosicionDetalle = 0;
            int PosicionFila = 2;
            int PositionFilaDetalle = 0;
            foreach (var iteml in lstFilterEnviado)
            {



                PosicionFila++;
                PosicionFila++;
                int rangoDerecha = 9;
                int rangoAbajo = PosicionFila; //rangoAbajo igual a rango rangoArriba
                int rangoIzquierda = 1;
                int rangoArriba = PosicionFila;
                int _rangoArriba = 0;
                int _rangoAbajo = 0;
                // Armar la cabecera
                int _detalleCabecara = rangoAbajo - 1;
                workSheet.Cells["A" + _detalleCabecara + ":I" + _detalleCabecara].Merge = true;
                workSheet.Cells[_detalleCabecara, 1].Value = iteml.Mensaje;
                workSheet.Cells["A" + _detalleCabecara + ":I" + _detalleCabecara].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells["A" + _detalleCabecara + ":I" + _detalleCabecara].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                if (cabeceras.Length > 0)
                {
                    foreach (string item in cabeceras)
                    {
                        PosicionCabecera++;
                        workSheet.Cells[PosicionFila, PosicionCabecera].Value = item;
                    }


                    using (ExcelRange range = workSheet.Cells[rangoArriba, rangoIzquierda, rangoAbajo, rangoDerecha])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.LightGreen);

                        range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Top.Color.SetColor(Color.Black);
                        range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Left.Color.SetColor(Color.Black);
                        range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Right.Color.SetColor(Color.Black);
                        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Bottom.Color.SetColor(Color.Black);
                    }
                    _rangoAbajo = rangoAbajo + 1;
                    _rangoArriba = rangoArriba + 1;
                }
                PosicionFila++;

                //Armar las columnas de los datos
                foreach (DigitalizacionExcelDto _object in _Colaborador.ListaColaboradorDigitalizacionExcel(iteml.IdPersona).ToList())
                {
                    foreach (var oPropiedadValor in _object.GetType().GetProperties())
                    {
                        PositionFilaDetalle++;
                        var positionValue = oPropiedadValor.GetValue(_object);
                        var nombre = _object.GetType().GetProperty(oPropiedadValor.Name);
                        if (positionValue != null)
                        {
                            workSheet.Cells[PosicionFila, PositionFilaDetalle].Value = positionValue;
                        }
                        else
                        {
                            workSheet.Cells[PosicionFila, PositionFilaDetalle].Value = "";
                        }
                        workSheet.Column(PositionFilaDetalle).AutoFit();
                    }
                    //Styles cells
                    //rangoArriba  igual a  rangoAbajo

                    using (ExcelRange range = workSheet.Cells[_rangoArriba, rangoIzquierda, _rangoAbajo, rangoDerecha])
                    {
                        _rangoAbajo++;
                        _rangoArriba++;
                        range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Top.Color.SetColor(Color.Black);
                        range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Left.Color.SetColor(Color.Black);
                        range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Right.Color.SetColor(Color.Black);
                        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Bottom.Color.SetColor(Color.Black);
                    }
                    //workSheet.Row(PosicionDetalle).Height = sizeFont;
                    PositionFilaDetalle = 0;
                    PosicionDetalle++;
                    PosicionFila++;
                }

                PosicionDetalle = 0;
                PosicionCabecera = 0;
            }
            //
            //
            fileContents = excel.GetAsByteArray();
            return fileContents;
            //if (fileContents == null || fileContents.Length == 0)
            //{
            //    return NotFound();
            //}

            //return File(
            //    fileContents: fileContents,
            //    contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            //    fileDownloadName: NombreArchivo + ".xlsx"
            //);
        }
        #endregion

        #region Vehiculo 
        public IActionResult DigitalizacionVehiculo()
        {
            VehiculoDigitalizacionFilterDto obj = new VehiculoDigitalizacionFilterDto();
            return PartialView("DigitalizacionVehiculo", obj);
        }
        [HttpPost]
        public IActionResult ListarVehiculo(DataTableModel<VehiculoDigitalizacionFilterDto> dataTableModel)
        {
            var jsonResponse = new JsonResponseDto() { Type = Constante.Error };

            try
            {
                FormatDataTableVehiculo(dataTableModel);
                var jsonResponseDto = new JsonResponseDto() { Type = Constante.Error };

                var paginationParameter = new PaginationParameter
                {
                    Start = dataTableModel.start,
                    AmountRows = dataTableModel.length,
                    WhereFilter = dataTableModel.whereFilter,
                    OrderBy = dataTableModel.orderBy
                };

                List<VehiculoDigitalizacionPaginationDto> lstvehiculo = _Vehiculo.PaginadoVehiculoDigitalizacion(paginationParameter);
                dataTableModel.data = lstvehiculo;
                if (lstvehiculo.Count > 0)
                {
                    dataTableModel.recordsTotal = lstvehiculo[0].Cantidad;
                    dataTableModel.recordsFiltered = dataTableModel.recordsTotal;
                }
            }


            catch (Exception ex)
            {
                jsonResponse.Mensaje = ex.Message;
            }
            return Json(dataTableModel);
        }

        [HttpPost]
        public IActionResult RegistrarVehiculo([FromForm] Entidad.Digitalizacion objDigitalizacion)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                objDigitalizacion.IdUsuario = GetUsuarioActual();
                var message = (objDigitalizacion.IdDigitalizacion == null) ? Constante.registroExitoso : Constante.actualizacionExitoso;
                if (!string.IsNullOrEmpty(objDigitalizacion.FechaVencimiento))
                {
                    DateTime _hoy = DateTime.Parse(objDigitalizacion.FechaVencimiento);
                    string _hoyFormato = _hoy.ToString("yyyy/MM/dd");
                    objDigitalizacion.FechaVencimiento = _hoyFormato;
                }
                if (objDigitalizacion.IdDigitalizacionDesaprobado != null)
                {
                    if (objDigitalizacion.IdDigitalizacionDesaprobado != "")
                    {
                        string[] separador = { ",", ".", "!", "?", ";", ":", " " };
                        string[] id = objDigitalizacion.IdDigitalizacionDesaprobado.Split(separador, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in id)
                        {
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Documentos", item);
                            if (Directory.Exists(path))
                                Directory.Delete(path, true);
                            var ok = _Digitalizacion.EliminarDigitalizacion(item);
                        }
                    }
                }
                objDigitalizacion.IdDigitalizacion = (objDigitalizacion.IdDigitalizacion == null) ? "" : objDigitalizacion.IdDigitalizacion;
                var response = _Digitalizacion.MantenimientoDigitalizacion(objDigitalizacion);
                if (response != "-1" || response != "-2")
                {
                    jsonResponseDto.Type = Constante.Success.ToLower();
                    jsonResponseDto.IsValid = true;
                    jsonResponseDto.Mensaje = message;
                    jsonResponseDto.data = response;
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
                // Log.Error(ex.Message);
                jsonResponseDto.Type = Constante.Warning.ToLower();
                jsonResponseDto.IsValid = false;
                jsonResponseDto.Mensaje = ex.Message.ToString();
            }
            return Json(jsonResponseDto);

        }

        public Byte[] GenerarVehiculo()
        {
            //lista de personas Vehiculoes
            var _empresa = GetEmpresa();
            var lstVehiculoes = _Vehiculo.ListadoVehiculo(GetEmpresa());
            var lstFilterEnviado = lstVehiculoes.Where(p => p.Enviado == 1 && p.Digitalizado > 0).ToList();
            var Perfil = GetPerfil();

            if (Perfil != "SUPERUSUARIO")
            {
                lstFilterEnviado.Where(p => p.IdUsuario == GetUsuarioActual()).ToList();
            }

            DateTime hoy = DateTime.Now;
            var hoyFormater = hoy.ToString("dd-MM-yyyy");
            var NombreArchivo = "Digitalizacion" + "-(" + hoyFormater + ")";
            byte[] fileContents;
            ExcelPackage excel = new ExcelPackage();
            string nameExcelContent = "Digitalizacion";
            string[] cabeceras = { "Codigo", "Documento", "Empresa", "Categoria", "Documento", "Observacion", "Obligatorio", "Con Fecha Vencimiento", "Estado" };

            var workSheet = excel.Workbook.Worksheets.Add(nameExcelContent);
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;
            //Header of table

            workSheet.Cells["A1:I1"].Merge = true;
            workSheet.Cells[1, 1].Value = "Lista de estados de digitalizacion de Vehiculoes  - " + GetNombreEmpresa();
            workSheet.Cells["A1:I1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["A1:I1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            int PosicionCabecera = 0;
            int PosicionDetalle = 0;
            int PosicionFila = 2;
            int PositionFilaDetalle = 0;
            foreach (var iteml in lstFilterEnviado)
            {



                PosicionFila++;
                PosicionFila++;
                int rangoDerecha = 9;
                int rangoAbajo = PosicionFila; //rangoAbajo igual a rango rangoArriba
                int rangoIzquierda = 1;
                int rangoArriba = PosicionFila;
                int _rangoArriba = 0;
                int _rangoAbajo = 0;
                // Armar la cabecera
                int _detalleCabecara = rangoAbajo - 1;
                workSheet.Cells["A" + _detalleCabecara + ":I" + _detalleCabecara].Merge = true;
                workSheet.Cells[_detalleCabecara, 1].Value = iteml.Mensaje;
                workSheet.Cells["A" + _detalleCabecara + ":I" + _detalleCabecara].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells["A" + _detalleCabecara + ":I" + _detalleCabecara].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                if (cabeceras.Length > 0)
                {
                    foreach (string item in cabeceras)
                    {
                        PosicionCabecera++;
                        workSheet.Cells[PosicionFila, PosicionCabecera].Value = item;
                    }


                    using (ExcelRange range = workSheet.Cells[rangoArriba, rangoIzquierda, rangoAbajo, rangoDerecha])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.LightGreen);

                        range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Top.Color.SetColor(Color.Black);
                        range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Left.Color.SetColor(Color.Black);
                        range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Right.Color.SetColor(Color.Black);
                        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Bottom.Color.SetColor(Color.Black);
                    }
                    _rangoAbajo = rangoAbajo + 1;
                    _rangoArriba = rangoArriba + 1;
                }
                PosicionFila++;

                //Armar las columnas de los datos
                foreach (DigitalizacionExcelDto _object in _Vehiculo.ListaVehiculoDigitalizacionExcel(iteml.IdPersona).ToList())
                {
                    foreach (var oPropiedadValor in _object.GetType().GetProperties())
                    {
                        PositionFilaDetalle++;
                        var positionValue = oPropiedadValor.GetValue(_object);
                        var nombre = _object.GetType().GetProperty(oPropiedadValor.Name);
                        if (positionValue != null)
                        {
                            workSheet.Cells[PosicionFila, PositionFilaDetalle].Value = positionValue;
                        }
                        else
                        {
                            workSheet.Cells[PosicionFila, PositionFilaDetalle].Value = "";
                        }
                        workSheet.Column(PositionFilaDetalle).AutoFit();
                    }
                    //Styles cells
                    //rangoArriba  igual a  rangoAbajo

                    using (ExcelRange range = workSheet.Cells[_rangoArriba, rangoIzquierda, _rangoAbajo, rangoDerecha])
                    {
                        _rangoAbajo++;
                        _rangoArriba++;
                        range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Top.Color.SetColor(Color.Black);
                        range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Left.Color.SetColor(Color.Black);
                        range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Right.Color.SetColor(Color.Black);
                        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Bottom.Color.SetColor(Color.Black);
                    }
                    //workSheet.Row(PosicionDetalle).Height = sizeFont;
                    PositionFilaDetalle = 0;
                    PosicionDetalle++;
                    PosicionFila++;
                }

                PosicionDetalle = 0;
                PosicionCabecera = 0;
            }
            //
            //
            fileContents = excel.GetAsByteArray();
            return fileContents;
            //if (fileContents == null || fileContents.Length == 0)
            //{
            //    return NotFound();
            //}

            //return File(
            //    fileContents: fileContents,
            //    contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            //    fileDownloadName: NombreArchivo + ".xlsx"
            //);
        }
        #endregion

        #region Maquinaria 
        public IActionResult DigitalizacionMaquinaria()
        {
            MaquinariaDigitalizacionFilterDto obj = new MaquinariaDigitalizacionFilterDto();
            return PartialView("DigitalizacionMaquinaria", obj);
        }
        [HttpPost]
        public IActionResult ListarMaquinaria(DataTableModel<MaquinariaDigitalizacionFilterDto> dataTableModel)
        {
            var jsonResponse = new JsonResponseDto() { Type = Constante.Error };

            try
            {
                FormatDataTableMaquinaria(dataTableModel);
                var jsonResponseDto = new JsonResponseDto() { Type = Constante.Error };

                var paginationParameter = new PaginationParameter
                {
                    Start = dataTableModel.start,
                    AmountRows = dataTableModel.length,
                    WhereFilter = dataTableModel.whereFilter,
                    OrderBy = dataTableModel.orderBy
                };

                List<MaquinariaDigitalizacionPaginationDto> lstMaquinaria = _Maquinaria.PaginadoMaquinariaDigitalizacion(paginationParameter);
                dataTableModel.data = lstMaquinaria;
                if (lstMaquinaria.Count > 0)
                {
                    dataTableModel.recordsTotal = lstMaquinaria[0].Cantidad;
                    dataTableModel.recordsFiltered = dataTableModel.recordsTotal;
                }
            }


            catch (Exception ex)
            {
                jsonResponse.Mensaje = ex.Message;
            }
            return Json(dataTableModel);
        }

        [HttpPost]
        public IActionResult RegistrarMaquinaria([FromForm] Entidad.Digitalizacion objDigitalizacion)
        {
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                objDigitalizacion.IdUsuario = GetUsuarioActual();
                var message = (objDigitalizacion.IdDigitalizacion == null) ? Constante.registroExitoso : Constante.actualizacionExitoso;
                if (!string.IsNullOrEmpty(objDigitalizacion.FechaVencimiento))
                {
                    DateTime _hoy = DateTime.Parse(objDigitalizacion.FechaVencimiento);
                    string _hoyFormato = _hoy.ToString("yyyy/MM/dd");
                    objDigitalizacion.FechaVencimiento = _hoyFormato;
                }
                if (objDigitalizacion.IdDigitalizacionDesaprobado != null)
                {
                    if (objDigitalizacion.IdDigitalizacionDesaprobado != "")
                    {
                        string[] separador = { ",", ".", "!", "?", ";", ":", " " };
                        string[] id = objDigitalizacion.IdDigitalizacionDesaprobado.Split(separador, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in id)
                        {
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Documentos", item);
                            if (Directory.Exists(path))
                                Directory.Delete(path, true);
                            var ok = _Digitalizacion.EliminarDigitalizacion(item);
                        }
                    }
                }
                objDigitalizacion.IdDigitalizacion = (objDigitalizacion.IdDigitalizacion == null) ? "" : objDigitalizacion.IdDigitalizacion;
                var response = _Digitalizacion.MantenimientoDigitalizacion(objDigitalizacion);
                if (response != "-1" || response != "-2")
                {
                    jsonResponseDto.Type = Constante.Success.ToLower();
                    jsonResponseDto.IsValid = true;
                    jsonResponseDto.Mensaje = message;
                    jsonResponseDto.data = response;
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
                // Log.Error(ex.Message);
                jsonResponseDto.Type = Constante.Warning.ToLower();
                jsonResponseDto.IsValid = false;
                jsonResponseDto.Mensaje = ex.Message.ToString();
            }
            return Json(jsonResponseDto);

        }

        public Byte[] GenerarMaquinaria()
        {
            //lista de personas Maquinariaes
            var _empresa = GetEmpresa();
            var lstMaquinariaes = _Maquinaria.ListadoMaquinaria(GetEmpresa());
            var lstFilterEnviado = lstMaquinariaes.Where(p => p.Enviado == 1 && p.Digitalizado > 0).ToList();
            var Perfil = GetPerfil();

            if (Perfil != "SUPERUSUARIO")
            {
                lstFilterEnviado.Where(p => p.IdUsuario == GetUsuarioActual()).ToList();
            }

            DateTime hoy = DateTime.Now;
            var hoyFormater = hoy.ToString("dd-MM-yyyy");
            var NombreArchivo = "Digitalizacion" + "-(" + hoyFormater + ")";
            byte[] fileContents;
            ExcelPackage excel = new ExcelPackage();
            string nameExcelContent = "Digitalizacion";
            string[] cabeceras = { "Codigo", "Documento", "Empresa", "Categoria", "Documento", "Observacion", "Obligatorio", "Con Fecha Vencimiento", "Estado" };

            var workSheet = excel.Workbook.Worksheets.Add(nameExcelContent);
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;
            //Header of table

            workSheet.Cells["A1:I1"].Merge = true;
            workSheet.Cells[1, 1].Value = "Lista de estados de digitalizacion de Maquinariaes  - " + GetNombreEmpresa();
            workSheet.Cells["A1:I1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["A1:I1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            int PosicionCabecera = 0;
            int PosicionDetalle = 0;
            int PosicionFila = 2;
            int PositionFilaDetalle = 0;
            foreach (var iteml in lstFilterEnviado)
            {



                PosicionFila++;
                PosicionFila++;
                int rangoDerecha = 9;
                int rangoAbajo = PosicionFila; //rangoAbajo igual a rango rangoArriba
                int rangoIzquierda = 1;
                int rangoArriba = PosicionFila;
                int _rangoArriba = 0;
                int _rangoAbajo = 0;
                // Armar la cabecera
                int _detalleCabecara = rangoAbajo - 1;
                workSheet.Cells["A" + _detalleCabecara + ":I" + _detalleCabecara].Merge = true;
                workSheet.Cells[_detalleCabecara, 1].Value = iteml.Mensaje;
                workSheet.Cells["A" + _detalleCabecara + ":I" + _detalleCabecara].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells["A" + _detalleCabecara + ":I" + _detalleCabecara].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                if (cabeceras.Length > 0)
                {
                    foreach (string item in cabeceras)
                    {
                        PosicionCabecera++;
                        workSheet.Cells[PosicionFila, PosicionCabecera].Value = item;
                    }


                    using (ExcelRange range = workSheet.Cells[rangoArriba, rangoIzquierda, rangoAbajo, rangoDerecha])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.LightGreen);

                        range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Top.Color.SetColor(Color.Black);
                        range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Left.Color.SetColor(Color.Black);
                        range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Right.Color.SetColor(Color.Black);
                        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Bottom.Color.SetColor(Color.Black);
                    }
                    _rangoAbajo = rangoAbajo + 1;
                    _rangoArriba = rangoArriba + 1;
                }
                PosicionFila++;

                //Armar las columnas de los datos
                foreach (DigitalizacionExcelDto _object in _Maquinaria.ListaMaquinariaDigitalizacionExcel(iteml.IdPersona).ToList())
                {
                    foreach (var oPropiedadValor in _object.GetType().GetProperties())
                    {
                        PositionFilaDetalle++;
                        var positionValue = oPropiedadValor.GetValue(_object);
                        var nombre = _object.GetType().GetProperty(oPropiedadValor.Name);
                        if (positionValue != null)
                        {
                            workSheet.Cells[PosicionFila, PositionFilaDetalle].Value = positionValue;
                        }
                        else
                        {
                            workSheet.Cells[PosicionFila, PositionFilaDetalle].Value = "";
                        }
                        workSheet.Column(PositionFilaDetalle).AutoFit();
                    }
                    //Styles cells
                    //rangoArriba  igual a  rangoAbajo

                    using (ExcelRange range = workSheet.Cells[_rangoArriba, rangoIzquierda, _rangoAbajo, rangoDerecha])
                    {
                        _rangoAbajo++;
                        _rangoArriba++;
                        range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Top.Color.SetColor(Color.Black);
                        range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Left.Color.SetColor(Color.Black);
                        range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Right.Color.SetColor(Color.Black);
                        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Bottom.Color.SetColor(Color.Black);
                    }
                    //workSheet.Row(PosicionDetalle).Height = sizeFont;
                    PositionFilaDetalle = 0;
                    PosicionDetalle++;
                    PosicionFila++;
                }

                PosicionDetalle = 0;
                PosicionCabecera = 0;
            }
            //
            //
            fileContents = excel.GetAsByteArray();
            return fileContents;
            //if (fileContents == null || fileContents.Length == 0)
            //{
            //    return NotFound();
            //}

            //return File(
            //    fileContents: fileContents,
            //    contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            //    fileDownloadName: NombreArchivo + ".xlsx"
            //);
        }
        #endregion

        #region Metodos reutilizables

        [HttpPost]
        public async Task<IActionResult> CargarArchivo(IFormFile file, string IdDocumento, string IdDigitalizacion, string IdPersona)
        {
            DocumentoAdjunto objDocumentoAdjunto = new DocumentoAdjunto();
            var jsonResponseDto = new JsonResponseDto();
            try
            {
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var stringChars = new char[8];
                var random = new Random();
                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

                var _hoyFormato = new String(stringChars);
                string _nombreArcghivo = _hoyFormato + ".pdf";
                if (file == null || file.Length == 0)
                    return Content("Ningun archivo cargado");

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Documentos", IdDigitalizacion);
                var pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Documentos", IdDigitalizacion, _nombreArcghivo);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                using (var stream = new FileStream(pathFile, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                DateTime _hoynuevo = DateTime.Now;
                string _hoyFormatoNuevo = _hoynuevo.ToString("yyyy/MM/dd");
                objDocumentoAdjunto.Nombre = file.FileName;
                objDocumentoAdjunto.IdDocumento = IdDocumento;
                objDocumentoAdjunto.IdDigitalizacion = IdDigitalizacion;
                objDocumentoAdjunto.IdPersona = IdPersona;
                objDocumentoAdjunto.Ruta = "/Documentos/" + IdDigitalizacion + "/" + _nombreArcghivo;
                objDocumentoAdjunto.Fecha = _hoyFormatoNuevo;
                var message = (objDocumentoAdjunto.IdDocumentoAdjunto == null) ? Constante.registroExitoso : Constante.actualizacionExitoso;
                objDocumentoAdjunto.IdDocumentoAdjunto = (objDocumentoAdjunto.IdDocumentoAdjunto == null) ? "" : objDocumentoAdjunto.IdDocumentoAdjunto;

                var ok = _DocumentoAdjunto.MantenimientoDocumentoAdjunto(objDocumentoAdjunto);
                jsonResponseDto.IsValid = true;
                jsonResponseDto.Type = Constante.Success;
                jsonResponseDto.Mensaje = "correcto";
                return Json(jsonResponseDto);
            }
            catch (Exception ex)
            {

                jsonResponseDto.IsValid = true;
                jsonResponseDto.Type = Constante.Success;
                jsonResponseDto.Mensaje = ex.Message.ToString();
                return Json(jsonResponseDto);
            }

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DropZone()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult DocumentosAdjuntos([FromForm] DocumentoAdjuntoConsultaDto objDocumentoAdjuntoConsultaDto)
        {
            var jsonResponse = new JsonResponseDto();

            try
            {
                objDocumentoAdjuntoConsultaDto.listDocumentoAdjunto = _DocumentoAdjunto.ListadoDocumentoAdjunto(objDocumentoAdjuntoConsultaDto.IdDigitalizacion);
                return PartialView("DocumentosAdjuntos", objDocumentoAdjuntoConsultaDto);
            }
            catch (Exception ex)
            {
                jsonResponse.Mensaje = ex.Message;
                return View("DocumentosAdjuntos");
            }
        }

        public IActionResult EliminarArchivo([FromForm] DocumentoAdjuntoConsultaDto objDocumentoAdjuntoConsultaDto)
        {
            var jsonResponse = new JsonResponseDto();

            try
            {
                var documentoDescripto = objDocumentoAdjuntoConsultaDto.Documento.Split("/");
                var ok = _DocumentoAdjunto.EliminarDocumentoAdjunto(objDocumentoAdjuntoConsultaDto.IdDocumentoAdjunto);
                var pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Documentos", documentoDescripto[2], documentoDescripto[3]);
                if (System.IO.File.Exists(pathFile))
                    System.IO.File.Delete(pathFile);

                if (ok == 0)
                {
                    jsonResponse.Mensaje = "Ocurrio un error al eliminar";
                    jsonResponse.IsValid = false;
                    jsonResponse.Type = Constante.Warning;
                    return Json(jsonResponse);
                }

                jsonResponse.Mensaje = "Se elemino correctamente";
                jsonResponse.IsValid = true;
                jsonResponse.Type = Constante.Success;
                return Json(jsonResponse);
            }
            catch (Exception ex)
            {
                jsonResponse.Mensaje = ex.Message;
                jsonResponse.IsValid = true;
                jsonResponse.Type = Constante.Success;
                return Json(jsonResponse);
            }

        }
        #endregion

        #region metodos privados
        private void FormatDataTable(DataTableModel<ColaboradorDigitalizacionFilterDto> dataTableModel)
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
                    dataTableModel.whereFilter += (" AND P.Nombre LIKE '%'+'" + dataTableModel.filter.NombreSearch + "'+'%'");
                if (!string.IsNullOrWhiteSpace(dataTableModel.filter.IdColaboradorSearch))
                    dataTableModel.whereFilter += (" AND P.IdColaborador ='" + dataTableModel.filter.IdColaboradorSearch + "'");
            }
        }

        private void FormatDataTableEmpresa(DataTableModel<EmpresaDigitalizacionFilterDto> dataTableModel)
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
                    dataTableModel.whereFilter += (" AND P.Nombre LIKE '%'+'" + dataTableModel.filter.NombreSearch + "'+'%'");
                if (!string.IsNullOrWhiteSpace(dataTableModel.filter.IdEmpresaSearch))
                    dataTableModel.whereFilter += (" AND P.IdEmpresa ='" + dataTableModel.filter.IdEmpresaSearch + "'");
            }
        }

        private void FormatDataTableVehiculo(DataTableModel<VehiculoDigitalizacionFilterDto> dataTableModel)
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
                    dataTableModel.whereFilter += (" AND P.Nombre LIKE '%'+'" + dataTableModel.filter.NombreSearch + "'+'%'");
                if (!string.IsNullOrWhiteSpace(dataTableModel.filter.IdEmpresaSearch))
                    dataTableModel.whereFilter += (" AND P.IdEmpresa ='" + dataTableModel.filter.IdEmpresaSearch + "'");
            }
        }

        private void FormatDataTableMaquinaria(DataTableModel<MaquinariaDigitalizacionFilterDto> dataTableModel)
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
                    dataTableModel.whereFilter += (" AND P.Nombre LIKE '%'+'" + dataTableModel.filter.NombreSearch + "'+'%'");
                if (!string.IsNullOrWhiteSpace(dataTableModel.filter.IdEmpresaSearch))
                    dataTableModel.whereFilter += (" AND P.IdEmpresa ='" + dataTableModel.filter.IdEmpresaSearch + "'");
            }
        }
        #endregion
    }
}