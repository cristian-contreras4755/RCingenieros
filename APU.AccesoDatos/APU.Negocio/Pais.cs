using System;
using System.Collections.Generic;
using APU.AccesoDatos;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.Negocio
{
    public class Pais
    {
        private readonly PaisAccesoDatos _paisAccesoDatos;
        public Pais()
        {
            _paisAccesoDatos = new PaisAccesoDatos();
        }
        public List<PaisInfo> Listar(int paisId)
        {
            return _paisAccesoDatos.Listar(paisId);
        }
        public List<PaisInfo> ListarPaginado(int paisId, int tamanioPagina, int numeroPagina)
        {
            return _paisAccesoDatos.ListarPaginado(paisId, tamanioPagina, numeroPagina);
        }
        public int Insertar(PaisInfo paisInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _paisAccesoDatos.Insertar(paisInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        //public int Actualizar(EmpresaInfo empresaInfo)
        //{
        //    int resultado = 0;
        //    try
        //    {
        //        resultado = _empresaAccesoDatos.Actualizar(empresaInfo);
        //    }
        //    catch (Exception ex)
        //    {
        //        bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
        //        if (rethrow)
        //            throw;
        //    }
        //    return resultado;
        //}
        //public int Eliminar(int empresaId)
        //{
        //    int resultado = 0;
        //    try
        //    {
        //        resultado = _empresaAccesoDatos.Eliminar(empresaId);
        //    }
        //    catch (Exception ex)
        //    {
        //        bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
        //        if (rethrow)
        //            throw;
        //    }
        //    return resultado;
        //}
    }
}
