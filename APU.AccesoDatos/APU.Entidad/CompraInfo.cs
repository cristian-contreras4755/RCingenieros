using System;
namespace APU.Entidad {
	[Serializable()]
	public class CompraInfo
	{
		private int _compraId; public int CompraId { get { return _compraId; } set { _compraId = value; } }
		private int _proveedorId; public int ProveedorId { get { return _proveedorId; } set { _proveedorId = value; } }
		private string _numeroComprobante; public string NumeroComprobante { get { return _numeroComprobante; } set { _numeroComprobante = value; } }
		private string _numeroSerie; public string NumeroSerie { get { return _numeroSerie; } set { _numeroSerie = value; } }
		private int _monedaId; public int MonedaId { get { return _monedaId; } set { _monedaId = value; } }
		private int _tipoDocumentoId; public int TipoDocumentoId { get { return _tipoDocumentoId; } set { _tipoDocumentoId = value; } }
		private int _formaPagoId; public int FormaPagoId { get { return _formaPagoId; } set { _formaPagoId = value; } }
		private DateTime _fechaEmision; public DateTime FechaEmision { get { return _fechaEmision; } set { _fechaEmision = value; } }
		private DateTime _fechaVencimiento; public DateTime FechaVencimiento { get { return _fechaVencimiento; } set { _fechaVencimiento = value; } }
		private DateTime _fechaPago; public DateTime FechaPago { get { return _fechaPago; } set { _fechaPago = value; } }
		private int _estadoComprobanteId; public int EstadoComprobanteId { get { return _estadoComprobanteId; } set { _estadoComprobanteId = value; } }
		private string _glosa; public string Glosa { get { return _glosa; } set { _glosa = value; } }
		private decimal _subTotal; public decimal SubTotal { get { return _subTotal; } set { _subTotal = value; } }
		private decimal _igv; public decimal Igv { get { return _igv; } set { _igv = value; } }
		private decimal _total; public decimal Total { get { return _total; } set { _total = value; } }

		public string Proveedor
		{
			get
			{
				return _proveedor;
			}

			set
			{
				_proveedor = value;
			}
		}

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

		public string FormaPago
		{
			get
			{
				return _formaPago;
			}

			set
			{
				_formaPago = value;
			}
		}

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

		public string Moneda
		{
			get
			{
				return _moneda;
			}

			set
			{
				_moneda = value;
			}
		}

		public string EstadoComprobante
		{
			get
			{
				return _estadoComprobante;
			}

			set
			{
				_estadoComprobante = value;
			}
		}

		private string _proveedor;
		private string _tipoDocumento;
		private string _formaPago;
		private string _moneda;
		private string _estadoComprobante;
		private int _numeroFilas;
		private int _totalFilas;

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
	    private int _motivoIngresoId;
        public int MotivoIngresoId
		{
			get{ return _motivoIngresoId; }

			set { _motivoIngresoId = value; }
		}
	    private int _tipoNegocioId;
	    public int TipoNegocioId
        {
	        get { return _tipoNegocioId; }

	        set { _tipoNegocioId = value; }
	    }
    }
}