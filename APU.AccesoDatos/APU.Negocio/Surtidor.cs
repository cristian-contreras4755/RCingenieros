using System;
using System.Collections.Generic;
using APU.AccesoDatos;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.Negocio
{
    public class Surtidor
    {
        private readonly SurtidorAccesoDatos _surtidorAccesoDatos;
        public Surtidor()
        {
            _surtidorAccesoDatos = new SurtidorAccesoDatos();
        }
        public List<SurtidorInfo> Listar(int surtidorId)
        {
            return _surtidorAccesoDatos.Listar(surtidorId);
        }
        public List<SurtidorInfo> ListarPaginado(int surtidorId, int tamanioPagina, int numeroPagina)
        {
            return _surtidorAccesoDatos.ListarPaginado(surtidorId, tamanioPagina, numeroPagina);
        }
        public int Insertar(SurtidorInfo surtidorInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _surtidorAccesoDatos.Insertar(surtidorInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Actualizar(SurtidorInfo surtidorInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _surtidorAccesoDatos.Actualizar(surtidorInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Eliminar(int surtidorId)
        {
            int resultado = 0;
            try
            {
                resultado = _surtidorAccesoDatos.Eliminar(surtidorId);
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