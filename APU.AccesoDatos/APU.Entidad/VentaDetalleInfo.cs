using System;

namespace APU.Entidad
{
    [Serializable()]
    public class VentaDetalleInfo
    {
        private int _ventaDetalleId;
        public int VentaDetalleId
        {
            get { return _ventaDetalleId; }
            set { _ventaDetalleId = value; }
        }
        private int _ventaId;
        public int VentaId
        {
            get { return _ventaId; }
            set { _ventaId = value; }
        }
        private int _productoId;
        public int ProductoId
        {
            get { return _productoId; }
            set { _productoId = value; }
        }
        private string _producto;
        public string Producto
        {
            get { return _producto; }
            set { _producto = value; }
        }
        private string _codigo;
        public string Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
        private int _unidadMedidaId;
        public int UnidadMedidaId
        {
            get { return _unidadMedidaId; }
            set { _unidadMedidaId = value; }
        }
        private string _unidadMedida;
        public string UnidadMedida
        {
            get { return _unidadMedida; }
            set { _unidadMedida = value; }
        }
        private decimal _cantidad;
        public decimal Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }
        private decimal _precioUnitario;
        public decimal PrecioUnitario
        {
            get { return _precioUnitario; }
            set { _precioUnitario = value; }
        }
        private decimal _subTotal;
        public decimal SubTotal
        {
            get { return _subTotal; }
            set { _subTotal = value; }
        }
        private decimal _descuento;
        public decimal Descuento
        {
            get { return _descuento; }
            set { _descuento = value; }
        }
        private decimal _igv;
        public decimal Igv
        {
            get { return _igv; }
            set { _igv = value; }
        }
        private decimal _montoTotal;
        public decimal MontoTotal
        {
            get { return _montoTotal; }
            set { _montoTotal = value; }
        }
        private string _placa;
        public string Placa
        {
            get { return _placa; }
            set { _placa = value; }
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