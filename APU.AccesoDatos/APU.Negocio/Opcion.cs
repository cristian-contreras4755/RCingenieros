using System.Collections.Generic;
using APU.AccesoDatos;
using APU.Entidad;

namespace APU.Negocio
{
    public class Opcion
    {
        private readonly OpcionAccesoDatos _opcionAccesoDatos;
        public Opcion()
        {
            _opcionAccesoDatos = new OpcionAccesoDatos();
        }
        public List<OpcionInfo> ListarOpciones(int perfilId)
        {
            return _opcionAccesoDatos.ListarOpciones(perfilId);
        }
        public List<OpcionInfo> Listar(int opcionId)
        {
            return _opcionAccesoDatos.Listar(opcionId);
        }
    }
}