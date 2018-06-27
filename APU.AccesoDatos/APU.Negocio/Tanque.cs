using System;
using System.Collections.Generic;
using APU.AccesoDatos;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.Negocio
{
    public class Tanque
    {
        private readonly TanqueAccesoDatos _tanqueAccesoDatos;
        public Tanque()
        {
            _tanqueAccesoDatos = new TanqueAccesoDatos();
        }
        public List<TanqueInfo> Listar(int tanqueId)
        {
            return _tanqueAccesoDatos.Listar(tanqueId);
        }
        public List<TanqueInfo> ListarPaginado(int tanqueId, int tamanioPagina, int numeroPagina)
        {
            return _tanqueAccesoDatos.ListarPaginado(tanqueId, tamanioPagina, numeroPagina);
        }
        public int Insertar(TanqueInfo tanqueInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _tanqueAccesoDatos.Insertar(tanqueInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Actualizar(TanqueInfo tanqueInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _tanqueAccesoDatos.Actualizar(tanqueInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Eliminar(int tanqueId)
        {
            int resultado = 0;
            try
            {
                resultado = _tanqueAccesoDatos.Eliminar(tanqueId);
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