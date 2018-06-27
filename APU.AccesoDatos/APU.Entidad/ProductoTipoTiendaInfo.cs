using System;

namespace APU.Entidad
{
    [Serializable()]
    public class ProductoTipoTiendaInfo
    {
        private int _productoTipoTiendaId;
        public int ProductoTipoTiendaId
        {
            get { return _productoTipoTiendaId; }
            set { _productoTipoTiendaId = value; }
        }
        private int _productoId;
        public int ProductoId
        {
            get { return _productoId; }
            set { _productoId = value; }
        }
        private int _tipoTiendaId;
        public int TipoTiendaId
        {
            get { return _tipoTiendaId; }
            set { _tipoTiendaId = value; }
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
    }
}