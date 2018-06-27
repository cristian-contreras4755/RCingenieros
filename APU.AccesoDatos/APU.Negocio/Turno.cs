using System;
using System.Collections.Generic;
using APU.AccesoDatos;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.Negocio
{
    public class Turno
    {
        private readonly TurnoAccesoDatos _turnoAccesoDatos;
        public Turno()
        {
            _turnoAccesoDatos = new TurnoAccesoDatos();
        }
        public List<TurnoInfo> Listar(int turnoId)
        {
            return _turnoAccesoDatos.Listar(turnoId);
        }
        public List<TurnoInfo> ListarPaginado(int turnoId, int tamanioPagina, int numeroPagina)
        {
            return _turnoAccesoDatos.ListarPaginado(turnoId, tamanioPagina, numeroPagina);
        }
        public int Insertar(TurnoInfo turnoInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _turnoAccesoDatos.Insertar(turnoInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Actualizar(TurnoInfo turnoInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _turnoAccesoDatos.Actualizar(turnoInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Eliminar(int turnoId)
        {
            int resultado = 0;
            try
            {
                resultado = _turnoAccesoDatos.Eliminar(turnoId);
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