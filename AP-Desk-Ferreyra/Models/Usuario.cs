using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AP_Desk_Ferreyra.Models
{
    public class Usuario
    {

        private int id;
        public string usuario { get; set; }
        public string password { get; set; }

        public Usuario(int id, string usuario, string password)
        {
            this.id = id;
            this.usuario = usuario;
            this.password = password;
        }

    }
}
