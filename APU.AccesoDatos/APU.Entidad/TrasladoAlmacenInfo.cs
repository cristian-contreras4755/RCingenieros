using System;
namespace APU.Entidad {[Serializable()]
	public class TrasladoAlmacenInfo
	{
		private int _trasladoAlmacenId; public int TrasladoAlmacenId { get { return _trasladoAlmacenId; } set { _trasladoAlmacenId = value; } }
		private int _almacenOrigenId; public int AlmacenOrigenId { get { return _almacenOrigenId; } set { _almacenOrigenId = value; } }
		private int _almacenDestinoId; public int AlmacenDestinoId { get { return _almacenDestinoId; } set { _almacenDestinoId = value; } }
		private int _productoId; public int ProductoId { get { return _productoId; } set { _productoId = value; } }
		private decimal _cantidadProducto; public decimal CantidadProducto { get { return _cantidadProducto; } set { _cantidadProducto = value; } }
		private int _usuarioCreacionId; public int UsuarioCreacionId { get { return _usuarioCreacionId; } set { _usuarioCreacionId = value; } }
		private DateTime _fechaCreacion; public DateTime FechaCreacion { get { return _fechaCreacion; } set { _fechaCreacion = value; } }
		private int _usuarioModificacionId; public int UsuarioModificacionId { get { return _usuarioModificacionId; } set { _usuarioModificacionId = value; } }
		private DateTime _fechaModificacion; public DateTime FechaModificacion { get { return _fechaModificacion; } set { _fechaModificacion = value; } }

		private DateTime _fechaTraslado; public DateTime FechaTraslado { get { return _fechaTraslado; } set { _fechaTraslado = value; } }

		private int _usuarioInventarioId; public int UsuarioInventarioId { get { return _usuarioInventarioId; } set { _usuarioInventarioId = value; } }

		private int _usuarioResponsableId; public int UsuarioResponsableId { get { return _usuarioResponsableId; } set { _usuarioResponsableId = value; } }

		private int _tipoNegocioId;
		public int TipoNegocioId
		{
			get { return _tipoNegocioId; }
			set { _tipoNegocioId = value; }
		}
	}
}