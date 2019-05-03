using Entidad;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using WEB.Models;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.IO;
using System.Net;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace WEB.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ILogger _logger;
        protected readonly ISession Context;
        public BaseController(IServiceProvider serviceProvider)
        {

        }
        string hostName = System.Net.Dns.GetHostName();
        public string GetUsuarioActual()
        {
            string iUsuario = "";
            try
            {
                iUsuario = (HttpContext.Session.GetObject<UsuarioConsultaDto>(Constante.UsuarioSesionKey)).IdUsuario;
            }
            catch (Exception ex)
            {
                iUsuario = iUsuario = "" ;
            }
            return iUsuario;
        }
        public string GetNombreUsuarioActual()
        {
            string iUsuario = "";
            try
            {
                iUsuario = (HttpContext.Session.GetObject<UsuarioConsultaDto>(Constante.UsuarioSesionKey)).Usuarios;
            }
            catch (Exception ex)
            {
                iUsuario = iUsuario = "";
            }
            return iUsuario;
        }
        public string GetTipoEmpresa()
        {
            string tipoEmpresa = "";
            try
            {
                tipoEmpresa = (HttpContext.Session.GetObject<UsuarioConsultaDto>(Constante.UsuarioSesionKey)).TipoEmpresa;
            }
            catch (Exception ex)
            {
                tipoEmpresa = tipoEmpresa = "";
            }
            return (tipoEmpresa==null)?"": tipoEmpresa;
        }

        public string GetEmpresa()
        {
            string empresa = "";
            try
            {
                empresa = (HttpContext.Session.GetObject<UsuarioConsultaDto>(Constante.UsuarioSesionKey)).IdEmpresa;
            }
            catch (Exception ex)
            {
                empresa = empresa = "";
            }
            return empresa;
        }
        public string GetNombreEmpresa()
        {
            string empresa = "";
            try
            {
                empresa = (HttpContext.Session.GetObject<UsuarioConsultaDto>(Constante.UsuarioSesionKey)).EmpresaNombre;
            }
            catch (Exception ex)
            {
                empresa = empresa = "";
            }
            return empresa;
        }
        public string GetEmpresaPadre()
        {
            string empresa = "";
            try
            {
                empresa = (HttpContext.Session.GetObject<UsuarioConsultaDto>(Constante.UsuarioSesionKey)).IdEmpresaPadre;
                empresa = (empresa == null) ? "" : empresa;
            }
            catch (Exception ex)
            {
            }
            return empresa;
        }
        public string GetPerfil()
        {
            string perfil = "";
            try
            {
                perfil = (HttpContext.Session.GetObject<UsuarioConsultaDto>(Constante.UsuarioSesionKey)).Perfil;
            }
            catch (Exception ex)
            {
                perfil = perfil = "";
            }
            return perfil;
        }
        public List<ModuloConsultaDto> ModulosActuales()
        {
            try
            {
                return HttpContext.Session.GetObject<List<ModuloConsultaDto>>(Constante.ModuloSesion).ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public List<OpcionConsultaDto> OpcionesActuales()
        {
            try
            {
                return HttpContext.Session.GetObject<List<OpcionConsultaDto>>(Constante.OpcionSesion).ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }
        public List<OpcionConsultaDto> OpcionesHijoActuales()
        {
            try
            {
                return HttpContext.Session.GetObject<List<OpcionConsultaDto>>(Constante.OpcionHijoSesion).ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public List<UsuarioAccionDto> AccionesActuales()
        {
            try
            {
                return HttpContext.Session.GetObject<List<UsuarioAccionDto>>(Constante.AccionSesion).ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public UsuarioConsultaDto UsuarioActual()
        {
            try
            {
                return HttpContext.Session.GetObject<UsuarioConsultaDto>(Constante.UsuarioSesionKey);
            }
            catch (Exception)
            {
                return null;
            }

        }
        /// <summary>
        /// Este metod genera el excel con una lista generica
        /// </summary>
        /// <typeparam name="T">Es el Objecto a enviar </typeparam>
        /// <param name="excel">ExcelPackage que se debe de instanciar en su metodo </param>
        /// <param name="cabeceras">Tipo de string[] que es la cabecera de la tabla del excel, si el arreglo es vacio por defecto tomara el nombre de las propiedades del objeto</param>
        /// <param name="dataColums">List acepta la lista generica</param>
        /// <param name="cellNumber">Es el numero de  de la pocicion de la celda en lo que se generará la tabla</param>
        /// <param name="nameExcelContent">Es el nombre de la pagina del excel</param>
        /// <param name="positionValues">Es la posicion de los valores de las filas left, center,right,etc </param>
        /// <param name="sizeFont">Es el tamaño de la Fuente</param>
        /// <param name="cellColumIndex">Es el Abecedario de las celdas en inicio</param>
        /// <param name="cellColumEnd">Es el Abecedario de las celdas en fin</param>
        public void FormatExcel<T>(ExcelPackage excel, string[] cabeceras, List<T> dataColums, int cellNumber, string nameExcelContent, string positionValues, int sizeFont, string cellColumIndex, string cellColumEnd)
        {
            int PositionCellData = 1;
            var workSheet = excel.Workbook.Worksheets.Add(nameExcelContent);
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;
            //Header of table

            using (ExcelRange range = workSheet.Cells[cellColumIndex + PositionCellData + ":" + cellColumEnd + PositionCellData])
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
            PositionCellData = 0;

            int Position = 0;
            // Armar la cabecera
            if (cabeceras.Length > 0)
            {
                foreach (string item in cabeceras)
                {
                    Position++;
                    workSheet.Cells[cellNumber, Position].Value = item;
                }
            }
            else
            {
                int recorrido = 0;
                foreach (T _object in dataColums)
                {
                    if (recorrido == 0)
                    {
                        foreach (var oPropiedadValor in _object.GetType().GetProperties())
                        {
                            Position++;
                            var prop = _object.GetType().GetProperty(oPropiedadValor.Name);
                            var nombre = prop.Name;
                            workSheet.Cells[cellNumber, Position].Value = nombre;
                        }
                    }
                    recorrido++;
                }
            }

            //Armar las columnas de los datos
            Position = 0;
            PositionCellData = cellNumber + 1;

            foreach (T _object in dataColums)
            {
                foreach (var oPropiedadValor in _object.GetType().GetProperties())
                {
                    Position++;
                    var positionValue = oPropiedadValor.GetValue(_object);
                    var nombre = _object.GetType().GetProperty(oPropiedadValor.Name);
                    if (positionValue != null)
                    {
                        workSheet.Cells[PositionCellData, Position].Value = positionValue;
                    }
                    else
                    {
                        workSheet.Cells[PositionCellData, Position].Value = "";
                    }
                    workSheet.Column(Position).AutoFit();
                }
                using (ExcelRange range = workSheet.Cells[cellColumIndex + PositionCellData + ":" + cellColumEnd + PositionCellData])
                {
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Top.Color.SetColor(Color.Black);
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Color.SetColor(Color.Black);
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Color.SetColor(Color.Black);
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Color.SetColor(Color.Black);
                }
                //Styles cells
                switch ((positionValues).ToLower())
                {
                    case "left":
                        workSheet.Row(PositionCellData).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        break;

                    case "center":
                        workSheet.Row(PositionCellData).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        break;

                    case "right":
                        workSheet.Row(PositionCellData).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        break;

                    case "fill":
                        workSheet.Row(PositionCellData).Style.HorizontalAlignment = ExcelHorizontalAlignment.Fill;
                        break;

                    case "justify":
                        workSheet.Row(PositionCellData).Style.HorizontalAlignment = ExcelHorizontalAlignment.Justify;
                        break;

                    case "distributed":
                        workSheet.Row(PositionCellData).Style.HorizontalAlignment = ExcelHorizontalAlignment.Distributed;
                        break;

                    case "general":
                        workSheet.Row(PositionCellData).Style.HorizontalAlignment = ExcelHorizontalAlignment.General;
                        break;
                }
                workSheet.Row(PositionCellData).Height = sizeFont;

                PositionCellData++;
                Position = 0;
            }
        }


        public void EnviarCorreoUsuarioCreado(ParametrosCorreoDto objParametrosCorreoDto, Usuario objUsuario)  {
            var location = new Uri($"{Request.Scheme}://{Request.Host}");
            var url = location.AbsoluteUri;
            var urlInicio = url + "Home";
            var tipoEmpersa = objUsuario.EmpresaPertence.Split("-");
            var empresaP = objUsuario.TipoEmpresa.Split(" ");
            // Montamos la estructura básica del mensaje...
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(objUsuario.Correo);
            mail.To.Add(objUsuario.Correo);
            mail.Subject = "Usuario Creado para la empresa "+objParametrosCorreoDto.Empresa ;

            // Creamos la vista para clientes que
            // sólo pueden acceder a texto plano...

            string text = "Correo enviadao con urgencia";

            AlternateView plainView =
             AlternateView.CreateAlternateViewFromString(text,
                                        Encoding.UTF8,
                                        MediaTypeNames.Text.Plain);


            // Ahora creamos la vista para clientes que 
            // pueden mostrar contenido HTML...

            string html = @"<html xmlns='http://www.w3.org/1999/xhtml'>
   <head>
      <meta http-equiv='Content-Type' content='text/html; charset=utf-8'>
      <title>[SUBJECT]</title>
      <style type='text/css'>
         body {
         padding-top: 0 !important;
         padding-bottom: 0 !important;
         padding-top: 0 !important;
         padding-bottom: 0 !important;
         margin:0 !important;
         width: 100% !important;
         -webkit-text-size-adjust: 100% !important;
         -ms-text-size-adjust: 100% !important;
         -webkit-font-smoothing: antialiased !important;
         }
         .tableContent img {
         border: 0 !important;
         display: block !important;
         outline: none !important;
         }
         a{
         color:#382F2E;
         }
         p, h1,h2,ul,ol,li,div{
         margin:0;
         padding:0;
         }
         h1,h2{
         font-weight: normal;
         background:transparent !important;
         border:none !important;
         }
         @media only screen and (max-width:480px)
         {
         table[class='MainContainer'], td[class='cell'] 
         {
         width: 100% !important;
         height:auto !important; 
         }
         td[class='specbundle'] 
         {
         width: 100% !important;
         float:left !important;
         font-size:13px !important;
         line-height:17px !important;
         display:block !important;
         padding-bottom:15px !important;
         }	
         td[class='specbundle2'] 
         {
         width:80% !important;
         float:left !important;
         font-size:13px !important;
         line-height:17px !important;
         display:block !important;
         padding-bottom:10px !important;
         padding-left:10% !important;
         padding-right:10% !important;
         }
         td[class='spechide'] 
         {
         display:none !important;
         }
         img[class='banner'] 
         {
         width: 100% !important;
         height: auto !important;
         }
         td[class='left_pad'] 
         {
         padding-left:15px !important;
         padding-right:15px !important;
         }
         }
         @media only screen and (max-width:540px) 
         {
         table[class='MainContainer'], td[class='cell'] 
         {
         width: 100% !important;
         height:auto !important; 
         }
         td[class='specbundle'] 
         {
         width: 100% !important;
         float:left !important;
         font-size:13px !important;
         line-height:17px !important;
         display:block !important;
         padding-bottom:15px !important;
         }	
         td[class='specbundle2'] 
         {
         width:80% !important;
         float:left !important;
         font-size:13px !important;
         line-height:17px !important;
         display:block !important;
         padding-bottom:10px !important;
         padding-left:10% !important;
         padding-right:10% !important;
         }
         td[class='spechide'] 
         {
         display:none !important;
         }
         img[class='banner'] 
         {
         width: 100% !important;
         height: auto !important;
         }
         td[class='left_pad'] 
         {
         padding-left:15px !important;
         padding-right:15px !important;
         }
         }
         .contentEditable h2.big,.contentEditable h1.big{
         font-size: 26px !important;
         }
         .contentEditable h2.bigger,.contentEditable h1.bigger{
         font-size: 37px !important;
         }
         td,table{
         vertical-align: top;
         }
         td.middle{
         vertical-align: middle;
         }
         a.link1{
         font-size:13px;
         color:#27A1E5;
         line-height: 24px;
         text-decoration:none;
         }
         a{
         text-decoration: none;
         }
         .link2{
         color:#ffffff;
         border-top:10px solid #27A1E5;
         border-bottom:10px solid #27A1E5;
         border-left:18px solid #27A1E5;
         border-right:18px solid #27A1E5;
         border-radius:3px;
         -moz-border-radius:3px;
         -webkit-border-radius:3px;
         background:#27A1E5;
         }
         .link3{
         color:#555555;
         border:1px solid #cccccc;
         padding:10px 18px;
         border-radius:3px;
         -moz-border-radius:3px;
         -webkit-border-radius:3px;
         background:#ffffff;
         }
         .link4{
         color:#27A1E5;
         line-height: 24px;
         }
         h2,h1{
         line-height: 20px;
         }
         p{
         font-size: 14px;
         line-height: 21px;
         color:#AAAAAA;
         }
         .contentEditable li{
         }
         .appart p{
         }
         .bgItem{
         background: #ffffff;
         }
         .bgBody{
         background: #ffffff;
         }
         img { 
         outline:none; 
         text-decoration:none; 
         -ms-interpolation-mode: bicubic;
         width: auto;
         max-width: 100%; 
         clear: both; 
         display: block;
         float: none;
         }
      </style>
      <script type='colorScheme' class='swatch active'>
         {
             'name':'Default',
             'bgBody':'ffffff',
             'link':'27A1E5',
             'color':'AAAAAA',
             'bgItem':'ffffff',
             'title':'444444'
         }
      </script>
   </head>
   <body paddingwidth='0' paddingheight='0' bgcolor='#d1d3d4' style='padding-top: 0; padding-bottom: 0; padding-top: 0; padding-bottom: 0; background-repeat: repeat; width: 100% !important; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; -webkit-font-smoothing: antialiased;' offset='0' toppadding='0' leftpadding='0'>
      <table width='100%' border='0' cellspacing='0' cellpadding='0'>
         <tbody>
            <tr>
               <td>
                  <table width='800' border='0' cellspacing='0' cellpadding='0' align='center' bgcolor='#ffffff' style='font-family:helvetica, sans-serif;' class='MainContainer'>
                     <!-- =============== START HEADER =============== -->
                     <tbody>
                        <tr>
                           <td>
                              <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                 <tbody>
                                    <tr>
                                       <td valign='top' width='20' style='
                                          background-color: #4a4a4a;
                                          '>&nbsp;</td>
                                       <td style='
                                          background-color: #f3f5f0;
                                          '>
                                          <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                             <tbody>
                                                <tr>
                                                   <td class='movableContentContainer'>
                                                      <div class='movableContent' style='border: 0px; padding-top: 0px; position: relative;'>
                                                         <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                               <tr>
                                                                  <td height='15'></td>
                                                               </tr>
                                                               <tr>
                                                                  <td>
                                                                     <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                                                        <tbody>
                                                                           <tr>
                                                                              <td valign='top'>
                                                                                 <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                                                                    <tbody>
                                                                                       <tr>
                                                                                          <td valign='top' width='60'></td>
                                                                                          <td width='10' valign='top'>&nbsp;</td>
                                                                                          <td valign='middle' style='vertical-align: middle;'>
                                                                                             <div class='contentEditableContainer contentTextEditable'>
                                                                                                <div class='contentEditable' style='text-align: left;font-weight: light; color:#555555;font-size:26;line-height: 39px;font-family: Helvetica Neue;'>
                                                                                                   <h1 class='big'><label style='color:#444444'>"+objParametrosCorreoDto.Empresa+ @"</label></h1>
                                                                                                </div>
                                                                                             </div>
                                                                                          </td>
                                                                                       </tr>
                                                                                    </tbody>
                                                                                 </table>
                                                                              </td>
                                                                              <td valign='top' width='90' class='spechide'>&nbsp;</td>
                                                                              <td valign='middle' style='vertical-align: middle;' width='250'>
                                                                                 <div class='contentEditableContainer contentTextEditable'>
                                                                                    <div class='contentEditable' style='text-align: right;'>
                                                                                       <span>Asunto: </span> <label>usuario creado</label>
                                                                                    </div>
                                                                                 </div>
                                                                              </td>
                                                                           </tr>
                                                                        </tbody>
                                                                     </table>
                                                                  </td>
                                                               </tr>
                                                               <tr>
                                                                  <td height='15'></td>
                                                               </tr>
                                                               <tr>
                                                                  <td>
                                                                     <hr style='height: 3px;background:#DDDDDD;border:none;'>
                                                                  </td>
                                                               </tr>
                                                            </tbody>
                                                         </table>
                                                      </div>
                                                      <!-- =============== END HEADER =============== -->
                                                      <!-- =============== START BODY =============== -->
                                                      <div class='movableContent' style='border: 0px; padding-top: 0px; position: relative;'>
                                                         <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                               <tr>
                                                                  <td style='background: #8cb15500;border-radius:6px;-moz-border-radius:6px;-webkit-border-radius:6px;'>
                                                                     <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                                                        <tbody>
                                                                           <tr>
                                                                              <td width='10' valign='top'>&nbsp;</td>
                                                                              <td valign='top'>
                                                                                 <table width='100%' border='0' cellspacing='0' cellpadding='0' align='center'>
                                                                                    <tbody>
                                                                                       <tr>
                                                                                          <td height='25'></td>
                                                                                       </tr>
                                                                                       <tr>
                                                                                          <td>
                                                                                             <div class='contentEditableContainer contentTextEditable'>
                                                                                                <div class='contentEditable' style='text-align: center;'>
                                                                                                   <h2 style='font-size: 23px;'>Se ha creado usuario para que pueda acceder, le sugerimos cambiar la clave temporal al realizar el acceso</h2>
                                                                                                   <br>
                                                                                                   <div style='width:100%'>
                                                                                                      <div style='width: 101%;padding-left: 0%;margin-bottom:15px;overflow-y:hidden;text-align:center;'>
                                                                                                         <table style='width:100%;margin-bottom:15px;overflow-y:hidden;border: 1px solid orange;text-align:center;'>
                                                                                                            <thead>
                                                                                                               <tr style='color:white;background-color: #FF9800;'>
                                                                                                                  <th>USUARIO</th>
                                                                                                                  <th>PASSWORD</th>
                                                                                                                  <th>PERFIL</th>
                                                                                                                  <th>EMPRESA</th>
                                                                                                                  <th>NIVEL</th>
                                                                                                               </tr>
                                                                                                            </thead>
                                                                                                            <tbody>
                                                                                                               <tr style='color: #4a4a4a;background-color: white;'>
                                                                                                                  <td>" + objUsuario.Usuarios + @"</td>
                                                                                                                  <td>" + objUsuario.Password + @"</td>
                                                                                                                  <td>" + objUsuario.Perfil + @"</td>
                                                                                                                  <td>" + tipoEmpersa[0] + @"</td>
                                                                                                                  <td>" + tipoEmpersa[1] + @"</td>
                                                                                                               </tr>
                                                                                                            </tbody>
                                                                                                         </table>
                                                                                                      </div>
                                                                                                   </div>
                                                                                                   <br>
                                                                                                   <a target='_blank' href='" + urlInicio+@"' class='link3' style='color:#555555;'>Ir a la pagina</a>
                                                                                                   <br>
                                                                                                </div>
                                                                                             </div>
                                                                                          </td>
                                                                                       </tr>
                                                                                       <tr>
                                                                                          <td height='24'></td>
                                                                                       </tr>
                                                                                    </tbody>
                                                                                 </table>
                                                                              </td>
                                                                              <td width='10' valign='top'>&nbsp;</td>
                                                                           </tr>
                                                                        </tbody>
                                                                     </table>
                                                                  </td>
                                                               </tr>
                                                            </tbody>
                                                         </table>
                                                      </div>
                                                      <!-- =============== END BODY =============== -->
                                                      <!-- =============== START FOOTER =============== -->
                                                      <div class='movableContent' style='border: 0px; padding-top: 0px; position: relative;'>
                                                         <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                               <tr>
                                                                  <td>
                                                                     <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                                                        <tbody>
                                                                           <tr>
                                                                              <td valign='top' width='90' class='spechide'>&nbsp;</td>
                                                                              <td>
                                                                                 <table width='100%' cellpadding='0' cellspacing='0' align='center'>
                                                                                    <tbody>
                                                                                       <tr>
                                                                                          <td>
                                                                                             <div class='contentEditableContainer contentTextEditable'>
                                                                                             </div>
                                                                                          </td>
                                                                                       </tr>
                                                                                    </tbody>
                                                                                 </table>
                                                                              </td>
                                                                              <td valign='top' width='90' class='spechide'>&nbsp;</td>
                                                                           </tr>
                                                                        </tbody>
                                                                     </table>
                                                                  </td>
                                                               </tr>
                                                            </tbody>
                                                         </table>
                                                      </div>
                                                   </td>
                                                </tr>
                                             </tbody>
                                          </table>
                                       </td>
                                       <td valign='top' width='20' style='
                                          background-color: #f3f5f0;
                                          '>&nbsp;</td>
                                    </tr>
                                 </tbody>
                              </table>
                           </td>
                        </tr>
                     </tbody>
                  </table>
               </td>
            </tr>
         </tbody>
      </table>
   </body>
</html>";

            AlternateView htmlView =
                AlternateView.CreateAlternateViewFromString(html,
                                        Encoding.UTF8,
                                        MediaTypeNames.Text.Html);

            // Creamos el recurso a incrustar. Observad
            // que el ID que le asignamos (arbitrario) está
            // referenciado desde el código HTML como origen

            // Por último, vinculamos ambas vistas al mensaje...

            mail.AlternateViews.Add(plainView);
            mail.AlternateViews.Add(htmlView);

            // Y lo enviamos a través del servidor SMTP...
            objParametrosCorreoDto.Password = Encriptador.Desencriptar(objParametrosCorreoDto.Password);
            var smtp = new SmtpClient
            {
                Host = objParametrosCorreoDto.Host, // set your SMTP server name here
                Port = objParametrosCorreoDto.Port, // Port 
                EnableSsl = true,
                Credentials = new NetworkCredential(objParametrosCorreoDto.Correo, objParametrosCorreoDto.Password)
            };

            smtp.Send(mail);
        }
        public void EnviarCorreoUsuarioRecuperado(ParametrosCorreoDto objParametrosCorreoDto, UsuarioConsultaDto objUsuario)
        {
            var location = new Uri($"{Request.Scheme}://{Request.Host}");
            var url = location.AbsoluteUri;
            var urlInicio = url + "Home";
            // Montamos la estructura básica del mensaje...
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(objUsuario.Correo);
            mail.To.Add(objUsuario.Correo);
            mail.Subject = "Usuario recuperado - empresa " + objParametrosCorreoDto.Empresa;

            // Creamos la vista para clientes que
            // sólo pueden acceder a texto plano...

            string text = "Correo enviadao con urgencia";

            AlternateView plainView =
             AlternateView.CreateAlternateViewFromString(text,
                                        Encoding.UTF8,
                                        MediaTypeNames.Text.Plain);


            // Ahora creamos la vista para clientes que 
            // pueden mostrar contenido HTML...

            string html = @"<html xmlns='http://www.w3.org/1999/xhtml'>
   <head>
      <meta http-equiv='Content-Type' content='text/html; charset=utf-8'>
      <title>[SUBJECT]</title>
      <style type='text/css'>
         body {
         padding-top: 0 !important;
         padding-bottom: 0 !important;
         padding-top: 0 !important;
         padding-bottom: 0 !important;
         margin:0 !important;
         width: 100% !important;
         -webkit-text-size-adjust: 100% !important;
         -ms-text-size-adjust: 100% !important;
         -webkit-font-smoothing: antialiased !important;
         }
         .tableContent img {
         border: 0 !important;
         display: block !important;
         outline: none !important;
         }
         a{
         color:#382F2E;
         }
         p, h1,h2,ul,ol,li,div{
         margin:0;
         padding:0;
         }
         h1,h2{
         font-weight: normal;
         background:transparent !important;
         border:none !important;
         }
         @media only screen and (max-width:480px)
         {
         table[class='MainContainer'], td[class='cell'] 
         {
         width: 100% !important;
         height:auto !important; 
         }
         td[class='specbundle'] 
         {
         width: 100% !important;
         float:left !important;
         font-size:13px !important;
         line-height:17px !important;
         display:block !important;
         padding-bottom:15px !important;
         }	
         td[class='specbundle2'] 
         {
         width:80% !important;
         float:left !important;
         font-size:13px !important;
         line-height:17px !important;
         display:block !important;
         padding-bottom:10px !important;
         padding-left:10% !important;
         padding-right:10% !important;
         }
         td[class='spechide'] 
         {
         display:none !important;
         }
         img[class='banner'] 
         {
         width: 100% !important;
         height: auto !important;
         }
         td[class='left_pad'] 
         {
         padding-left:15px !important;
         padding-right:15px !important;
         }
         }
         @media only screen and (max-width:540px) 
         {
         table[class='MainContainer'], td[class='cell'] 
         {
         width: 100% !important;
         height:auto !important; 
         }
         td[class='specbundle'] 
         {
         width: 100% !important;
         float:left !important;
         font-size:13px !important;
         line-height:17px !important;
         display:block !important;
         padding-bottom:15px !important;
         }	
         td[class='specbundle2'] 
         {
         width:80% !important;
         float:left !important;
         font-size:13px !important;
         line-height:17px !important;
         display:block !important;
         padding-bottom:10px !important;
         padding-left:10% !important;
         padding-right:10% !important;
         }
         td[class='spechide'] 
         {
         display:none !important;
         }
         img[class='banner'] 
         {
         width: 100% !important;
         height: auto !important;
         }
         td[class='left_pad'] 
         {
         padding-left:15px !important;
         padding-right:15px !important;
         }
         }
         .contentEditable h2.big,.contentEditable h1.big{
         font-size: 26px !important;
         }
         .contentEditable h2.bigger,.contentEditable h1.bigger{
         font-size: 37px !important;
         }
         td,table{
         vertical-align: top;
         }
         td.middle{
         vertical-align: middle;
         }
         a.link1{
         font-size:13px;
         color:#27A1E5;
         line-height: 24px;
         text-decoration:none;
         }
         a{
         text-decoration: none;
         }
         .link2{
         color:#ffffff;
         border-top:10px solid #27A1E5;
         border-bottom:10px solid #27A1E5;
         border-left:18px solid #27A1E5;
         border-right:18px solid #27A1E5;
         border-radius:3px;
         -moz-border-radius:3px;
         -webkit-border-radius:3px;
         background:#27A1E5;
         }
         .link3{
         color:#555555;
         border:1px solid #cccccc;
         padding:10px 18px;
         border-radius:3px;
         -moz-border-radius:3px;
         -webkit-border-radius:3px;
         background:#ffffff;
         }
         .link4{
         color:#27A1E5;
         line-height: 24px;
         }
         h2,h1{
         line-height: 20px;
         }
         p{
         font-size: 14px;
         line-height: 21px;
         color:#AAAAAA;
         }
         .contentEditable li{
         }
         .appart p{
         }
         .bgItem{
         background: #ffffff;
         }
         .bgBody{
         background: #ffffff;
         }
         img { 
         outline:none; 
         text-decoration:none; 
         -ms-interpolation-mode: bicubic;
         width: auto;
         max-width: 100%; 
         clear: both; 
         display: block;
         float: none;
         }
      </style>
      <script type='colorScheme' class='swatch active'>
         {
             'name':'Default',
             'bgBody':'ffffff',
             'link':'27A1E5',
             'color':'AAAAAA',
             'bgItem':'ffffff',
             'title':'444444'
         }
      </script>
   </head>
   <body paddingwidth='0' paddingheight='0' bgcolor='#d1d3d4' style='padding-top: 0; padding-bottom: 0; padding-top: 0; padding-bottom: 0; background-repeat: repeat; width: 100% !important; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; -webkit-font-smoothing: antialiased;' offset='0' toppadding='0' leftpadding='0'>
      <table width='100%' border='0' cellspacing='0' cellpadding='0'>
         <tbody>
            <tr>
               <td>
                  <table width='800' border='0' cellspacing='0' cellpadding='0' align='center' bgcolor='#ffffff' style='font-family:helvetica, sans-serif;' class='MainContainer'>
                     <!-- =============== START HEADER =============== -->
                     <tbody>
                        <tr>
                           <td>
                              <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                 <tbody>
                                    <tr>
                                       <td valign='top' width='20' style='
                                          background-color: #4a4a4a;
                                          '>&nbsp;</td>
                                       <td style='
                                          background-color: #f3f5f0;
                                          '>
                                          <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                             <tbody>
                                                <tr>
                                                   <td class='movableContentContainer'>
                                                      <div class='movableContent' style='border: 0px; padding-top: 0px; position: relative;'>
                                                         <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                               <tr>
                                                                  <td height='15'></td>
                                                               </tr>
                                                               <tr>
                                                                  <td>
                                                                     <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                                                        <tbody>
                                                                           <tr>
                                                                              <td valign='top'>
                                                                                 <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                                                                    <tbody>
                                                                                       <tr>
                                                                                          <td valign='top' width='60'></td>
                                                                                          <td width='10' valign='top'>&nbsp;</td>
                                                                                          <td valign='middle' style='vertical-align: middle;'>
                                                                                             <div class='contentEditableContainer contentTextEditable'>
                                                                                                <div class='contentEditable' style='text-align: left;font-weight: light; color:#555555;font-size:26;line-height: 39px;font-family: Helvetica Neue;'>
                                                                                                   <h1 class='big'><label style='color:#444444'>" + objParametrosCorreoDto.Empresa + @"</label></h1>
                                                                                                </div>
                                                                                             </div>
                                                                                          </td>
                                                                                       </tr>
                                                                                    </tbody>
                                                                                 </table>
                                                                              </td>
                                                                              <td valign='top' width='90' class='spechide'>&nbsp;</td>
                                                                              <td valign='middle' style='vertical-align: middle;' width='250'>
                                                                                 <div class='contentEditableContainer contentTextEditable'>
                                                                                    <div class='contentEditable' style='text-align: right;'>
                                                                                       <span>Asunto: </span> <label>usuario creado</label>
                                                                                    </div>
                                                                                 </div>
                                                                              </td>
                                                                           </tr>
                                                                        </tbody>
                                                                     </table>
                                                                  </td>
                                                               </tr>
                                                               <tr>
                                                                  <td height='15'></td>
                                                               </tr>
                                                               <tr>
                                                                  <td>
                                                                     <hr style='height: 3px;background:#DDDDDD;border:none;'>
                                                                  </td>
                                                               </tr>
                                                            </tbody>
                                                         </table>
                                                      </div>
                                                      <!-- =============== END HEADER =============== -->
                                                      <!-- =============== START BODY =============== -->
                                                      <div class='movableContent' style='border: 0px; padding-top: 0px; position: relative;'>
                                                         <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                               <tr>
                                                                  <td style='background: #8cb15500;border-radius:6px;-moz-border-radius:6px;-webkit-border-radius:6px;'>
                                                                     <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                                                        <tbody>
                                                                           <tr>
                                                                              <td width='10' valign='top'>&nbsp;</td>
                                                                              <td valign='top'>
                                                                                 <table width='100%' border='0' cellspacing='0' cellpadding='0' align='center'>
                                                                                    <tbody>
                                                                                       <tr>
                                                                                          <td height='25'></td>
                                                                                       </tr>
                                                                                       <tr>
                                                                                          <td>
                                                                                             <div class='contentEditableContainer contentTextEditable'>
                                                                                                <div class='contentEditable' style='text-align: center;'>
                                                                                                   <h2 style='font-size: 23px;'>Se recupero su usuario, al ingresar sugerimos cambiar de clave</h2>
                                                                                                   <br>
                                                                                                   <div style='width:100%'>
                                                                                                      <div style='width: 101%;padding-left: 0%;margin-bottom:15px;overflow-y:hidden;text-align:center;'>
                                                                                                         <table style='width:100%;margin-bottom:15px;overflow-y:hidden;border: 1px solid orange;text-align:center;'>
                                                                                                            <thead>
                                                                                                               <tr style='color:white;background-color: #FF9800;'>
                                                                                                                  <th>USUARIO</th>
                                                                                                                  <th>PASSWORD</th>
                                                                                                                  <th>PERFIL</th>
                                                                                                                  <th>EMPRESA</th>
                                                                                                                  <th>NIVEL</th>
                                                                                                               </tr>
                                                                                                            </thead>
                                                                                                            <tbody>
                                                                                                               <tr style='color: #4a4a4a;background-color: white;'>
                                                                                                                  <td>" + objUsuario.Usuarios + @"</td>
                                                                                                                  <td>" + objUsuario.Password + @"</td>
                                                                                                                  <td>" + objUsuario.Perfil + @"</td>
                                                                                                                  <td>" + objUsuario.EmpresaNombre + @"</td>
                                                                                                                  <td>" + objUsuario.Tipo + @"</td>
                                                                                                               </tr>
                                                                                                            </tbody>
                                                                                                         </table>
                                                                                                      </div>
                                                                                                   </div>
                                                                                                   <br>
                                                                                                   <a target='_blank' href='" + urlInicio + @"' class='link3' style='color:#555555;'>Ir a la pagina</a>
                                                                                                   <br>
                                                                                                </div>
                                                                                             </div>
                                                                                          </td>
                                                                                       </tr>
                                                                                       <tr>
                                                                                          <td height='24'></td>
                                                                                       </tr>
                                                                                    </tbody>
                                                                                 </table>
                                                                              </td>
                                                                              <td width='10' valign='top'>&nbsp;</td>
                                                                           </tr>
                                                                        </tbody>
                                                                     </table>
                                                                  </td>
                                                               </tr>
                                                            </tbody>
                                                         </table>
                                                      </div>
                                                      <!-- =============== END BODY =============== -->
                                                      <!-- =============== START FOOTER =============== -->
                                                      <div class='movableContent' style='border: 0px; padding-top: 0px; position: relative;'>
                                                         <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                               <tr>
                                                                  <td>
                                                                     <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                                                        <tbody>
                                                                           <tr>
                                                                              <td valign='top' width='90' class='spechide'>&nbsp;</td>
                                                                              <td>
                                                                                 <table width='100%' cellpadding='0' cellspacing='0' align='center'>
                                                                                    <tbody>
                                                                                       <tr>
                                                                                          <td>
                                                                                             <div class='contentEditableContainer contentTextEditable'>
                                                                                             </div>
                                                                                          </td>
                                                                                       </tr>
                                                                                    </tbody>
                                                                                 </table>
                                                                              </td>
                                                                              <td valign='top' width='90' class='spechide'>&nbsp;</td>
                                                                           </tr>
                                                                        </tbody>
                                                                     </table>
                                                                  </td>
                                                               </tr>
                                                            </tbody>
                                                         </table>
                                                      </div>
                                                   </td>
                                                </tr>
                                             </tbody>
                                          </table>
                                       </td>
                                       <td valign='top' width='20' style='
                                          background-color: #f3f5f0;
                                          '>&nbsp;</td>
                                    </tr>
                                 </tbody>
                              </table>
                           </td>
                        </tr>
                     </tbody>
                  </table>
               </td>
            </tr>
         </tbody>
      </table>
   </body>
</html>";

            AlternateView htmlView =
                AlternateView.CreateAlternateViewFromString(html,
                                        Encoding.UTF8,
                                        MediaTypeNames.Text.Html);

            // Creamos el recurso a incrustar. Observad
            // que el ID que le asignamos (arbitrario) está
            // referenciado desde el código HTML como origen

            // Por último, vinculamos ambas vistas al mensaje...

            mail.AlternateViews.Add(plainView);
            mail.AlternateViews.Add(htmlView);

            // Y lo enviamos a través del servidor SMTP...

            var smtp = new SmtpClient
            {
                Host = objParametrosCorreoDto.Host, // set your SMTP server name here
                Port = objParametrosCorreoDto.Port, // Port 
                EnableSsl = true,
                Credentials = new NetworkCredential(objParametrosCorreoDto.Correo, objParametrosCorreoDto.Password)
            };

            smtp.Send(mail);
        }

        public string EnviarCorreoDigitalizacion(ParametrosCorreoDto objParametrosCorreoDto, List<UsuarioConsultaDto> lstUsuario, CorreoConsultaDto objCorreoConsultaDto,byte[] excel)
        {
            // Montamos la estructura básica del mensaje...
            var location = new Uri($"{Request.Scheme}://{Request.Host}");
            var url = location.AbsoluteUri;
            var urlInicio = url + "Home";
            var usuarioEnviado = "";
            var usuarioNoEnviados = "";
            objParametrosCorreoDto.Password = Encriptador.Desencriptar(objParametrosCorreoDto.Password);
            foreach (var itemUsuario in lstUsuario.ToList())
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(itemUsuario.Correo);
                    mail.To.Add(itemUsuario.Correo);
                    //Asunto
                    mail.Subject = "Digitalizacion estado " + objCorreoConsultaDto.Estado;

                    // Creamos la vista para clientes que
                    string text = "Hola, ayer estuve disfrutando de " +
                                  "un paisaje estupendo.";

                    AlternateView plainView = AlternateView.CreateAlternateViewFromString(text, Encoding.UTF8, MediaTypeNames.Text.Plain);
                    // Ahora creamos la vista para clientes que 
                    // pueden mostrar contenido HTML...

                    //string _html = @"<img src='cid:imagen' />";
                    string html = @"<html xmlns='http://www.w3.org/1999/xhtml'>
   <head>
      <meta http-equiv='Content-Type' content='text/html; charset=utf-8'>
      <title>[SUBJECT]</title>
      <style type='text/css'>
         body {
         padding-top: 0 !important;
         padding-bottom: 0 !important;
         padding-top: 0 !important;
         padding-bottom: 0 !important;
         margin:0 !important;
         width: 100% !important;
         -webkit-text-size-adjust: 100% !important;
         -ms-text-size-adjust: 100% !important;
         -webkit-font-smoothing: antialiased !important;
         }
         .tableContent img {
         border: 0 !important;
         display: block !important;
         outline: none !important;
         }
         a{
         color:#382F2E;
         }
         p, h1,h2,ul,ol,li,div{
         margin:0;
         padding:0;
         }
         h1,h2{
         font-weight: normal;
         background:transparent !important;
         border:none !important;
         }
         @media only screen and (max-width:480px)
         {
         table[class='MainContainer'], td[class='cell'] 
         {
         width: 100% !important;
         height:auto !important; 
         }
         td[class='specbundle'] 
         {
         width: 100% !important;
         float:left !important;
         font-size:13px !important;
         line-height:17px !important;
         display:block !important;
         padding-bottom:15px !important;
         }	
         td[class='specbundle2'] 
         {
         width:80% !important;
         float:left !important;
         font-size:13px !important;
         line-height:17px !important;
         display:block !important;
         padding-bottom:10px !important;
         padding-left:10% !important;
         padding-right:10% !important;
         }
         td[class='spechide'] 
         {
         display:none !important;
         }
         img[class='banner'] 
         {
         width: 100% !important;
         height: auto !important;
         }
         td[class='left_pad'] 
         {
         padding-left:15px !important;
         padding-right:15px !important;
         }
         }
         @media only screen and (max-width:540px) 
         {
         table[class='MainContainer'], td[class='cell'] 
         {
         width: 100% !important;
         height:auto !important; 
         }
         td[class='specbundle'] 
         {
         width: 100% !important;
         float:left !important;
         font-size:13px !important;
         line-height:17px !important;
         display:block !important;
         padding-bottom:15px !important;
         }	
         td[class='specbundle2'] 
         {
         width:80% !important;
         float:left !important;
         font-size:13px !important;
         line-height:17px !important;
         display:block !important;
         padding-bottom:10px !important;
         padding-left:10% !important;
         padding-right:10% !important;
         }
         td[class='spechide'] 
         {
         display:none !important;
         }
         img[class='banner'] 
         {
         width: 100% !important;
         height: auto !important;
         }
         td[class='left_pad'] 
         {
         padding-left:15px !important;
         padding-right:15px !important;
         }
         }
         .contentEditable h2.big,.contentEditable h1.big{
         font-size: 26px !important;
         }
         .contentEditable h2.bigger,.contentEditable h1.bigger{
         font-size: 37px !important;
         }
         td,table{
         vertical-align: top;
         }
         td.middle{
         vertical-align: middle;
         }
         a.link1{
         font-size:13px;
         color:#27A1E5;
         line-height: 24px;
         text-decoration:none;
         }
         a{
         text-decoration: none;
         }
         .link2{
         color:#ffffff;
         border-top:10px solid #27A1E5;
         border-bottom:10px solid #27A1E5;
         border-left:18px solid #27A1E5;
         border-right:18px solid #27A1E5;
         border-radius:3px;
         -moz-border-radius:3px;
         -webkit-border-radius:3px;
         background:#27A1E5;
         }
         .link3{
         color:#555555;
         border:1px solid #cccccc;
         padding:10px 18px;
         border-radius:3px;
         -moz-border-radius:3px;
         -webkit-border-radius:3px;
         background:#ffffff;
         }
         .link4{
         color:#27A1E5;
         line-height: 24px;
         }
         h2,h1{
         line-height: 20px;
         }
         p{
         font-size: 14px;
         line-height: 21px;
         color:#AAAAAA;
         }
         .contentEditable li{
         }
         .appart p{
         }
         .bgItem{
         background: #ffffff;
         }
         .bgBody{
         background: #ffffff;
         }
         img { 
         outline:none; 
         text-decoration:none; 
         -ms-interpolation-mode: bicubic;
         width: auto;
         max-width: 100%; 
         clear: both; 
         display: block;
         float: none;
         }
      </style>
      <script type='colorScheme' class='swatch active'>
         {
             'name':'Default',
             'bgBody':'ffffff',
             'link':'27A1E5',
             'color':'AAAAAA',
             'bgItem':'ffffff',
             'title':'444444'
         }
      </script>
   </head>
   <body paddingwidth='0' paddingheight='0' bgcolor='#d1d3d4' style='padding-top: 0; padding-bottom: 0; padding-top: 0; padding-bottom: 0; background-repeat: repeat; width: 100% !important; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; -webkit-font-smoothing: antialiased;' offset='0' toppadding='0' leftpadding='0'>
      <table width='100%' border='0' cellspacing='0' cellpadding='0'>
         <tbody>
            <tr>
               <td>
                  <table width='800' border='0' cellspacing='0' cellpadding='0' align='center' bgcolor='#ffffff' style='font-family:helvetica, sans-serif;' class='MainContainer'>
                     <!-- =============== START HEADER =============== -->
                     <tbody>
                        <tr>
                           <td>
                              <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                 <tbody>
                                    <tr>
                                       <td valign='top' width='20' style='
                                          background-color: orange;
                                          '>&nbsp;</td>
                                       <td style='
                                          background-color: #f3f5f0;
                                          '>
                                          <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                             <tbody>
                                                <tr>
                                                   <td class='movableContentContainer'>
                                                      <div class='movableContent' style='border: 0px; padding-top: 0px; position: relative;'>
                                                         <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                               <tr>
                                                                  <td height='15'></td>
                                                               </tr>
                                                               <tr>
                                                                  <td>
                                                                     <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                                                        <tbody>
                                                                           <tr>
                                                                              <td valign='top'>
                                                                                 <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                                                                    <tbody>
                                                                                       <tr>
                                                                                          <td valign='top' width='60'></td>
                                                                                          <td width='10' valign='top'>&nbsp;</td>
                                                                                          <td valign='middle' style='vertical-align: middle;'>
                                                                                             <div class='contentEditableContainer contentTextEditable'>
                                                                                                <div class='contentEditable' style='text-align: left;font-weight: light; color:#555555;font-size:26;line-height: 39px;font-family: Helvetica Neue;'>
                                                                                                   <h1 class='big'><label style='color:#444444'>" + objParametrosCorreoDto.Empresa + @"</label></h1>
                                                                                                </div>
                                                                                             </div>
                                                                                          </td>
                                                                                       </tr>
                                                                                    </tbody>
                                                                                 </table>
                                                                              </td>
                                                                              <td valign='top' width='90' class='spechide'>&nbsp;</td>
                                                                              <td valign='middle' style='vertical-align: middle;' width='250'>
                                                                                 <div class='contentEditableContainer contentTextEditable'>
                                                                                    <div class='contentEditable' style='text-align: right;'>
                                                                                       <span>Asunto: </span> <label> Digitalizacion estado " + objCorreoConsultaDto.Estado + @"</label>
                                                                                    </div>
                                                                                 </div>
                                                                              </td>
                                                                           </tr>
                                                                        </tbody>
                                                                     </table>
                                                                  </td>
                                                               </tr>
                                                               <tr>
                                                                  <td height='15'></td>
                                                               </tr>
                                                               <tr>
                                                                  <td>
                                                                     <hr style='height:1px;background:#DDDDDD;border:none;'>
                                                                  </td>
                                                               </tr>
                                                            </tbody>
                                                         </table>
                                                      </div>
                                                      <!-- =============== END HEADER =============== -->
                                                      <!-- =============== START BODY =============== -->
                                                      <div class='movableContent' style='border: 0px; padding-top: 0px; position: relative;'>
                                                         <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                               <tr>
                                                                  <td style='background: #8cb15500;border-radius:6px;-moz-border-radius:6px;-webkit-border-radius:6px;'>
                                                                     <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                                                        <tbody>
                                                                           <tr>
                                                                              <td width='10' valign='top'>&nbsp;</td>
                                                                              <td valign='top'>
                                                                                 <table width='100%' border='0' cellspacing='0' cellpadding='0' align='center'>
                                                                                    <tbody>
                                                                                       <tr>
                                                                                          <td height='25'></td>
                                                                                       </tr>
                                                                                       <tr>
                                                                                          <td>
                                                                                             <div class='contentEditableContainer contentTextEditable'>
                                                                                                <div class='contentEditable' style='text-align: center;'>
                                                                                                   <h2 style='font-size: 23px;'>Estados de la digitalizacion en Revision y Aprobados </h2>
                                                                                                   <br>
                                                                                                   <p style='
                                                                                                      font-size: 17px;text-align: initial;color:#4a4a4a;
                                                                                                      '>Estimado usuario, se genero el resumen de la digitalzaion de sus trabajadores, favor de revisar las actividades que se estan realizando, descarge el archivo excel para ver los detalles</p>
                                                                                                   <div style='width:100%'>
                                                                                                      <div style='width: 101%;padding-left: 0%;margin-bottom:15px;overflow-y:hidden;text-align:center;'>
                                                                                                        <p style='
                                                                                                      font-size: 20px;
                                                                                                      color: #4a4a4a;
                                                                                                      '>Usuario emisor : " + objCorreoConsultaDto.Usuario + @"</p> 
                                                                                                      </div>
                                                                                                   </div>
                                                                                                   <br>
                                                                                                   <a target='_blank' href='" + urlInicio + @"' class='link3' style='color:#555555;'>Ir a la pagina</a>
                                                                                                   <br>
                                                                                                </div>
                                                                                             </div>
                                                                                          </td>
                                                                                       </tr>
                                                                                       <tr>
                                                                                          <td height='5'></td>
                                                                                       </tr>
                                                                                    </tbody>
                                                                                 </table>
                                                                              </td>
                                                                              <td width='10' valign='top'>&nbsp;</td>
                                                                           </tr>
                                                                        </tbody>
                                                                     </table>
                                                                  </td>
                                                               </tr>
                                                            </tbody>
                                                         </table>
                                                      </div>
                                                      <!-- =============== END BODY =============== -->
                                                      <!-- =============== START FOOTER =============== -->
                                                      <div class='movableContent' style='border: 0px; padding-top: 0px; position: relative;'>
                                                         <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                               <tr>
                                                                  <td>
                                                                     <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                                                        <tbody>
                                                                           <tr>
                                                                              <td valign='top' width='90' class='spechide'>&nbsp;</td>
                                                                              <td>
                                                                                 <table width='100%' cellpadding='0' cellspacing='0' align='center'>
                                                                                    <tbody>
                                                                                       <tr>
                                                                                          <td>
                                                                                             <div class='contentEditableContainer contentTextEditable'>
                                                                                             </div>
                                                                                          </td>
                                                                                       </tr>
                                                                                    </tbody>
                                                                                 </table>
                                                                              </td>
                                                                              <td valign='top' width='90' class='spechide'>&nbsp;</td>
                                                                           </tr>
                                                                        </tbody>
                                                                     </table>
                                                                  </td>
                                                               </tr>
                                                            </tbody>
                                                         </table>
                                                      </div>
                                                   </td>
                                                </tr>
                                             </tbody>
                                          </table>
                                       </td>
                                       <td valign='top' width='20' style='
                                          background-color: #f3f5f0;
                                          '>&nbsp;</td>
                                    </tr>
                                 </tbody>
                              </table>
                           </td>
                        </tr>
                     </tbody>
                  </table>
               </td>
            </tr>
         </tbody>
      </table>
   </body>
</html>";

                    AlternateView htmlView =
                        AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8,
                                                MediaTypeNames.Text.Html);
                    // de la image
                    //var _logo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "logo-small.png");
                    //LinkedResource img = new LinkedResource(_logo, MediaTypeNames.Image.Jpeg);
                    //img.ContentId = "imagen";

                    //// Lo incrustamos en la vista HTML...
                    //htmlView.LinkedResources.Add(img);

                    // Por último, vinculamos ambas vistas al mensaje...
                    var _hoy = DateTime.Now;
                    var nombreArchivo = "Digitalizacion("+_hoy.ToString("ddMMyyy")+ ").xlsx";
                    MemoryStream ms = new MemoryStream(excel);
                    //var memory = new MemoryStream();
                    //using (var stream = new FileStream(_excel, FileMode.Open))
                    //{
                    //    stream.CopyToAsync(memory);
                    //}
                    //memory.Position = 0;
                    //if (mail.Attachments[0].ContentStream == ms) Console.WriteLine("Streams are referencing the same resource");
                    //Console.WriteLine("Stream length: " + mail.Attachments[0].ContentStream.Length);
                    mail.Attachments.Add(new Attachment(ms, nombreArchivo));
                    mail.AlternateViews.Add(plainView);
                    mail.AlternateViews.Add(htmlView);
                    
                    // Y lo enviamos a través del servidor SMTP...
                    objParametrosCorreoDto.Password = (objParametrosCorreoDto.Password);
                    var smtp = new SmtpClient
                    {
                        Host = objParametrosCorreoDto.Host, // set your SMTP server name here
                        Port = objParametrosCorreoDto.Port, // Port 
                        EnableSsl = true,
                        Credentials = new NetworkCredential(objParametrosCorreoDto.Correo, objParametrosCorreoDto.Password)
                    };

                    smtp.Send(mail);
                    usuarioEnviado += itemUsuario.Usuarios + ",";
                }
                catch (Exception ex)
                {
                    usuarioNoEnviados+= itemUsuario.Usuarios + ",";
                    throw;
                }
            }
            var resultado = usuarioEnviado + "\n" + usuarioNoEnviados;
            return resultado;
        }

        public string EnviarCorreoDigitalizacion2(ParametrosCorreoDto objParametrosCorreoDto, List<UsuarioConsultaDto> lstUsuario, List<DigitalizacionCorreoDto> lstDigitalizacion, CorreoConsultaDto objCorreoConsultaDto)
        {
            // Montamos la estructura básica del mensaje...
            var location = new Uri($"{Request.Scheme}://{Request.Host}");
            var url = location.AbsoluteUri;
            var urlInicio = url + "Home";
            var usuarioEnviado = "";
            var usuarioNoEnviados = "";
            foreach (var itemUsuario in lstUsuario.ToList())
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(itemUsuario.Correo);
                    mail.To.Add(itemUsuario.Correo);
                    //Asunto
                    mail.Subject = "Digitalizacion estado " + objCorreoConsultaDto.Estado;

                    // Creamos la vista para clientes que
                    string text = "Hola, ayer estuve disfrutando de " +
                                  "un paisaje estupendo.";

                    AlternateView plainView = AlternateView.CreateAlternateViewFromString(text, Encoding.UTF8, MediaTypeNames.Text.Plain);
                    // Ahora creamos la vista para clientes que 
                    // pueden mostrar contenido HTML...
                    var _thTabla = "";
                    foreach (var item in lstDigitalizacion.ToList())
                    {
                        _thTabla += "<tr style='color:white;background-color: orange;'><td>" + item.Nombre + "</td>" +
                                       "<td>" + item.Numero + "</td>" +
                                       "<td>" + item.Empresa + "</td>" +
                                       "<td>" + item.Categoria + "</td>" +
                                       "<td>" + item.TipoEmpresa + "</td></tr>";
                    }
                    //string _html = @"<img src='cid:imagen' />";
                    string html = @"<html xmlns='http://www.w3.org/1999/xhtml'>
   <head>
      <meta http-equiv='Content-Type' content='text/html; charset=utf-8'>
      <title>[SUBJECT]</title>
      <style type='text/css'>
         body {
         padding-top: 0 !important;
         padding-bottom: 0 !important;
         padding-top: 0 !important;
         padding-bottom: 0 !important;
         margin:0 !important;
         width: 100% !important;
         -webkit-text-size-adjust: 100% !important;
         -ms-text-size-adjust: 100% !important;
         -webkit-font-smoothing: antialiased !important;
         }
         .tableContent img {
         border: 0 !important;
         display: block !important;
         outline: none !important;
         }
         a{
         color:#382F2E;
         }
         p, h1,h2,ul,ol,li,div{
         margin:0;
         padding:0;
         }
         h1,h2{
         font-weight: normal;
         background:transparent !important;
         border:none !important;
         }
         @media only screen and (max-width:480px)
         {
         table[class='MainContainer'], td[class='cell'] 
         {
         width: 100% !important;
         height:auto !important; 
         }
         td[class='specbundle'] 
         {
         width: 100% !important;
         float:left !important;
         font-size:13px !important;
         line-height:17px !important;
         display:block !important;
         padding-bottom:15px !important;
         }	
         td[class='specbundle2'] 
         {
         width:80% !important;
         float:left !important;
         font-size:13px !important;
         line-height:17px !important;
         display:block !important;
         padding-bottom:10px !important;
         padding-left:10% !important;
         padding-right:10% !important;
         }
         td[class='spechide'] 
         {
         display:none !important;
         }
         img[class='banner'] 
         {
         width: 100% !important;
         height: auto !important;
         }
         td[class='left_pad'] 
         {
         padding-left:15px !important;
         padding-right:15px !important;
         }
         }
         @media only screen and (max-width:540px) 
         {
         table[class='MainContainer'], td[class='cell'] 
         {
         width: 100% !important;
         height:auto !important; 
         }
         td[class='specbundle'] 
         {
         width: 100% !important;
         float:left !important;
         font-size:13px !important;
         line-height:17px !important;
         display:block !important;
         padding-bottom:15px !important;
         }	
         td[class='specbundle2'] 
         {
         width:80% !important;
         float:left !important;
         font-size:13px !important;
         line-height:17px !important;
         display:block !important;
         padding-bottom:10px !important;
         padding-left:10% !important;
         padding-right:10% !important;
         }
         td[class='spechide'] 
         {
         display:none !important;
         }
         img[class='banner'] 
         {
         width: 100% !important;
         height: auto !important;
         }
         td[class='left_pad'] 
         {
         padding-left:15px !important;
         padding-right:15px !important;
         }
         }
         .contentEditable h2.big,.contentEditable h1.big{
         font-size: 26px !important;
         }
         .contentEditable h2.bigger,.contentEditable h1.bigger{
         font-size: 37px !important;
         }
         td,table{
         vertical-align: top;
         }
         td.middle{
         vertical-align: middle;
         }
         a.link1{
         font-size:13px;
         color:#27A1E5;
         line-height: 24px;
         text-decoration:none;
         }
         a{
         text-decoration: none;
         }
         .link2{
         color:#ffffff;
         border-top:10px solid #27A1E5;
         border-bottom:10px solid #27A1E5;
         border-left:18px solid #27A1E5;
         border-right:18px solid #27A1E5;
         border-radius:3px;
         -moz-border-radius:3px;
         -webkit-border-radius:3px;
         background:#27A1E5;
         }
         .link3{
         color:#555555;
         border:1px solid #cccccc;
         padding:10px 18px;
         border-radius:3px;
         -moz-border-radius:3px;
         -webkit-border-radius:3px;
         background:#ffffff;
         }
         .link4{
         color:#27A1E5;
         line-height: 24px;
         }
         h2,h1{
         line-height: 20px;
         }
         p{
         font-size: 14px;
         line-height: 21px;
         color:#AAAAAA;
         }
         .contentEditable li{
         }
         .appart p{
         }
         .bgItem{
         background: #ffffff;
         }
         .bgBody{
         background: #ffffff;
         }
         img { 
         outline:none; 
         text-decoration:none; 
         -ms-interpolation-mode: bicubic;
         width: auto;
         max-width: 100%; 
         clear: both; 
         display: block;
         float: none;
         }
      </style>
      <script type='colorScheme' class='swatch active'>
         {
             'name':'Default',
             'bgBody':'ffffff',
             'link':'27A1E5',
             'color':'AAAAAA',
             'bgItem':'ffffff',
             'title':'444444'
         }
      </script>
   </head>
   <body paddingwidth='0' paddingheight='0' bgcolor='#d1d3d4' style='padding-top: 0; padding-bottom: 0; padding-top: 0; padding-bottom: 0; background-repeat: repeat; width: 100% !important; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; -webkit-font-smoothing: antialiased;' offset='0' toppadding='0' leftpadding='0'>
      <table width='100%' border='0' cellspacing='0' cellpadding='0'>
         <tbody>
            <tr>
               <td>
                  <table width='800' border='0' cellspacing='0' cellpadding='0' align='center' bgcolor='#ffffff' style='font-family:helvetica, sans-serif;' class='MainContainer'>
                     <!-- =============== START HEADER =============== -->
                     <tbody>
                        <tr>
                           <td>
                              <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                 <tbody>
                                    <tr>
                                       <td valign='top' width='20' style='
                                          background-color: orange;
                                          '>&nbsp;</td>
                                       <td style='
                                          background-color: #f3f5f0;
                                          '>
                                          <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                             <tbody>
                                                <tr>
                                                   <td class='movableContentContainer'>
                                                      <div class='movableContent' style='border: 0px; padding-top: 0px; position: relative;'>
                                                         <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                               <tr>
                                                                  <td height='15'></td>
                                                               </tr>
                                                               <tr>
                                                                  <td>
                                                                     <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                                                        <tbody>
                                                                           <tr>
                                                                              <td valign='top'>
                                                                                 <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                                                                    <tbody>
                                                                                       <tr>
                                                                                          <td valign='top' width='60'></td>
                                                                                          <td width='10' valign='top'>&nbsp;</td>
                                                                                          <td valign='middle' style='vertical-align: middle;'>
                                                                                             <div class='contentEditableContainer contentTextEditable'>
                                                                                                <div class='contentEditable' style='text-align: left;font-weight: light; color:#555555;font-size:26;line-height: 39px;font-family: Helvetica Neue;'>
                                                                                                   <h1 class='big'><label style='color:#444444'>" + objParametrosCorreoDto.Empresa + @"</label></h1>
                                                                                                </div>
                                                                                             </div>
                                                                                          </td>
                                                                                       </tr>
                                                                                    </tbody>
                                                                                 </table>
                                                                              </td>
                                                                              <td valign='top' width='90' class='spechide'>&nbsp;</td>
                                                                              <td valign='middle' style='vertical-align: middle;' width='250'>
                                                                                 <div class='contentEditableContainer contentTextEditable'>
                                                                                    <div class='contentEditable' style='text-align: right;'>
                                                                                       <span>Asunto: </span> <label> Digitalizacion estado " + objCorreoConsultaDto.Estado + @"</label>
                                                                                    </div>
                                                                                 </div>
                                                                              </td>
                                                                           </tr>
                                                                        </tbody>
                                                                     </table>
                                                                  </td>
                                                               </tr>
                                                               <tr>
                                                                  <td height='15'></td>
                                                               </tr>
                                                               <tr>
                                                                  <td>
                                                                     <hr style='height:1px;background:#DDDDDD;border:none;'>
                                                                  </td>
                                                               </tr>
                                                            </tbody>
                                                         </table>
                                                      </div>
                                                      <!-- =============== END HEADER =============== -->
                                                      <!-- =============== START BODY =============== -->
                                                      <div class='movableContent' style='border: 0px; padding-top: 0px; position: relative;'>
                                                         <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                               <tr>
                                                                  <td style='background: #8cb15500;border-radius:6px;-moz-border-radius:6px;-webkit-border-radius:6px;'>
                                                                     <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                                                        <tbody>
                                                                           <tr>
                                                                              <td width='10' valign='top'>&nbsp;</td>
                                                                              <td valign='top'>
                                                                                 <table width='100%' border='0' cellspacing='0' cellpadding='0' align='center'>
                                                                                    <tbody>
                                                                                       <tr>
                                                                                          <td height='25'></td>
                                                                                       </tr>
                                                                                       <tr>
                                                                                          <td>
                                                                                             <div class='contentEditableContainer contentTextEditable'>
                                                                                                <div class='contentEditable' style='text-align: center;'>
                                                                                                   <h2 style='font-size: 23px;'>Estado de la digitalizacion " + objCorreoConsultaDto.Estado + @"</h2>
                                                                                                   <br>
                                                                                                   <p style='
                                                                                                      font-size: 20px;
                                                                                                      color: #4a4a4a;
                                                                                                      '>Usuario emisor : " + objCorreoConsultaDto.Usuario + @"</p>
                                                                                                   <div style='width:100%'>
                                                                                                      <div style='width: 101%;padding-left: 0%;margin-bottom:15px;overflow-y:hidden;text-align:center;'>
                                                                                                         <table style='width:100%;margin-bottom:15px;overflow-y:hidden;border: 1px solid orange;text-align:center;'>
                                                                                                            <thead>
                                                                                                               <tr style='color:white;background-color: #4a4a4a;'>
                                                                                                                  <th>NOMBRE</th>
                                                                                                                  <th>CODIGO</th>
                                                                                                                  <th>EMPRESA</th>
                                                                                                                  <th>CATEGORIA</th>
                                                                                                                  <th>TIPO EMPRESA</th>
                                                                                                               </tr>
                                                                                                            </thead>
                                                                                                            <tbody>
                                                                                                               " + _thTabla + @"
                                                                                                                                                                                                                    
                                                                                                            </tbody>
                                                                                                         </table>
                                                                                                      </div>
                                                                                                   </div>
                                                                                                   <br>
                                                                                                   <a target='_blank' href='" + urlInicio + @"' class='link3' style='color:#555555;'>Ir a la pagina</a>
                                                                                                   <br>
                                                                                                </div>
                                                                                             </div>
                                                                                          </td>
                                                                                       </tr>
                                                                                       <tr>
                                                                                          <td height='5'></td>
                                                                                       </tr>
                                                                                    </tbody>
                                                                                 </table>
                                                                              </td>
                                                                              <td width='10' valign='top'>&nbsp;</td>
                                                                           </tr>
                                                                        </tbody>
                                                                     </table>
                                                                  </td>
                                                               </tr>
                                                            </tbody>
                                                         </table>
                                                      </div>
                                                      <!-- =============== END BODY =============== -->
                                                      <!-- =============== START FOOTER =============== -->
                                                      <div class='movableContent' style='border: 0px; padding-top: 0px; position: relative;'>
                                                         <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                               <tr>
                                                                  <td>
                                                                     <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                                                        <tbody>
                                                                           <tr>
                                                                              <td valign='top' width='90' class='spechide'>&nbsp;</td>
                                                                              <td>
                                                                                 <table width='100%' cellpadding='0' cellspacing='0' align='center'>
                                                                                    <tbody>
                                                                                       <tr>
                                                                                          <td>
                                                                                             <div class='contentEditableContainer contentTextEditable'>
                                                                                             </div>
                                                                                          </td>
                                                                                       </tr>
                                                                                    </tbody>
                                                                                 </table>
                                                                              </td>
                                                                              <td valign='top' width='90' class='spechide'>&nbsp;</td>
                                                                           </tr>
                                                                        </tbody>
                                                                     </table>
                                                                  </td>
                                                               </tr>
                                                            </tbody>
                                                         </table>
                                                      </div>
                                                   </td>
                                                </tr>
                                             </tbody>
                                          </table>
                                       </td>
                                       <td valign='top' width='20' style='
                                          background-color: #f3f5f0;
                                          '>&nbsp;</td>
                                    </tr>
                                 </tbody>
                              </table>
                           </td>
                        </tr>
                     </tbody>
                  </table>
               </td>
            </tr>
         </tbody>
      </table>
   </body>
</html>";

                    AlternateView htmlView =
                        AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8,
                                                MediaTypeNames.Text.Html);
                    // de la image
                    //var _logo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "logo-small.png");
                    //LinkedResource img = new LinkedResource(_logo, MediaTypeNames.Image.Jpeg);
                    //img.ContentId = "imagen";

                    //// Lo incrustamos en la vista HTML...
                    //htmlView.LinkedResources.Add(img);

                    // Por último, vinculamos ambas vistas al mensaje...
                    var _excel = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "logo-small.png");

                    var FileName = Path.GetFileName(_excel);
                    var memory = new MemoryStream();
                    using (var stream = new FileStream(_excel, FileMode.Open))
                    {
                         stream.CopyToAsync(memory);
                    }
                    mail.Attachments.Add(new Attachment("",FileName));


                    mail.AlternateViews.Add(plainView);
                    mail.AlternateViews.Add(htmlView);

                    // Y lo enviamos a través del servidor SMTP...
                    objParametrosCorreoDto.Password = (objParametrosCorreoDto.Password);
                    var smtp = new SmtpClient
                    {
                        Host = objParametrosCorreoDto.Host, // set your SMTP server name here
                        Port = objParametrosCorreoDto.Port, // Port 
                        EnableSsl = true,
                        Credentials = new NetworkCredential(objParametrosCorreoDto.Correo, objParametrosCorreoDto.Password)
                    };

                    smtp.Send(mail);
                    usuarioEnviado += itemUsuario.Usuarios + ",";
                }
                catch (Exception ex)
                {
                    usuarioNoEnviados += itemUsuario.Usuarios + ",";
                    throw;
                }
            }
            var resultado = usuarioEnviado + "\n" + usuarioNoEnviados;
            return resultado;
        }

    }
}