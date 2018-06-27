using System;

namespace APU.Entidad
{
    [Serializable()]
    public class VentaDetalleGasolutionsInfo
    {
        private int _ventaDetalleGasolutionsId;
        public int VentaDetalleGasolutionsId
        {
            get { return _ventaDetalleGasolutionsId; }
            set { _ventaDetalleGasolutionsId = value; }
        }
        private int _ventaGasolutionsId;
        public int VentaGasolutionsId
        {
            get { return _ventaGasolutionsId; }
            set { _ventaGasolutionsId = value; }
        }
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private decimal _cantidad;
        public decimal Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }
        private string _codigoItem;
        public string CodigoItem
        {
            get { return _codigoItem; }
            set { _codigoItem = value; }
        }
        private string _descripcion;
        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        private decimal _impuesto;
        public decimal Impuesto
        {
            get { return _impuesto; }
            set { _impuesto = value; }
        }
        private decimal _precioUnitario;
        public decimal PrecioUnitario
        {
            get { return _precioUnitario; }
            set { _precioUnitario = value; }
        }
        private decimal _totalVenta;
        public decimal TotalVenta
        {
            get { return _totalVenta; }
            set { _totalVenta = value; }
        }
        private string _unidadMedida;
        public string UnidadMedida
        {
            get { return _unidadMedida; }
            set { _unidadMedida = value; }
        }
        private string _tipoImpuesto;
        public string TipoImpuesto
        {
            get { return _tipoImpuesto; }
            set { _tipoImpuesto = value; }
        }
        private string _tipoPrecio;
        public string TipoPrecio
        {
            get { return _tipoPrecio; }
            set { _tipoPrecio = value; }
        }
        private DateTime _fechaCreacion;
        public DateTime FechaCreacion
        {
            get { return _fechaCreacion; }
            set { _fechaCreacion = value; }
        }
    }
}