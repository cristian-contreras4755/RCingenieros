using System;
using System.Collections.Generic;
using APU.AccesoDatos;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.Negocio
{
    public class ParametrosGlobales
    {
        private readonly ParametrosGlobalesAccesoDatos _parametrosGlobalesAccesoDatos;
        public ParametrosGlobales()
        {
            _parametrosGlobalesAccesoDatos = new ParametrosGlobalesAccesoDatos();
        }
        public List<ParametrosGlobalesInfo> Listar(int codigoParametro)
        {
            return _parametrosGlobalesAccesoDatos.Listar(codigoParametro);
        }
        public List<ParametrosGlobalesInfo> ListarParametrosGlobalesPaginado(int codigoParametro, int tamanioPagina, int numeroPagina)
        {
            return _parametrosGlobalesAccesoDatos.ListarParametrosGlobalesPaginado(codigoParametro, tamanioPagina, numeroPagina);
        }

        public int Insertar(ParametrosGlobalesInfo oParametrosGlobalesInfo)
        {

            int resultado = 0;
            try
            {
                return _parametrosGlobalesAccesoDatos.Insertar(oParametrosGlobalesInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }

        public int Actualizar(ParametrosGlobalesInfo oParametrosGlobalesInfo)
        {

            int resultado = 0;
            try
            {
                return _parametrosGlobalesAccesoDatos.Actualizar(oParametrosGlobalesInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }

        public int Eliminar(int codigoParametrosGlobales)
        {

            int resultado = 0;
            try
            {
                return _parametrosGlobalesAccesoDatos.Eliminar(codigoParametrosGlobales);
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