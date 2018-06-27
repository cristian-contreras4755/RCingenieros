using System;
using System.Collections.Generic;
using APU.AccesoDatos;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.Negocio
{
    public class Correlativo
    {
        private readonly CorrelativoAccesoDatos _correlativoAccesoDatos;
        public Correlativo()
        {
            _correlativoAccesoDatos = new CorrelativoAccesoDatos();
        }
        public List<CorrelativoInfo> Listar(string tipoComprobanteId, int serieId, int correlativoId)
        {
            return _correlativoAccesoDatos.Listar(tipoComprobanteId, serieId, correlativoId);
        }
        public List<CorrelativoInfo> ListarPaginado(string tipoComprobanteId, int serieId, int correlativoId, int tamanioPagina, int numeroPagina)
        {
            return _correlativoAccesoDatos.ListarPaginado(tipoComprobanteId, serieId, correlativoId, tamanioPagina, numeroPagina);
        }
        public int Insertar(CorrelativoInfo correlativoInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _correlativoAccesoDatos.Insertar(correlativoInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Actualizar(CorrelativoInfo correlativoInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _correlativoAccesoDatos.Actualizar(correlativoInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Eliminar(int correlativoId)
        {
            int resultado = 0;
            try
            {
                resultado = _correlativoAccesoDatos.Eliminar(correlativoId);
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