using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APU.Entidad
{
	[Serializable()]
	public class TipoProductoInfo
	{
		private int _tipoProductoId; public int TipoProductoId { get { return _tipoProductoId; } set { _tipoProductoId = value; } }
		private string _nombre; public string Nombre { get { return _nombre; } set { _nombre = value; } }
		private string _descripcion; public string Descripcion { get { return _descripcion; } set { _descripcion = value; } }
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

		public int Nivel
		{
			get
			{
				return _nivel;
			}

			set
			{
				_nivel = value;
			}
		}

		public int TipoProductoPadreId
		{
			get
			{
				return _tipoProductoPadreId;
			}

			set
			{
				_tipoProductoPadreId = value;
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

		private int _numeroFila;
		private int _totalFilas;
		private int _nivel;
		private int _tipoProductoPadreId;
		private string _tipoProducto;
	}
}