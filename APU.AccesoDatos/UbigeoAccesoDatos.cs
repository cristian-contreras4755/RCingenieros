using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.AccesoDatos
{
    public class UbigeoAccesoDatos
    {
        public List<UbigeoInfo> Listar(int codigoUbigeo, int codigoUbigeoPadre, int tipoUbigeo)
        {
            var dealerListaInfo = new List<UbigeoInfo>(); ;
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerUbigeo";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("UbigeoId", SqlDbType.Int).Value = codigoUbigeo;
                sqlComando.Parameters.Add("UbigeoPadreId", SqlDbType.Int).Value = codigoUbigeoPadre;
                sqlComando.Parameters.Add("TipoUbigeoId", SqlDbType.Int).Value = tipoUbigeo;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        dealerListaInfo.Add(CargarUbigeoInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return dealerListaInfo;
        }
        public List<UbigeoInfo> ListarUbigeo(int codigoDepartamento, int codigoProvincia, int codigoDistrito, int tamanioPagina, int numeroPagina)
        {
            var ubigeoListaInfo = new List<UbigeoInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerUbigeoPaginacion";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("DepartamentoId", SqlDbType.Int).Value = codigoDepartamento;
                sqlComando.Parameters.Add("ProvinciaId", SqlDbType.Int).Value = codigoProvincia;
                sqlComando.Parameters.Add("DistritoId", SqlDbType.Int).Value = codigoDistrito;
                sqlComando.Parameters.Add("PageSize", SqlDbType.Int).Value = tamanioPagina;
                sqlComando.Parameters.Add("PageNumber", SqlDbType.Int).Value = numeroPagina;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ubigeoListaInfo.Add(CargarUbigeoPaginacionInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return ubigeoListaInfo;
        }
        private static UbigeoInfo CargarUbigeoPaginacionInfo(IDataReader dr)
        {
            int colCount = dr.FieldCount;
            var values = new object[colCount];

            #region Indices
            int indDepartamentoId = dr.GetOrdinal("DepartamentoId");
            int indCodigoDepartamento = dr.GetOrdinal("CodigoDepartamento");
            int indDepartamento = dr.GetOrdinal("Departamento");
            int indUbigeoPadreDepartamentoId = dr.GetOrdinal("UbigeoPadreDepartamentoId");
            int indTipoUbigeoDepartamentoId = dr.GetOrdinal("TipoUbigeoDepartamentoId");
            int indTipoUbigeoDepartamento = dr.GetOrdinal("TipoUbigeoDepartamento");

            int indProvinciaId = dr.GetOrdinal("ProvinciaId");
            int indCodigoProvincia = dr.GetOrdinal("CodigoProvincia");
            int indProvincia = dr.GetOrdinal("Provincia");
            int indUbigeoPadreProvinciaId = dr.GetOrdinal("UbigeoPadreProvinciaId");
            int indTipoUbigeoProvinciaId = dr.GetOrdinal("TipoUbigeoProvinciaId");
            int indTipoUbigeoProvincia = dr.GetOrdinal("TipoUbigeoProvincia");

            int indDistritoId = dr.GetOrdinal("DistritoId");
            int indCodigoDistrito = dr.GetOrdinal("CodigoDistrito");
            int indDistrito = dr.GetOrdinal("Distrito");
            int indUbigeoPadreDistritoId = dr.GetOrdinal("UbigeoPadreDistritoId");
            int indTipoUbigeoDistritoId = dr.GetOrdinal("TipoUbigeoDistritoId");
            int indTipoUbigeoDistrito = dr.GetOrdinal("TipoUbigeoDistrito");

            int indNumeroFila = dr.GetOrdinal("NumeroFila");
            int indTotalFilas = dr.GetOrdinal("TotalFilas");
            #endregion

            var ubigeoInfo = new UbigeoInfo();
            dr.GetValues(values);

            #region Campos
            ubigeoInfo.DepartamentoId = Convert.ToInt32(values[indDepartamentoId]);
            ubigeoInfo.CodigoDepartamento = Convert.ToString(values[indCodigoDepartamento]);
            ubigeoInfo.Departamento = Convert.ToString(values[indDepartamento]);
            ubigeoInfo.UbigeoPadreDepartamentoId = Convert.ToInt32(values[indUbigeoPadreDepartamentoId]);
            ubigeoInfo.TipoUbigeoDepartamentoId = Convert.ToInt32(values[indTipoUbigeoDepartamentoId]);
            ubigeoInfo.TipoUbigeoDepartamento = Convert.ToString(values[indTipoUbigeoDepartamento]);

            ubigeoInfo.ProvinciaId = Convert.ToInt32(values[indProvinciaId]);
            ubigeoInfo.CodigoProvincia = Convert.ToString(values[indCodigoProvincia]);
            ubigeoInfo.Provincia = Convert.ToString(values[indProvincia]);
            ubigeoInfo.UbigeoPadreProvinciaId = Convert.ToInt32(values[indUbigeoPadreProvinciaId]);
            ubigeoInfo.TipoUbigeoProvinciaId = Convert.ToInt32(values[indTipoUbigeoProvinciaId]);
            ubigeoInfo.TipoUbigeoProvincia = Convert.ToString(values[indTipoUbigeoProvincia]);

            ubigeoInfo.DistritoId = Convert.ToInt32(values[indDistritoId]);
            ubigeoInfo.CodigoDistrito = Convert.ToString(values[indCodigoDistrito]);
            ubigeoInfo.Distrito = Convert.ToString(values[indDistrito]);
            ubigeoInfo.UbigeoPadreDistritoId = Convert.ToInt32(values[indUbigeoPadreDistritoId]);
            ubigeoInfo.TipoUbigeoDistritoId = Convert.ToInt32(values[indTipoUbigeoDistritoId]);
            ubigeoInfo.TipoUbigeoDistrito = Convert.ToString(values[indTipoUbigeoDistrito]);

            ubigeoInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
            ubigeoInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);
            #endregion

            return ubigeoInfo;
        }
        public List<UbigeoInfo> ListarDep(int codigoUbigeo)
        {
            var ubigeoListaInfo = new List<UbigeoInfo>(); ;
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerUbigeoDep";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("UbigeoId", SqlDbType.Int).Value = codigoUbigeo;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ubigeoListaInfo.Add(CargarUbigeoDepInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return ubigeoListaInfo;
        }
        public List<UbigeoInfo> ListarProv(int codigoUbigeo)
        {
            var ubigeoListaInfo = new List<UbigeoInfo>(); ;
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerUbigeoProv";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("UbigeoId", SqlDbType.Int).Value = codigoUbigeo;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ubigeoListaInfo.Add(CargarUbigeoProvInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return ubigeoListaInfo;
        }
        private static UbigeoInfo CargarUbigeoInfo(IDataReader dr)
        {
            int colCount = dr.FieldCount;
            var values = new object[colCount];

            #region Indices
            int indUbigeoId = dr.GetOrdinal("UbigeoId");
            int indCodigo = dr.GetOrdinal("Codigo");
            int indNombre = dr.GetOrdinal("Nombre");
            int indUbigeoPadreId = dr.GetOrdinal("UbigeoPadreId");
            int indTipoUbigeoId = dr.GetOrdinal("TipoUbigeoId");
            #endregion

            var ubigeoInfo = new UbigeoInfo();
            dr.GetValues(values);

            #region Campos
            ubigeoInfo.UbigeoId = Convert.ToInt32(values[indUbigeoId]);
            ubigeoInfo.Codigo = Convert.ToString(values[indCodigo]);
            ubigeoInfo.Nombre = Convert.ToString(values[indNombre]);
            ubigeoInfo.UbigeoPadreId = Convert.ToInt16(values[indUbigeoPadreId]);
            ubigeoInfo.TipoUbigeoId = Convert.ToInt16(values[indTipoUbigeoId]);
            #endregion

            return ubigeoInfo;
        }
        private static UbigeoInfo CargarUbigeoPagInfo(IDataReader dr)
        {
            int colCount = dr.FieldCount;
            var values = new object[colCount];

            #region Indices
            int indUbigeoId = dr.GetOrdinal("UbigeoId");
            int indCodDepartamento = dr.GetOrdinal("CodDepartamento");
            int indDepartamento = dr.GetOrdinal("Departamento");
            int indCodProvincia = dr.GetOrdinal("CodProvincia");
            int indProvincia = dr.GetOrdinal("Provincia");
            int indCodDistrito = dr.GetOrdinal("CodDistrito");
            int indDistrito = dr.GetOrdinal("Distrito");
            int indNumeroFila = dr.GetOrdinal("NumeroFila");
            int indTotalFilas = dr.GetOrdinal("TotalFilas");
            #endregion

            var ubigeoInfo = new UbigeoInfo();
            dr.GetValues(values);

            #region Campos
            ubigeoInfo.UbigeoId = Convert.ToInt32(values[indUbigeoId]);
            ubigeoInfo.CodDepartamento = Convert.ToInt32(values[indCodDepartamento]);
            ubigeoInfo.Departamento = Convert.ToString(values[indDepartamento]);
            ubigeoInfo.CodProvincia = Convert.ToInt32(values[indCodProvincia]);
            ubigeoInfo.Provincia = Convert.ToString(values[indProvincia]);
            ubigeoInfo.CodDistrito = Convert.ToInt32(values[indCodDistrito]);
            ubigeoInfo.Distrito = Convert.ToString(values[indDistrito]);
            ubigeoInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
            ubigeoInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);
            #endregion

            return ubigeoInfo;
        }
        private static UbigeoInfo CargarUbigeoDepInfo(IDataReader dr)
        {
            int colCount = dr.FieldCount;
            var values = new object[colCount];

            #region Indices
            int indUbigeoId = dr.GetOrdinal("UbigeoId");
            int Nombre = dr.GetOrdinal("Nombre");

            #endregion

            var ubigeoInfo = new UbigeoInfo();
            dr.GetValues(values);

            #region Campos
            ubigeoInfo.UbigeoId = Convert.ToInt32(values[indUbigeoId]);
            ubigeoInfo.Nombre = Convert.ToString(values[Nombre]);

            #endregion

            return ubigeoInfo;
        }
        private static UbigeoInfo CargarUbigeoProvInfo(IDataReader dr)
        {
            int colCount = dr.FieldCount;
            var values = new object[colCount];

            #region Indices
            int indUbigeoId = dr.GetOrdinal("UbigeoId");
            int Provincia = dr.GetOrdinal("Provincia");

            #endregion

            var ubigeoInfo = new UbigeoInfo();
            dr.GetValues(values);

            #region Campos
            ubigeoInfo.UbigeoId = Convert.ToInt32(values[indUbigeoId]);
            ubigeoInfo.Provincia = Convert.ToString(values[Provincia]);

            #endregion

            return ubigeoInfo;
        }
    }
}