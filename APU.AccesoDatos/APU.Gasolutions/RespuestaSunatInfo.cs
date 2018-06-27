using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APU.Gasolutions
{
    [Serializable()]
    public class RespuestaSunatInfo
    {
        private FirmaSunat _firma;
        public FirmaSunat Firma
        {
            get { return this._firma; }
            set { this._firma = value; }
        }

        private RespuestaSunat _respuesta;
        public RespuestaSunat Respuesta
        {
            get { return this._respuesta; }
            set { this._respuesta = value; }
        }

        public RespuestaSunatInfo()
        {
            this.Firma = new FirmaSunat();
            this.Respuesta = new RespuestaSunat();
        }
    }

    public class FirmaSunat
    {
        private bool _exito;
        public bool Exito
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
        private string _pila;
        public string Pila
        {
            get { return _pila; }
            set { _pila = value; }
        }
        private string _resumenFirma;
        public string ResumenFirma
        {
            get { return _resumenFirma; }
            set { _resumenFirma = value; }
        }
        private string _tramaXmlFirmado;
        public string TramaXmlFirmado
        {
            get { return _tramaXmlFirmado; }
            set { _tramaXmlFirmado = value; }
        }
        private string _valorFirma;
        public string ValorFirma
        {
            get { return _valorFirma; }
            set { _valorFirma = value; }
        }
    }

    public class RespuestaSunat
    {
        private string _codigoRespuesta;
        public string CodigoRespuesta
        {
            get { return _codigoRespuesta; }
            set { _codigoRespuesta = value; }
        }
        private bool _exito;
        public bool Exito
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
    }
}