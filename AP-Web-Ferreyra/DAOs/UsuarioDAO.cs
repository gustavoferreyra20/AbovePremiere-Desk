using AP_Desk_Ferreyra;
using AP_Web_Ferreyra.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
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

            queryBuilder.setQuery("INSERT INTO usuarios (user,permisos,pwd,id_pass) VALUES (@username,@permisos,@password,@pass)");
            queryBuilder.addParam("@username", user.username);
            queryBuilder.addParam("@permisos", user.permisos);
            queryBuilder.addParam("@password", user.password);
            queryBuilder.addParam("@pass", user.pass);

            DBConnection.getInstance().abm(queryBuilder);
        }


        public Usuario buscarUsuario(string username)
        {
            var queryBuilder = DBConnection.getInstance().getQueryBuilder();

            queryBuilder.setQuery("SELECT * FROM usuarios WHERE user=@username");
            queryBuilder.addParam("@username", username);

            var dataReader = DBConnection.getInstance().select(queryBuilder);
            Usuario usuario = null;
            while (dataReader.Read())
            {
                usuario = new Usuario(dataReader.GetString(1), dataReader.GetString(3), dataReader.GetInt32(4));
                usuario.permisos = dataReader.GetString(2);
            }

            return usuario;
        }

        public int devolverIdUsuario(string username)
        {
            var queryBuilder = DBConnection.getInstance().getQueryBuilder();

            queryBuilder.setQuery("SELECT id FROM usuarios WHERE user=@username");
            queryBuilder.addParam("@username", username);

            var dataReader = DBConnection.getInstance().select(queryBuilder);
            var id_usuario = -1;
            while (dataReader.Read())
            {
                id_usuario = dataReader.GetInt32(0);
            }

            return id_usuario;
        }

        internal void editNombre(string userBase, string usuario)
        {
            var queryBuilder = DBConnection.getInstance().getQueryBuilder();

            queryBuilder.setQuery("UPDATE usuarios SET user=@usuario WHERE user=@userBase");
            queryBuilder.addParam("@usuario", usuario);
            queryBuilder.addParam("@userBase", userBase);

            DBConnection.getInstance().abm(queryBuilder);
        }

        internal void editPassword(string userBase, string password2)
        {
            var queryBuilder = DBConnection.getInstance().getQueryBuilder();

            queryBuilder.setQuery("UPDATE usuarios SET pwd=@password2 WHERE user=@userBase");
            queryBuilder.addParam("@password2", password2);
            queryBuilder.addParam("@userBase", userBase);

            DBConnection.getInstance().abm(queryBuilder);
        }

        internal void Eliminar(string username)
        {
            var queryBuilder = DBConnection.getInstance().getQueryBuilder();

            queryBuilder.setQuery("DELETE FROM usuarios WHERE user=@username");
            queryBuilder.addParam("@username", username);

            DBConnection.getInstance().abm(queryBuilder);
        }
    }


}
