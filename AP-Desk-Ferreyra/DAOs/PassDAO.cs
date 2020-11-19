using AP_Desk_Ferreyra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AP_Desk_Ferreyra.DAOs
{
    public class PassDAO
    {
        public static PassDAO instancia = null;

        public static PassDAO getInstancia()
        {

            if (instancia == null)
            {
                instancia = new PassDAO();
            }

            return instancia;

        }

        public int validarPase(string pass)
        {
            var queryBuilder = DBConnection.getInstance().getQueryBuilder();

            queryBuilder.setQuery("SELECT id FROM pases WHERE pass=@pass AND id_user IS NULL");
            queryBuilder.addParam("@pass", pass);

            var dataReader = DBConnection.getInstance().select(queryBuilder);
            var id_pase = -1;
            while (dataReader.Read())
            {
                id_pase = dataReader.GetInt32(0);
            }

            return id_pase;
        }


    }
}
