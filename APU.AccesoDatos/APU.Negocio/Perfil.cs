using System;
using System.Collections.Generic;
using APU.AccesoDatos;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.Negocio
{
    public class Perfil
    {
        private readonly PerfilAccesoDatos _perfilAccesoDatos;
        public Perfil()
        {
            _perfilAccesoDatos = new PerfilAccesoDatos();
        }
        public List<PerfilInfo> Listar(int perfilId)
        {
            return _perfilAccesoDatos.Listar(perfilId);
        }
        public List<PerfilInfo> ListarPaginado(int perfilId, int tamanioPagina, int numeroPagina)
        {
            return _perfilAccesoDatos.ListarPaginado(perfilId, tamanioPagina, numeroPagina);
        }
        public int Insertar(PerfilInfo perfilInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _perfilAccesoDatos.Insertar(perfilInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Actualizar(PerfilInfo perfilInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _perfilAccesoDatos.Actualizar(perfilInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Eliminar(int perfilId)
        {
            int resultado = 0;
            try
            {
                resultado = _perfilAccesoDatos.Eliminar(perfilId);
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