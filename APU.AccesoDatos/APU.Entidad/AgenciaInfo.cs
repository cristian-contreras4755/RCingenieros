using System;

namespace APU.Entidad
{
    [Serializable()]
    public class AgenciaInfo
    {
        private int _agenciaId;
        public int AgenciaId
        {
            get { return _agenciaId; }
            set { _agenciaId = value; }
        }
        private int _empresaId;
        public int EmpresaId
        {
            get { return _empresaId; }
            set { _empresaId = value; }
        }
        private string _empresa;
        public string Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
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
        private int _paisId;
        public int PaisId
        {
            get { return _paisId; }
            set { _paisId = value; }
        }
        private int _departamentoId;
        public int DepartamentoId
        {
            get { return _departamentoId; }
            set { _departamentoId = value; }
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
        private int _contactoId;
        public int ContactoId
        {
            get { return _contactoId; }
            set { _contactoId = value; }
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