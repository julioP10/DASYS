using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entidad;
using Microsoft.AspNetCore.Mvc;
using Utilis;

namespace WEB.Areas.Mantenimiento.Controllers
{
    [Area("Mantenimiento")]
    public class ComunController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ListaCategoria(string codigo)
        {
            JsonResponseDto objJsonResponseDto = new JsonResponseDto();
            codigo = (codigo == null) ? "" : codigo;
            List<DropDownDto> lista = Utils.ListaCategoria(codigo,"");
            objJsonResponseDto.data = lista;
            objJsonResponseDto.Type = Constante.Success;
            objJsonResponseDto.IsValid = true;
            return Json( objJsonResponseDto);
        }
    }
}