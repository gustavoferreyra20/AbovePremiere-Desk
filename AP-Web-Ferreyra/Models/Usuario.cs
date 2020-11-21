using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AP_Web_Ferreyra.Models
{
    public class Usuario
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int pass { get; set; }

        public Usuario(string username, string password, int pass)
        {
            this.username = username;
            this.password = password;
            this.pass = pass;
        }

    }
}
