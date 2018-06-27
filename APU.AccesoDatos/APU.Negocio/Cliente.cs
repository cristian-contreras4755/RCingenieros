using System;
using System.Collections.Generic;
using APU.AccesoDatos;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.Negocio
{
    public class Cliente
    {
        private readonly ClienteAccesoDatos _clienteAccesoDatos;
        public Cliente()
        {
            _clienteAccesoDatos = new ClienteAccesoDatos();
        }
        public List<ClienteInfo> Listar(int clienteId)
        {
            return _clienteAccesoDatos.Listar(clienteId);
        }
        public List<ClienteInfo> ListarPaginado(int clienteId, int tipoPersonaId, int tipoDocumentoId, string numeroDocumento, string nombres, string apellidoPaterno, string apellidoMaterno,
                                                string ruc, string razonSocial, string codigo, int tamanioPagina, int numeroPagina)
        {
            return _clienteAccesoDatos.ListarPaginado(clienteId, tipoPersonaId, tipoDocumentoId, numeroDocumento, nombres, apellidoPaterno, apellidoMaterno, ruc, razonSocial, codigo, tamanioPagina, numeroPagina);
        }
        public int Insertar(ClienteInfo clienteInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _clienteAccesoDatos.Insertar(clienteInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Actualizar(ClienteInfo clienteInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _clienteAccesoDatos.Actualizar(clienteInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Eliminar(int clienteId)
        {
            int resultado = 0;
            try
            {
                resultado = _clienteAccesoDatos.Eliminar(clienteId);
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