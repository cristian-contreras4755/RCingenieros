using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.AccesoDatos
{
    public class ClienteAccesoDatos
    {
        public List<ClienteInfo> Listar(int clienteId)
        {
            var clienteListaInfo = new List<ClienteInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerCliente";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("ClienteId", SqlDbType.Int).Value = clienteId;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        clienteListaInfo.Add(CargarClienteInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return clienteListaInfo;
        }
        public List<ClienteInfo> ListarPaginado(int clienteId, int tipoPersonaId, int tipoDocumentoId, string numeroDocumento, string nombres, string apellidoPaterno, string apellidoMaterno,
                                                string ruc, string razonSocial, string codigo, int tamanioPagina, int numeroPagina)
        {
            var clienteListaInfo = new List<ClienteInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerClientePaginado";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("ClienteId", SqlDbType.Int).Value = clienteId;
                sqlComando.Parameters.Add("TipoPersonaId", SqlDbType.Int).Value = tipoPersonaId;
                sqlComando.Parameters.Add("TipoDocumentoId", SqlDbType.Int).Value = tipoDocumentoId;
                sqlComando.Parameters.Add("NumeroDocumento", SqlDbType.VarChar).Value = numeroDocumento;
                sqlComando.Parameters.Add("Nombres", SqlDbType.VarChar).Value = nombres;
                sqlComando.Parameters.Add("ApellidoPaterno", SqlDbType.VarChar).Value = apellidoPaterno;
                sqlComando.Parameters.Add("ApellidoMaterno", SqlDbType.VarChar).Value = apellidoMaterno;
                sqlComando.Parameters.Add("Ruc", SqlDbType.VarChar).Value = ruc;
                sqlComando.Parameters.Add("RazonSocial", SqlDbType.VarChar).Value = razonSocial;
                sqlComando.Parameters.Add("Codigo", SqlDbType.VarChar).Value = codigo;
                sqlComando.Parameters.Add("TamanioPagina", SqlDbType.Int).Value = tamanioPagina;
                sqlComando.Parameters.Add("NumeroPagina", SqlDbType.Int).Value = numeroPagina;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        clienteListaInfo.Add(CargarClienteInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return clienteListaInfo;
        }
        private static ClienteInfo CargarClienteInfo(IDataReader dr)
        {
            int colCount = dr.FieldCount;
            var values = new object[colCount];

            #region Indices
            int indClienteId = dr.GetOrdinal("ClienteId");
            int indCliente = dr.GetOrdinal("Cliente");
            int indCodigo = dr.GetOrdinal("Codigo");
            int indTipoPersonaId = dr.GetOrdinal("TipoPersonaId");
            int indNombres = dr.GetOrdinal("Nombres");
            int indApellidoPaterno = dr.GetOrdinal("ApellidoPaterno");
            int indApellidoMaterno = dr.GetOrdinal("ApellidoMaterno");
            int indRazonSocial = dr.GetOrdinal("RazonSocial");
            int indDescripcion = dr.GetOrdinal("Descripcion");
            int indTipoDocumentoId = dr.GetOrdinal("TipoDocumentoId");
            int indTipoDocumento = dr.GetOrdinal("TipoDocumento");
            int indNumeroDocumento = dr.GetOrdinal("NumeroDocumento");
            int indPais = dr.GetOrdinal("PaisId");
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
            int indCorreo = dr.GetOrdinal("Correo");
            int indContacto = dr.GetOrdinal("Contacto");
            int indUrl = dr.GetOrdinal("Url");
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

            var clienteInfo = new ClienteInfo();
            dr.GetValues(values);

            #region Campos
            clienteInfo.ClienteId = Convert.ToInt32(values[indClienteId]);
            clienteInfo.Cliente = Convert.ToString(values[indCliente]);
            clienteInfo.Codigo = Convert.ToString(values[indCodigo]);
            clienteInfo.TipoPersonaId = Convert.ToInt32(values[indTipoPersonaId]);
            clienteInfo.Nombres = Convert.ToString(values[indNombres]);
            clienteInfo.ApellidoPaterno = Convert.ToString(values[indApellidoPaterno]);
            clienteInfo.ApellidoMaterno = Convert.ToString(values[indApellidoMaterno]);
            clienteInfo.RazonSocial = Convert.ToString(values[indRazonSocial]);
            clienteInfo.Descripcion = Convert.ToString(values[indDescripcion]);
            clienteInfo.TipoDocumentoId = Convert.ToInt32(values[indTipoDocumentoId]);
            clienteInfo.TipoDocumento = Convert.ToString(values[indTipoDocumento]);
            clienteInfo.NumeroDocumento = Convert.ToString(values[indNumeroDocumento]);
            clienteInfo.PaisId = Convert.ToInt32(values[indPais]);
            clienteInfo.DepartamentoId = Convert.ToInt32(values[indDepartamentoId]);
            clienteInfo.Departamento = Convert.ToString(values[indDepartamento]);
            clienteInfo.ProvinciaId = Convert.ToInt32(values[indProvinciaId]);
            clienteInfo.Provincia = Convert.ToString(values[indProvincia]);
            clienteInfo.DistritoId = Convert.ToInt32(values[indDistritoId]);
            clienteInfo.Distrito = Convert.ToString(values[indDistrito]);
            clienteInfo.Ciudad = Convert.ToString(values[indCiudad]);
            clienteInfo.Direccion = Convert.ToString(values[indDireccion]);
            clienteInfo.Telefono = Convert.ToString(values[indTelefono]);
            clienteInfo.Celular = Convert.ToString(values[indCelular]);
            clienteInfo.Fax = Convert.ToString(values[indFax]);
            clienteInfo.Correo = Convert.ToString(values[indCorreo]);
            clienteInfo.Contacto = Convert.ToString(values[indContacto]);
            clienteInfo.Url = Convert.ToString(values[indUrl]);
            clienteInfo.Imagen = Convert.ToString(values[indImagen]);
            clienteInfo.Activo = Convert.ToInt32(values[indActivo]);
            clienteInfo.UsuarioCreacionId = Convert.ToInt32(values[indUsuarioCreacionId]);
            if (values[indUsuarioCreacion] != DBNull.Value) clienteInfo.UsuarioCreacion = Convert.ToString(values[indUsuarioCreacion]);

            clienteInfo.FechaCreacion = Convert.ToDateTime(values[indFechaCreacion]);
            if (values[indUsuarioModificacionId] != DBNull.Value) clienteInfo.UsuarioModificacionId = Convert.ToInt32(values[indUsuarioModificacionId]);
            if (values[indUsuarioModificacion] != DBNull.Value) clienteInfo.UsuarioModificacion = Convert.ToString(values[indUsuarioModificacion]);
            if (values[indFechaModificacion] != DBNull.Value) clienteInfo.FechaModificacion = Convert.ToDateTime(values[indFechaModificacion]);
            clienteInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
            clienteInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);
            #endregion

            return clienteInfo;
        }
        public int Insertar(ClienteInfo clienteInfo)
        {
            int resultado;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarCliente", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("Codigo", SqlDbType.VarChar).Value = clienteInfo.Codigo;
                    sqlComando.Parameters.Add("TipoPersonaId", SqlDbType.Int).Value = clienteInfo.TipoPersonaId;
                    sqlComando.Parameters.Add("Nombres", SqlDbType.VarChar).Value = clienteInfo.Nombres;
                    sqlComando.Parameters.Add("ApellidoPaterno", SqlDbType.VarChar).Value = clienteInfo.ApellidoPaterno;
                    sqlComando.Parameters.Add("ApellidoMaterno", SqlDbType.VarChar).Value = clienteInfo.ApellidoMaterno;
                    sqlComando.Parameters.Add("RazonSocial", SqlDbType.VarChar).Value = clienteInfo.RazonSocial;
                    sqlComando.Parameters.Add("Descripcion", SqlDbType.VarChar).Value = clienteInfo.Descripcion;
                    sqlComando.Parameters.Add("TipoDocumentoId", SqlDbType.Int).Value = clienteInfo.TipoDocumentoId;
                    sqlComando.Parameters.Add("NumeroDocumento", SqlDbType.VarChar).Value = clienteInfo.NumeroDocumento;
                    sqlComando.Parameters.Add("PaisId", SqlDbType.Int).Value = clienteInfo.PaisId;
                    sqlComando.Parameters.Add("DepartamentoId", SqlDbType.Int).Value = clienteInfo.DepartamentoId;
                    sqlComando.Parameters.Add("ProvinciaId", SqlDbType.Int).Value = clienteInfo.ProvinciaId;
                    sqlComando.Parameters.Add("DistritoId", SqlDbType.Int).Value = clienteInfo.DistritoId;
                    sqlComando.Parameters.Add("Ciudad", SqlDbType.VarChar).Value = clienteInfo.Ciudad;
                    sqlComando.Parameters.Add("Direccion", SqlDbType.VarChar).Value = clienteInfo.Direccion;
                    sqlComando.Parameters.Add("Telefono", SqlDbType.VarChar).Value = clienteInfo.Telefono;
                    sqlComando.Parameters.Add("Celular", SqlDbType.VarChar).Value = clienteInfo.Celular;
                    sqlComando.Parameters.Add("Fax", SqlDbType.VarChar).Value = clienteInfo.Fax;
                    sqlComando.Parameters.Add("Correo", SqlDbType.VarChar).Value = clienteInfo.Correo;
                    sqlComando.Parameters.Add("Contacto", SqlDbType.VarChar).Value = clienteInfo.Contacto;
                    sqlComando.Parameters.Add("Url", SqlDbType.VarChar).Value = clienteInfo.Url;
                    sqlComando.Parameters.Add("Imagen", SqlDbType.VarChar).Value = clienteInfo.Imagen;
                    sqlComando.Parameters.Add("Activo", SqlDbType.VarChar).Value = clienteInfo.Activo;
                    sqlComando.Parameters.Add("UsuarioCreacionId", SqlDbType.Int).Value = clienteInfo.UsuarioCreacionId;

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
        public int Actualizar(ClienteInfo clienteInfo)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "ActualizarCliente", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("ClienteId", SqlDbType.Int).Value = clienteInfo.ClienteId;
                    sqlComando.Parameters.Add("Codigo", SqlDbType.VarChar).Value = clienteInfo.Codigo;
                    sqlComando.Parameters.Add("TipoPersonaId", SqlDbType.Int).Value = clienteInfo.TipoPersonaId;
                    sqlComando.Parameters.Add("Nombres", SqlDbType.VarChar).Value = clienteInfo.Nombres;
                    sqlComando.Parameters.Add("ApellidoPaterno", SqlDbType.VarChar).Value = clienteInfo.ApellidoPaterno;
                    sqlComando.Parameters.Add("ApellidoMaterno", SqlDbType.VarChar).Value = clienteInfo.ApellidoMaterno;
                    sqlComando.Parameters.Add("RazonSocial", SqlDbType.VarChar).Value = clienteInfo.RazonSocial;
                    sqlComando.Parameters.Add("Descripcion", SqlDbType.VarChar).Value = clienteInfo.Descripcion;
                    sqlComando.Parameters.Add("TipoDocumentoId", SqlDbType.Int).Value = clienteInfo.TipoDocumentoId;
                    sqlComando.Parameters.Add("NumeroDocumento", SqlDbType.VarChar).Value = clienteInfo.NumeroDocumento;
                    sqlComando.Parameters.Add("PaisId", SqlDbType.Int).Value = clienteInfo.PaisId;
                    sqlComando.Parameters.Add("DepartamentoId", SqlDbType.Int).Value = clienteInfo.DepartamentoId;
                    sqlComando.Parameters.Add("ProvinciaId", SqlDbType.Int).Value = clienteInfo.ProvinciaId;
                    sqlComando.Parameters.Add("DistritoId", SqlDbType.Int).Value = clienteInfo.DistritoId;
                    sqlComando.Parameters.Add("Ciudad", SqlDbType.VarChar).Value = clienteInfo.Ciudad;
                    sqlComando.Parameters.Add("Direccion", SqlDbType.VarChar).Value = clienteInfo.Direccion;
                    sqlComando.Parameters.Add("Telefono", SqlDbType.VarChar).Value = clienteInfo.Telefono;
                    sqlComando.Parameters.Add("Celular", SqlDbType.VarChar).Value = clienteInfo.Celular;
                    sqlComando.Parameters.Add("Fax", SqlDbType.VarChar).Value = clienteInfo.Fax;
                    sqlComando.Parameters.Add("Correo", SqlDbType.VarChar).Value = clienteInfo.Correo;
                    sqlComando.Parameters.Add("Contacto", SqlDbType.VarChar).Value = clienteInfo.Contacto;
                    sqlComando.Parameters.Add("Url", SqlDbType.VarChar).Value = clienteInfo.Url;
                    sqlComando.Parameters.Add("Imagen", SqlDbType.VarChar).Value = clienteInfo.Imagen;
                    sqlComando.Parameters.Add("Activo", SqlDbType.VarChar).Value = clienteInfo.Activo;
                    sqlComando.Parameters.Add("UsuarioModificacionId", SqlDbType.Int).Value = clienteInfo.UsuarioModificacionId;

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
        public int Eliminar(int clienteId)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "EliminarCliente", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("ClienteId", SqlDbType.Int).Value = clienteId;

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