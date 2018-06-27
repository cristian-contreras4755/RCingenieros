using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using APU.Entidad;
using APU.Negocio;

namespace APU.WCF.Gasolutions
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IGasolutions
    {
        // TODO: agregue aquí sus operaciones de servicio

        #region VentaGasolutions
        [OperationContract]
        List<VentaGasolutionsInfo> VentaGasolutions_ListarCodigo(int ventaGasolutionsId);
        List<VentaGasolutionsInfo> VentaGasolutions_ListarPaginado(int ventaId, string numeroDocumento, string tipoComprobanteId, string numeroComprobante, DateTime fechaInicio, DateTime fechaFin, int estadoId, int monedaId, int tamanioPagina, int numeroPagina);
        [OperationContract]
        int VentaGasolutions_Insertar(VentaGasolutionsInfo ventaPetroamericaInfo);
        [OperationContract]
        int VentaGasolutions_InsertarLote(List<VentaGasolutionsInfo> ventaPetroamericaInfo);
        [OperationContract]
        string VentaGasolutions_InsertarXml(string ventaXml);

        [OperationContract]
        string VentaGasolutions_InsertarGnvXml(string ventaXml);
        [OperationContract]
        Facturacion GenerarDocumento(Documento documento);

        [OperationContract]
        string VentaGasolutions_InsertarNotaCreditoXml(string notaCreditoXml);
        [OperationContract]
        string VentaGasolutions_InsertarNotaDebitoXml(string notaDebitoXml);
        #endregion
    }

    // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
