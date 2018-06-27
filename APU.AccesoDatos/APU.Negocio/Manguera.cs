using System;
using System.Collections.Generic;
using APU.AccesoDatos;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.Negocio
{
    public class Manguera
    {
        private readonly MangueraAccesoDatos _mangueraAccesoDatos;
        public Manguera()
        {
            _mangueraAccesoDatos = new MangueraAccesoDatos();
        }
        public List<MangueraInfo> Listar(int mangueraId)
        {
            return _mangueraAccesoDatos.Listar(mangueraId);
        }
        public List<MangueraInfo> ListarPaginado(int mangueraId, int tamanioPagina, int numeroPagina)
        {
            return _mangueraAccesoDatos.ListarPaginado(mangueraId, tamanioPagina, numeroPagina);
        }
        public int Insertar(MangueraInfo mangueraInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _mangueraAccesoDatos.Insertar(mangueraInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Actualizar(MangueraInfo mangueraInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _mangueraAccesoDatos.Actualizar(mangueraInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Eliminar(int mangueraId)
        {
            int resultado = 0;
            try
            {
                resultado = _mangueraAccesoDatos.Eliminar(mangueraId);
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