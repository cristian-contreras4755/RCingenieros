using System;

namespace APU.Entidad
{
    [Serializable()]
    public class UsuarioInfo
    {
        private int _usuarioId;
        public int UsuarioId
        {
            get { return _usuarioId; }
            set { _usuarioId = value; }
        }
        private string _codigo;
        public string Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
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
        private string _login;
        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        private int _sexoId;
        public int SexoId
        {
            get { return _sexoId; }
            set { _sexoId = value; }
        }
        private int _estadoCivilId;
        public int EstadoCivilId
        {
            get { return _estadoCivilId; }
            set { _estadoCivilId = value; }
        }
        private string _correo;
        public string Correo
        {
            get { return _correo; }
            set { _correo = value; }
        }
        private string _correoTrabajo;
        public string CorreoTrabajo
        {
            get { return _correoTrabajo; }
            set { _correoTrabajo = value; }
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
        private string _telefonoTrabajo;
        public string TelefonoTrabajo
        {
            get { return _telefonoTrabajo; }
            set { _telefonoTrabajo = value; }
        }
        private int _tipoDocumentoId;
        public int TipoDocumentoId
        {
            get { return _tipoDocumentoId; }
            set { _tipoDocumentoId = value; }
        }
        private string _numeroDocumento;
        public string NumeroDocumento
        {
            get { return _numeroDocumento; }
            set { _numeroDocumento = value; }
        }
        private int _activo;
        public int Activo
        {
            get { return _activo; }
            set { _activo = value; }
        }
        private int _intento;
        public int Intento
        {
            get { return _intento; }
            set { _intento = value; }
        }
        private int _perfilId;
        public int PerfilId
        {
            get { return _perfilId; }
            set { _perfilId = value; }
        }
        private string _perfil;
        public string Perfil
        {
            get { return _perfil; }
            set { _perfil = value; }
        }
        private int _empresaId;
        public int EmpresaId
        {
            get { return _empresaId; }
            set { _empresaId = value; }
        }
        private int _agenciaId;
        public int AgenciaId
        {
            get { return _agenciaId; }
            set { _agenciaId = value; }
        }
        private string _agencia;
        public string Agencia
        {
            get { return _agencia; }
            set { _agencia = value; }
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
        private int _cargoId;
        public int CargoId
        {
            get { return _cargoId; }
            set { _cargoId = value; }
        }
        private string _cargo;
        public string Cargo
        {
            get { return _cargo; }
            set { _cargo = value; }
        }
        private string _director;
        public string Director
        {
            get { return _director; }
            set { _director = value; }
        }
        private string _foto;
        public string Foto
        {
            get { return _foto; }
            set { _foto = value; }
        }
        private string _imagenEmpresa;
        public string ImagenEmpresa
        {
            get { return _imagenEmpresa; }
            set { _imagenEmpresa = value; }
        }
        private int _tipoNegocioId;
        public int TipoNegocioId
        {
            get { return _tipoNegocioId; }
            set { _tipoNegocioId = value; }
        }
        private int _usuarioCreacionId;
        public int UsuarioCreacionId
        {
            get { return _usuarioCreacionId; }
            set { _usuarioCreacionId = value; }
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
        private DateTime _fechaModificacion;
        public DateTime FechaModificacion
        {
            get { return _fechaModificacion; }
            set { _fechaModificacion = value; }
        }
        private int _opcionInicioId;
        public int OpcionInicioId
        {
            get { return _opcionInicioId; }
            set { _opcionInicioId = value; }
        }
        private string _opcionInicio;
        public string OpcionInicio
        {
            get { return _opcionInicio; }
            set { _opcionInicio = value; }
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
        private string _usuario;
        public string Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }
        private string _guid;
        public string Guid
        {
            get { return _guid; }
            set { _guid = value; }
        }
    }
}