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
    public class EmpresaAccesoDatos
    {
        public List<EmpresaInfo> Listar(int empresaId)
        {
            var empresaListaInfo = new List<EmpresaInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerEmpresa";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("EmpresaId", SqlDbType.Int).Value = empresaId;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        empresaListaInfo.Add(CargarEmpresaInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return empresaListaInfo;
        }
        public List<EmpresaInfo> ListarPaginado(int empresaId, int tamanioPagina, int numeroPagina)
        {
            var empresaListaInfo = new List<EmpresaInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerEmpresaPaginado";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("EmpresaId", SqlDbType.Int).Value = empresaId;
                sqlComando.Parameters.Add("TamanioPagina", SqlDbType.Int).Value = tamanioPagina;
                sqlComando.Parameters.Add("NumeroPagina", SqlDbType.Int).Value = numeroPagina;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        empresaListaInfo.Add(CargarEmpresaInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return empresaListaInfo;
        }
        private static EmpresaInfo CargarEmpresaInfo(IDataReader dr)
        {
            int colCount = dr.FieldCount;
            var values = new object[colCount];

            #region Indices
            int indEmpresaId = dr.GetOrdinal("EmpresaId");
            int indTipoDocumentoId = dr.GetOrdinal("TipoDocumentoId");
            int indNumeroDocumento = dr.GetOrdinal("NumeroDocumento");
            int indDescripcion = dr.GetOrdinal("Descripcion");
            int indRazonSocial = dr.GetOrdinal("RazonSocial");
            int indCiiu = dr.GetOrdinal("Ciiu");
            int indPaisId = dr.GetOrdinal("PaisId");
            int indPais = dr.GetOrdinal("Pais");
            int indDepartamentoId = dr.GetOrdinal("DepartamentoId");
            int indDepartamento = dr.GetOrdinal("Departamento");
            int indProvinciaId = dr.GetOrdinal("ProvinciaId");
            int indProvincia = dr.GetOrdinal("Provincia");
            int indDistritoId = dr.GetOrdinal("DistritoId");
            int indDistrito = dr.GetOrdinal("Distrito");
            int indCiudad = dr.GetOrdinal("Ciudad");
            int indDireccion = dr.GetOrdinal("Direccion");
            int indTelefono = dr.GetOrdinal("Telefono");
            int indCelular = dr.GetOrdinal("Celular");
            int indFax = dr.GetOrdinal("Fax");
            int indImagen = dr.GetOrdinal("Imagen");
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

            var empresaInfo = new EmpresaInfo();
            dr.GetValues(values);

            #region Campos
            empresaInfo.EmpresaId = Convert.ToInt32(values[indEmpresaId]);
            empresaInfo.TipoDocumentoId = Convert.ToInt32(values[indTipoDocumentoId]);
            empresaInfo.NumeroDocumento = Convert.ToString(values[indNumeroDocumento]);
            empresaInfo.Descripcion = Convert.ToString(values[indDescripcion]);
            empresaInfo.RazonSocial = Convert.ToString(values[indRazonSocial]);
            empresaInfo.Ciiu = Convert.ToString(values[indCiiu]);
            empresaInfo.PaisId = Convert.ToInt32(values[indPaisId]);
            empresaInfo.Pais = Convert.ToString(values[indPais]);
            empresaInfo.DepartamentoId = Convert.ToInt32(values[indDepartamentoId]);
            empresaInfo.Departamento = Convert.ToString(values[indDepartamento]);
            empresaInfo.ProvinciaId = Convert.ToInt32(values[indProvinciaId]);
            empresaInfo.Provincia = Convert.ToString(values[indProvincia]);
            empresaInfo.DistritoId = Convert.ToInt32(values[indDistritoId]);
            empresaInfo.Distrito = Convert.ToString(values[indDistrito]);
            empresaInfo.Ciudad = Convert.ToString(values[indCiudad]);
            empresaInfo.Direccion = Convert.ToString(values[indDireccion]);
            empresaInfo.Telefono = Convert.ToString(values[indTelefono]);
            empresaInfo.Celular = Convert.ToString(values[indCelular]);
            empresaInfo.Fax = Convert.ToString(values[indFax]);
            empresaInfo.Imagen = Convert.ToString(values[indImagen]);
            empresaInfo.Activo = Convert.ToInt32(values[indActivo]);
            empresaInfo.UsuarioCreacionId = Convert.ToInt32(values[indUsuarioCreacionId]);

            if (values[indUsuarioCreacion] != DBNull.Value) empresaInfo.UsuarioCreacion = Convert.ToString(values[indUsuarioCreacion]);

            empresaInfo.FechaCreacion = Convert.ToDateTime(values[indFechaCreacion]);

            if (values[indUsuarioModificacionId] != DBNull.Value) empresaInfo.UsuarioModificacionId = Convert.ToInt32(values[indUsuarioModificacionId]);
            if (values[indUsuarioModificacion] != DBNull.Value) empresaInfo.UsuarioModificacion = Convert.ToString(values[indUsuarioModificacion]);
            if (values[indFechaModificacion] != DBNull.Value) empresaInfo.FechaModificacion = Convert.ToDateTime(values[indFechaModificacion]);
            empresaInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
            empresaInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);
            #endregion

            return empresaInfo;
        }
        public int Insertar(EmpresaInfo empresaInfo)
        {
            int resultado;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarEmpresa", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("TipoDocumentoId", SqlDbType.Int).Value = empresaInfo.TipoDocumentoId;
                    sqlComando.Parameters.Add("NumeroDocumento", SqlDbType.VarChar).Value = empresaInfo.NumeroDocumento;
                    sqlComando.Parameters.Add("Descripcion", SqlDbType.VarChar).Value = empresaInfo.Descripcion;
                    sqlComando.Parameters.Add("RazonSocial", SqlDbType.VarChar).Value = empresaInfo.RazonSocial;
                    sqlComando.Parameters.Add("Ciiu", SqlDbType.VarChar).Value = empresaInfo.Ciiu;
                    sqlComando.Parameters.Add("PaisId", SqlDbType.Int).Value = empresaInfo.PaisId;
                    sqlComando.Parameters.Add("DepartamentoId", SqlDbType.Int).Value = empresaInfo.DepartamentoId;
                    sqlComando.Parameters.Add("ProvinciaId", SqlDbType.Int).Value = empresaInfo.ProvinciaId;
                    sqlComando.Parameters.Add("DistritoId", SqlDbType.Int).Value = empresaInfo.DistritoId;
                    sqlComando.Parameters.Add("Ciudad", SqlDbType.VarChar).Value = empresaInfo.Ciudad;
                    sqlComando.Parameters.Add("Direccion", SqlDbType.VarChar).Value = empresaInfo.Direccion;
                    sqlComando.Parameters.Add("Imagen", SqlDbType.VarChar).Value = empresaInfo.Imagen;
                    sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = empresaInfo.Activo;
                    sqlComando.Parameters.Add("UsuarioCreacionId", SqlDbType.Int).Value = empresaInfo.UsuarioCreacionId;

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
        public int Actualizar(EmpresaInfo empresaInfo)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "ActualizarEmpresa", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("EmpresaId", SqlDbType.Int).Value = empresaInfo.EmpresaId;
                    sqlComando.Parameters.Add("TipoDocumentoId", SqlDbType.Int).Value = empresaInfo.TipoDocumentoId;
                    sqlComando.Parameters.Add("NumeroDocumento", SqlDbType.VarChar).Value = empresaInfo.NumeroDocumento;
                    sqlComando.Parameters.Add("Descripcion", SqlDbType.VarChar).Value = empresaInfo.Descripcion;
                    sqlComando.Parameters.Add("RazonSocial", SqlDbType.VarChar).Value = empresaInfo.RazonSocial;
                    sqlComando.Parameters.Add("Ciiu", SqlDbType.VarChar).Value = empresaInfo.Ciiu;
                    sqlComando.Parameters.Add("PaisId", SqlDbType.Int).Value = empresaInfo.PaisId;
                    sqlComando.Parameters.Add("DepartamentoId", SqlDbType.Int).Value = empresaInfo.DepartamentoId;
                    sqlComando.Parameters.Add("ProvinciaId", SqlDbType.Int).Value = empresaInfo.ProvinciaId;
                    sqlComando.Parameters.Add("DistritoId", SqlDbType.Int).Value = empresaInfo.DistritoId;
                    sqlComando.Parameters.Add("Ciudad", SqlDbType.VarChar).Value = empresaInfo.Ciudad;
                    sqlComando.Parameters.Add("Direccion", SqlDbType.VarChar).Value = empresaInfo.Direccion;
                    sqlComando.Parameters.Add("Imagen", SqlDbType.VarChar).Value = empresaInfo.Imagen;
                    sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = empresaInfo.Activo;
                    sqlComando.Parameters.Add("UsuarioModificacionId", SqlDbType.Int).Value = empresaInfo.UsuarioModificacionId;

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
        public int Eliminar(int empresaId)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "EliminarEmpresa", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("EmpresaId", SqlDbType.Int).Value = empresaId;

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
