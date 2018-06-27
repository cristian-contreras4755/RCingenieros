using System;
using System.Collections.Generic;
using APU.AccesoDatos;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.Negocio
{
    public class Isla
    {
        private readonly IslaAccesoDatos _islaAccesoDatos;
        public Isla()
        {
            _islaAccesoDatos = new IslaAccesoDatos();
        }
        public List<IslaInfo> Listar(int islaId)
        {
            return _islaAccesoDatos.Listar(islaId);
        }
        public List<IslaInfo> ListarPaginado(int islaId, int tamanioPagina, int numeroPagina)
        {
            return _islaAccesoDatos.ListarPaginado(islaId, tamanioPagina, numeroPagina);
        }
        public int Insertar(IslaInfo islaInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _islaAccesoDatos.Insertar(islaInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Actualizar(IslaInfo islaInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _islaAccesoDatos.Actualizar(islaInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Eliminar(int islaId)
        {
            int resultado = 0;
            try
            {
                resultado = _islaAccesoDatos.Eliminar(islaId);
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