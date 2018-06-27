using System;

namespace APU.Entidad
{
    [Serializable()]
    public class ProveedorInfo
    {
        private int _proveedorId;
        public int ProveedorId
        {
            get { return _proveedorId; }
            set { _proveedorId = value; }
        }
        private int _empresaId;
        public int EmpresaId
        {
            get { return _empresaId; }
            set { _empresaId = value; }
        }
        private string _nombre;
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        private string _descripcion;
        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        private string _tipoDocumento;
        public string TipoDocumento
        {
            get { return _tipoDocumento; }
            set { _tipoDocumento = value; }
        }
        private string _numeroDocumento;
        public string NumeroDocumento
        {
            get { return _numeroDocumento; }
            set { _numeroDocumento = value; }
        }
        private int _paisId;
        public int PaisId
        {
            get { return _paisId; }
            set { _paisId = value; }
        }
        private int _provinciaId;
        public int ProvinciaId
        {
            get { return _provinciaId; }
            set { _provinciaId = value; }
        }
        private int _distritoId;
        public int DistritoId
        {
            get { return _distritoId; }
            set { _distritoId = value; }
        }
        private string _direccion;
        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }
        private string _telefono;
        public string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }
        private string _celular;
        public string Celular
        {
            get { return _celular; }
            set { _celular = value; }
        }
        private string _fax;
        public string Fax
        {
            get { return _fax; }
            set { _fax = value; }
        }
        private string _correo;
        public string Correo
        {
            get { return _correo; }
            set { _correo = value; }
        }
        private string _contacto;
        public string Contacto
        {
            get { return _contacto; }
            set { _contacto = value; }
        }
        private string _url;
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
        private string _imagen;
        public string Imagen
        {
            get { return _imagen; }
            set { _imagen = value; }
        }
        private int _activo;
        public int Activo
        {
            get { return _activo; }
            set { _activo = value; }
        }
        private int _usuarioCreacion;
        public int UsuarioCreacionId
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
        private int _usuarioModificacion;
        public int UsuarioModificacionId
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

		public string Empresa
		{
			get
			{
				return _empresa;
			}

			set
			{
				_empresa = value;
			}
		}

		private int _numeroFila;
		private int _totalFilas;
		private string _empresa;

		private int _tipoDocumentoId;
		public int TipoDocumentoId
		{
			get { return _tipoDocumentoId; }
			set { _tipoDocumentoId = value; }
		}

		private int _departamentoId;
		public int DepartamentoId
		{
			get { return _departamentoId; }
			set { _departamentoId = value; }
		}

		private string _ciudad;
		public string Ciudad
		{
			get { return _ciudad; }
			set { _ciudad = value; }
		}
	}
}
