using System;

namespace APU.Entidad
{
    public class ParametrosGlobalesInfo
    {
        private int _CodigoParametro;
        public int CodigoParametro
        {
            get { return _CodigoParametro; }
            set { _CodigoParametro = value; }
        }
        private string _NombreParametro;
        public string NombreParametro
        {
            get { return _NombreParametro; }
            set { _NombreParametro = value; }
        }
        private string _ValorParametro;
        public string ValorParametro
        {
            get { return _ValorParametro; }
            set { _ValorParametro = value; }
        }
        private string _Descripcion;
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        private int _Editable;
        public int Editable
        {
            get { return _Editable; }
            set { _Editable = value; }
        }

        private int _usuarioIdCreacion;

        public int UsuarioIdCreacion
        {
            get { return _usuarioIdCreacion; }
            set { _usuarioIdCreacion = value; }
        }
        private DateTime _fechaCreacion;

        public DateTime FechaCreacion
        {
            get { return _fechaCreacion; }
            set { _fechaCreacion = value; }
        }
        private int _usuarioIdModificacion;
        public int UsuarioIdModificacion
        {
            get { return _usuarioIdModificacion; }
            set { _usuarioIdModificacion = value; }
        }
        private DateTime _fechaModificacion;
        public DateTime FechaModificacion
        {
            get { return _fechaModificacion; }
            set { _fechaModificacion = value; }
        }




        private int _totalFilas;

        private int _numeroFila;

        public int NumeroFila
        {
            get { return _numeroFila; }
            set { _numeroFila = value; }
        }
        public int TotalFilas
        {
            get { return _totalFilas; }
            set { _totalFilas = value; }
        }
    }
}