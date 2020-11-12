using AP_Desk_Ferreyra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AP_Desk_Ferreyra.DAOs
{
    public class UsuarioDAO
    {
        public static UsuarioDAO instancia = null;
        public static List<Usuario> usuarios = new List<Usuario>();

        public static UsuarioDAO getInstancia()
        {

            if (instancia == null)
            {
                instancia = new UsuarioDAO();
            }

            return instancia;

        }

        public UsuarioDAO add(Usuario user)
        {
            usuarios.Add(user);
            return this;

        }

        public Usuario buscarUsuario(string username, string password)
        {
            return usuarios.Find(x => x.username == username && x.password == password);
        }

    }


}
