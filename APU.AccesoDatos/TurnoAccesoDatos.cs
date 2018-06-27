using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.AccesoDatos
{
    public class TurnoAccesoDatos
    {
        public List<TurnoInfo> Listar(int turnoId)
        {
            var turnoListaInfo = new List<TurnoInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerTurno";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("TurnoId", SqlDbType.Int).Value = turnoId;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        turnoListaInfo.Add(CargarTurnoInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return turnoListaInfo;
        }
        public List<TurnoInfo> ListarPaginado(int turnoId, int tamanioPagina, int numeroPagina)
        {
            var turnoListaInfo = new List<TurnoInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerTurnoPaginado";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("TurnoId", SqlDbType.Int).Value = turnoId;
                sqlComando.Parameters.Add("TamanioPagina", SqlDbType.Int).Value = tamanioPagina;
                sqlComando.Parameters.Add("NumeroPagina", SqlDbType.Int).Value = numeroPagina;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        turnoListaInfo.Add(CargarTurnoInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return turnoListaInfo;
        }
        private static TurnoInfo CargarTurnoInfo(IDataReader dr)
        {
            int colCount = dr.FieldCount;
            var values = new object[colCount];

            #region Indices
            int indTurnoId = dr.GetOrdinal("TurnoId");
            int indCodigo = dr.GetOrdinal("Codigo");
            int indNombre = dr.GetOrdinal("Nombre");
            int indHoraInicio = dr.GetOrdinal("HoraInicio");
            int indHoraFin = dr.GetOrdinal("HoraFin");
            int indActivo = dr.GetOrdinal("Activo");
            int indUsuarioCreacionId = dr.GetOrdinal("UsuarioCreacionId");
            int indUsuarioCreacion = dr.GetOrdinal("UsuarioCreacion");
            int indFechaCreacion = dr.GetOrdinal("FechaCreacion");
            int indUsuarioModificacionId = dr.GetOrdinal("UsuarioModificacionId");
            int indUsuarioModificacion = dr.GetOrdinal("UsuarioModificacion");
            int indFechaModificacion = dr.GetOrdinal("FechaModificacion");
            int indNumeroFila = dr.GetOrdinal("NumeroFila");
            int indTotalFilas = dr.GetOrdinal("TotalFilas");
            #endregion

            var turnoInfo = new TurnoInfo();
            dr.GetValues(values);

            #region Campos
            turnoInfo.TurnoId = Convert.ToInt32(values[indTurnoId]);
            turnoInfo.Codigo = Convert.ToString(values[indCodigo]);
            turnoInfo.Nombre = Convert.ToString(values[indNombre]);
            turnoInfo.HoraInicio = Convert.ToString(values[indHoraInicio]);
            turnoInfo.HoraFin = Convert.ToString(values[indHoraFin]);
            turnoInfo.Activo = Convert.ToInt32(values[indActivo]);
            turnoInfo.UsuarioCreacionId = Convert.ToInt32(values[indUsuarioCreacionId]);
            turnoInfo.UsuarioCreacion = Convert.ToString(values[indUsuarioCreacion]);
            turnoInfo.FechaCreacion = Convert.ToDateTime(values[indFechaCreacion]);
            turnoInfo.UsuarioModificacionId = Convert.ToInt32(values[indUsuarioModificacionId]);
            turnoInfo.UsuarioModificacion = Convert.ToString(values[indUsuarioModificacion]);
            turnoInfo.FechaModificacion = Convert.ToDateTime(values[indFechaModificacion]);
            turnoInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
            turnoInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);
            #endregion

            return turnoInfo;
        }
        public int Insertar(TurnoInfo turnoInfo)
        {
            int resultado;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarTurno", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("TurnoId", SqlDbType.Int).Value = turnoInfo.TurnoId;
                    sqlComando.Parameters.Add("Codigo", SqlDbType.VarChar).Value = turnoInfo.Codigo;
                    sqlComando.Parameters.Add("Nombre", SqlDbType.VarChar).Value = turnoInfo.Nombre;
                    sqlComando.Parameters.Add("HoraInicio", SqlDbType.VarChar).Value = turnoInfo.HoraInicio;
                    sqlComando.Parameters.Add("HoraFin", SqlDbType.VarChar).Value = turnoInfo.HoraFin;
                    sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = turnoInfo.Activo;
                    sqlComando.Parameters.Add("UsuarioCreacionId", SqlDbType.Int).Value = turnoInfo.UsuarioCreacionId;

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
        public int Actualizar(TurnoInfo turnoInfo)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "ActualizarTurno", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("TurnoId", SqlDbType.Int).Value = turnoInfo.TurnoId;
                    sqlComando.Parameters.Add("Codigo", SqlDbType.VarChar).Value = turnoInfo.Codigo;
                    sqlComando.Parameters.Add("Nombre", SqlDbType.VarChar).Value = turnoInfo.Nombre;
                    sqlComando.Parameters.Add("HoraInicio", SqlDbType.VarChar).Value = turnoInfo.HoraInicio;
                    sqlComando.Parameters.Add("HoraFin", SqlDbType.VarChar).Value = turnoInfo.HoraFin;
                    sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = turnoInfo.Activo;
                    sqlComando.Parameters.Add("UsuarioModificacionId", SqlDbType.Int).Value = turnoInfo.UsuarioModificacionId;

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
        public int Eliminar(int turnoId)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "EliminarTurno", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("TurnoId", SqlDbType.Int).Value = turnoId;

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