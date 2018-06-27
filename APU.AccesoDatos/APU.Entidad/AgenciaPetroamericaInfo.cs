using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APU.Entidad
{
    [Serializable()]
    public class AgenciaPetroamericaInfo
    {
        private int _agenciaId;
        public int AgenciaId
        {
            get { return _agenciaId; }
            set { _agenciaId = value; }
        }
        private int _empresaId;
        public int EmpresaId
        {
            get { return _empresaId; }
            set { _empresaId = value; }
        }
        private string _empresa;
        public string Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }
        private string _agencia;
        public string Agencia
        {
            get { return _agencia; }
            set { _agencia = value; }
        }
        private string _descripcion;
        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
    }
}
