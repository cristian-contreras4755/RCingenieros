using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using APU.Entidad;

namespace APU.WCF.Petroamerica
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IPetroamerica
    {
        // TODO: agregue aquí sus operaciones de servicio

        #region VentaPetroamerica
        [OperationContract]
        List<VentaPetroamericaInfo> VentaPetroamerica_ListarCodigo(int ventaPetroamericaId, int ventaId);
        List<VentaPetroamericaInfo> VentaPetroamerica_ListarPaginado(int ventaId, string numeroDocumento, string tipoComprobanteId, string serie, string correlativo, DateTime fechaInicio, DateTime fechaFin, int estadoId, int monedaId, string agencia, int tamanioPagina, int numeroPagina);
        [OperationContract]
        int VentaPetroamerica_Insertar(VentaPetroamericaInfo ventaPetroamericaInfo);
        [OperationContract]
        int VentaPetroamerica_InsertarLote(List<VentaPetroamericaInfo> ventaPetroamericaInfo);
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