using System;

namespace APU.Entidad
{
    [Serializable()]
    public class OpcionInfo
    {
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
        private int _orden;
        public int Orden
        {
            get { return _orden; }
            set { _orden = value; }
        }
        private string _nombre;
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        private string _url;
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
        private string _imagen;
        public string Imagen
        {
            get { return _imagen; }
            set { _imagen = value; }
        }
        private int _usuarioCreacionId;
        public int UsuarioCreacionId
        {
            get { return _usuarioCreacionId; }
            set { _usuarioCreacionId = value; }
        }
        private DateTime _fechaCreacion;
        public DateTime FechaCreacion
        {
            get { return _fechaCreacion; }
            set { _fechaCreacion = value; }
        }
        private int _usuarioModificacionId;
        public int UsuarioModificacionId
        {
            get { return _usuarioModificacionId; }
            set { _usuarioModificacionId = value; }
        }
        private DateTime _fechaModificacion;
        public DateTime FechaModificacion
        {
            get { return _fechaModificacion; }
            set { _fechaModificacion = value; }
        }
        private int _activo;
        public int Activo
        {
            get { return _activo; }
            set { _activo = value; }
        }
        private int _perfilId;
        public int PerfilId
        {
            get { return _perfilId; }
            set { _perfilId = value; }
        }
    }
}
