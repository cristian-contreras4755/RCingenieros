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
    public class UsuarioAccesoDatos
    {
        public List<UsuarioInfo> Listar(int usuarioId, string login, string password, string correo, int perfilId, int empresaId)
        {
            var usuarioListaInfo = new List<UsuarioInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerUsuario";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("UsuarioId", SqlDbType.Int).Value = usuarioId;
                sqlComando.Parameters.Add("Login", SqlDbType.VarChar).Value = login;
                sqlComando.Parameters.Add("Password", SqlDbType.VarChar).Value = password;
                sqlComando.Parameters.Add("Correo", SqlDbType.VarChar).Value = correo;
                sqlComando.Parameters.Add("PerfilId", SqlDbType.Int).Value = perfilId;
                sqlComando.Parameters.Add("EmpresaId", SqlDbType.Int).Value = empresaId;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        usuarioListaInfo.Add(CargarUsuarioInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return usuarioListaInfo;
        }
        public List<UsuarioInfo> ListarLogin(string login)
        {
            var usuarioListaInfo = new List<UsuarioInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerUsuarioLogin";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("Login", SqlDbType.VarChar).Value = login;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        usuarioListaInfo.Add(CargarUsuarioInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return usuarioListaInfo;
        }
        public List<UsuarioInfo> ListarPaginado(int usuarioId, string login, string password, string correo, int perfilId, int tamanioPagina, int numeroPagina)
        {
            var usuarioListaInfo = new List<UsuarioInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerUsuarioPaginado";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("UsuarioId", SqlDbType.Int).Value = usuarioId;
                sqlComando.Parameters.Add("Login", SqlDbType.VarChar).Value = login;
                sqlComando.Parameters.Add("Password", SqlDbType.VarChar).Value = password;
                sqlComando.Parameters.Add("Correo", SqlDbType.VarChar).Value = correo;
                sqlComando.Parameters.Add("PerfilId", SqlDbType.Int).Value = perfilId;
                sqlComando.Parameters.Add("TamanioPagina", SqlDbType.Int).Value = tamanioPagina;
                sqlComando.Parameters.Add("NumeroPagina", SqlDbType.Int).Value = numeroPagina;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        usuarioListaInfo.Add(CargarUsuarioInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return usuarioListaInfo;
        }
        private static UsuarioInfo CargarUsuarioInfo(IDataReader dr)
        {
            int colCount = dr.FieldCount;
            var values = new object[colCount];

            #region Indices
            int indUsuarioId = dr.GetOrdinal("UsuarioId");
            int indNombres = dr.GetOrdinal("Nombres");
            int indCodigo = dr.GetOrdinal("Codigo");
            int indApellidoPaterno = dr.GetOrdinal("ApellidoPaterno");
            int indApellidoMaterno = dr.GetOrdinal("ApellidoMaterno");
            int indLogin = dr.GetOrdinal("Login");
            int indPassword = dr.GetOrdinal("Password");
            int indSexoId = dr.GetOrdinal("SexoId");
            int indEstadoCivilId = dr.GetOrdinal("EstadoCivilId");
            int indCorreo = dr.GetOrdinal("Correo");
            int indCorreoTrabajo = dr.GetOrdinal("CorreoTrabajo");
            int indTelefono = dr.GetOrdinal("Telefono");
            int indCelular = dr.GetOrdinal("Celular");
            int indTelefonoTrabajo = dr.GetOrdinal("TelefonoTrabajo");
            int indTipoDocumentoId = dr.GetOrdinal("TipoDocumentoId");
            int indNumeroDocumento = dr.GetOrdinal("NumeroDocumento");
            int indActivo = dr.GetOrdinal("Activo");
            int indIntento = dr.GetOrdinal("Intento");
            int indPerfilId = dr.GetOrdinal("PerfilId");
            int indPerfil = dr.GetOrdinal("Perfil");
            int indEmpresaId = dr.GetOrdinal("EmpresaId");
            int indAgenciaId = dr.GetOrdinal("AgenciaId");
            int indAgencia = dr.GetOrdinal("Agencia");
            int indDepartamentoId = dr.GetOrdinal("DepartamentoId");
            int indDepartamento = dr.GetOrdinal("Departamento");
            int indCargoId = dr.GetOrdinal("CargoId");
            int indCargo = dr.GetOrdinal("Cargo");
            int indDirector = dr.GetOrdinal("Director");
            int indFoto = dr.GetOrdinal("Foto");
            int indTipoNegocioId = dr.GetOrdinal("TipoNegocioId");
            int indImagenEmpresa = dr.GetOrdinal("ImagenEmpresa");
            int indUsuarioCreacionId = dr.GetOrdinal("UsuarioCreacionId");
            int indFechaCreacion = dr.GetOrdinal("FechaCreacion");
            int indUsuarioModificacionId = dr.GetOrdinal("UsuarioModificacionId");
            int indFechaModificacion = dr.GetOrdinal("FechaModificacion");
            int indOpcionInicioId = dr.GetOrdinal("OpcionInicioId");
            int indOpcionInicio = dr.GetOrdinal("OpcionInicio");
            int indNumeroFila = dr.GetOrdinal("NumeroFila");
            int indTotalFilas = dr.GetOrdinal("TotalFilas");
            int indUsuario = dr.GetOrdinal("Usuario");
            int indGuid = dr.GetOrdinal("Guid");
            #endregion

            var usuarioInfo = new UsuarioInfo();
            dr.GetValues(values);

            #region Campos
            usuarioInfo.UsuarioId = Convert.ToInt32(values[indUsuarioId]);
            usuarioInfo.Nombres = Convert.ToString(values[indNombres]);
            usuarioInfo.Codigo = Convert.ToString(values[indCodigo]);
            usuarioInfo.ApellidoPaterno = Convert.ToString(values[indApellidoPaterno]);
            usuarioInfo.ApellidoMaterno = Convert.ToString(values[indApellidoMaterno]);
            usuarioInfo.Login = Convert.ToString(values[indLogin]);
            usuarioInfo.Password = Convert.ToString(values[indPassword]);
            usuarioInfo.SexoId = Convert.ToInt32(values[indSexoId]);
            usuarioInfo.EstadoCivilId = Convert.ToInt32(values[indEstadoCivilId]);
            usuarioInfo.Correo = Convert.ToString(values[indCorreo]);
            usuarioInfo.CorreoTrabajo = Convert.ToString(values[indCorreoTrabajo]);
            usuarioInfo.Telefono = Convert.ToString(values[indTelefono]);
            usuarioInfo.Celular = Convert.ToString(values[indCelular]);
            usuarioInfo.TelefonoTrabajo = Convert.ToString(values[indTelefonoTrabajo]);
            usuarioInfo.TipoDocumentoId = Convert.ToInt32(values[indTipoDocumentoId]);
            
            usuarioInfo.NumeroDocumento = Convert.ToString(values[indNumeroDocumento]);
            usuarioInfo.Activo = Convert.ToInt32(values[indActivo]);
            usuarioInfo.Intento = Convert.ToInt32(values[indIntento]);
            usuarioInfo.PerfilId = Convert.ToInt32(values[indPerfilId]);
            usuarioInfo.Perfil = Convert.ToString(values[indPerfil]);
            usuarioInfo.EmpresaId = Convert.ToInt32(values[indEmpresaId]);
            usuarioInfo.AgenciaId = Convert.ToInt32(values[indAgenciaId]);
            usuarioInfo.Agencia = Convert.ToString(values[indAgencia]);
            usuarioInfo.DepartamentoId = Convert.ToInt32(values[indDepartamentoId]);
            usuarioInfo.Departamento = Convert.ToString(values[indDepartamento]);
            usuarioInfo.CargoId = Convert.ToInt32(values[indCargoId]);
            usuarioInfo.Cargo = Convert.ToString(values[indCargo]);
            usuarioInfo.Director = Convert.ToString(values[indDirector]);
            usuarioInfo.Foto = Convert.ToString(values[indFoto]);
            usuarioInfo.ImagenEmpresa = Convert.ToString(values[indImagenEmpresa]);
            usuarioInfo.TipoNegocioId = Convert.ToInt32(values[indTipoNegocioId]);
            usuarioInfo.UsuarioCreacionId = Convert.ToInt32(values[indUsuarioCreacionId]);
            usuarioInfo.FechaCreacion = Convert.ToDateTime(values[indFechaCreacion]);
            usuarioInfo.UsuarioModificacionId = Convert.ToInt32(values[indUsuarioModificacionId]);

            if (values[indFechaModificacion] != DBNull.Value) usuarioInfo.FechaModificacion = Convert.ToDateTime(values[indFechaModificacion]);

            usuarioInfo.OpcionInicioId = Convert.ToInt32(values[indOpcionInicioId]);
            usuarioInfo.OpcionInicio = Convert.ToString(values[indOpcionInicio]);
            usuarioInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
            usuarioInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);
            usuarioInfo.Usuario = Convert.ToString(values[indUsuario]);
            usuarioInfo.Guid = Convert.ToString(values[indGuid]);
            #endregion

            return usuarioInfo;
        }
        public int Insertar(UsuarioInfo usuarioInfo)
        {
            int resultado;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarUsuario", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("Codigo", SqlDbType.VarChar).Value = usuarioInfo.Codigo;
                    sqlComando.Parameters.Add("Nombres", SqlDbType.VarChar).Value = usuarioInfo.Nombres;
                    sqlComando.Parameters.Add("ApellidoPaterno", SqlDbType.VarChar).Value = usuarioInfo.ApellidoPaterno;
                    sqlComando.Parameters.Add("ApellidoMaterno", SqlDbType.VarChar).Value = usuarioInfo.ApellidoMaterno;
                    sqlComando.Parameters.Add("Login", SqlDbType.VarChar).Value = usuarioInfo.Login;
                    sqlComando.Parameters.Add("Password", SqlDbType.VarChar).Value = usuarioInfo.Password;
                    sqlComando.Parameters.Add("SexoId", SqlDbType.Int).Value = usuarioInfo.SexoId;
                    sqlComando.Parameters.Add("EstadoCivilId", SqlDbType.Int).Value = usuarioInfo.EstadoCivilId;
                    sqlComando.Parameters.Add("Correo", SqlDbType.VarChar).Value = usuarioInfo.Correo;
                    sqlComando.Parameters.Add("Telefono", SqlDbType.VarChar).Value = usuarioInfo.Telefono;
                    sqlComando.Parameters.Add("Celular", SqlDbType.VarChar).Value = usuarioInfo.Celular;
                    sqlComando.Parameters.Add("CorreoTrabajo", SqlDbType.VarChar).Value = usuarioInfo.CorreoTrabajo;
                    sqlComando.Parameters.Add("TelefonoTrabajo", SqlDbType.VarChar).Value = usuarioInfo.TelefonoTrabajo;
                    sqlComando.Parameters.Add("TipoDocumentoId", SqlDbType.Int).Value = usuarioInfo.TipoDocumentoId;
                    sqlComando.Parameters.Add("NumeroDocumento", SqlDbType.VarChar).Value = usuarioInfo.NumeroDocumento;
                    sqlComando.Parameters.Add("Activo", SqlDbType.VarChar).Value = usuarioInfo.Activo;
                    sqlComando.Parameters.Add("Intento", SqlDbType.VarChar).Value = usuarioInfo.Intento;
                    sqlComando.Parameters.Add("PerfilId", SqlDbType.Int).Value = usuarioInfo.PerfilId;
                    sqlComando.Parameters.Add("EmpresaId", SqlDbType.Int).Value = usuarioInfo.EmpresaId;
                    sqlComando.Parameters.Add("AgenciaId", SqlDbType.Int).Value = usuarioInfo.AgenciaId;
                    sqlComando.Parameters.Add("DepartamentoId", SqlDbType.Int).Value = usuarioInfo.DepartamentoId;
                    sqlComando.Parameters.Add("CargoId", SqlDbType.Int).Value = usuarioInfo.CargoId;
                    sqlComando.Parameters.Add("Foto", SqlDbType.VarChar).Value = usuarioInfo.Foto;
                    sqlComando.Parameters.Add("TipoNegocioId", SqlDbType.Int).Value = usuarioInfo.TipoNegocioId;
                    sqlComando.Parameters.Add("UsuarioCreacionId", SqlDbType.Int).Value = usuarioInfo.UsuarioCreacionId;

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
        public int Actualizar(UsuarioInfo usuarioInfo)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "ActualizarUsuario", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("UsuarioId", SqlDbType.Int).Value = usuarioInfo.UsuarioId;
                    sqlComando.Parameters.Add("Codigo", SqlDbType.VarChar).Value = usuarioInfo.Codigo;
                    sqlComando.Parameters.Add("Nombres", SqlDbType.VarChar).Value = usuarioInfo.Nombres;
                    sqlComando.Parameters.Add("ApellidoPaterno", SqlDbType.VarChar).Value = usuarioInfo.ApellidoPaterno;
                    sqlComando.Parameters.Add("ApellidoMaterno", SqlDbType.VarChar).Value = usuarioInfo.ApellidoMaterno;
                    sqlComando.Parameters.Add("Login", SqlDbType.VarChar).Value = usuarioInfo.Login;
                    sqlComando.Parameters.Add("Password", SqlDbType.VarChar).Value = usuarioInfo.Password;
                    sqlComando.Parameters.Add("SexoId", SqlDbType.Int).Value = usuarioInfo.SexoId;
                    sqlComando.Parameters.Add("EstadoCivilId", SqlDbType.Int).Value = usuarioInfo.EstadoCivilId;
                    sqlComando.Parameters.Add("Correo", SqlDbType.VarChar).Value = usuarioInfo.Correo;
                    sqlComando.Parameters.Add("Telefono", SqlDbType.VarChar).Value = usuarioInfo.Telefono;
                    sqlComando.Parameters.Add("Celular", SqlDbType.VarChar).Value = usuarioInfo.Celular;
                    sqlComando.Parameters.Add("TelefonoTrabajo", SqlDbType.VarChar).Value = usuarioInfo.TelefonoTrabajo;
                    sqlComando.Parameters.Add("CorreoTrabajo", SqlDbType.VarChar).Value = usuarioInfo.CorreoTrabajo;
                    sqlComando.Parameters.Add("TipoDocumentoId", SqlDbType.Int).Value = usuarioInfo.TipoDocumentoId;
                    sqlComando.Parameters.Add("NumeroDocumento", SqlDbType.VarChar).Value = usuarioInfo.NumeroDocumento;
                    sqlComando.Parameters.Add("Activo", SqlDbType.VarChar).Value = usuarioInfo.Activo;
                    sqlComando.Parameters.Add("Intento", SqlDbType.VarChar).Value = usuarioInfo.Intento;
                    sqlComando.Parameters.Add("PerfilId", SqlDbType.Int).Value = usuarioInfo.PerfilId;
                    sqlComando.Parameters.Add("EmpresaId", SqlDbType.Int).Value = usuarioInfo.EmpresaId;
                    sqlComando.Parameters.Add("AgenciaId", SqlDbType.Int).Value = usuarioInfo.AgenciaId;
                    sqlComando.Parameters.Add("DepartamentoId", SqlDbType.Int).Value = usuarioInfo.DepartamentoId;
                    sqlComando.Parameters.Add("CargoId", SqlDbType.Int).Value = usuarioInfo.CargoId;
                    sqlComando.Parameters.Add("Foto", SqlDbType.VarChar).Value = usuarioInfo.Foto;
                    sqlComando.Parameters.Add("TipoNegocioId", SqlDbType.Int).Value = usuarioInfo.TipoNegocioId;
                    sqlComando.Parameters.Add("UsuarioModificacionId", SqlDbType.Int).Value = usuarioInfo.UsuarioModificacionId;

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
        public int Eliminar(int usuarioId)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "EliminarUsuario", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("UsuarioId", SqlDbType.Int).Value = usuarioId;

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