using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.AccesoDatos
{
    public class AgenciaAccesoDatos
    {
        public List<AgenciaInfo> Listar(int agenciaId)
        {
            var agenciaListaInfo = new List<AgenciaInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerAgencia";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("AgenciaId", SqlDbType.Int).Value = agenciaId;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        agenciaListaInfo.Add(CargarAgenciaInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return agenciaListaInfo;
        }
        public List<AgenciaInfo> ListarPaginado(int agenciaId, int empresaId, int clienteId, int tamanioPagina, int numeroPagina)
        {
            var agenciaListaInfo = new List<AgenciaInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerAgenciaPaginado";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("AgenciaId", SqlDbType.Int).Value = agenciaId;
                sqlComando.Parameters.Add("EmpresaId", SqlDbType.Int).Value = empresaId;
                sqlComando.Parameters.Add("ClienteId", SqlDbType.Int).Value = clienteId;
                sqlComando.Parameters.Add("TamanioPagina", SqlDbType.Int).Value = tamanioPagina;
                sqlComando.Parameters.Add("NumeroPagina", SqlDbType.Int).Value = numeroPagina;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        agenciaListaInfo.Add(CargarAgenciaInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return agenciaListaInfo;
        }
        private static AgenciaInfo CargarAgenciaInfo(IDataReader dr)
        {
            int colCount = dr.FieldCount;
            var values = new object[colCount];

            #region Indices
            int indAgenciaId = dr.GetOrdinal("AgenciaId");
            int indEmpresaId = dr.GetOrdinal("EmpresaId");
            int indEmpresa = dr.GetOrdinal("Empresa");
            int indNombre = dr.GetOrdinal("Nombre");
            int indDescripcion = dr.GetOrdinal("Descripcion");
            int indPaisId = dr.GetOrdinal("PaisId");
            int indDepartamentoId = dr.GetOrdinal("DepartamentoId");
            int indProvinciaId = dr.GetOrdinal("ProvinciaId");
            int indDistritoId = dr.GetOrdinal("DistritoId");
            int indCiudad = dr.GetOrdinal("Ciudad");
            int indDireccion = dr.GetOrdinal("Direccion");
            int indContactoId = dr.GetOrdinal("ContactoId");
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

            var agenciaInfo = new AgenciaInfo();
            dr.GetValues(values);

            #region Campos
            agenciaInfo.AgenciaId = Convert.ToInt32(values[indAgenciaId]);
            agenciaInfo.EmpresaId = Convert.ToInt32(values[indEmpresaId]);
            agenciaInfo.Empresa = Convert.ToString(values[indEmpresa]);
            agenciaInfo.Nombre = Convert.ToString(values[indNombre]);
            agenciaInfo.Descripcion = Convert.ToString(values[indDescripcion]);
            agenciaInfo.PaisId = Convert.ToInt32(values[indPaisId]);
            agenciaInfo.DepartamentoId = Convert.ToInt32(values[indDepartamentoId]);
            agenciaInfo.ProvinciaId = Convert.ToInt32(values[indProvinciaId]);
            agenciaInfo.DistritoId = Convert.ToInt32(values[indDistritoId]);
            agenciaInfo.Ciudad = Convert.ToString(values[indCiudad]);
            agenciaInfo.Direccion = Convert.ToString(values[indDireccion]);
            agenciaInfo.Activo = Convert.ToInt32(values[indActivo]);
            agenciaInfo.UsuarioCreacionId = Convert.ToInt32(values[indUsuarioCreacionId]);

            agenciaInfo.ContactoId = Convert.ToInt32(values[indContactoId]);

            if (values[indUsuarioCreacion] != DBNull.Value) agenciaInfo.UsuarioCreacion = Convert.ToString(values[indUsuarioCreacion]);

            agenciaInfo.FechaCreacion = Convert.ToDateTime(values[indFechaCreacion]);

            if (values[indUsuarioModificacionId] != DBNull.Value) agenciaInfo.UsuarioModificacionId = Convert.ToInt32(values[indUsuarioModificacionId]);
            if (values[indUsuarioModificacion] != DBNull.Value) agenciaInfo.UsuarioModificacion = Convert.ToString(values[indUsuarioModificacion]);
            if (values[indFechaModificacion] != DBNull.Value) agenciaInfo.FechaModificacion = Convert.ToDateTime(values[indFechaModificacion]);
            agenciaInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
            agenciaInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);
            #endregion

            return agenciaInfo;
        }
        public int Insertar(AgenciaInfo agenciaInfo)
        {
            int resultado;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarAgencia", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("EmpresaId", SqlDbType.Int).Value = agenciaInfo.EmpresaId;
                    sqlComando.Parameters.Add("Nombre", SqlDbType.VarChar).Value = agenciaInfo.Nombre;
                    sqlComando.Parameters.Add("Descripcion", SqlDbType.VarChar).Value = agenciaInfo.Descripcion;
                    sqlComando.Parameters.Add("PaisId", SqlDbType.Int).Value = agenciaInfo.PaisId;
                    sqlComando.Parameters.Add("DepartamentoId", SqlDbType.Int).Value = agenciaInfo.DepartamentoId;
                    sqlComando.Parameters.Add("ProvinciaId", SqlDbType.Int).Value = agenciaInfo.ProvinciaId;
                    sqlComando.Parameters.Add("DistritoId", SqlDbType.Int).Value = agenciaInfo.DistritoId;
                    sqlComando.Parameters.Add("Ciudad", SqlDbType.VarChar).Value = agenciaInfo.Ciudad;
                    sqlComando.Parameters.Add("Direccion", SqlDbType.VarChar).Value = agenciaInfo.Direccion;
                    sqlComando.Parameters.Add("ContactoId", SqlDbType.Int).Value = agenciaInfo.ContactoId;
                    sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = agenciaInfo.Activo;
                    sqlComando.Parameters.Add("UsuarioCreacionId", SqlDbType.Int).Value = agenciaInfo.UsuarioCreacionId;

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
        public int Actualizar(AgenciaInfo agenciaInfo)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "ActualizarAgencia", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("AgenciaId", SqlDbType.Int).Value = agenciaInfo.AgenciaId;
                    sqlComando.Parameters.Add("EmpresaId", SqlDbType.Int).Value = agenciaInfo.EmpresaId;
                    sqlComando.Parameters.Add("Nombre", SqlDbType.VarChar).Value = agenciaInfo.Nombre;
                    sqlComando.Parameters.Add("Descripcion", SqlDbType.VarChar).Value = agenciaInfo.Descripcion;
                    sqlComando.Parameters.Add("PaisId", SqlDbType.Int).Value = agenciaInfo.PaisId;
                    sqlComando.Parameters.Add("DepartamentoId", SqlDbType.Int).Value = agenciaInfo.DepartamentoId;
                    sqlComando.Parameters.Add("ProvinciaId", SqlDbType.Int).Value = agenciaInfo.ProvinciaId;
                    sqlComando.Parameters.Add("DistritoId", SqlDbType.Int).Value = agenciaInfo.DistritoId;
                    sqlComando.Parameters.Add("Ciudad", SqlDbType.VarChar).Value = agenciaInfo.Ciudad;
                    sqlComando.Parameters.Add("Direccion", SqlDbType.VarChar).Value = agenciaInfo.Direccion;
                    sqlComando.Parameters.Add("ContactoId", SqlDbType.Int).Value = agenciaInfo.ContactoId;
                    sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = agenciaInfo.Activo;
                    sqlComando.Parameters.Add("UsuarioModificacionId", SqlDbType.Int).Value = agenciaInfo.UsuarioModificacionId;

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
        public int Eliminar(int agenciaId)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "EliminarAgencia", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("AgenciaId", SqlDbType.Int).Value = agenciaId;

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
