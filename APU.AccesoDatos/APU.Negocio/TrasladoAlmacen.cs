using System;
using System.Collections.Generic;
using APU.AccesoDatos;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.Negocio
{
	public class TrasladoAlmacen
	{
		private readonly TrasladoAlmacenAccesoDatos _trasladoAlmacenAccesoDatos;
		public TrasladoAlmacen()
		{
			_trasladoAlmacenAccesoDatos = new TrasladoAlmacenAccesoDatos();
		}

		public int Insertar(TrasladoAlmacenInfo trasladoAlmacenInfo)
		{
			int resultado = 0;
			try
			{
				resultado = _trasladoAlmacenAccesoDatos.Insertar(trasladoAlmacenInfo);
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
