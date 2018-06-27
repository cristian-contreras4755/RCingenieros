using System;

namespace APU.Entidad
{
    [Serializable()]
    public class TipoCambioInfo
    {
        private int _tipoCambioId;
        public int TipoCambioId
        {
            get { return _tipoCambioId; }
            set { _tipoCambioId = value; }
        }
        private int _tipoCotizacionId;
        public int TipoCotizacionId
        {
            get { return _tipoCotizacionId; }
            set { _tipoCotizacionId = value; }
        }
        private string _tipoCotizacion;
        public string TipoCotizacion
        {
            get { return _tipoCotizacion; }
            set { _tipoCotizacion = value; }
        }
        private DateTime _fecha;
        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }
        private decimal _venta;
        public decimal Venta
        {
            get { return _venta; }
            set { _venta = value; }
        }
        private decimal _compra;
        public decimal Compra
        {
            get { return _compra; }
            set { _compra = value; }
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