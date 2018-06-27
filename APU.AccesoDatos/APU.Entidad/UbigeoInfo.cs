using System;

namespace APU.Entidad
{
    [Serializable()]
    public class UbigeoInfo
    {
        private int _ubigeoId;
        public int UbigeoId
        {
            get { return _ubigeoId; }
            set { _ubigeoId = value; }
        }
        private string _codigo;
        public string Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
        private string _nombre;
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        private int _ubigeoPadreId;
        public int UbigeoPadreId
        {
            get { return _ubigeoPadreId; }
            set { _ubigeoPadreId = value; }
        }
        private int _tipoUbigeoId;
        public int TipoUbigeoId
        {
            get { return _tipoUbigeoId; }
            set { _tipoUbigeoId = value; }
        }
        private int _codDepartamento;
        public int CodDepartamento
        {
            get { return _codDepartamento; }
            set { _codDepartamento = value; }
        }
        //private string _departamento;
        //public string Departamento
        //{
        //	get { return _departamento; }
        //	set { _departamento = value; }
        //}
        private int _codProvincia;
        public int CodProvincia
        {
            get { return _codProvincia; }
            set { _codProvincia = value; }
        }
        //private string _provincia;
        //public string Provincia
        //{
        //	get { return _provincia; }
        //	set { _provincia = value; }
        //}
        private int _codDistrito;
        public int CodDistrito
        {
            get { return _codDistrito; }
            set { _codDistrito = value; }
        }
        //private string _distrito;
        //public string Distrito
        //{
        //	get { return _distrito; }
        //	set { _distrito = value; }
        //}
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



        #region Departamento
        private int _departamentoId;
        public int DepartamentoId
        {
            get { return _departamentoId; }
            set { _departamentoId = value; }
        }
        private string _codigoDepartamento;
        public string CodigoDepartamento
        {
            get { return _codigoDepartamento; }
            set { _codigoDepartamento = value; }
        }
        private string _departamento;
        public string Departamento
        {
            get { return _departamento; }
            set { _departamento = value; }
        }
        private int _ubigeoPadreDepartamentoId;
        public int UbigeoPadreDepartamentoId
        {
            get { return _ubigeoPadreDepartamentoId; }
            set { _ubigeoPadreDepartamentoId = value; }
        }
        private int _tipoUbigeoDepartamentoId;
        public int TipoUbigeoDepartamentoId
        {
            get { return _tipoUbigeoDepartamentoId; }
            set { _tipoUbigeoDepartamentoId = value; }
        }
        private string _tipoUbigeoDepartamento;
        public string TipoUbigeoDepartamento
        {
            get { return _tipoUbigeoDepartamento; }
            set { _tipoUbigeoDepartamento = value; }
        }
        #endregion

        #region Provincia
        private int _provinciaId;
        public int ProvinciaId
        {
            get { return _provinciaId; }
            set { _provinciaId = value; }
        }
        private string _codigoProvincia;
        public string CodigoProvincia
        {
            get { return _codigoProvincia; }
            set { _codigoProvincia = value; }
        }
        private string _provincia;
        public string Provincia
        {
            get { return _provincia; }
            set { _provincia = value; }
        }
        private int _ubigeoPadreProvinciaId;
        public int UbigeoPadreProvinciaId
        {
            get { return _ubigeoPadreProvinciaId; }
            set { _ubigeoPadreProvinciaId = value; }
        }
        private int _tipoUbigeoProvinciaId;
        public int TipoUbigeoProvinciaId
        {
            get { return _tipoUbigeoProvinciaId; }
            set { _tipoUbigeoProvinciaId = value; }
        }
        private string _tipoUbigeoProvincia;
        public string TipoUbigeoProvincia
        {
            get { return _tipoUbigeoProvincia; }
            set { _tipoUbigeoProvincia = value; }
        }
        #endregion

        #region Distrito
        private int _distritoId;
        public int DistritoId
        {
            get { return _distritoId; }
            set { _distritoId = value; }
        }
        private string _codigoDistrito;
        public string CodigoDistrito
        {
            get { return _codigoDistrito; }
            set { _codigoDistrito = value; }
        }
        private string _distrito;
        public string Distrito
        {
            get { return _distrito; }
            set { _distrito = value; }
        }
        private int _ubigeoPadreDistritoId;
        public int UbigeoPadreDistritoId
        {
            get { return _ubigeoPadreDistritoId; }
            set { _ubigeoPadreDistritoId = value; }
        }
        private int _tipoUbigeoDistritoId;
        public int TipoUbigeoDistritoId
        {
            get { return _tipoUbigeoDistritoId; }
            set { _tipoUbigeoDistritoId = value; }
        }
        private string _tipoUbigeoDistrito;
        public string TipoUbigeoDistrito
        {
            get { return _tipoUbigeoDistrito; }
            set { _tipoUbigeoDistrito = value; }
        }
        #endregion
    }
}