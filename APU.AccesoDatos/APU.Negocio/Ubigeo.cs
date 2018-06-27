using System.Collections.Generic;
using APU.AccesoDatos;
using APU.Entidad;

namespace APU.Negocio
{
    public class Ubigeo
    {
        private UbigeoAccesoDatos _ubigeoAccesoDatos;
        public Ubigeo()
        {
            _ubigeoAccesoDatos = new UbigeoAccesoDatos();
        }
        public List<UbigeoInfo> Listar(int codigoUbigeo, int codigoUbigeoPadre, int tipoUbigeo)
        {
            return _ubigeoAccesoDatos.Listar(codigoUbigeo, codigoUbigeoPadre, tipoUbigeo);
        }
        public List<UbigeoInfo> ListarUbigeo(int codigoDepartamento, int codigoProvincia, int codigoDistrito, int tamanioPagina, int numeroPagina)
        {
            return _ubigeoAccesoDatos.ListarUbigeo(codigoDepartamento, codigoProvincia, codigoDistrito, tamanioPagina, numeroPagina);
        }
        public List<UbigeoInfo> ListarDep(int codigoUbigeo)
        {
            return _ubigeoAccesoDatos.ListarDep(codigoUbigeo);
        }
        public List<UbigeoInfo> ListarProv(int codigoUbigeo)
        {
            return _ubigeoAccesoDatos.ListarProv(codigoUbigeo);
        }
    }
}