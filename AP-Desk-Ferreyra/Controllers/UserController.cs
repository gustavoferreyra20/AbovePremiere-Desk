using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AP_Desk_Ferreyra.Controllers;
using AP_Web_Ferreyra.DAOs;
using AP_Web_Ferreyra.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AP_Web_Ferreyra.Controllers
{
    public class UserController : Controller
    {

        [HttpPost]
        public IActionResult Login(string usuario, string password)
        {

            var usuarioEncontrado = UsuarioDAO.getInstancia().buscarUsuario(usuario, password);

            if (usuarioEncontrado != null)
            {

                HttpContext.Session.SetString("usuario", JsonConvert.SerializeObject(usuarioEncontrado));

                return Redirect("/Home/Programa");

            }
            else
            {

                ViewBag.msg = "El usuario no existe";
                return View("../Home/IniciarSesion");

            }

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/Home/Index");
        }

        [HttpPost]
        public IActionResult Registrar(string usuario, string password, int id_pass)
        {

            UsuarioDAO.getInstancia().add(new Usuario(usuario, password, id_pass));

            var passControler = new PassController();
            passControler.UsarPass(usuario);

            var usuarioJson = HttpContext.Session.GetString("usuario");

            if (usuarioJson == null)
            {
                Login(usuario, password);
            }

            return Redirect("/Home/Index");

        }


    }
}
