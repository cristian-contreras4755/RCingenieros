using System;
using System.Collections.Generic;
using APU.AccesoDatos;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.Negocio
{
    public class Departamento
    {
        private readonly DepartamentoAccesoDatos _departamentoAccesoDatos;
        public Departamento()
        {
            _departamentoAccesoDatos = new DepartamentoAccesoDatos();
        }
        public List<DepartamentoInfo> Listar(int departamentoId)
        {
            return _departamentoAccesoDatos.Listar(departamentoId);
        }
        public List<DepartamentoInfo> ListarPaginado(int departamentoId, int tamanioPagina, int numeroPagina)
        {
            return _departamentoAccesoDatos.ListarPaginado(departamentoId, tamanioPagina, numeroPagina);
        }
        public int Insertar(DepartamentoInfo departamentoInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _departamentoAccesoDatos.Insertar(departamentoInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Actualizar(DepartamentoInfo departamentoInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _departamentoAccesoDatos.Actualizar(departamentoInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Eliminar(int departamentoId)
        {
            int resultado = 0;
            try
            {
                resultado = _departamentoAccesoDatos.Eliminar(departamentoId);
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