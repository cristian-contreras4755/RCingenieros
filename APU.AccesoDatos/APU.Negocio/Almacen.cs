using System;
using System.Collections.Generic;
using APU.AccesoDatos;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.Negocio
{
    public class Almacen
    {
        private readonly AlmacenAccesoDatos _almacenAccesoDatos;
        public Almacen()
        {
            _almacenAccesoDatos = new AlmacenAccesoDatos();
        }
        public List<AlmacenInfo> Listar(int almacenId)
        {
            return _almacenAccesoDatos.Listar(almacenId);
        }
        public List<AlmacenInfo> ListarPaginado(int almacenId, int empresaId, int tamanioPagina, int numeroPagina)
        {
            return _almacenAccesoDatos.ListarPaginado(almacenId, empresaId, tamanioPagina, numeroPagina);
        }
        public int Insertar(AlmacenInfo almacenInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _almacenAccesoDatos.Insertar(almacenInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Actualizar(AlmacenInfo almacenInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _almacenAccesoDatos.Actualizar(almacenInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Eliminar(int almacenId)
        {
            int resultado = 0;
            try
            {
                resultado = _almacenAccesoDatos.Eliminar(almacenId);
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
