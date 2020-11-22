using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AP_Web_Ferreyra.Models;
using AP_Desk_Ferreyra.DAOs;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AP_Web_Ferreyra.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var usuarioJson = HttpContext.Session.GetString("usuario");

            if (usuarioJson != null)
            {
                return Redirect("/Home/Programa");
            }
            else
            {
                return View();
            }
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }

        public IActionResult Registrar()
        {
            return View();
        }

        public IActionResult Editar()
        {
            var usuarioJson = HttpContext.Session.GetString("usuario");

            if (usuarioJson != null)
            {

                var usuario = JsonConvert.DeserializeObject<dynamic>(usuarioJson);

                TempData["userBase"] = usuario;
                return View();

            }
            else
            {
                return View("Index");
            }
        }

        public IActionResult Programa()
        {

            var usuarioJson = HttpContext.Session.GetString("usuario");

            if (usuarioJson != null)
            {

                var usuario = JsonConvert.DeserializeObject<dynamic>(usuarioJson);

                ViewBag.usuario = usuario;
                return View();

            }
            else
            {
                return View("Index");
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
