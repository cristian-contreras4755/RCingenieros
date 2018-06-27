using System;
using System.Collections.Generic;
using APU.AccesoDatos;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.Negocio
{
	public class TipoProducto
	{
		private readonly TipoProductoAccesoDatos _tipoProductoAccesoDatos;
		public TipoProducto()
		{
			_tipoProductoAccesoDatos = new TipoProductoAccesoDatos();
		}
		
		public List<TipoProductoInfo> ListarPaginado(int tipoProductoId, int tamanioPagina, int numeroPagina)
		{
			return _tipoProductoAccesoDatos.ListarPaginado(tipoProductoId, tamanioPagina, numeroPagina);
		}
		public List<TipoProductoInfo> ListarPaginadoSubProducto(int tipoProductoId, int tamanioPagina, int numeroPagina)
		{
			return _tipoProductoAccesoDatos.ListarPaginadoSubProducto(tipoProductoId, tamanioPagina, numeroPagina);
		}

		public List<TipoProductoInfo> Listar(int tipoProductoId, int tipoProductoPadreId, int nivel)
		{
			return _tipoProductoAccesoDatos.Listar(tipoProductoId, tipoProductoPadreId, nivel);
		}

		public int Insertar(TipoProductoInfo clienteInfo)
		{
			int resultado = 0;
			try
			{
				resultado = _tipoProductoAccesoDatos.Insertar(clienteInfo);
			}
			catch (Exception ex)
			{
				bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
				if (rethrow)
					throw;
			}
			return resultado;
		}

		public int Actualizar(TipoProductoInfo clienteInfo)
		{
			int resultado = 0;
			try
			{
				resultado = _tipoProductoAccesoDatos.Actualizar(clienteInfo);
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
