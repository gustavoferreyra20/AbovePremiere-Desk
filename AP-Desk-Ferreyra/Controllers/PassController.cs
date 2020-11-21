using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AP_Desk_Ferreyra.DAOs;
using Microsoft.AspNetCore.Mvc;

namespace AP_Desk_Ferreyra.Controllers
{
    public class PassController : Controller
    {
        public IActionResult Validar(string pass)
        {

            var claveValida = PassDAO.getInstancia().validarPase(pass);

            if (claveValida != -1)
            {
                TempData["id_pass"] = claveValida;
                return Redirect("../Home/Registrar");
            }
            else
            {
                //Movimiento entre controladores
                TempData["msg"] = "Clave invalida";
                return Redirect("../Home/Index");
            }

        }

        internal object UsarPass(string usuario)
        {
            throw new NotImplementedException();
        }
    }
}
