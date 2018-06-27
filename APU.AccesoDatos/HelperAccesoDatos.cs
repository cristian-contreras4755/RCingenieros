using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Text;

namespace APU.AccesoDatos
{
    public class HelperAccesoDatos
    {
        #region Declaracion Variables
        private SqlConnection _oConnection;
        private OleDbConnection _oConnectionExcel;
        #endregion
        public bool OpenConnection(string servidor, string usuario, string password)
        {
            try
            {
                _oConnection = new SqlConnection("Data Source=" + servidor + "; User Id=" + usuario + "; Password=" + password + ";");
                _oConnection.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool CloseConnection()
        {
            try
            {
                _oConnection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static string GetCadenaConexion()
        {
            //return ConfigurationManager.ConnectionStrings["APU.ConnectionString"].ConnectionString;
            return ConfigurationManager.ConnectionStrings["APU.ConnectionString"].ConnectionString;
            //return "Data Source=a-solutions.net;Initial Catalog=APUBD;User Id=mabarcau;Password=abcd1234.";
        }
        public int EjecutarScript(string strScript)
        {
            int resultado;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand();
                    sqlComando.Connection = oConnection;
                    sqlComando.CommandTimeout = 0;
                    sqlComando.CommandText = "ems_script_ejecutar";
                    sqlComando.CommandType = CommandType.StoredProcedure;
                    sqlComando.Parameters.Add("vchScript", SqlDbType.NVarChar).Value = strScript;

                    oConnection.Open();
                    resultado = sqlComando.ExecuteNonQuery();
                    oConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }
        public string EjecutarScriptValor(string strScript)
        {
            var resultado = String.Empty;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand();
                    sqlComando.Connection = oConnection;
                    sqlComando.CommandTimeout = 0;
                    sqlComando.CommandText = "ems_script_ejecutar";
                    sqlComando.CommandType = CommandType.StoredProcedure;
                    sqlComando.Parameters.Add("vchScript", SqlDbType.NVarChar).Value = strScript;

                    oConnection.Open();
                    resultado = sqlComando.ExecuteScalar().ToString();
                    oConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }
        public object ObtenerValorParametro(string nombreParametro)
        {
            object valorRetorno = null;
            var sql = new StringBuilder();

            sql.Append(String.Format("SELECT ValorParametro FROM ParametrosGlobales WHERE NombreParametro = '{0}'", nombreParametro));

            using (var oConnection = new SqlConnection(GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand(sql.ToString()) { Connection = oConnection, CommandTimeout = 0, CommandType = CommandType.Text };

                oConnection.Open();
                valorRetorno = sqlComando.ExecuteScalar();
                oConnection.Close();
            }
            return valorRetorno;
        }
        public DbType ObtenerTipoBaseDatos(Type tipo)
        {
            if (tipo.Equals(typeof(int)))
            {
                return DbType.Int32;
            }
            if (tipo.Equals(typeof(byte[])))
            {
                return DbType.Binary;
            }
            if (!tipo.Equals(typeof(string)))
            {
                if (tipo.Equals(typeof(DateTime)))
                {
                    return DbType.DateTime;
                }
                if (tipo.Equals(typeof(decimal)))
                {
                    return DbType.Decimal;
                }
                if (tipo.Equals(typeof(double)))
                {
                    return DbType.Double;
                }
            }
            return DbType.String;
        }
        public string ActualizarColumnaTabla(string tabla, string columna, string valor, string columnaWhere, string valorWhere)
        {
            var resultado = string.Empty;
            var where = string.Empty;

            if (!columnaWhere.Equals(string.Empty)) where = " where " + columnaWhere + " = '" + valorWhere + "'";

            var query = @"UPDATE " + tabla + " SET " + columna + " = '" + valor + "'" + where;

            using (var oConnection = new SqlConnection(GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand(query)
                {
                    Connection = oConnection,
                    CommandTimeout = 0,
                    CommandType = CommandType.Text
                };

                oConnection.Open();
                resultado = sqlComando.ExecuteNonQuery().ToString();
                oConnection.Close();
            }

            return resultado;
        }

        public string ActualizarColumnasTabla(string tabla, string[] columna, string[] valor, string[] columnaWhere, string[] valorWhere)
        {
            var resultado = string.Empty;
            var where = (columnaWhere.Length > 0) ? " Where " : string.Empty;
            var sets = string.Empty;

            for (int i = 0; i < columna.Length; i++)
            {
                sets += columna[i] + "='" + valor[i] + "'" + (i != columna.Length - 1 ? "," : "");
            }

            for (int i = 0; i < columnaWhere.Length; i++)
            {
                where += columnaWhere[i] + "='" + valorWhere[i] + ((i != columnaWhere.Length - 1) ? "' AND " : "");
            }
            where += "'";
            var query = @"UPDATE " + tabla + " SET " + sets + where;

            using (var oConnection = new SqlConnection(GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand(query)
                {
                    Connection = oConnection,
                    CommandTimeout = 0,
                    CommandType = CommandType.Text
                };

                oConnection.Open();
                resultado = sqlComando.ExecuteNonQuery().ToString("");
                oConnection.Close();
            }

            return resultado;
        }

        public DataTable EjecutarSelect(string strScript)
        {
            var resultado = new DataTable();
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand();
                    sqlComando.Connection = oConnection;
                    sqlComando.CommandText = "ems_script_ejecutar";
                    sqlComando.CommandType = CommandType.StoredProcedure;
                    sqlComando.Parameters.Add("vchScript", SqlDbType.NVarChar).Value = strScript;

                    oConnection.Open();
                    resultado.Load(sqlComando.ExecuteReader());
                    oConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }

        #region Excel
        public bool OpenConnectionExcel(string servidor, string usuario, string password)
        {
            try
            {
                _oConnectionExcel = new OleDbConnection("Data Source=" + servidor + "; User Id=" + usuario + "; Password=" + password + ";");
                _oConnectionExcel.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool CloseConnectionExcel()
        {
            try
            {
                _oConnectionExcel.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static string GetCadenaConexionExcel()
        {
            return ConfigurationManager.ConnectionStrings["SMART4OleDbConnectionString"].ConnectionString;
        }
        #endregion
    }
}