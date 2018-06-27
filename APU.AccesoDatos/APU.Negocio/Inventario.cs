using System;
using System.Collections.Generic;
using APU.AccesoDatos;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.Negocio
{
	public class Inventario
	{
		private readonly InventarioAccesoDatos _inventarioAccesoDatos;
		public Inventario()
		{
			_inventarioAccesoDatos = new InventarioAccesoDatos();
		}
		public List<InventarioInfo> Listar(int almacenId, int productoId, int tipoNegocioId)
		{
			return _inventarioAccesoDatos.Listar(almacenId, productoId, tipoNegocioId);
		}

		public List<InventarioInfo> ListarPaginado(int inventarioId, int almacenId, int tipoNegocioId, int tamanioPagina, int numeroPagina)
		{
			return _inventarioAccesoDatos.ListarPaginado(inventarioId, almacenId, tipoNegocioId, tamanioPagina, numeroPagina);
		}

		public int Actualizar(InventarioInfo inventarioInfo)
		{
			int resultado = 0;
			try
			{
				resultado = _inventarioAccesoDatos.Actualizar(inventarioInfo);
			}
			catch (Exception ex)
			{
				bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
				if (rethrow)
					throw;
			}
			return resultado;
		}
		
		public int Insertar(InventarioInfo inventarioInfo)
		{
			int resultado = 0;
			try
			{
				resultado = _inventarioAccesoDatos.Insertar(inventarioInfo);
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
