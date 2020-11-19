using AP_Desk_Ferreyra;
using AP_Web_Ferreyra.Models;
using System;
using System.Collections.Generic;

namespace AP_Web_Ferreyra.DAOs
{
    public class UsuarioDAO
    {
        public static UsuarioDAO instancia = null;

        public static UsuarioDAO getInstancia()
        {

            if (instancia == null)
            {
                instancia = new UsuarioDAO();
            }

            return instancia;

        }

        public void add(Usuario user)
        {
            var queryBuilder = DBConnection.getInstance().getQueryBuilder();

            queryBuilder.setQuery("INSERT INTO usuarios (user,pwd,id_pass) VALUES (@username,@password,@pass)");
            queryBuilder.addParam("@username", user.username);
            queryBuilder.addParam("@password", user.password);
            queryBuilder.addParam("@pass", user.pass);

            //this.personas.Add(persona)
            DBConnection.getInstance().abm(queryBuilder);

        }

        public Usuario buscarUsuario(string username, string password)
        {
            var queryBuilder = DBConnection.getInstance().getQueryBuilder();

            queryBuilder.setQuery("SELECT * FROM usuarios WHERE user=@username AND pwd=@password");
            queryBuilder.addParam("@username", username);
            queryBuilder.addParam("@password", password);

            var dataReader = DBConnection.getInstance().select(queryBuilder);
            Usuario usuario = null;
            while (dataReader.Read())
            {
                usuario = new Usuario(dataReader.GetString(1), dataReader.GetString(2), dataReader.GetInt32(3));
            }

            return usuario;
        }

    }


}
