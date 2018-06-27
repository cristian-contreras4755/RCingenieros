using System;
using System.Collections.Generic;
using APU.AccesoDatos;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.Negocio
{
	public class Movimientos
	{
		private readonly MovimientosAccesoDatos _movimientoAccesoDatos;
		public Movimientos()
		{
			_movimientoAccesoDatos = new MovimientosAccesoDatos();
		}
		public int InsertarMovimientos(MovimientosInfo movimientosInfo)
		{
			int resultado = 0;
			try
			{
				resultado = _movimientoAccesoDatos.InsertarMovimientos(movimientosInfo);
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
