using System;

namespace APU.Entidad
{
    [Serializable()]
    public class VentaInfo
    {
        private int _ventaId;
        public int VentaId
        {
            get { return _ventaId; }
            set { _ventaId = value; }
        }
        private int _agenciaId;
        public int AgenciaId
        {
            get { return _agenciaId; }
            set { _agenciaId = value; }
        }
        private string _agencia;
        public string Agencia
        {
            get { return _agencia; }
            set { _agencia = value; }
        }
        private int _clienteId;
        public int ClienteId
        {
            get { return _clienteId; }
            set { _clienteId = value; }
        }
        private string _cliente;
        public string Cliente
        {
            get { return _cliente; }
            set { _cliente = value; }
        }
        private string _numeroDocumento;
        public string NumeroDocumento
        {
            get { return _numeroDocumento; }
            set { _numeroDocumento = value; }
        }
        private string _direccion;
        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
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
        private string _numeroComprobante;
        public string NumeroComprobante
        {
            get { return _numeroComprobante; }
            set { _numeroComprobante = value; }
        }
        private string _numeroSerie;
        public string NumeroSerie
        {
            get { return _numeroSerie; }
            set { _numeroSerie = value; }
        }
        private string _numeroGuia;
        public string NumeroGuia
        {
            get { return _numeroGuia; }
            set { _numeroGuia = value; }
        }
        private string _glosa;
        public string Glosa
        {
            get { return _glosa; }
            set { _glosa = value; }
        }
        private int _estadoId;
        public int EstadoId
        {
            get { return _estadoId; }
            set { _estadoId = value; }
        }
        private string _estado;
        public string Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }
        private int _formaPagoId;
        public int FormaPagoId
        {
            get { return _formaPagoId; }
            set { _formaPagoId = value; }
        }
        private string _formaPago;
        public string FormaPago
        {
            get { return _formaPago; }
            set { _formaPago = value; }
        }
        private DateTime _fechaEmision;
        public DateTime FechaEmision
        {
            get { return _fechaEmision; }
            set { _fechaEmision = value; }
        }
        private DateTime _fechaVencimiento;
        public DateTime FechaVencimiento
        {
            get { return _fechaVencimiento; }
            set { _fechaVencimiento = value; }
        }
        private DateTime _fechaPago;
        public DateTime FechaPago
        {
            get { return _fechaPago; }
            set { _fechaPago = value; }
        }
        private int _monedaId;
        public int MonedaId
        {
            get { return _monedaId; }
            set { _monedaId = value; }
        }
        private string _moneda;
        public string Moneda
        {
            get { return _moneda; }
            set { _moneda = value; }
        }
        private string _simboloMoneda;
        public string SimboloMoneda
        {
            get { return _simboloMoneda; }
            set { _simboloMoneda = value; }
        }
        private decimal _tipoCambio;
        public decimal TipoCambio
        {
            get { return _tipoCambio; }
            set { _tipoCambio = value; }
        }
        private decimal _cantidad;
        public decimal Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }
        private decimal _montoVenta;
        public decimal MontoVenta
        {
            get { return _montoVenta; }
            set { _montoVenta = value; }
        }
        private decimal _descuento;
        public decimal Descuento
        {
            get { return _descuento; }
            set { _descuento = value; }
        }
        private decimal _montoImpuesto;
        public decimal MontoImpuesto
        {
            get { return _montoImpuesto; }
            set { _montoImpuesto = value; }
        }
        private decimal _montoTotal;
        public decimal MontoTotal
        {
            get { return _montoTotal; }
            set { _montoTotal = value; }
        }
        private string _comprobanteImpreso;
        public string ComprobanteImpreso
        {
            get { return _comprobanteImpreso; }
            set { _comprobanteImpreso = value; }
        }
        private int _activo;
        public int Activo
        {
            get { return _activo; }
            set { _activo = value; }
        }
        private int _tipoNegocioId;
        public int TipoNegocioId
        {
            get { return _tipoNegocioId; }
            set { _tipoNegocioId = value; }
        }
        private int _comprobanteRelacionadoId;
        public int ComprobanteRelacionadoId
        {
            get { return _comprobanteRelacionadoId; }
            set { _comprobanteRelacionadoId = value; }
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



        private int _ventaSunatId;
        public int VentaSunatId
        {
            get { return _ventaSunatId; }
            set { _ventaSunatId = value; }
        }
        private string _codigoRespuesta;
        public string CodigoRespuesta
        {
            get { return _codigoRespuesta; }
            set { _codigoRespuesta = value; }
        }
        private int _exito;
        public int Exito
        {
            get { return _exito; }
            set { _exito = value; }
        }
        private string _mensajeError;
        public string MensajeError
        {
            get { return _mensajeError; }
            set { _mensajeError = value; }
        }
        private string _mensajeRespuesta;
        public string MensajeRespuesta
        {
            get { return _mensajeRespuesta; }
            set { _mensajeRespuesta = value; }
        }
        private string _nombreArchivo;
        public string NombreArchivo
        {
            get { return _nombreArchivo; }
            set { _nombreArchivo = value; }
        }
    }
}