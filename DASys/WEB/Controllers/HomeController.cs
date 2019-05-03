using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB.Models;

namespace WEB.Controllers
{
    
    public class HomeController : BaseController
    {
        public HomeController(IServiceProvider serviceProvider)
                   : base(serviceProvider)
        {

        }
        public IActionResult Index()
        {
            //SendEmail();
            if (ModulosActuales()==null)
            {
                return Redirect("/Login/Index");

            }
            else
            {
                HttpContext.Session.SetString("_perfil",GetPerfil());
                ViewBag.Menu = ModulosActuales();
                ViewBag.Opciones = OpcionesActuales();
                ViewBag.UsuarioActual = UsuarioActual();
                ViewBag.OpcionesHijo = OpcionesHijoActuales();
                ViewBag.TipoEmpresa = (GetTipoEmpresa()=="" || GetTipoEmpresa()==null)?"SUPER": GetTipoEmpresa();
                ViewBag.TipoPerfil = GetPerfil();
                return View();
            }
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
