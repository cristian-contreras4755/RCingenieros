using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace APU.Negocio
{
    public static class RestHelper<TRequest, TResponse>
        where TRequest : class
        where TResponse : class, new()
    {

        private static readonly string BaseUrl = ConfigurationManager.AppSettings["APU.Sunat.BaseUrl"];
        private static readonly string UrlSunat = ConfigurationManager.AppSettings["APU.Sunat.UrlSunat"];
        private static readonly string FormatoFecha = ConfigurationManager.AppSettings["APU.Sunat.FormatoFecha"];
        public static TResponse Execute(string metodo, TRequest request)
        {
            //var client = new RestClient("http://localhost:88/OpenInvoicePeru/api");
            var client = new RestClient(BaseUrl);

            var restRequest = new RestRequest(metodo, Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            restRequest.AddBody(request);

            var restResponse = client.Execute<TResponse>(restRequest);
            return restResponse.Data;
        }
    }
}