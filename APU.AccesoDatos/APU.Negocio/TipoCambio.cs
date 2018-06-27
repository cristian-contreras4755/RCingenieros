using System;
using System.Collections.Generic;
using APU.AccesoDatos;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.Negocio
{
    public class TipoCambio
    {
        private readonly TipoCambioAccesoDatos _tipoCambioAccesoDatos;
        public TipoCambio()
        {
            _tipoCambioAccesoDatos = new TipoCambioAccesoDatos();
        }
        public List<TipoCambioInfo> Listar(int tipoCambioId)
        {
            return _tipoCambioAccesoDatos.Listar(tipoCambioId);
        }
        public List<TipoCambioInfo> ListarPaginado(int tipoCambioId, int tipoCotizacionId, string fecha, int tamanioPagina, int numeroPagina)
        {
            return _tipoCambioAccesoDatos.ListarPaginado(tipoCambioId, tipoCotizacionId, fecha, tamanioPagina, numeroPagina);
        }
        public int Insertar(TipoCambioInfo tipoCambioInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _tipoCambioAccesoDatos.Insertar(tipoCambioInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Actualizar(TipoCambioInfo tipoCambioInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _tipoCambioAccesoDatos.Actualizar(tipoCambioInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Eliminar(int tipoCambioId)
        {
            int resultado = 0;
            try
            {
                resultado = _tipoCambioAccesoDatos.Eliminar(tipoCambioId);
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