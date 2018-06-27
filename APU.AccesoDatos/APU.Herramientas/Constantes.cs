using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APU.Herramientas
{
    public static class Constantes
    {
        #region Tipos Excepcion
        public static readonly string ExcepcionPoliticaAccesoDatos = "Politica ExcepcionAccesoDatos";
        public static readonly string ExcepcionPoliticaLogicaNegocio = "Politica ExcepcionLogicaNegocio";
        public static readonly string ExcepcionPoliticaPresentacion = "Politica ExcepcionPresentacion";
        #endregion

        #region Eventos JavaScript
        public static readonly string EventoOnClick = "OnClick";
        public static readonly string EventoOnBlur = "OnBlur";
        public static readonly string EventoOnChange = "OnChange";
        public static readonly string EventoOnDblClick = "OnDblClick";
        public static readonly string EventoOnFocus = "OnFocus";
        public static readonly string EventoOnKeyDown = "OnKeyDown";
        public static readonly string EventoOnKeyPress = "OnKeyPress";
        public static readonly string EventoOnKeyUp = "OnKeyUp";
        public static readonly string EventoOnMouseDown = "OnMouseDown";
        public static readonly string EventoOnMouseMove = "OnMouseMove";
        public static readonly string EventoOnMouseOut = "OnMouseOut";
        public static readonly string EventoOnMouseOver = "OnMouseOver";
        public static readonly string EventoOnMouseUp = "OnMouseUp";
        public static readonly string EventoOnPaste = "OnPaste";
        public static readonly string EventoOnReset = "OnReset";
        public static readonly string EventoOnResize = "OnResize";
        public static readonly string EventoOnSelect = "OnSelect";
        public static readonly string EventoOnSubmit = "OnSubmit";
        public static readonly string EventoOnUnload = "OnUnload";
        #endregion

        #region TablaMaestra
        public static readonly int TablaSexo = 1;
        public static readonly int TablaEstadoCivil = 2;

        public static readonly int TablaTipoDocumento = 3;
        public static readonly int TipoDocumentoDni = 1;
        public static readonly int TipoDocumentoCarnetExtranjeria = 4;
        public static readonly int TipoDocumentoRuc = 6;
        public static readonly int TipoDocumentoPasaporte = 7;

        public static readonly int TablaCargo = 4;
        public static readonly int TablaTamanioPagina = 5;

        public static readonly int TablaTipoUbigeo = 6;
        public static readonly int TipoUbigeoDepartamento = 1;
        public static readonly int TipoUbigeoProvincia = 2;
        public static readonly int TipoUbigeoDistrito = 3;

        public static readonly int TablaTipoPersona = 7;
        public static readonly int TipoPersonaNatural = 1;
        public static readonly int TipoPersonaJuridica = 2;

        public static readonly int TablaTipoComprobante = 8;
		public static readonly int TablaTipoComprobanteCompra = 12;
		public static readonly string TipoComprobanteFactura = "01";
        public static readonly string TipoComprobanteBoletaVenta = "03";
        public static readonly string TipoComprobanteNotaCredito = "07";
        public static readonly string TipoComprobanteNotaDebito = "08";
		public static readonly string TipoComprobanteCompraGuiaRemision = "09";
		public static readonly string TipoComprobanteCompraFactura = "01";

		public static readonly int TablaMoneda = 9;
        public static readonly int MonedaSoles = 1;
        public static readonly int MonedaDolares = 2;

        public static readonly string MonedaSolesSunat = "PEN";
        public static readonly string MonedaDolaresSunat = "USD";

        public static readonly int TablaSerie = 10;

        public static readonly int TablaUnidades = 11;

        public static readonly int TablaTipoCotizacion = 13;
        public static readonly int TipoCotizacionLibre = 1;
        public static readonly int TipoCotizacionPreferencial = 2;
        public static readonly int TipoCotizacionSbs = 3;
        public static readonly int TipoCotizacionEspecial = 4;

        public static readonly int TablaTipoImpuestoIgv = 17;

        public static readonly int TablaTipoSistemaCalculoIsc = 20;

        public static readonly int TablaTipoNegocio = 24;
        #endregion

        public static readonly string Seleccione = "-Seleccione-";
        public static readonly string Seleccione_Value = "0";

        public static readonly int PaisPeru = 203;

		#region Nivel Tipo Producto

		public static readonly int PrimerNivelProducto = 1;
		public static readonly int SegundoNivelProducto = 2;
		public static readonly int TercerNivelProducto = 3;

		#endregion

		public static readonly int PerfilAdministrador = 1;

        public static readonly int EstadoComprobanteAceptado = 1;
        public static readonly int EstadoComprobanteRechazado = 2;
        public static readonly int EstadoComprobantePendiente = 3;
        public static readonly int EstadoComprobanteAnulado = 4;

		public static readonly int TablaFormaPago = 14;

		public const int FormaPagoCompraEfectivo = 1;

		public static readonly int EstadoComprobanteCompraIngresado = 1;
		public static readonly int EstadoComprobanteCompraPagado = 2;
		public static readonly int EstadoComprobanteCompraAnulado = 3;

		public static readonly int TablaMotivoIngresoCompra = 22;

		public const int MotivoIngresoCompraPorVenta = 1;

		public static readonly int TablaTipoMovimiento = 23;

		public const int TipoMovimientoCompra = 1;
		public const int TipoMovimientoVenta = 2;		
		public const int TipoMovimientoTrasladoEgreso = 3;
		public const int TipoMovimientoTrasladoIngreso = 4;

        public static readonly int TablaTipoNotaCredito = 25;
        public static readonly int TablaTipoNotaDebito = 26;
    }
}