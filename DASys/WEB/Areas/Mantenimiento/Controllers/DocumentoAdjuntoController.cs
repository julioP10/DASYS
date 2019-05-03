using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Areas.Mantenimiento.Controllers
{
    [Area("Mantenimiento")]
    public class DocumentoAdjuntoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}