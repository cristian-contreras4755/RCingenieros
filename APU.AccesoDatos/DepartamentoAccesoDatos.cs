using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.AccesoDatos
{
    public class DepartamentoAccesoDatos
    {
        public List<DepartamentoInfo> Listar(int departamentoId)
        {
            var departamentoListaInfo = new List<DepartamentoInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerDepartamento";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("DepartamentoId", SqlDbType.Int).Value = departamentoId;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        departamentoListaInfo.Add(CargarDepartamentoInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return departamentoListaInfo;
        }
        public List<DepartamentoInfo> ListarPaginado(int departamentoId, int tamanioPagina, int numeroPagina)
        {
            var departamentoListaInfo = new List<DepartamentoInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerDepartamentoPaginado";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("DepartamentoId", SqlDbType.Int).Value = departamentoId;
                sqlComando.Parameters.Add("TamanioPagina", SqlDbType.Int).Value = tamanioPagina;
                sqlComando.Parameters.Add("NumeroPagina", SqlDbType.Int).Value = numeroPagina;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        departamentoListaInfo.Add(CargarDepartamentoInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return departamentoListaInfo;
        }
        private static DepartamentoInfo CargarDepartamentoInfo(IDataReader dr)
        {
            int colCount = dr.FieldCount;
            var values = new object[colCount];

            #region Indices
            int indDepartamentoId = dr.GetOrdinal("DepartamentoId");
            int indDirectorId = dr.GetOrdinal("DirectorId");
            int indNombre = dr.GetOrdinal("Nombre");
            int indActivo = dr.GetOrdinal("Activo");
            int indUsuarioCreacionId = dr.GetOrdinal("UsuarioCreacionId");
            int indFechaCreacion = dr.GetOrdinal("FechaCreacion");
            int indUsuarioModificacionId = dr.GetOrdinal("UsuarioModificacionId");
            int indFechaModificacion = dr.GetOrdinal("FechaModificacion");
            int indNumeroFila = dr.GetOrdinal("NumeroFila");
            int indTotalFilas = dr.GetOrdinal("TotalFilas");
            #endregion

            var departamentoInfo = new DepartamentoInfo();
            dr.GetValues(values);

            #region Campos
            departamentoInfo.DepartamentoId = Convert.ToInt32(values[indDepartamentoId]);
            departamentoInfo.DirectorId = Convert.ToInt32(values[indDirectorId]);
            departamentoInfo.Nombre = Convert.ToString(values[indNombre]);
            departamentoInfo.Activo = Convert.ToInt32(values[indActivo]);
            departamentoInfo.UsuarioCreacionId = Convert.ToInt32(values[indUsuarioCreacionId]);
            departamentoInfo.FechaCreacion = Convert.ToDateTime(values[indFechaCreacion]);
            departamentoInfo.UsuarioModificacionId = Convert.ToInt32(values[indUsuarioModificacionId]);
            departamentoInfo.FechaModificacion = Convert.ToDateTime(values[indFechaModificacion]);
            departamentoInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
            departamentoInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);
            #endregion

            return departamentoInfo;
        }
        public int Insertar(DepartamentoInfo departamentoInfo)
        {
            int resultado;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarDepartamento", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("DirectorId", SqlDbType.Int).Value = departamentoInfo.DirectorId;
                    sqlComando.Parameters.Add("Nombre", SqlDbType.VarChar).Value = departamentoInfo.Nombre;
                    sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = departamentoInfo.Activo;
                    sqlComando.Parameters.Add("UsuarioCreacionId", SqlDbType.Int).Value = departamentoInfo.UsuarioCreacionId;

                    oConnection.Open();
                    resultado = Convert.ToInt32(sqlComando.ExecuteScalar());

                    oConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }
        public int Actualizar(DepartamentoInfo departamentoInfo)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "ActualizarDepartamento", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("DepartamentoId", SqlDbType.Int).Value = departamentoInfo.DepartamentoId;
                    sqlComando.Parameters.Add("DirectorId", SqlDbType.Int).Value = departamentoInfo.DirectorId;
                    sqlComando.Parameters.Add("Nombre", SqlDbType.VarChar).Value = departamentoInfo.Nombre;
                    sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = departamentoInfo.Activo;
                    sqlComando.Parameters.Add("UsuarioModificacionId", SqlDbType.Int).Value = departamentoInfo.UsuarioModificacionId;

                    oConnection.Open();
                    resultado = Convert.ToInt32(sqlComando.ExecuteScalar());

                    oConnection.Close();
                }
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaAccesoDatos);
                if (rethrow)
                    throw ex;
            }
            return resultado;
        }
        public int Eliminar(int departamentoId)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "EliminarDepartamento", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("DepartamentoId", SqlDbType.Int).Value = departamentoId;

                    oConnection.Open();
                    resultado = sqlComando.ExecuteNonQuery();

                    oConnection.Close();
                }
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaAccesoDatos);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
    }
}