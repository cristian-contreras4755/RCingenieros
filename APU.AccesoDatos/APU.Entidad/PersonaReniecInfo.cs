using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APU.Entidad
{
    [Serializable()]
    public class PersonaReniecInfo
    {
        private string _dni;
        public string Dni
        {
            get { return _dni; }
            set { _dni = value; }
        }
        private string _nombre;
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        private string _codVerificacion;
        public string CodVerificacion
        {
            get { return _codVerificacion; }
            set { _codVerificacion = value; }
        }
    }
}