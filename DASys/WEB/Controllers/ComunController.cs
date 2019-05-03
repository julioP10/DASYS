using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entidad;
using Microsoft.AspNetCore.Mvc;
using Utilis;

namespace WEB.Controllers
{
    public class ComunController : BaseController
    {
  
        public ComunController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ListaCategoria(string codigo,string empresa)
        {
            JsonResponseDto objJsonResponseDto = new JsonResponseDto();
            codigo = (codigo == null) ? "" : codigo;
            if (GetPerfil().ToUpper()!="SUPERUSUARIO")
            {
                empresa = GetEmpresaPadre();
            }
            List<DropDownDto> lista = Utils.ListaCategoria(codigo, empresa);
            objJsonResponseDto.data = lista;
            objJsonResponseDto.Type = Constante.Success;
            objJsonResponseDto.IsValid = true;
            return Json(objJsonResponseDto);
        }
        [HttpPost]
        public IActionResult ListaCategoriaSolo(string codigo, string empresa)
        {
            JsonResponseDto objJsonResponseDto = new JsonResponseDto();

            if (GetPerfil().ToUpper()!="SUPERUSUARIO")
            {
                empresa = (empresa == null) ? GetEmpresaPadre() : empresa;
            }
            else
            {
                empresa = "";
            }
            List<DropDownDto> lista = Utils.ListaCategoria(codigo, empresa);
            objJsonResponseDto.data = lista;
            objJsonResponseDto.Type = Constante.Success;
            objJsonResponseDto.IsValid = true;
            return Json(objJsonResponseDto);
        }
        [HttpPost]
        public IActionResult ListaProvincia (string codigo)
        {
            JsonResponseDto objJsonResponseDto = new JsonResponseDto();
            codigo = (codigo == null) ? "" : codigo;
            List<DropDownDto> lista = Utils.ListaProvincia(codigo);
            objJsonResponseDto.data = lista;
            objJsonResponseDto.Type = Constante.Success;
            objJsonResponseDto.IsValid = true;
            return Json(objJsonResponseDto);
        }
        [HttpPost]
        public IActionResult ListaDistrito(string codigo, string codigo2)
        {
            JsonResponseDto objJsonResponseDto = new JsonResponseDto();
            codigo = (codigo == null) ? "" : codigo;
            codigo2 = (codigo2 == null) ? "" : codigo2;
            List<DropDownDto> lista = Utils.ListaDistrito(codigo,codigo2);
            objJsonResponseDto.data = lista;
            objJsonResponseDto.Type = Constante.Success;
            objJsonResponseDto.IsValid = true;
            return Json(objJsonResponseDto);
        }

        [HttpPost]
        public IActionResult ListaUbicacion(string codigo)
        {
            JsonResponseDto objJsonResponseDto = new JsonResponseDto();
            codigo = (codigo == null) ? "" : codigo;
            List<DropDownDto> lista = Utils.ListaUbicacion(codigo);
            objJsonResponseDto.data = lista;
            objJsonResponseDto.Type = Constante.Success;
            objJsonResponseDto.IsValid = true;
            return Json(objJsonResponseDto);
        }
        [HttpPost]
        public IActionResult ListaEmpresa(string codigo,string empresaSelceccionada)
        {
            JsonResponseDto objJsonResponseDto = new JsonResponseDto();
            codigo = (codigo == null) ? "" : codigo;
            List<DropDownDto> lista;
            if (!string.IsNullOrWhiteSpace(empresaSelceccionada))
            {
                lista = Utils.ListaEmpresaSuperUsuario(codigo).Where(p=>p.Value!=empresaSelceccionada).ToList();
            }
            else
            {
                lista = Utils.ListaEmpresaSuperUsuario(codigo);
            }
            objJsonResponseDto.data = lista;
            objJsonResponseDto.Type = Constante.Success;
            objJsonResponseDto.IsValid = true;
            return Json(objJsonResponseDto);
        }
        [HttpPost]
        public IActionResult ListaEmpresaUsuario(string codigo)
        {
            var _empresa = GetEmpresaPadre();
            JsonResponseDto objJsonResponseDto = new JsonResponseDto();
            codigo = (codigo == null) ? "" : codigo;
            List<DropDownDto> lista = Utils.ListaEmpresaUsuario(codigo, _empresa);
            if (GetPerfil().ToUpper()=="SUPERUSUARIO")
            {
                objJsonResponseDto.data = lista; 
            }
            else
            {
                objJsonResponseDto.data = lista.Where(p => p.Value == _empresa || p.Valor3 == _empresa || p.Valor2 == _empresa);
            }
            objJsonResponseDto.Type = Constante.Success;
            objJsonResponseDto.IsValid = true;
            return Json(objJsonResponseDto);
        }

        [HttpPost]
        public IActionResult ListaModelo(string codigo, string empresa)
        {
            JsonResponseDto objJsonResponseDto = new JsonResponseDto();
            codigo = (codigo == null) ? "" : codigo;
            List<DropDownDto> lista = Utils.ListaModelo(codigo, empresa);
            objJsonResponseDto.data = lista;
            objJsonResponseDto.Type = Constante.Success;
            objJsonResponseDto.IsValid = true;
            return Json(objJsonResponseDto);
        }
        [HttpPost]
        public IActionResult ListaMarca(string codigo, string empresa)
        {
            JsonResponseDto objJsonResponseDto = new JsonResponseDto();
            codigo = (codigo == null) ? "" : codigo;
            List<DropDownDto> lista = Utils.ListaMarca(codigo, empresa);
            objJsonResponseDto.data = lista;
            objJsonResponseDto.Type = Constante.Success;
            objJsonResponseDto.IsValid = true;
            return Json(objJsonResponseDto);
        }
        [HttpPost]
        public IActionResult ListaConfiguiracion(string codigo, string empresa)
        {
            JsonResponseDto objJsonResponseDto = new JsonResponseDto();
            codigo = (codigo == null) ? "" : codigo;
            List<DropDownDto> lista = Utils.ListaConfiguiracion(codigo, empresa);
            objJsonResponseDto.data = lista;
            objJsonResponseDto.Type = Constante.Success;
            objJsonResponseDto.IsValid = true;
            return Json(objJsonResponseDto);
        }

        [HttpPost]
        public IActionResult ListaArea(string codigo, string empresa)
        {
            JsonResponseDto objJsonResponseDto = new JsonResponseDto();
            codigo = (codigo == null) ? "" : codigo;
            List<DropDownDto> lista = Utils.ListaArea(codigo, empresa);
            objJsonResponseDto.data = lista;
            objJsonResponseDto.Type = Constante.Success;
            objJsonResponseDto.IsValid = true;
            return Json(objJsonResponseDto);
        }
    }
}