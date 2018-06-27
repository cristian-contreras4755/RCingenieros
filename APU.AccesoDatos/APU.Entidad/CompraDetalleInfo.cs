using System;
namespace APU.Entidad {
	[Serializable()]
	public class ComprasDetalleInfo {
		private int _comprasDetalleId; public int ComprasDetalleId { get { return _comprasDetalleId; } set { _comprasDetalleId = value; } }
		private int _compraId; public int CompraId { get { return _compraId; } set { _compraId = value; } }
		private int _productoId; public int ProductoId { get { return _productoId; } set { _productoId = value; } }
		private decimal _cantidad; public decimal Cantidad { get { return _cantidad; } set { _cantidad = value; } }
		private decimal _precioUnitario; public decimal PrecioUnitario { get { return _precioUnitario; } set { _precioUnitario = value; } }
		private decimal _subTotal; public decimal SubTotal { get { return _subTotal; } set { _subTotal = value; } }
		private decimal _igv; public decimal Igv { get { return _igv; } set { _igv = value; } }
		private decimal _Total; public decimal Total { get { return _Total; } set { _Total = value; } }

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

		private string _producto;
		private string _codigo;
		public int NumeroFila
		{
			get
			{
				return _numeroFilas;
			}

			set
			{
				_numeroFilas = value;
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

		private int _unidadMedidaId;
		private int _numeroFilas;
		private int _totalFilas;
		private string _unidadMedida;

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

		private string _numeroComprobante; public string NumeroComprobante { get { return _numeroComprobante; } set { _numeroComprobante = value; } }
		private string _numeroSerie; public string NumeroSerie { get { return _numeroSerie; } set { _numeroSerie = value; } }
		private string _tipoDocumento;
		public string TipoDocumento
		{
			get
			{
				return _tipoDocumento;
			}

			set
			{
				_tipoDocumento = value;
			}
		}

		public int AlmacenId
		{
			get
			{
				return _almacenId;
			}

			set
			{
				_almacenId = value;
			}
		}

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

		public int AsignacionAlmacen
		{
			get
			{
				return _asignacionAlmacen;
			}

			set
			{
				_asignacionAlmacen = value;
			}
		}

		public int Eliminado
		{
			get
			{
				return _eliminado;
			}

			set
			{
				_eliminado = value;
			}
		}

		private int _almacenId;
		private string _almacen;
		private int _asignacionAlmacen;
		private int _eliminado;
	}
}