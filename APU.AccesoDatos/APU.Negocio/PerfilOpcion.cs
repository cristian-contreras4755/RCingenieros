using System;
using System.Collections.Generic;
using APU.AccesoDatos;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.Negocio
{
    public class PerfilOpcion
    {
        private readonly PerfilOpcionAccesoDatos _perfilOpcionAccesoDatos;
        public PerfilOpcion()
        {
            _perfilOpcionAccesoDatos = new PerfilOpcionAccesoDatos();
        }
        public List<PerfilOpcionInfo> Listar(int perfilId)
        {
            return _perfilOpcionAccesoDatos.Listar(perfilId);
        }
        public int Insertar(PerfilOpcionInfo perfilOpcionInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _perfilOpcionAccesoDatos.Insertar(perfilOpcionInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Eliminar(int perfilId, int opcionId)
        {
            int resultado = 0;
            try
            {
                resultado = _perfilOpcionAccesoDatos.Eliminar(perfilId, opcionId);
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
