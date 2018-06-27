using System;
using System.Collections.Generic;
using APU.AccesoDatos;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.Negocio
{
    public class VentaPetroamerica
    {
        private readonly VentaPetroamericaAccesoDatos _ventaAccesoDatos;
        public VentaPetroamerica()
        {
            _ventaAccesoDatos = new VentaPetroamericaAccesoDatos();
        }
        #region Venta
        public List<VentaPetroamericaInfo> Listar(int ventaPetroamericaId, int ventaId)
        {
            return _ventaAccesoDatos.Listar(ventaPetroamericaId, ventaId);
        }
        public List<VentaPetroamericaInfo> ListarPaginado(int ventaId, string numeroDocumento, string tipoComprobanteId, string serie, string correlativo, DateTime fechaInicio, DateTime fechaFin, int estadoId, int monedaId, string agencia, int tamanioPagina, int numeroPagina)
        {
            return _ventaAccesoDatos.ListarPaginado(ventaId, numeroDocumento, tipoComprobanteId, serie, correlativo, fechaInicio, fechaFin, estadoId, monedaId, agencia, tamanioPagina, numeroPagina);
        }
        public int Insertar(VentaPetroamericaInfo ventaInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _ventaAccesoDatos.Insertar(ventaInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int InsertarLote(List<VentaPetroamericaInfo> ventaListaInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _ventaAccesoDatos.InsertarLote(ventaListaInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Actualizar(VentaInfo ventaInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _ventaAccesoDatos.Actualizar(ventaInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Eliminar(int ventaId)
        {
            int resultado = 0;
            try
            {
                resultado = _ventaAccesoDatos.Eliminar(ventaId);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        #endregion

        #region Venta Detalle
        public List<VentaDetalleInfo> ListarDetalle(int ventaId, int ventaDetalleId)
        {
            return _ventaAccesoDatos.ListarDetalle(ventaId, ventaDetalleId);
        }
        public List<VentaDetalleInfo> ListarDetallePaginado(int ventaId, int ventaDetalleId, int tamanioPagina, int numeroPagina)
        {
            return _ventaAccesoDatos.ListarDetallePaginado(ventaId, ventaDetalleId, tamanioPagina, numeroPagina);
        }
        public int InsertarDetalle(VentaDetalleInfo ventaDetalleInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _ventaAccesoDatos.InsertarDetalle(ventaDetalleInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int ActualizarDetalle(VentaDetalleInfo ventaDetalleInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _ventaAccesoDatos.ActualizarDetalle(ventaDetalleInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int EliminarDetalle(int ventaDetalleId)
        {
            int resultado = 0;
            try
            {
                resultado = _ventaAccesoDatos.EliminarDetalle(ventaDetalleId);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        #endregion

        public List<AgenciaPetroamericaInfo> ListarAgencia(string agenciaPetroamerica)
        {
            return _ventaAccesoDatos.ListarAgencia(agenciaPetroamerica);
        }
    }
}