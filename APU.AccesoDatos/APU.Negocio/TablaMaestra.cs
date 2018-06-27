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
    public class TablaMaestra
    {
        private readonly TablaMaestraAccesoDatos _tablaMaestraAccesoDatos;
        public TablaMaestra()
        {
            _tablaMaestraAccesoDatos = new TablaMaestraAccesoDatos();
        }
        public List<TablaMaestraInfo> Listar(int tablaMaestraId, int tablaId)
        {
            return _tablaMaestraAccesoDatos.Listar(tablaMaestraId, tablaId);
        }
        public List<TablaMaestraInfo> ListarPaginado(int tablaMaestraId, int tablaId, int tamanioPagina, int numeroPagina)
        {
            return _tablaMaestraAccesoDatos.ListarPaginado(tablaMaestraId, tablaId, tamanioPagina, numeroPagina);
        }
        public int Insertar(TablaMaestraInfo tablaMaestraInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _tablaMaestraAccesoDatos.Insertar(tablaMaestraInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Actualizar(TablaMaestraInfo tablaMaestraInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _tablaMaestraAccesoDatos.Actualizar(tablaMaestraInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Eliminar(int tablaMaestraId)
        {
            int resultado = 0;
            try
            {
                resultado = _tablaMaestraAccesoDatos.Eliminar(tablaMaestraId);
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