using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APU.AccesoDatos;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.Negocio
{
    public class Agencia
    {
        private readonly AgenciaAccesoDatos _agenciaAccesoDatos;
        public Agencia()
        {
            _agenciaAccesoDatos = new AgenciaAccesoDatos();
        }
        public List<AgenciaInfo> Listar(int agenciaId)
        {
            return _agenciaAccesoDatos.Listar(agenciaId);
        }
        public List<AgenciaInfo> ListarPaginado(int agenciaId, int empresaId, int clienteId, int tamanioPagina, int numeroPagina)
        {
            return _agenciaAccesoDatos.ListarPaginado(agenciaId, empresaId, clienteId, tamanioPagina, numeroPagina);
        }
        public int Insertar(AgenciaInfo agenciaInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _agenciaAccesoDatos.Insertar(agenciaInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Actualizar(AgenciaInfo agenciaInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _agenciaAccesoDatos.Actualizar(agenciaInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Eliminar(int agenciaId)
        {
            int resultado = 0;
            try
            {
                resultado = _agenciaAccesoDatos.Eliminar(agenciaId);
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
