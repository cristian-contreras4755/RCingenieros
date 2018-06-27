using System;
using System.Collections.Generic;
using APU.AccesoDatos;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.Negocio
{
	public class Compra
	{
		private readonly CompraAccesoDatos _ventaAccesoDatos;
		public Compra()
		{
			_ventaAccesoDatos = new CompraAccesoDatos();
		}
		
		public List<CompraInfo> ListarPaginado(int ventaId, int proveedorId, int tipoNegocioId, int tamanioPagina, int numeroPagina)
		{
			return _ventaAccesoDatos.ListarPaginado(ventaId, proveedorId, tipoNegocioId, tamanioPagina, numeroPagina);
		}

		public List<ComprasDetalleInfo> ListarComprasDetalle(int compraDetalleId, int compraId)
		{
			return _ventaAccesoDatos.ListarComprasDetalle(compraDetalleId, compraId);
		}

		public List<ComprasDetalleInfo> ListarComprasDetalleAlmacen(string fechaInicio, string fechaFin, int tamanioPagina, int numeroPagina)
		{
			return _ventaAccesoDatos.ListarComprasDetalleAlmacen(fechaInicio, fechaFin, tamanioPagina, numeroPagina);
		}
		public int InsertarCompraDetalle(ComprasDetalleInfo ventaInfo)
		{
			int resultado = 0;
			try
			{
				resultado = _ventaAccesoDatos.InsertarCompraDetalle(ventaInfo);
			}
			catch (Exception ex)
			{
				bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
				if (rethrow)
					throw;
			}
			return resultado;
		}
		public int ActualizarCompraDetalle(ComprasDetalleInfo ventaInfo)
		{
			int resultado = 0;
			try
			{
				resultado = _ventaAccesoDatos.ActualizarCompraDetalle(ventaInfo);
			}
			catch (Exception ex)
			{
				bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
				if (rethrow)
					throw;
			}
			return resultado;
		}
		public int EliminarCompraDetalle(int compraDetalleId)
		{
			int resultado = 0;
			try
			{
				resultado = _ventaAccesoDatos.EliminarCompraDetalle(compraDetalleId);
			}
			catch (Exception ex)
			{
				bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
				if (rethrow)
					throw;
			}
			return resultado;
		}

		public int InsertarCompra(CompraInfo ventaInfo)
		{
			int resultado = 0;
			try
			{
				resultado = _ventaAccesoDatos.InsertarCompra(ventaInfo);
			}
			catch (Exception ex)
			{
				bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
				if (rethrow)
					throw;
			}
			return resultado;
		}
		public int ActualizarCompra(CompraInfo ventaInfo)
		{
			int resultado = 0;
			try
			{
				resultado = _ventaAccesoDatos.ActualizarCompra(ventaInfo);
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
