using System;
namespace APU.Entidad
{
	[Serializable()]
	public class MovimientosInfo
	{
		private int _movimientoId;
		public int MovimientoId { get { return _movimientoId; } set { _movimientoId = value; } }
		private int _operacionId; public int OperacionId { get { return _operacionId; } set { _operacionId = value; } }
		private int _tipoMovimientoId; public int TipoMovimientoId { get { return _tipoMovimientoId; } set { _tipoMovimientoId = value; } }
		private DateTime _fechaOperacion; public DateTime FechaOperacion { get { return _fechaOperacion; } set { _fechaOperacion = value; } }
		private int _usuarioCreacionId; public int UsuarioCreacionId { get { return _usuarioCreacionId; } set { _usuarioCreacionId = value; } }
		private DateTime _fechaCreacion; public DateTime FechaCreacion { get { return _fechaCreacion; } set { _fechaCreacion = value; } }
		private string _glosa; public string Glosa { get { return _glosa; } set { _glosa = value; } }
	}
}
