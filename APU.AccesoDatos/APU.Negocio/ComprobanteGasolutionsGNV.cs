using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APU.Negocio
{
    public class Documento
    {
        public int TipoDocumento { get; set; }
        public DateTime FechaEmision { get; set; 
        }
        public string RucEmisor{ get; set; }
        public string Numeracion { get; set; }
        public int TipoDocumentoAdquirente { get; set; }
        public string NumeroDocumentoAdquirente { get;  set; }
        public string NombreAdquirente{ get; set; }
        public string DireccionAdquirente { get; set; }
        public string UnidadItem{ get; set; }
        public decimal CantidadItem { get; set; }
        public string DescripcionItem { get; set; }
        public string CodigoPrecioUnitario { get; set; }
        public decimal PrecioUnitario { get; set; }
        // operacion
        public List<Operacion> Operacion { get; set; }
        // tributos
        public List<Tributo> Tributo { get; set; }
        public decimal OtrosCargos { get; set; }
        public decimal TotalVenta { get; set; }
        public string TipoMoneda { get; set; }
        // item
        public List<Item> Item { get; set; }
        // leyenda
        public string CodigoNotaCreditoDebito { get; set; }
        public string DocumentoAfectado { get; set; }
        public string TipoDocumentoAfectado { get; set; }
        public string MotivoSustento { get; set; }
        // Venta
        public List<VentaGnv> Venta { get; set; }
    }
    public class Item
    {
        public int CodigoProducto { get; set; }
        public decimal Valor { get; set; }
    }
    public class Leyenda
    {
        public int TipoLeyenda { get; set; }
        public string Descripcion { get; set; }
    }
    public class Tributo
    {
        public int TipoTributo { get; set; }
        public decimal Valor { get; set; }
    }
    public class Operacion
    {
        public int TipoOperacion { get; set; }
        public decimal Monto { get; set; }
    }
    public class VentaGnv
    {
        public string MaquinaRegistradora { get; set; }
        public string Hora { get; set; }
        public int Turno { get; set; }
        public int Cara { get; set; }
        public int Isla { get; set; }
        public int Manguera { get; set; }
        public int Recibo { get; set; }
        public string FechaHora { get; set; }
        public string FechaProximoMantenimiento { get; set; }
        public string FechaRevisionCilindro { get; set; }
    }
    public class Facturacion
    {
        public string MensajeError { get; set; }
        public bool SolicitudProcesada { get; set; }
        public string ValorResumen { get; set; }
        public string ValorFirma { get; set; }
    }
}