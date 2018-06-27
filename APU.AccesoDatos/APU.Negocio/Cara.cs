using System;
using System.Collections.Generic;
using APU.AccesoDatos;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.Negocio
{
    public class Cara
    {
        private readonly CaraAccesoDatos _caraAccesoDatos;
        public Cara()
        {
            _caraAccesoDatos = new CaraAccesoDatos();
        }
        public List<CaraInfo> Listar(int caraId)
        {
            return _caraAccesoDatos.Listar(caraId);
        }
        public List<CaraInfo> ListarPaginado(int caraId, int tamanioPagina, int numeroPagina)
        {
            return _caraAccesoDatos.ListarPaginado(caraId, tamanioPagina, numeroPagina);
        }
        public int Insertar(CaraInfo caraInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _caraAccesoDatos.Insertar(caraInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Actualizar(CaraInfo caraInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _caraAccesoDatos.Actualizar(caraInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Eliminar(int caraId)
        {
            int resultado = 0;
            try
            {
                resultado = _caraAccesoDatos.Eliminar(caraId);
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