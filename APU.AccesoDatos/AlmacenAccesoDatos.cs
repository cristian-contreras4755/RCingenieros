using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.AccesoDatos
{
    public class AlmacenAccesoDatos
    {
        public List<AlmacenInfo> Listar(int almacenId)
        {
            var almacenListaInfo = new List<AlmacenInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerAlmacen";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("AlmacenId", SqlDbType.Int).Value = almacenId;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        almacenListaInfo.Add(CargarAlmacenInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return almacenListaInfo;
        }
        public List<AlmacenInfo> ListarPaginado(int almacenId, int empresaId, int tamanioPagina, int numeroPagina)
        {
            var almacenListaInfo = new List<AlmacenInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerAlmacenPaginado";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("AlmacenId", SqlDbType.Int).Value = almacenId;
                sqlComando.Parameters.Add("EmpresaId", SqlDbType.Int).Value = empresaId;
                sqlComando.Parameters.Add("TamanioPagina", SqlDbType.Int).Value = tamanioPagina;
                sqlComando.Parameters.Add("NumeroPagina", SqlDbType.Int).Value = numeroPagina;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        almacenListaInfo.Add(CargarAlmacenInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return almacenListaInfo;
        }
        private static AlmacenInfo CargarAlmacenInfo(IDataReader dr)
        {
            int colCount = dr.FieldCount;
            var values = new object[colCount];

            #region Indices
            int indAlmacenId = dr.GetOrdinal("AlmacenId");
            int indEmpresaId = dr.GetOrdinal("EmpresaId");
            int indEmpresa = dr.GetOrdinal("Empresa");
            int indCodigo = dr.GetOrdinal("Codigo");
            int indNombre = dr.GetOrdinal("Nombre");
            int indDescripcion = dr.GetOrdinal("Descripcion");
            int indDireccion = dr.GetOrdinal("Direccion");
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

            var almacenInfo = new AlmacenInfo();
            dr.GetValues(values);

            #region Campos
            almacenInfo.AlmacenId = Convert.ToInt32(values[indAlmacenId]);
            almacenInfo.EmpresaId = Convert.ToInt32(values[indEmpresaId]);
            almacenInfo.Empresa = Convert.ToString(values[indEmpresa]);
            almacenInfo.Codigo = Convert.ToString(values[indCodigo]);
            almacenInfo.Nombre = Convert.ToString(values[indNombre]);
            almacenInfo.Descripcion = Convert.ToString(values[indDescripcion]);
            almacenInfo.Direccion = Convert.ToString(values[indDireccion]);
            almacenInfo.Activo = Convert.ToInt32(values[indActivo]);
            almacenInfo.UsuarioCreacionId = Convert.ToInt32(values[indUsuarioCreacionId]);

            if (values[indUsuarioCreacion] != DBNull.Value) almacenInfo.UsuarioCreacion = Convert.ToString(values[indUsuarioCreacion]);
            almacenInfo.FechaCreacion = Convert.ToDateTime(values[indFechaCreacion]);
            if (values[indUsuarioModificacionId] != DBNull.Value) almacenInfo.UsuarioModificacionId = Convert.ToInt32(values[indUsuarioModificacionId]);
            if (values[indUsuarioModificacion] != DBNull.Value) almacenInfo.UsuarioModificacion = Convert.ToString(values[indUsuarioModificacion]);
            if (values[indFechaModificacion] != DBNull.Value) almacenInfo.FechaModificacion = Convert.ToDateTime(values[indFechaModificacion]);
            almacenInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
            almacenInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);
            #endregion

            return almacenInfo;
        }
        public int Insertar(AlmacenInfo almacenInfo)
        {
            int resultado;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarAlmacen", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("EmpresaId", SqlDbType.Int).Value = almacenInfo.EmpresaId;
                    sqlComando.Parameters.Add("Codigo", SqlDbType.VarChar).Value = almacenInfo.Codigo;
                    sqlComando.Parameters.Add("Nombre", SqlDbType.VarChar).Value = almacenInfo.Nombre;
                    sqlComando.Parameters.Add("Descripcion", SqlDbType.VarChar).Value = almacenInfo.Descripcion;
                    sqlComando.Parameters.Add("Direccion", SqlDbType.VarChar).Value = almacenInfo.Direccion;
                    sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = almacenInfo.Activo;
                    sqlComando.Parameters.Add("UsuarioCreacionId", SqlDbType.Int).Value = almacenInfo.UsuarioCreacionId;

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
        public int Actualizar(AlmacenInfo almacenInfo)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "ActualizarAlmacen", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("AlmacenId", SqlDbType.Int).Value = almacenInfo.AlmacenId;
                    sqlComando.Parameters.Add("EmpresaId", SqlDbType.Int).Value = almacenInfo.EmpresaId;
                    sqlComando.Parameters.Add("Codigo", SqlDbType.VarChar).Value = almacenInfo.Codigo;
                    sqlComando.Parameters.Add("Nombre", SqlDbType.VarChar).Value = almacenInfo.Nombre;
                    sqlComando.Parameters.Add("Descripcion", SqlDbType.VarChar).Value = almacenInfo.Descripcion;
                    sqlComando.Parameters.Add("Direccion", SqlDbType.VarChar).Value = almacenInfo.Direccion;
                    sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = almacenInfo.Activo;
                    sqlComando.Parameters.Add("UsuarioModificacionId", SqlDbType.Int).Value = almacenInfo.UsuarioModificacionId;

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
        public int Eliminar(int almacenId)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "EliminarAlmacen", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("AlmacenId", SqlDbType.Int).Value = almacenId;

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