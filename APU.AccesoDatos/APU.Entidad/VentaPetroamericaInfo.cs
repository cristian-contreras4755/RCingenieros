using System;
namespace APU.Entidad {
    [Serializable()]
    public class VentaPetroamericaInfo {
        private int _ventaPetroamericaId; public int VentaPetroamericaId { get { return _ventaPetroamericaId; } set { _ventaPetroamericaId = value; } }
        private int _ventaId; public int VentaId { get { return _ventaId; } set { _ventaId = value; } }
        private int _agenciaId; public int AgenciaId { get { return _agenciaId; } set { _agenciaId = value; } }
        private DateTime _fechaEmision; public DateTime FechaEmision { get { return _fechaEmision; } set { _fechaEmision = value; } }
        //private DateTime _horaEmision; public DateTime HoraEmision { get { return _horaEmision; } set { _horaEmision = value; } }
        private int _serieId; public int SerieId { get { return _serieId; } set { _serieId = value; } }
        private string _serie; public string Serie { get { return _serie; } set { _serie = value; } }
        private int _numeroComprobante; public int NumeroComprobante { get { return _numeroComprobante; } set { _numeroComprobante = value; } }
        private decimal _montoVenta; public decimal MontoVenta { get { return _montoVenta; } set { _montoVenta = value; } }
        private decimal _montoImpuesto; public decimal MontoImpuesto { get { return _montoImpuesto; } set { _montoImpuesto = value; } }
        private decimal _montoTotal; public decimal MontoTotal { get { return _montoTotal; } set { _montoTotal = value; } }
        private decimal _cantidad; public decimal Cantidad { get { return _cantidad; } set { _cantidad = value; } }
        private decimal _precio; public decimal Precio { get { return _precio; } set { _precio = value; } }
        private int _monedaId; public int MonedaId { get { return _monedaId; } set { _monedaId = value; } }
        private string _moneda; public string Moneda { get { return _moneda; } set { _moneda = value; } }
        private string _simboloMoneda; public string SimboloMoneda { get { return _simboloMoneda; } set { _simboloMoneda = value; } }
        private string _tipoComprobanteId; public string TipoComprobanteId { get { return _tipoComprobanteId; } set { _tipoComprobanteId = value; } }
        private string _tipoComprobante; public string TipoComprobante { get { return _tipoComprobante; } set { _tipoComprobante = value; } }
        private string _placaVehiculo; public string PlacaVehiculo { get { return _placaVehiculo; } set { _placaVehiculo = value; } }
        private int _clienteId; public int ClienteId { get { return _clienteId; } set { _clienteId = value; } }
        private int _tipoPersonaIdCliente; public int TipoPersonaIdCliente { get { return _tipoPersonaIdCliente; }set { _tipoPersonaIdCliente = value; } }
        private string _cliente; public string Cliente { get { return _cliente; } set { _cliente = value; } }
        private string _tipoDocumentoIdCliente; public string TipoDocumentoIdCliente { get { return _tipoDocumentoIdCliente; } set { _tipoDocumentoIdCliente = value; } }
        private string _tipoDocumentoCliente; public string TipoDocumentoCliente { get { return _tipoDocumentoCliente; } set { _tipoDocumentoCliente = value; } }
        private string _numeroDocumentoCliente; public string NumeroDocumentoCliente { get { return _numeroDocumentoCliente; } set { _numeroDocumentoCliente = value; } }
        private string _direccionCliente; public string DireccionCliente { get { return _direccionCliente; } set { _direccionCliente = value; } }
        private string _telefonoCliente; public string TelefonoCliente { get { return _telefonoCliente; } set { _telefonoCliente = value; } }
        private string _agencia; public string Agencia { get { return _agencia; } set { _agencia = value; } }
        private string _direccionAgencia; public string DireccionAgencia { get { return _direccionAgencia; } set { _direccionAgencia = value; } }
        private string _impresoraAgencia; public string ImpresoraAgencia { get { return _impresoraAgencia; } set { _impresoraAgencia = value; } }
        private string _rucEmpresa; public string RucEmpresa { get { return _rucEmpresa; } set { _rucEmpresa = value; } }
        private string _razonSocialEmpresa; public string RazonSocialEmpresa { get { return _razonSocialEmpresa; } set { _razonSocialEmpresa = value; } }
        private string _direccionEmpresa; public string DireccionEmpresa { get { return _direccionEmpresa; } set { _direccionEmpresa = value; } }
        private int _productoId; public int ProductoId { get { return _productoId; } set { _productoId = value; } }
        private string _producto; public string Producto { get { return _producto; } set { _producto = value; } }
        private int _usuarioCreacionId; public int UsuarioCreacionId { get { return _usuarioCreacionId; } set { _usuarioCreacionId = value; } }
        private string _usuarioCreacion; public string UsuarioCreacion { get { return _usuarioCreacion; } set { _usuarioCreacion = value; } }
        private int _ventaSunatId; public int VentaSunatId { get { return _ventaSunatId; } set { _ventaSunatId = value; } }
        private string _codigoRespuesta; public string CodigoRespuesta { get { return _codigoRespuesta; } set { _codigoRespuesta = value; } }
        private int _exito; public int Exito { get { return _exito; } set { _exito = value; } }
        private string _mensajeError; public string MensajeError { get { return _mensajeError; } set { _mensajeError = value; } }
        private string _mensajeRespuesta; public string MensajeRespuesta { get { return _mensajeRespuesta; } set { _mensajeRespuesta = value; } }
        private string _nombreArchivo; public string NombreArchivo { get { return _nombreArchivo; } set { _nombreArchivo = value; } }
        private string _nroTicket; public string NroTicket { get { return _nroTicket; } set { _nroTicket = value; } }
        private int _estadoId; public int EstadoId { get { return _estadoId; } set { _estadoId = value; } }
        private string _estado; public string Estado { get { return _estado; } set { _estado = value; } }
        private string _comprobanteImpreso; public string ComprobanteImpreso { get { return _comprobanteImpreso; } set { _comprobanteImpreso = value; } }
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