using System;

namespace APU.Entidad
{
    [Serializable()]
    public class PerfilOpcionInfo
    {
        private int _perfilId;
        public int PerfilId
        {
            get { return _perfilId; }
            set { _perfilId = value; }
        }
        private int _opcionId;
        public int OpcionId
        {
            get { return _opcionId; }
            set { _opcionId = value; }
        }
        private int _opcionPadreId;
        public int OpcionPadreId
        {
            get { return _opcionPadreId; }
            set { _opcionPadreId = value; }
        }
        private string _nombre;
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        private int _orden;
        public int Orden
        {
            get { return _orden; }
            set { _orden = value; }
        }
        private int _activo;
        public int Activo
        {
            get { return _activo; }
            set { _activo = value; }
        }
    }
}
