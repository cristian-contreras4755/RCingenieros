using System;
using System.Collections.Generic;
using APU.AccesoDatos;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.Negocio
{
    public class VentaSunat
    {
        private readonly VentaSunatAccesoDatos _ventaSunatAccesoDatos;
        public VentaSunat()
        {
            _ventaSunatAccesoDatos = new VentaSunatAccesoDatos();
        }
        public List<VentaSunatInfo> Listar(int ventaSunatId, int ventaId)
        {
            return _ventaSunatAccesoDatos.Listar(ventaSunatId, ventaId);
        }
        public List<VentaSunatInfo> ListarPaginado(int surtidorId, int tamanioPagina, int numeroPagina)
        {
            return _ventaSunatAccesoDatos.ListarPaginado(surtidorId, tamanioPagina, numeroPagina);
        }
        public int Insertar(VentaSunatInfo ventaSunatInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _ventaSunatAccesoDatos.Insertar(ventaSunatInfo);
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
                resultado = _ventaSunatAccesoDatos.Actualizar(surtidorInfo);
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
                resultado = _ventaSunatAccesoDatos.Eliminar(surtidorId);
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