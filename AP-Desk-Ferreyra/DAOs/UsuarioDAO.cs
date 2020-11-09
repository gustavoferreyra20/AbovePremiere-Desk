using AP_Desk_Ferreyra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AP_Desk_Ferreyra.DAOs
{
    public class UsuarioDAO
    {

        public static List<Usuario> listaUsuarios = new List<Usuario>();

        public static void iniciar()
        {
            listaUsuarios.Add(new Usuario(1, "usuario1", "12345"));
            listaUsuarios.Add(new Usuario(2, "usuario2", "12345"));
            listaUsuarios.Add(new Usuario(3, "usuario3", "12345"));
        }

        public static bool existeUsuario(String usuario, string password)
        {

            var usuarioEncontrado = listaUsuarios.Find(usuarioObj => usuarioObj.getUsuario() == usuario && usuarioObj.getPassword() == password);

            return (usuarioEncontrado != null) ? true : false;

          
        }

    }


}
