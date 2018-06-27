using System;
using System.Collections.Generic;

namespace APU.Entidad
{
    [Serializable()]
    public class VentaGasolutionsInfo
    {
        private int _ventaGasolutionsId;
        public int VentaGasolutionsId
        {
            get { return _ventaGasolutionsId; }
            set { _ventaGasolutionsId = value; }
        }
        private DateTime _fechaEmision;
        public DateTime FechaEmision
        {
            get { return _fechaEmision; }
            set { _fechaEmision = value; }
        }
        private decimal _gravadas;
        public decimal Gravadas
        {
            get { return _gravadas; }
            set { _gravadas = value; }
        }
        private string _idDocumento;
        public string IdDocumento
        {
            get { return _idDocumento; }
            set { _idDocumento = value; }
        }
        private decimal _calculoIgv;
        public decimal CalculoIgv
        {
            get { return _calculoIgv; }
            set { _calculoIgv = value; }
        }
        private string _monedaId;
        public string MonedaId
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
        private string _montoEnLetras;
        public string MontoEnLetras
        {
            get { return _montoEnLetras; }
            set { _montoEnLetras = value; }
        }
        private string _placaVehiculo;
        public string PlacaVehiculo
        {
            get { return _placaVehiculo; }
            set { _placaVehiculo = value; }
        }
        private string _tipoDocumento;
        public string TipoDocumento
        {
            get { return _tipoDocumento; }
            set { _tipoDocumento = value; }
        }
        private decimal _totalIgv;
        public decimal TotalIgv
        {
            get { return _totalIgv; }
            set { _totalIgv = value; }
        }
        private decimal _totalVenta;
        public decimal TotalVenta
        {
            get { return _totalVenta; }
            set { _totalVenta = value; }
        }
        private decimal _descuento;
        public decimal Descuento
        {
            get { return _descuento; }
            set { _descuento = value; }
        }
        private string _tipoDocumentoEmisor;
        public string TipoDocumentoEmisor
        {
            get { return _tipoDocumentoEmisor; }
            set { _tipoDocumentoEmisor = value; }
        }
        private string _nroDocumentoEmisor;
        public string NroDocumentoEmisor
        {
            get { return _nroDocumentoEmisor; }
            set { _nroDocumentoEmisor = value; }
        }
        private string _nombreComercialEmisor;
        public string NombreComercialEmisor
        {
            get { return _nombreComercialEmisor; }
            set { _nombreComercialEmisor = value; }
        }
        private string _nombreLegalEmisor;
        public string NombreLegalEmisor
        {
            get { return _nombreLegalEmisor; }
            set { _nombreLegalEmisor = value; }
        }
        private string _departamentoEmisor;
        public string DepartamentoEmisor
        {
            get { return _departamentoEmisor; }
            set { _departamentoEmisor = value; }
        }
        private string _provinciaEmisor;
        public string ProvinciaEmisor
        {
            get { return _provinciaEmisor; }
            set { _provinciaEmisor = value; }
        }
        private string _distritoEmisor;
        public string DistritoEmisor
        {
            get { return _distritoEmisor; }
            set { _distritoEmisor = value; }
        }
        private string _ubigeoEmisor;
        public string UbigeoEmisor
        {
            get { return _ubigeoEmisor; }
            set { _ubigeoEmisor = value; }
        }
        private string _direccionEmisor;
        public string DireccionEmisor
        {
            get { return _direccionEmisor; }
            set { _direccionEmisor = value; }
        }
        private string _tipoDocumentoReceptor;
        public string TipoDocumentoReceptor
        {
            get { return _tipoDocumentoReceptor; }
            set { _tipoDocumentoReceptor = value; }
        }
        private string _nroDocumentoReceptor;
        public string NroDocumentoReceptor
        {
            get { return _nroDocumentoReceptor; }
            set { _nroDocumentoReceptor = value; }
        }
        private string _nombreComercialReceptor;
        public string NombreComercialReceptor
        {
            get { return _nombreComercialReceptor; }
            set { _nombreComercialReceptor = value; }
        }
        private string _nombreLegalReceptor;
        public string NombreLegalReceptor
        {
            get { return _nombreLegalReceptor; }
            set { _nombreLegalReceptor = value; }
        }
        private string _direccionReceptor;
        public string DireccionReceptor
        {
            get { return _direccionReceptor; }
            set { _direccionReceptor = value; }
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
        private string _nroTicket;
        public string NroTicket
        {
            get { return _nroTicket; }
            set { _nroTicket = value; }
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
        private string _comprobanteImpreso;
        public string ComprobanteImpreso
        {
            get { return _comprobanteImpreso; }
            set { _comprobanteImpreso = value; }
        }
        private DateTime _fechaCreacion;
        public DateTime FechaCreacion
        {
            get { return _fechaCreacion; }
            set { _fechaCreacion = value; }
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

        public VentaDetalleGasolutionsInfo Item { get; set; }
    }
    //public class VentaDetalleGasolutionsInfo
    //{
    //    public int VentaDetalleGasolutionsId { get; set; }
    //    public int VentaGasolutionsId { get; set; }
    //    public int Id { get; set; }
    //    public decimal Cantidad { get; set; }
    //    public string CodigoItem { get; set; }
    //    public string Descripcion { get; set; }
    //    public decimal Impuesto { get; set; }
    //    public decimal PrecioUnitario { get; set; }
    //    public decimal TotalVenta { get; set; }
    //    public string UnidadMedida { get; set; }
    //    public string TipoImpuesto { get; set; }
    //    public string TipoPrecio { get; set; }



    //    public decimal Descuento { get; set; }
    //    public decimal ImpuestoSelectivo { get; set; }
    //    public decimal OtroImpuesto { get; set; }
    //    public decimal PrecioReferencial { get; set; }
    //}
}