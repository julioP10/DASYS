using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Areas.Equipo.Controllers
{
    [Area("Equipo")]
    public class EquiposController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}