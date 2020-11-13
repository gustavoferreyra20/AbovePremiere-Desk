using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AP_Desk_Ferreyra.Models
{
    public class Usuario
    {

        private int id;
        public string username { get; set; }
        public string password { get; set; }
        public string permisos { get; set; }

        public Usuario(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

    }
}
