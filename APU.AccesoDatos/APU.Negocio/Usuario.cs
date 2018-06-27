using System;
using System.Collections.Generic;
using APU.AccesoDatos;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.Negocio
{
    public class Usuario
    {
        private readonly UsuarioAccesoDatos _usuarioAccesoDatos;
        public Usuario()
        {
            _usuarioAccesoDatos = new UsuarioAccesoDatos();
        }
        public List<UsuarioInfo> Listar(int usuarioId, string login, string password, string correo, int perfilId, int empresaId)
        {
            return _usuarioAccesoDatos.Listar(usuarioId, login, password, correo, perfilId, empresaId);
        }
        public List<UsuarioInfo> ListarLogin(string login)
        {
            return _usuarioAccesoDatos.ListarLogin(login);
        }
        public List<UsuarioInfo> ListarPaginado(int usuarioId, string login, string password, string correo, int perfilId, int tamanioPagina, int numeroPagina)
        {
            return _usuarioAccesoDatos.ListarPaginado(usuarioId, login, password, correo, perfilId, tamanioPagina, numeroPagina);
        }
        public int Insertar(UsuarioInfo usuarioInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _usuarioAccesoDatos.Insertar(usuarioInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Actualizar(UsuarioInfo usuarioInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _usuarioAccesoDatos.Actualizar(usuarioInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Eliminar(int usuarioId)
        {
            int resultado = 0;
            try
            {
                resultado = _usuarioAccesoDatos.Eliminar(usuarioId);
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