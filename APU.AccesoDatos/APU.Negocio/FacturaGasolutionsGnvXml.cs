using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APU.Negocio
{
    //class FacturaGasolutionsGnvXml
    //{

    //}

    // NOTA: El código generado puede requerir, como mínimo, .NET Framework 4.5 o .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public partial class Envelope
    {
        private EnvelopeBody bodyField;
        /// <remarks/>
        public EnvelopeBody Body
        {
            get
            {
                return this.bodyField;
            }
            set
            {
                this.bodyField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public partial class EnvelopeBody
    {
        private GenerarDocumento generarDocumentoField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://tempuri.org/")]
        public GenerarDocumento GenerarDocumento
        {
            get
            {
                return this.generarDocumentoField;
            }
            set
            {
                this.generarDocumentoField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://tempuri.org/", IsNullable = false)]
    public partial class GenerarDocumento
    {
        private GenerarDocumentoODocumento oDocumentoField;

        /// <remarks/>
        public GenerarDocumentoODocumento oDocumento
        {
            get
            {
                return this.oDocumentoField;
            }
            set
            {
                this.oDocumentoField = value;
            }
        }
    }
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/")]
    public partial class GenerarDocumentoODocumento
    {
        private decimal cantidadItemField;
        private object codigoNotaCreditoDebitoField;
        private byte codigoPrecioUnitarioField;
        private string descripcionItemField;
        private string direccionAdquirienteField;
        private byte documentoAfectadoField;
        private System.DateTime fechaEmisionField;
        private item itemField;
        private leyenda leyendaField;
        private object motivoSustentoField;
        private string nombreAdquirienteField;
        private string numeracionField;
        private string numeroDocumentoAdquirienteField;
        private operacion operacionField;
        private byte otrosCargosField;
        private decimal precioUnitarioField;
        private string rucEmisorField;
        private string tipoDocumentoField;
        private string tipoDocumentoAdquirienteField;
        private byte tipoDocumentoAfectadoField;
        private string tipoMonedaField;
        private decimal totalVentaField;
        private tributos tributosField;
        private string unidadItemField;
        private venta ventaField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
        public decimal cantidadItem
        {
            get
            {
                return this.cantidadItemField;
            }
            set
            {
                this.cantidadItemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
        public object codigoNotaCreditoDebito
        {
            get
            {
                return this.codigoNotaCreditoDebitoField;
            }
            set
            {
                this.codigoNotaCreditoDebitoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
        public byte codigoPrecioUnitario
        {
            get
            {
                return this.codigoPrecioUnitarioField;
            }
            set
            {
                this.codigoPrecioUnitarioField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
        public string descripcionItem
        {
            get
            {
                return this.descripcionItemField;
            }
            set
            {
                this.descripcionItemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
        public string direccionAdquiriente
        {
            get
            {
                return this.direccionAdquirienteField;
            }
            set
            {
                this.direccionAdquirienteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
        public byte documentoAfectado
        {
            get
            {
                return this.documentoAfectadoField;
            }
            set
            {
                this.documentoAfectadoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
        public System.DateTime fechaEmision
        {
            get
            {
                return this.fechaEmisionField;
            }
            set
            {
                this.fechaEmisionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
        public item item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
        public leyenda leyenda
        {
            get
            {
                return this.leyendaField;
            }
            set
            {
                this.leyendaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
        public object motivoSustento
        {
            get
            {
                return this.motivoSustentoField;
            }
            set
            {
                this.motivoSustentoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
        public string nombreAdquiriente
        {
            get
            {
                return this.nombreAdquirienteField;
            }
            set
            {
                this.nombreAdquirienteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
        public string numeracion
        {
            get
            {
                return this.numeracionField;
            }
            set
            {
                this.numeracionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
        public string numeroDocumentoAdquiriente
        {
            get
            {
                return this.numeroDocumentoAdquirienteField;
            }
            set
            {
                this.numeroDocumentoAdquirienteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
        public operacion operacion
        {
            get
            {
                return this.operacionField;
            }
            set
            {
                this.operacionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
        public byte otrosCargos
        {
            get
            {
                return this.otrosCargosField;
            }
            set
            {
                this.otrosCargosField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
        public decimal precioUnitario
        {
            get
            {
                return this.precioUnitarioField;
            }
            set
            {
                this.precioUnitarioField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
        public string rucEmisor
        {
            get
            {
                return this.rucEmisorField;
            }
            set
            {
                this.rucEmisorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
        public string tipoDocumento
        {
            get
            {
                return this.tipoDocumentoField;
            }
            set
            {
                this.tipoDocumentoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
        public string tipoDocumentoAdquiriente
        {
            get
            {
                return this.tipoDocumentoAdquirienteField;
            }
            set
            {
                this.tipoDocumentoAdquirienteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
        public byte tipoDocumentoAfectado
        {
            get
            {
                return this.tipoDocumentoAfectadoField;
            }
            set
            {
                this.tipoDocumentoAfectadoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
        public string tipoMoneda
        {
            get
            {
                return this.tipoMonedaField;
            }
            set
            {
                this.tipoMonedaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
        public decimal totalVenta
        {
            get
            {
                return this.totalVentaField;
            }
            set
            {
                this.totalVentaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
        public tributos tributos
        {
            get
            {
                return this.tributosField;
            }
            set
            {
                this.tributosField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
        public string unidadItem
        {
            get
            {
                return this.unidadItemField;
            }
            set
            {
                this.unidadItemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
        public venta venta
        {
            get
            {
                return this.ventaField;
            }
            set
            {
                this.ventaField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE", IsNullable = false)]
    public partial class item
    {

        private itemItem itemField;

        /// <remarks/>
        public itemItem Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
    public partial class itemItem
    {
        private string codigoProductoField;
        private decimal valorField;

        /// <remarks/>
        public string codigoProducto
        {
            get
            {
                return this.codigoProductoField;
            }
            set
            {
                this.codigoProductoField = value;
            }
        }

        /// <remarks/>
        public decimal valor
        {
            get
            {
                return this.valorField;
            }
            set
            {
                this.valorField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE", IsNullable = false)]
    public partial class leyenda
    {

        private leyendaLeyenda leyendaField;

        /// <remarks/>
        public leyendaLeyenda Leyenda
        {
            get
            {
                return this.leyendaField;
            }
            set
            {
                this.leyendaField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
    public partial class leyendaLeyenda
    {

        private string descripcionField;

        private ushort tipoLeyendaField;

        /// <remarks/>
        public string descripcion
        {
            get
            {
                return this.descripcionField;
            }
            set
            {
                this.descripcionField = value;
            }
        }

        /// <remarks/>
        public ushort tipoLeyenda
        {
            get
            {
                return this.tipoLeyendaField;
            }
            set
            {
                this.tipoLeyendaField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE", IsNullable = false)]
    public partial class operacion
    {

        private operacionOperacion operacionField;

        /// <remarks/>
        public operacionOperacion Operacion
        {
            get
            {
                return this.operacionField;
            }
            set
            {
                this.operacionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
    public partial class operacionOperacion
    {

        private decimal montoField;

        private byte tipoOperacionField;

        /// <remarks/>
        public decimal monto
        {
            get
            {
                return this.montoField;
            }
            set
            {
                this.montoField = value;
            }
        }

        /// <remarks/>
        public byte tipoOperacion
        {
            get
            {
                return this.tipoOperacionField;
            }
            set
            {
                this.tipoOperacionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE", IsNullable = false)]
    public partial class tributos
    {

        private tributosTributo tributoField;

        /// <remarks/>
        public tributosTributo Tributo
        {
            get
            {
                return this.tributoField;
            }
            set
            {
                this.tributoField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
    public partial class tributosTributo
    {

        private byte tipoTributoField;

        private decimal valorField;

        /// <remarks/>
        public byte tipoTributo
        {
            get
            {
                return this.tipoTributoField;
            }
            set
            {
                this.tipoTributoField = value;
            }
        }

        /// <remarks/>
        public decimal valor
        {
            get
            {
                return this.valorField;
            }
            set
            {
                this.valorField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE", IsNullable = false)]
    public partial class venta
    {

        private ventaVenta ventaField;

        /// <remarks/>
        public ventaVenta Venta
        {
            get
            {
                return this.ventaField;
            }
            set
            {
                this.ventaField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/ServicioGasFE")]
    public partial class ventaVenta
    {

        private byte caraField;

        private string fechaHoraField;

        private string fechaProximoMantField;

        private object fechaRevisionClindroField;

        private System.DateTime horaField;

        private byte islaField;

        private byte mangueraField;

        private string maquinaRegistradoraField;

        private uint reciboField;

        private byte turnoField;

        /// <remarks/>
        public byte Cara
        {
            get
            {
                return this.caraField;
            }
            set
            {
                this.caraField = value;
            }
        }

        /// <remarks/>
        public string FechaHora
        {
            get
            {
                return this.fechaHoraField;
            }
            set
            {
                this.fechaHoraField = value;
            }
        }

        /// <remarks/>
        public string FechaProximoMant
        {
            get
            {
                return this.fechaProximoMantField;
            }
            set
            {
                this.fechaProximoMantField = value;
            }
        }

        /// <remarks/>
        public object FechaRevisionClindro
        {
            get
            {
                return this.fechaRevisionClindroField;
            }
            set
            {
                this.fechaRevisionClindroField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "time")]
        public System.DateTime Hora
        {
            get
            {
                return this.horaField;
            }
            set
            {
                this.horaField = value;
            }
        }

        /// <remarks/>
        public byte Isla
        {
            get
            {
                return this.islaField;
            }
            set
            {
                this.islaField = value;
            }
        }

        /// <remarks/>
        public byte Manguera
        {
            get
            {
                return this.mangueraField;
            }
            set
            {
                this.mangueraField = value;
            }
        }

        /// <remarks/>
        public string MaquinaRegistradora
        {
            get
            {
                return this.maquinaRegistradoraField;
            }
            set
            {
                this.maquinaRegistradoraField = value;
            }
        }

        /// <remarks/>
        public uint Recibo
        {
            get
            {
                return this.reciboField;
            }
            set
            {
                this.reciboField = value;
            }
        }

        /// <remarks/>
        public byte Turno
        {
            get
            {
                return this.turnoField;
            }
            set
            {
                this.turnoField = value;
            }
        }
    }
}
