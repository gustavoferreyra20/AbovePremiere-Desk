using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AP_Desk_Ferreyra.Models
{
    public class Usuario
    {

        private int id;
        private string usuario;
        private string password;

        public Usuario(int id, string usuario, string password)
        {
            this.id = id;
            this.usuario = usuario;
            this.password = password;
        }

        public string getUsuario()
        {
            return this.usuario;
        }

        public string getPassword()
        {
            return this.password;
        }
    }
}
