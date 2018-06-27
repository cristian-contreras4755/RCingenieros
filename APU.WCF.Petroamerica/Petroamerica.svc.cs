using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using APU.Entidad;
using APU.Negocio;

namespace APU.WCF.Petroamerica
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Petroamerica : IPetroamerica
    {
        public List<VentaPetroamericaInfo> VentaPetroamerica_ListarCodigo(int ventaPetroamericaId, int ventaId)
        {
            return new VentaPetroamerica().Listar(ventaPetroamericaId, ventaId);
        }
        public List<VentaPetroamericaInfo> VentaPetroamerica_ListarPaginado(int ventaId, string numeroDocumento, string tipoComprobanteId, string serie, string correlativo, DateTime fechaInicio, DateTime fechaFin, int estadoId, int monedaId, string agencia, int tamanioPagina, int numeroPagina)
        {
            return new VentaPetroamerica().ListarPaginado(ventaId, numeroDocumento, tipoComprobanteId, serie, correlativo, fechaInicio, fechaFin, estadoId, monedaId, agencia, tamanioPagina, numeroPagina);
        }
        public int VentaPetroamerica_Insertar(VentaPetroamericaInfo ventaInfo)
        {
                return new VentaPetroamerica().Insertar(ventaInfo);
        }
        public int VentaPetroamerica_InsertarLote(List<VentaPetroamericaInfo> ventaListaInfo)
        {
            return new VentaPetroamerica().InsertarLote(ventaListaInfo);
        }
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}