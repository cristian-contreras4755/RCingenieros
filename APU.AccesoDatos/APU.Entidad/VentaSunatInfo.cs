using System;

namespace APU.Entidad
{
    [Serializable()]
    public class VentaSunatInfo
    {
        private int _ventaSunatId;
        public int VentaSunatId
        {
            get { return _ventaSunatId; }
            set { _ventaSunatId = value; }
        }
        private int _ventaId;
        public int VentaId
        {
            get { return _ventaId; }
            set { _ventaId = value; }
        }
        private string _codigoRespuesta;
        public string CodigoRespuesta
        {
            get { return _codigoRespuesta; }
            set { _codigoRespuesta = value; }
        }
        private int _exito;
        public int Exito
        {
            get { return _exito; }
            set { _exito = value; }
        }
        private string _mensajeError;
        public string MensajeError
        {
            get { return _mensajeError; }
            set { _mensajeError = value; }
        }
        private string _mensajeRespuesta;
        public string MensajeRespuesta
        {
            get { return _mensajeRespuesta; }
            set { _mensajeRespuesta = value; }
        }
        private string _nombreArchivo;
        public string NombreArchivo
        {
            get { return _nombreArchivo; }
            set { _nombreArchivo = value; }
        }
        private string _pila;
        public string Pila
        {
            get { return _pila; }
            set { _pila = value; }
        }
        private string _tramaZipCdr;
        public string TramaZipCdr
        {
            get { return _tramaZipCdr; }
            set { _tramaZipCdr = value; }
        }
        private string _nroTicket;
        public string NroTicket
        {
            get { return _nroTicket; }
            set { _nroTicket = value; }
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
    }
}