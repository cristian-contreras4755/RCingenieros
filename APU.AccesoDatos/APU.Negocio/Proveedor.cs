using System;
using System.Collections.Generic;
using APU.AccesoDatos;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.Negocio
{
	public class Proveedor
	{
		private readonly ProveedorAccesoDatos _proveedorAccesoDatos;
		public Proveedor()
		{
			_proveedorAccesoDatos = new ProveedorAccesoDatos();
		}
		public List<ProveedorInfo> Listar(int clienteId)
		{
			return _proveedorAccesoDatos.Listar(clienteId);
		}
		public List<ProveedorInfo> ListarPaginado(int proveedorId, string ruc, string proveedor, int tamanioPagina, int numeroPagina)
		{
			return _proveedorAccesoDatos.ListarPaginado(proveedorId, ruc, proveedor, tamanioPagina, numeroPagina);
		}
		public int Insertar(ProveedorInfo clienteInfo)
		{
			int resultado = 0;
			try
			{
				resultado = _proveedorAccesoDatos.Insertar(clienteInfo);
			}
			catch (Exception ex)
			{
				bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
				if (rethrow)
					throw;
			}
			return resultado;
		}
		public int Actualizar(ProveedorInfo clienteInfo)
		{
			int resultado = 0;
			try
			{
				resultado = _proveedorAccesoDatos.Actualizar(clienteInfo);
			}
			catch (Exception ex)
			{
				bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
				if (rethrow)
					throw;
			}
			return resultado;
		}
		public int Eliminar(int clienteId)
		{
			int resultado = 0;
			try
			{
				resultado = _proveedorAccesoDatos.Eliminar(clienteId);
			}
			catch (Exception ex)
			{
				bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
				if (rethrow)
					throw;
			}
			return resultado;
		}
	}
}
