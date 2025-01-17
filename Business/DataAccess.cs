﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;
using System.Data;
namespace Negocio
{
    public class DataAccess : IDisposable
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;
        public SqlDataReader Lector
        {
            get { return lector; }
        }
        public DataAccess()
        {
            // Replace "your_password" with the actual SA password you set when running the Docker container
            string connectionString = "Server=localhost,1433;Database=emmanueltest;User Id=sa;Password=NiceTry!;";
            conexion = new SqlConnection(connectionString);
            comando = new SqlCommand();
        }

        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void ejecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public void openCon()
        {
            if (conexion.State != ConnectionState.Open)
            {
                conexion.Open();
            }
        }
        public void limpiarParametros()
        {
            comando.Parameters.Clear();
            conexion.Close();

        }
        public void ejecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { conexion.Close(); }
        }

        public void setearParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        public object ejecutarEscalar()
        {
             conexion.Close(); 
                conexion.Open();
            object result = comando.ExecuteScalar();
            conexion.Close();

            return result;
        }
        public void cerrarConexion()
        {
            if (lector != null)
                lector.Close();
            conexion.Close();
        }
        public void Dispose()
        {
            cerrarConexion();
            if (conexion != null)
            {
                conexion.Dispose();
            }
            if (comando != null)
            {
                comando.Dispose();
            }
        }
    }
}
