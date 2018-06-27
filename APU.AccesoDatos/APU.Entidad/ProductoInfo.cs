using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APU.Entidad {
	[Serializable()]
	public class ProductoInfo
	{
		private int _productoId; public int ProductoId { get { return _productoId; } set { _productoId = value; } }
		private string _codigo; public string Codigo { get { return _codigo; } set { _codigo = value; } }
		private string _producto; public string Producto { get { return _producto; } set { _producto = value; } }
		private string _descripcion; public string Descripcion { get { return _descripcion; } set { _descripcion = value; } }
		private string _marca; public string Marca { get { return _marca; } set { _marca = value; } }
		private int _tipoProductoId; public int TipoProductoId { get { return _tipoProductoId; } set { _tipoProductoId = value; } }
		private int _subTipoProductoId; public int SubTipoProductoId { get { return _subTipoProductoId; } set { _subTipoProductoId = value; } }

		private string _subTipoProducto; public string SubTipoProducto { get { return _subTipoProducto; } set { _subTipoProducto = value; } }
		private decimal _precioNormal; public decimal PrecioNormal { get { return _precioNormal; } set { _precioNormal = value; } }
		private decimal _precioDescuento; public decimal PrecioDescuento { get { return _precioDescuento; } set { _precioDescuento = value; } }
		private decimal _precioCompra; public decimal PrecioCompra { get { return _precioCompra; } set { _precioCompra = value; } }
		private int _activo; public int Activo { get { return _activo; } set { _activo = value; } }
		private int _usuarioCreacionId; public int UsuarioCreacionId { get { return _usuarioCreacionId; } set { _usuarioCreacionId = value; } }
		private DateTime _fechaCreacion; public DateTime FechaCreacion { get { return _fechaCreacion; } set { _fechaCreacion = value; } }
		private int _usuarioModificacionId; public int UsuarioModificacionId { get { return _usuarioModificacionId; } set { _usuarioModificacionId = value; } }
		private DateTime _fechaModificacion; public DateTime FechaModificacion { get { return _fechaModificacion; } set { _fechaModificacion = value; } }

		public int NumeroFila
		{
			get
			{
				return _numeroFila;
			}

			set
			{
				_numeroFila = value;
			}
		}

		public int TotalFilas
		{
			get
			{
				return _totalFilas;
			}

			set
			{
				_totalFilas = value;
			}
		}

		public string TipoProducto
		{
			get
			{
				return _tipoProducto;
			}

			set
			{
				_tipoProducto = value;
			}
		}

		public int UnidadMedidaId
		{
			get
			{
				return _unidadMedidaId;
			}

			set
			{
				_unidadMedidaId = value;
			}
		}

		public string UnidadMedida
		{
			get
			{
				return _unidadMedida;
			}
			set
			{
				_unidadMedida = value;
			}
		}
	    private int _disponibleEstacion;
	    public int DisponibleEstacion
        {
	        get { return _disponibleEstacion; }
	        set { _disponibleEstacion = value; }
	    }
	    private int _disponibleMarket;
	    public int DisponibleMarket
	    {
	        get { return _disponibleMarket; }
	        set { _disponibleMarket = value; }
	    }
	    private int _disponibleCanastilla;
	    public int DisponibleCanastilla
	    {
	        get { return _disponibleCanastilla; }
	        set { _disponibleCanastilla = value; }
	    }

	    private int _stockActual;
        public int StockActual
		{
			get { return _stockActual; }

			set { _stockActual = value; }
		}

	    private string _tipoNegocioId;
	    public string TipoNegocioId
        {
	        get { return _tipoNegocioId; }

	        set { _tipoNegocioId = value; }
	    }

        private int _numeroFila;
		private int _totalFilas;
		private string _tipoProducto;
		private int _unidadMedidaId;
		private string _unidadMedida;

		
	}
}