using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AP_Desk_Ferreyra.DAOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AP_Desk_Ferreyra.Controllers
{
    public class PassController : Controller
    {
        public IActionResult Validar(string pass)
        {

            var claveValida = PassDAO.GetInstancia().ValidarPase(pass);

            if (claveValida != -1)
            {
                ViewBag.idPass = claveValida;
                return View("../Home/Registrar");
            }
            else
            {
                ViewBag.msg = "Clave invalida";
                return View("../Home/Index");
            }

        }

        public void UsarPass(int id_pass, int id_user)
        {
            PassDAO.GetInstancia().UsarPass(id_pass, id_user);
        }

        public IActionResult Agregar(int cantidad)
        {
            var passInstacia = PassDAO.GetInstancia();
            for (int i = 0; i <= cantidad; i++)
            {
                var nuevaKey = GenerarNuevaKey(20);
                var claveValida = passInstacia.ValidarPase(nuevaKey);

                if(claveValida != -1)
                {
                    i--;
                }
                else
                {
                    passInstacia.GuardarPase(nuevaKey);
                }
            }

            var usuarioJson = HttpContext.Session.GetString("usuario");
            var usuario = JsonConvert.DeserializeObject<dynamic>(usuarioJson);
            ViewBag.msg = "Claves generadas con exito";
            ViewBag.usuario = usuario;
            return View("../Home/Programa");
        }

        private string GenerarNuevaKey(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        internal void EliminarPass(int id_pass)
        {
            PassDAO.GetInstancia().Eliminar(id_pass);
        }
    }
}
