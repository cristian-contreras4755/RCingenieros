using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APU.Entidad
{
    [Serializable()]
    public class EmpresaSunatInfo
    {
        private string _razonSocial;
        public string RazonSocial
        {
            get { return _razonSocial; }
            set { _razonSocial = value; }
        }
        private string _estado;
        public string Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }
        private string _direccion;
        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }
    }
}