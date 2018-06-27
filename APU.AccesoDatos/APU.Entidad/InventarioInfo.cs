using System;
namespace APU.Entidad {
	[Serializable()]
	public class InventarioInfo
	{
		private int _inventarioId; public int InventarioId { get { return _inventarioId; } set { _inventarioId = value; } }
		private int _almacenId; public int AlmacenId { get { return _almacenId; } set { _almacenId = value; } }
		private int _productoId; public int ProductoId { get { return _productoId; } set { _productoId = value; } }
		private decimal _inventarioActual; public decimal InventarioActual { get { return _inventarioActual; } set { _inventarioActual = value; } }
		private decimal _inventarioMinimo; public decimal InventarioMinimo { get { return _inventarioMinimo; } set { _inventarioMinimo = value; } }
		private int _usuarioCreacionId; public int UsuarioCreacionId { get { return _usuarioCreacionId; } set { _usuarioCreacionId = value; } }
		private DateTime _fechaCreacion; public DateTime FechaCreacion { get { return _fechaCreacion; } set { _fechaCreacion = value; } }
		private int _usuarioModificacionId; public int UsuarioModificacionId { get { return _usuarioModificacionId; } set { _usuarioModificacionId = value; } }
		private DateTime _fechaModificacion; public DateTime FechaModificacion { get { return _fechaModificacion; } set { _fechaModificacion = value; } }

		public string Almacen
		{
			get
			{
				return _almacen;
			}

			set
			{
				_almacen = value;
			}
		}

		public string Producto
		{
			get
			{
				return _producto;
			}

			set
			{
				_producto = value;
			}
		}

		private string _almacen;
		private string _producto;

	    private int _tipoNegocioId;
	    public int TipoNegocioId
        {
	        get { return _tipoNegocioId; }
	        set { _tipoNegocioId = value; }
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
	    private string _codigo;
        public string Codigo
		{
			get
			{
				return _codigo;
			}

			set
			{
				_codigo = value;
			}
		}
	    private string _tipoProducto;
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
	    private string _unidadMedida;
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

	}
}
