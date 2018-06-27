using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.AccesoDatos
{
	public class ProveedorAccesoDatos
	{

		public List<ProveedorInfo> Listar(int clienteId)
		{
			var clienteListaInfo = new List<ProveedorInfo>();
			using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
			{
				var sqlComando = new SqlCommand();
				sqlComando.Connection = oConexion;
				sqlComando.CommandText = "ObtenerProveedor";
				sqlComando.CommandType = CommandType.StoredProcedure;
				sqlComando.Parameters.Add("ProveedorId", SqlDbType.Int).Value = clienteId;

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
		public List<ProveedorInfo> ListarPaginado(int proveedorId, string ruc, string proveedor, int tamanioPagina, int numeroPagina)
		{
			var clienteListaInfo = new List<ProveedorInfo>();
			using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
			{
				var sqlComando = new SqlCommand();
				sqlComando.Connection = oConexion;
				sqlComando.CommandText = "ObtenerProveedorPaginado";
				sqlComando.CommandType = CommandType.StoredProcedure;
				sqlComando.Parameters.Add("ProveedorId", SqlDbType.Int).Value = proveedorId;
				sqlComando.Parameters.Add("Ruc", SqlDbType.VarChar).Value = ruc;
				sqlComando.Parameters.Add("Proveedor", SqlDbType.VarChar).Value = proveedor;
				sqlComando.Parameters.Add("TamanioPagina", SqlDbType.Int).Value = tamanioPagina;
				sqlComando.Parameters.Add("NumeroPagina", SqlDbType.Int).Value = numeroPagina;

				oConexion.Open();

				using (SqlDataReader dr = sqlComando.ExecuteReader())
				{
					while (dr.Read())
					{
						clienteListaInfo.Add(CargarProveedorInfoPag(dr));
					}
				}
				oConexion.Close();
			}
			return clienteListaInfo;
		}
		private static ProveedorInfo CargarProveedorInfoPag(IDataReader dr)
		{
			int colCount = dr.FieldCount;
			var values = new object[colCount];

			#region Indices
			int indProveedorId = dr.GetOrdinal("ProveedorId");
			//int indEmpresaId = dr.GetOrdinal("EmpresaId");
			//int indEmpresa = dr.GetOrdinal("Empresa");
			int indNombre = dr.GetOrdinal("Nombre");
			int indDescripcion = dr.GetOrdinal("Descripcion");			
			//int indRazonSocial = dr.GetOrdinal("RazonSocial");
			
			int indTipoDocumentoId = dr.GetOrdinal("TipoDocumentoId");
			int indTipoDocumento = dr.GetOrdinal("TipoDocumento");
			int indNumeroDocumento = dr.GetOrdinal("NumeroDocumento");
			int indPais = dr.GetOrdinal("PaisId");
			int indDepartamentoId = dr.GetOrdinal("DepartamentoId");
			int indProvinciaId = dr.GetOrdinal("ProvinciaId");
			int indDistritoId = dr.GetOrdinal("DistritoId");
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
			//int indUsuarioCreacionId = dr.GetOrdinal("UsuarioCreacionId");
			//int indUsuarioCreacion = dr.GetOrdinal("UsuarioCreacion");
			//int indFechaCreacion = dr.GetOrdinal("FechaCreacion");
			//int indUsuarioModificacionId = dr.GetOrdinal("UsuarioModificacionId");
			//int indUsuarioModificacion = dr.GetOrdinal("UsuarioModificacion");
			//int indFechaModificacion = dr.GetOrdinal("FechaModificacion");
			int indNumeroFila = dr.GetOrdinal("NumeroFila");
			int indTotalFilas = dr.GetOrdinal("TotalFilas");
			#endregion

			var clienteInfo = new ProveedorInfo();
			dr.GetValues(values);

			#region Campos
			clienteInfo.ProveedorId = Convert.ToInt32(values[indProveedorId]);
			//clienteInfo.EmpresaId = Convert.ToInt32(values[indEmpresaId]);
			//clienteInfo.Empresa = Convert.ToString(values[indEmpresa]);
			
			clienteInfo.Nombre = Convert.ToString(values[indNombre]);
			
			//clienteInfo.RazonSocial = Convert.ToString(values[indRazonSocial]);
			clienteInfo.Descripcion = Convert.ToString(values[indDescripcion]);
			clienteInfo.TipoDocumentoId = Convert.ToInt32(values[indTipoDocumentoId]);
			clienteInfo.TipoDocumento = Convert.ToString(values[indTipoDocumento]);
			clienteInfo.NumeroDocumento = Convert.ToString(values[indNumeroDocumento]);
			clienteInfo.PaisId = Convert.ToInt32(values[indPais]);
			clienteInfo.DepartamentoId = Convert.ToInt32(values[indDepartamentoId]);
			clienteInfo.ProvinciaId = Convert.ToInt32(values[indProvinciaId]);
			clienteInfo.DistritoId = Convert.ToInt32(values[indDistritoId]);
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
		
			clienteInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
			clienteInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);
			#endregion

			return clienteInfo;
		}
		private static ProveedorInfo CargarClienteInfo(IDataReader dr)
		{
			int colCount = dr.FieldCount;
			var values = new object[colCount];

			#region Indices
			int indProveedorId = dr.GetOrdinal("ProveedorId");
			//int indEmpresaId = dr.GetOrdinal("EmpresaId");
			//int indEmpresa = dr.GetOrdinal("Empresa");
			int indNombre = dr.GetOrdinal("Nombre");
			int indDescripcion = dr.GetOrdinal("Descripcion");
			//int indRazonSocial = dr.GetOrdinal("RazonSocial");

			int indTipoDocumentoId = dr.GetOrdinal("TipoDocumentoId");
			int indTipoDocumento = dr.GetOrdinal("TipoDocumento");
			int indNumeroDocumento = dr.GetOrdinal("NumeroDocumento");
			int indPais = dr.GetOrdinal("PaisId");
			int indDepartamentoId = dr.GetOrdinal("DepartamentoId");
			int indProvinciaId = dr.GetOrdinal("ProvinciaId");
			int indDistritoId = dr.GetOrdinal("DistritoId");
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
			//int indUsuarioCreacionId = dr.GetOrdinal("UsuarioCreacionId");
			//int indUsuarioCreacion = dr.GetOrdinal("UsuarioCreacion");
			//int indFechaCreacion = dr.GetOrdinal("FechaCreacion");
			//int indUsuarioModificacionId = dr.GetOrdinal("UsuarioModificacionId");
			//int indUsuarioModificacion = dr.GetOrdinal("UsuarioModificacion");
			//int indFechaModificacion = dr.GetOrdinal("FechaModificacion");
			//int indNumeroFila = dr.GetOrdinal("NumeroFila");
			//int indTotalFilas = dr.GetOrdinal("TotalFilas");
			#endregion

			var clienteInfo = new ProveedorInfo();
			dr.GetValues(values);

			#region Campos
			clienteInfo.ProveedorId = Convert.ToInt32(values[indProveedorId]);
			//clienteInfo.EmpresaId = Convert.ToInt32(values[indEmpresaId]);
			//clienteInfo.Empresa = Convert.ToString(values[indEmpresa]);

			clienteInfo.Nombre = Convert.ToString(values[indNombre]);

			//clienteInfo.RazonSocial = Convert.ToString(values[indRazonSocial]);
			clienteInfo.Descripcion = Convert.ToString(values[indDescripcion]);
			clienteInfo.TipoDocumentoId = Convert.ToInt32(values[indTipoDocumentoId]);
			clienteInfo.TipoDocumento = Convert.ToString(values[indTipoDocumento]);
			clienteInfo.NumeroDocumento = Convert.ToString(values[indNumeroDocumento]);
			clienteInfo.PaisId = Convert.ToInt32(values[indPais]);
			clienteInfo.DepartamentoId = Convert.ToInt32(values[indDepartamentoId]);
			clienteInfo.ProvinciaId = Convert.ToInt32(values[indProvinciaId]);
			clienteInfo.DistritoId = Convert.ToInt32(values[indDistritoId]);
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

			//clienteInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
			//clienteInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);
			#endregion

			return clienteInfo;
		}
		public int Insertar(ProveedorInfo clienteInfo)
		{
			int resultado;
			try
			{
				using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
				{
					var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarProveedor", CommandType = CommandType.StoredProcedure };

					//sqlComando.Parameters.Add("Codigo", SqlDbType.VarChar).Value = clienteInfo.Codigo;
					//sqlComando.Parameters.Add("EmpresaId", SqlDbType.Int).Value = clienteInfo.EmpresaId;
					sqlComando.Parameters.Add("Nombre", SqlDbType.VarChar).Value = clienteInfo.Nombre;
					//sqlComando.Parameters.Add("ApellidoPaterno", SqlDbType.VarChar).Value = clienteInfo.ApellidoPaterno;
					//sqlComando.Parameters.Add("ApellidoMaterno", SqlDbType.VarChar).Value = clienteInfo.ApellidoMaterno;
					//sqlComando.Parameters.Add("RazonSocial", SqlDbType.VarChar).Value = clienteInfo.RazonSocial;
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
		public int Actualizar(ProveedorInfo clienteInfo)
		{
			int resultado = 0;
			try
			{
				using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
				{
					var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "ActualizarProveedor", CommandType = CommandType.StoredProcedure };

					sqlComando.Parameters.Add("ProveedorId", SqlDbType.Int).Value = clienteInfo.ProveedorId;
					//sqlComando.Parameters.Add("Codigo", SqlDbType.VarChar).Value = clienteInfo.Codigo;
					//sqlComando.Parameters.Add("EmpresaId", SqlDbType.Int).Value = clienteInfo.EmpresaId;
					sqlComando.Parameters.Add("Nombre", SqlDbType.VarChar).Value = clienteInfo.Nombre;
					//sqlComando.Parameters.Add("ApellidoPaterno", SqlDbType.VarChar).Value = clienteInfo.ApellidoPaterno;
					//sqlComando.Parameters.Add("ApellidoMaterno", SqlDbType.VarChar).Value = clienteInfo.ApellidoMaterno;
					//sqlComando.Parameters.Add("RazonSocial", SqlDbType.VarChar).Value = clienteInfo.RazonSocial;
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
