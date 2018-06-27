using System;

namespace APU.Entidad
{
    [Serializable()]
    public class ClienteInfo
    {
        private int _clienteId;
        public int ClienteId
        {
            get { return _clienteId; }
            set { _clienteId = value; }
        }
        private string _cliente;
        public string Cliente
        {
            get { return _cliente; }
            set { _cliente = value; }
        }
        private string _codigo;
        public string Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
        private int _tipoPersonaId;
        public int TipoPersonaId
        {
            get { return _tipoPersonaId; }
            set { _tipoPersonaId = value; }
        }
        private string _nombres;
        public string Nombres
        {
            get { return _nombres; }
            set { _nombres = value; }
        }
        private string _apellidoPaterno;
        public string ApellidoPaterno
        {
            get { return _apellidoPaterno; }
            set { _apellidoPaterno = value; }
        }
        private string _apellidoMaterno;
        public string ApellidoMaterno
        {
            get { return _apellidoMaterno; }
            set { _apellidoMaterno = value; }
        }
        private string _razonSocial;
        public string RazonSocial
        {
            get { return _razonSocial; }
            set { _razonSocial = value; }
        }
        private string _descripcion;
        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        private int _tipoDocumentoId;
        public int TipoDocumentoId
        {
            get { return _tipoDocumentoId; }
            set { _tipoDocumentoId = value; }
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
        private string _pais;
        public string Pais
        {
            get { return _pais; }
            set { _pais = value; }
        }
        private int _departamentoId;
        public int DepartamentoId
        {
            get { return _departamentoId; }
            set { _departamentoId = value; }
        }
        private string _departamento;
        public string Departamento
        {
            get { return _departamento; }
            set { _departamento = value; }
        }
        private int _provinciaId;
        public int ProvinciaId
        {
            get { return _provinciaId; }
            set { _provinciaId = value; }
        }
        private string _provincia;
        public string Provincia
        {
            get { return _provincia; }
            set { _provincia = value; }
        }
        private int _distritoId;
        public int DistritoId
        {
            get { return _distritoId; }
            set { _distritoId = value; }
        }
        private string _distrito;
        public string Distrito
        {
            get { return _distrito; }
            set { _distrito = value; }
        }
        private string _ciudad;
        public string Ciudad
        {
            get { return _ciudad; }
            set { _ciudad = value; }
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