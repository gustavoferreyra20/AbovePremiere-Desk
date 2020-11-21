using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AP_Desk_Ferreyra.Models
{
    public class Pass
    {
        public int id { get; set; }
        public string pass { get; set; }
        public int id_user { get; set; }

        public Pass(int id, string pass, int id_user)
        {
            this.id = id;
            this.pass = pass;
            this.id_user = id_user;
        }
    }
}
