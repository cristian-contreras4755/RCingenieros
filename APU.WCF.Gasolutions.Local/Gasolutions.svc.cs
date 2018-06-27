using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using APU.Entidad;
using APU.Negocio;

namespace APU.WCF.Gasolutions.Local
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Gasolutions : IGasolutions
    {
        public List<VentaGasolutionsInfo> VentaGasolutions_ListarCodigo(int ventaGasolutionsId)
        {
            return new VentaGasolutions().Listar(ventaGasolutionsId);
        }
        public List<VentaGasolutionsInfo> VentaGasolutions_ListarPaginado(int ventaId, string numeroDocumento, string tipoComprobanteId, string numeroComprobante, DateTime fechaInicio, DateTime fechaFin, int estadoId, int monedaId, int tamanioPagina, int numeroPagina)
        {
            return new VentaGasolutions().ListarPaginado(ventaId, numeroDocumento, tipoComprobanteId, numeroComprobante, fechaInicio, fechaFin, estadoId, monedaId, tamanioPagina, numeroPagina);
        }
        public int VentaGasolutions_Insertar(VentaGasolutionsInfo ventaInfo)
        {
            return new VentaGasolutions().Insertar(ventaInfo);
        }
        public int VentaGasolutions_InsertarLote(List<VentaGasolutionsInfo> ventaListaInfo)
        {
            return new VentaGasolutions().InsertarLote(ventaListaInfo);
        }
        public string VentaGasolutions_InsertarXml(string ventaXml)
        {
            return new VentaGasolutions().Insertar(ventaXml);
        }
        public string VentaGasolutions_InsertarGnvXml(string ventaXml)
        {
            return new VentaGasolutions().InsertarGnv(ventaXml);
        }
        public Facturacion GenerarDocumento(Documento documento)
        {
            return new VentaGasolutions().GenerarDocumento(documento);
        }
        public string VentaGasolutions_InsertarNotaCreditoXml(string notaCreditoXml)
        {
            return new VentaGasolutions().InsertarNotaCredito(notaCreditoXml);
        }
        public string VentaGasolutions_InsertarNotaDebitoXml(string notaDebitoXml)
        {
            return new VentaGasolutions().InsertarNotaDebito(notaDebitoXml);
        }
        //public string GetData(int value)
        //{
        //    return string.Format("You entered: {0}", value);
        //}

        //public CompositeType GetDataUsingDataContract(CompositeType composite)
        //{
        //    if (composite == null)
        //    {
        //        throw new ArgumentNullException("composite");
        //    }
        //    if (composite.BoolValue)
        //    {
        //        composite.StringValue += "Suffix";
        //    }
        //    return composite;
        //}
    }
}
