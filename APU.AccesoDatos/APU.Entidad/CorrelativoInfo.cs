using System;

namespace APU.Entidad
{
    [Serializable()]
    public class CorrelativoInfo
    {
        private int _correlativoId;
        public int CorrelativoId
        {
            get { return _correlativoId; }
            set { _correlativoId = value; }
        }
        private string _tipoComprobanteId;
        public string TipoComprobanteId
        {
            get { return _tipoComprobanteId; }
            set { _tipoComprobanteId = value; }
        }
        private string _tipoComprobante;
        public string TipoComprobante
        {
            get { return _tipoComprobante; }
            set { _tipoComprobante = value; }
        }
        private int _serieId;
        public int SerieId
        {
            get { return _serieId; }
            set { _serieId = value; }
        }
        private string _serie;
        public string Serie
        {
            get { return _serie; }
            set { _serie = value; }
        }
        private string _inicio;
        public string Inicio
        {
            get { return _inicio; }
            set { _inicio = value; }
        }
        private string _fin;
        public string Fin
        {
            get { return _fin; }
            set { _fin = value; }
        }
        private string _actual;
        public string Actual
        {
            get { return _actual; }
            set { _actual = value; }
        }
        private int _activo;
        public int Activo
        {
            get { return _activo; }
            set { _activo = value; }
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