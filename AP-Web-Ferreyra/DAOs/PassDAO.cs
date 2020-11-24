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

        public static PassDAO GetInstancia()
        {

            if (instancia == null)
            {
                instancia = new PassDAO();
            }

            return instancia;

        }

        public int ValidarPase(string pass)
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

        public void UsarPass(int id_pass, int id_user)
        {
            var queryBuilder = DBConnection.getInstance().getQueryBuilder();

            queryBuilder.setQuery("UPDATE pases SET id_user=@id_user WHERE id=@id_pass");
            queryBuilder.addParam("@id_user", id_user);
            queryBuilder.addParam("@id_pass", id_pass);

            DBConnection.getInstance().abm(queryBuilder);
        }

        internal void GuardarPase(string pass)
        {
            var queryBuilder = DBConnection.getInstance().getQueryBuilder();

            queryBuilder.setQuery("INSERT INTO pases (pass) VALUES (@pass)");
            queryBuilder.addParam("@pass", pass);

            DBConnection.getInstance().abm(queryBuilder);
        }

        internal void Eliminar(int id_pass)
        {
            var queryBuilder = DBConnection.getInstance().getQueryBuilder();

            queryBuilder.setQuery("DELETE FROM pases WHERE id=@id_pass");
            queryBuilder.addParam("@id_pass", id_pass);

            DBConnection.getInstance().abm(queryBuilder);
        }
    }
}
