using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APU.AccesoDatos;

namespace APU.Negocio
{
    public static class Helper
    {
        public static object ObtenerValorParametro(string nombreParametro)
        {
            var _helperAccesoDatos = new HelperAccesoDatos();
            return _helperAccesoDatos.ObtenerValorParametro(nombreParametro);
        }
        public static string ActualizarValorTabla(string tabla, string columna, string valor, string columnaWhere, string valorWhere)
        {
            var _helperAccesoDatos = new HelperAccesoDatos();
            return _helperAccesoDatos.ActualizarColumnaTabla(tabla, columna, valor, columnaWhere, valorWhere);
        }
        public static string ActualizarColumnasTabla(string tabla, string[] columna, string[] valor, string[] columnaWhere, string[] valorWhere)
        {
            var _helperAccesoDatos = new HelperAccesoDatos();
            return _helperAccesoDatos.ActualizarColumnasTabla(tabla, columna, valor, columnaWhere, valorWhere);
        }
    }
}