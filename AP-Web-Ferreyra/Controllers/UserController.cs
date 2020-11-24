using System;
using System.Security.Cryptography;
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
                var usuarioEncontrado = UsuarioDAO.getInstancia().buscarUsuario(usuario);

                if (usuarioEncontrado != null)
                {

                    if (Verify(password, usuarioEncontrado.password))
                    {
                        HttpContext.Session.SetString("usuario", JsonConvert.SerializeObject(usuarioEncontrado));
                        ViewBag.usuario = usuarioEncontrado;
                        return View("../Home/Programa");
                    }
                    else
                    {
                        ViewBag.msg = "Contraseña incorrecta";
                        return View("../Home/IniciarSesion");
                    }

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
            
            if (UsuarioDAO.getInstancia().devolverIdUsuario(usuario) == -1)
            {
                var psw = Hash(password);
                UsuarioDAO.getInstancia().add(new Usuario(usuario, psw, id_pass));

                var id_user = UsuarioDAO.getInstancia().devolverIdUsuario(usuario);
                var passControler = new PassController();

                passControler.UsarPass(id_pass, id_user);

                var usuarioJson = HttpContext.Session.GetString("usuario");

                if (usuarioJson == null)
                {
                    return Login(usuario, password);
                }

                return View("../Home/Index");
            }else
            {
                ViewBag.idPass = id_pass;
                ViewBag.msg = "Ya existe ese usuario";
                return View("../Home/Registrar");
            }
        }

        [HttpPost]
        public IActionResult Editar(string usuario, string password, string password2)
        {
            var userBaseJson = HttpContext.Session.GetString("usuario");
            var userBase = JsonConvert.DeserializeObject<dynamic>(userBaseJson);

            string usernameBase = userBase.username;
            if (usuario != null)
            {
                EditarNombre(usernameBase, usuario);
                usernameBase = usuario;
            }

            if (password != null || password2 != null)
            {
                EditarPassword(usernameBase, password, password2);
            }

            var usuarioJson = HttpContext.Session.GetString("usuario");
            var usuarioNuevo = JsonConvert.DeserializeObject<dynamic>(usuarioJson);

            ViewBag.usuario = usuarioNuevo;
            return View("../Home/Editar");
        }

        private void EditarNombre(string userBase, string usuario)
        {
            var usuarioExistente = UsuarioDAO.getInstancia().buscarUsuario(usuario);

            if (usuarioExistente != null)
            {
                ViewBag.msg = "Ya existe ese usuario";
            }
            else
            {
                UsuarioDAO.getInstancia().editNombre(userBase, usuario);

                var usuarioActual = UsuarioDAO.getInstancia().buscarUsuario(usuario);
                usuarioActual.username = usuario;
                HttpContext.Session.SetString("usuario", JsonConvert.SerializeObject(usuarioActual));
                ViewBag.msg = "Cambio exitoso";
            }

        }
        private void EditarPassword(string usuario, string password, string password2)
        {

            if(password == null || password2 == null)
            {
                ViewBag.msg = "Debe completar los campos de contraseña";
            }
            else{
                var usuarioEncontrado = UsuarioDAO.getInstancia().buscarUsuario(usuario);

                if(Verify(password, usuarioEncontrado.password))
                {
                    UsuarioDAO.getInstancia().editPassword(usuario, Hash(password2));
                    usuarioEncontrado.password = Hash(password2);
                    HttpContext.Session.SetString("usuario", JsonConvert.SerializeObject(usuarioEncontrado));
                    ViewBag.msg = "Cambio exitoso";
                }
                else
                {
                    ViewBag.msg = "Las contraseñas no coinciden";
                }

            }
        }

        public IActionResult Eliminar()
        {
            var usuarioJson = HttpContext.Session.GetString("usuario");
            var usuario = JsonConvert.DeserializeObject<dynamic>(usuarioJson);

            ViewBag.usuario = usuario;
            return View("../Home/Eliminar");
        }

        [HttpPost]
        public IActionResult ConfirmarEliminar(Boolean confirmacion)
        {
            var usuarioJson = HttpContext.Session.GetString("usuario");
            var usuario = JsonConvert.DeserializeObject<dynamic>(usuarioJson);

            if (confirmacion)
            {
                UsuarioDAO.getInstancia().Eliminar((string)usuario.username);
                var passControler = new PassController();
                passControler.EliminarPass((int)usuario.pass);
                return Logout();
            }
            else
            {
                ViewBag.usuario = usuario;
                return View("../Home/Editar");
            }

        }

        [HttpPost]
        public JsonResult BuscarUsuarioRegistrado([FromBody] JsonUsuario jsonUsuario)
        {

            // Busco usuario en DAO
            Usuario usuario = UsuarioDAO.getInstancia().buscarUsuario(jsonUsuario.username);

            var jsonResult = "";
            if (usuario != null)
            {
                if (Verify(jsonUsuario.pwd, usuario.password))
                {
                    jsonResult = JsonConvert.SerializeObject(usuario);
                }
            }
            return Json(jsonResult);

        }

        public class JsonUsuario
        {
            public string pwd { get; set; }

            public string username { get; set; }
        }

        private string Hash(string password)
        {
            int SaltSize = 16;
            int HashSize = 20;
            int iterations = 10000;

            // Create salt
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

            // Create hash
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            var hash = pbkdf2.GetBytes(HashSize);

            // Combine salt and hash
            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            // Convert to base64
            var base64Hash = Convert.ToBase64String(hashBytes);

            // Format hash with extra information
            return string.Format("$MYHASH$V1${0}${1}", iterations, base64Hash);
        }

        private bool Verify(string password, string hashedPassword)
        {
            int SaltSize = 16;
            int HashSize = 20;
            // Check hash
            if (!hashedPassword.Contains("$MYHASH$V1$"))
            {
                throw new NotSupportedException("The hashtype is not supported");
            }

            // Extract iteration and Base64 string
            var splittedHashString = hashedPassword.Replace("$MYHASH$V1$", "").Split('$');
            var iterations = int.Parse(splittedHashString[0]);
            var base64Hash = splittedHashString[1];

            // Get hash bytes
            var hashBytes = Convert.FromBase64String(base64Hash);

            // Get salt
            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Create hash with given salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            // Get result
            for (var i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }




    }
}
