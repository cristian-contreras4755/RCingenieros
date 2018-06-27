using System;
using System.Collections.Generic;
using APU.AccesoDatos;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.Negocio
{
	public class Producto
	{
		private readonly ProductoAccesoDatos _productoAccesoDatos;
		public Producto()
		{
		    _productoAccesoDatos = new ProductoAccesoDatos();
		}

		public List<ProductoInfo> ListarPaginado(int tipoProductoId, string codigo, string producto, int tamanioPagina, int numeroPagina)
		{
			return _productoAccesoDatos.ListarPaginado(tipoProductoId,  codigo,  producto, tamanioPagina, numeroPagina);
		}

		public int Insertar(ProductoInfo clienteInfo)
		{
			int resultado = 0;
			try
			{
				resultado = _productoAccesoDatos.Insertar(clienteInfo);
			}
			catch (Exception ex)
			{
				bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
				if (rethrow)
					throw;
			}
			return resultado;
		}

		public int Actualizar(ProductoInfo clienteInfo)
		{
			int resultado = 0;
			try
			{
				resultado = _productoAccesoDatos.Actualizar(clienteInfo);
			}
			catch (Exception ex)
			{
				bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
				if (rethrow)
					throw;
			}
			return resultado;
		}
	    public List<ProductoTipoTiendaInfo> ListarProductoTipoTienda(int productoId, int tipoTiendaId)
        {
	        return _productoAccesoDatos.ListarProductoTipoTienda(productoId, tipoTiendaId);
	    }
	    public int InsertarProductoTipoTienda(ProductoTipoTiendaInfo productoTipoTiendaInfo)
        {
	        int resultado = 0;
	        try
	        {
	            resultado = _productoAccesoDatos.InsertarProductoTipoTienda(productoTipoTiendaInfo);
	        }
	        catch (Exception ex)
	        {
	            bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
	            if (rethrow)
	                throw;
	        }
	        return resultado;
	    }
        public int EliminarProductoTipoTienda(int productoId, int tipoTiendaId)
        {
	        return _productoAccesoDatos.EliminarProductoTipoTienda(productoId, tipoTiendaId);
	    }
    }
}