using System;

namespace APU.Entidad
{
    [Serializable()]
    public class TablaMaestraInfo
    {
        private int _tablaMaestraId;
        public int TablaMaestraId
        {
            get { return _tablaMaestraId; }
            set { _tablaMaestraId = value; }
        }
        private int _tablaId;
        public int TablaId
        {
            get { return _tablaId; }
            set { _tablaId = value; }
        }
        private string _codigo;
        public string Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
        private string _nombreCorto;
        public string NombreCorto
        {
            get { return _nombreCorto; }
            set { _nombreCorto = value; }
        }
        private string _nombreLargo;
        public string NombreLargo
        {
            get { return _nombreLargo; }
            set { _nombreLargo = value; }
        }
        private int _activo;
        public int Activo
        {
            get { return _activo; }
            set { _activo = value; }
        }
        private int _editable;
        public int Editable
        {
            get { return _editable; }
            set { _editable = value; }
        }
        private int _usuarioCreacionId;
        public int UsuarioCreacionId
        {
            get { return _usuarioCreacionId; }
            set { _usuarioCreacionId = value; }
        }
        private string _usuarioCreacion;
        public string UsuarioCreacion
        {
            get { return _usuarioCreacion; }
            set { _usuarioCreacion = value; }
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
        private string _usuarioModificacion;
        public string UsuarioModificacion
        {
            get { return _usuarioModificacion; }
            set { _usuarioModificacion = value; }
        }
        private DateTime _fechaModificacion;
        public DateTime FechaModificacion
        {
            get { return _fechaModificacion; }
            set { _fechaModificacion = value; }
        }
        private int _numeroFila;
        public int NumeroFila
        {
            get { return _numeroFila; }
            set { _numeroFila = value; }
        }
        private int _totalFilas;
        public int TotalFilas
        {
            get { return _totalFilas; }
            set { _totalFilas = value; }
        }
    }
}