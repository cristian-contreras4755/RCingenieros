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
    public class Empresa
    {
        private readonly EmpresaAccesoDatos _empresaAccesoDatos;
        public Empresa()
        {
            _empresaAccesoDatos = new EmpresaAccesoDatos();
        }
        public List<EmpresaInfo> Listar(int empresaId)
        {
            return _empresaAccesoDatos.Listar(empresaId);
        }
        public List<EmpresaInfo> ListarPaginado(int empresaId, int tamanioPagina, int numeroPagina)
        {
            return _empresaAccesoDatos.ListarPaginado(empresaId, tamanioPagina, numeroPagina);
        }
        public int Insertar(EmpresaInfo empresaInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _empresaAccesoDatos.Insertar(empresaInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Actualizar(EmpresaInfo empresaInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _empresaAccesoDatos.Actualizar(empresaInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Eliminar(int empresaId)
        {
            int resultado = 0;
            try
            {
                resultado = _empresaAccesoDatos.Eliminar(empresaId);
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
