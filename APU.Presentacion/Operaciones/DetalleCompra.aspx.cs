using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;
using System.Web.UI.WebControls;
using APU.Entidad;
using APU.Herramientas;
using APU.Negocio;

using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.Presentacion.Operaciones
{
    public partial class DetalleCompra : PaginaBase
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!Page.IsPostBack)
            {
                var tipoTiendaId = (Request["TipoTiendaId"] == null) ? 0 : Convert.ToInt32(Request["TipoTiendaId"]);
                var codigoCompra = String.IsNullOrEmpty(Request["CompraId"]) ? 0 : Convert.ToInt32(Request["CompraId"]);
				hdnCompraId.Value = codigoCompra.ToString();
				CargarInicial();
				if (codigoCompra > 0)
				{
					CargarDatos(codigoCompra);
				}
				else
				{
					txtFechaEmision.Text = DateTime.Now.ToString("dd/MM/yyyy");
					ddlMoneda.SelectedValue = Constantes.MonedaSoles.ToString();
					Session["CompraDetalle"] = null;
				}
                
            }
            else
            {
                if (ViewState["PageIndex"] != null)
                {
                    pageIndex = Convert.ToInt32(ViewState["PageIndex"]);
                }
                if (ViewState["PageCount"] != null)
                {
                    pageCount = Convert.ToInt32(ViewState["PageCount"]);
                }
            }
        }
        private void CargarInicial()
        {
			var tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaTipoComprobanteCompra).Where(x => x.Activo.Equals(1)).ToList();
			LlenarCombo(ddlTipoComprobante, tablaMaestraInfo, "Codigo", "NombreLargo");
			ddlTipoComprobante.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));						

			tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaMoneda).Where(x => x.Activo.Equals(1)).ToList();
			LlenarCombo(ddlMoneda, tablaMaestraInfo, "Codigo", "NombreLargo");
			ddlMoneda.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

			//tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaMotivoIngresoCompra).Where(x => x.Activo.Equals(1)).ToList();
			//LlenarCombo(ddlMotivoIngreso, tablaMaestraInfo, "Codigo", "NombreLargo");
			ddlMotivoIngreso.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

			tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaUnidades).Where(x => x.Activo.Equals(1)).ToList();
			LlenarCombo(ddlUnidad, tablaMaestraInfo, "Codigo", "NombreCorto");
			ddlUnidad.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

			var listaAlmacen = new Negocio.Almacen().Listar(0).Where(x => x.Activo.Equals(1)).ToList();
			LlenarCombo(ddlAlmacen, listaAlmacen, "AlmacenId", "Nombre");
			ddlAlmacen.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

		}

        private void CargarDatos(int codigoCompra)
        {
            var usuarioInfo = ObtenerUsuarioInfo();
            var script = new StringBuilder("");

            grvVenta.DataBind();
            int numeroRegistros = 10;
            int numeroPagina = Convert.ToInt16(ViewState["PageIndex"]);

            int indicePagina = numeroPagina == 0 ? 0 : numeroPagina - 1;
            pageIndex = indicePagina + 1;

			#region Cargar Datos Compra
			var compra = new Negocio.Compra().ListarPaginado(codigoCompra, 0, usuarioInfo.TipoNegocioId, 0, 0).First();
			var proveedor = new Negocio.Proveedor().Listar(compra.ProveedorId).First();
			ddlTipoComprobante.SelectedValue = "0" + compra.TipoDocumentoId.ToString();
			txtSerie.Text = compra.NumeroSerie;
			txtNumeroDocumento.Text = compra.NumeroComprobante;

			ddlMoneda.SelectedValue = compra.MonedaId.ToString();

			txtFechaEmision.Text = compra.FechaEmision.ToString("dd/MM/yyyy");
			txtProveedor.Text = proveedor.Nombre;			
			txtDireccion.Text = proveedor.Direccion;
			txtNumeroDocumentoProveedor.Text = proveedor.NumeroDocumento;			
			txtGlosa.Text = compra.Glosa;
			lblComprobanteSubTotal.Text = compra.SubTotal.ToString("###,##0.#0");
			lblComprobanteIgv.Text = compra.Igv.ToString("###,##0.#0");
			lblComprobanteTotal.Text = compra.Total.ToString("###,##0.#0");

			hdnProveedorId.Value = proveedor.ProveedorId.ToString();
			hdnEstadoComprobanteId.Value = compra.EstadoComprobanteId.ToString();

			var tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaMotivoIngresoCompra).ToList();
			if (("0" + compra.TipoDocumentoId.ToString()).Equals("01") || ("0" + compra.TipoDocumentoId.ToString()).Equals("09"))
			{
				tablaMaestraInfo = tablaMaestraInfo.Where(x => x.Codigo == Constantes.MotivoIngresoCompraPorVenta.ToString()).ToList();
			}
			else
			{
				tablaMaestraInfo = tablaMaestraInfo.Where(x => x.Codigo != Constantes.MotivoIngresoCompraPorVenta.ToString()).ToList();
			}
			LlenarCombo(ddlMotivoIngreso, tablaMaestraInfo, "Codigo", "NombreLargo");
			ddlMotivoIngreso.SelectedValue = compra.MotivoIngresoId.ToString();

			if (compra.EstadoComprobanteId == Constantes.EstadoComprobanteCompraAnulado)
			{
				ddlTipoComprobante.Enabled = false;
				txtSerie.ReadOnly = true;
				txtNumeroDocumento.ReadOnly = true;
				ddlMoneda.Enabled = false;
				ddlAlmacen.Enabled = false;
				txtFechaEmision.ReadOnly = true;
				txtProveedor.ReadOnly = true;
				txtGlosa.ReadOnly = true;

				btnAgregarItem.Enabled = false;
				btnCancelar.Enabled = false;				
				ddlMotivoIngreso.Enabled = false;

				lblDetalleCompra.Text = "DETALLE COMPRA";

				script.Append("document.getElementById('lblDetalleCompra').className ='lblTitulo';");
				script.Append("document.getElementById('trDatosProducto').style.display='none';");
				script.Append("document.getElementById('trMontosProducto').style.display='none';");
				script.Append("document.getElementById('trOpcionesItem').style.display='none';");
				script.Append("document.getElementById('imgBuscarCliente').style.display='none';");
				script.Append("document.getElementById('imgFechaEmision').style.display='none';");

				script.Append("document.getElementById('btnGuardar').style.display='none';");
				script.Append("document.getElementById('btnCerrar').style.display='';");
			}
			else
			{
				txtProveedor.ReadOnly = true;
				txtGlosa.ReadOnly = true;
				script.Append("document.getElementById('imgBuscarCliente').style.display='none';");
				script.Append("document.getElementById('btnAnular').style.display='';");
			}

			#endregion

			var compraInfoLista = new Negocio.Compra().ListarComprasDetalle(0, codigoCompra);
            grvVenta.DataSource = compraInfoLista;
            grvVenta.DataBind();
			
			if (compraInfoLista.Count > 0)
            {
                grvVenta.HeaderRow.Attributes["style"] = "display: none";
                grvVenta.UseAccessibleHeader = true;
                grvVenta.HeaderRow.TableSection = TableRowSection.TableHeader;

				Session["CompraDetalle"] = compraInfoLista;				

			}
			else
			{
				Session["CompraDetalle"] = null;
			}
            rowCount = compraInfoLista.Count > 0 ? compraInfoLista.First().TotalFilas : 0;
            pageCount = CalcPageCount(rowCount);
            ViewState["PageCount"] = pageCount;

            #region Texto del Pie de Página
            if (compraInfoLista.Count > 0)
            {
                if (numeroRegistros == 0)
                {
                    lblPaginacion.Text = "Página " + pageIndex.ToString("") + " de 1, con un Total de " + rowCount.ToString("") + " registros";
                    script.Append("document.getElementById('lblPaginacion').innerText = '");
                    script.Append("Página " + pageIndex.ToString("") + " de 1, con un Total de " + rowCount.ToString("") + " registros';");
                }
                else
                {
                    lblPaginacion.Text = "Página " + pageIndex.ToString("") + " de " + pageCount.ToString("") + ", con un Total de " + rowCount.ToString("") + " registros";
                    script.Append("document.getElementById('lblPaginacion').innerText = '");
                    script.Append("Página " + pageIndex.ToString("") + " de " + pageCount.ToString("") + ", con un Total de " + rowCount.ToString("") + " registros';");
                }
            }
            else
            {
                lblPaginacion.Text = "No se obtuvieron resultados";
                script.Append("document.getElementById('lblPaginacion').innerText = 'No se obtuvieron resultados';");
            }
			#endregion			

			RefreshPageButtons();
            RegistrarScript(script.ToString(), "Paginacion");
        }

        private int CalcPageCount(int totalRows)
        {
            int numeroRegistros = 10;
            numeroRegistros = (numeroRegistros == 0) ? totalRows : numeroRegistros;

            numeroRegistros = (numeroRegistros == 0) ? 1 : numeroRegistros;

            int cociente = totalRows % numeroRegistros;

            if (cociente > 0)
            {
                return ((int)(totalRows / numeroRegistros)) + 1;
            }
            else
            {
                return (int)(totalRows / numeroRegistros);
            }
        }
        protected int pageIndex = 1;
        protected int pageCount = 0;
        protected int rowCount = 0;
        private const int CONST_PAGE_SIZE = 5;
        protected void PageChangeEventHandler(object sender, CommandEventArgs e)
        {
            //tipoFiltro = String.IsNullOrEmpty(Request["filtro"]) ? 0 : Convert.ToInt16(Request["filtro"]);
            switch (e.CommandArgument.ToString())
            {
                case "First":
                    pageIndex = 1;
                    break;
                case "Prev":
                    pageIndex = pageIndex - 1;
                    break;
                case "Next":
                    pageIndex = pageIndex + 1;
                    break;
                case "Last":
                    pageIndex = pageCount;
                    break;
            }
            ViewState["PageIndex"] = pageIndex;
            //CargarDatos();
            RefreshPageButtons();
        }
        protected void PageSortingEventHandler(object sender, CommandEventArgs e)
        {
            //tipoFiltro = String.IsNullOrEmpty(Request["filtro"]) ? 0 : Convert.ToInt16(Request["filtro"]);
            switch (e.CommandArgument.ToString())
            {
                case "First":
                    pageIndex = 1;
                    break;
                case "Prev":
                    pageIndex = pageIndex - 1;
                    break;
                case "Next":
                    pageIndex = pageIndex + 1;
                    break;
                case "Last":
                    pageIndex = pageCount;
                    break;
            }
            ViewState["PageIndex"] = pageIndex;
            //CargarDatos();
            RefreshPageButtons();
        }
        private void RefreshPageButtons()
        {
            var script = new StringBuilder("");
            btnPrimero.Disabled = false;
            script.Append("document.getElementById('btnPrimero').disabled = false;");
            script.Append("document.getElementById('btnPrimero').setAttribute('onclick', 'Paginacion(btnPrimero)');");

            btnAnterior.Disabled = false;
            script.Append("document.getElementById('btnAnterior').disabled = false;");
            script.Append("document.getElementById('btnAnterior').setAttribute('onclick', 'Paginacion(btnAnterior)');");
            btnSiguiente.Disabled = false;
            script.Append("document.getElementById('btnSiguiente').disabled = false;");
            script.Append("document.getElementById('btnSiguiente').setAttribute('onclick', 'Paginacion(btnSiguiente)');");
            btnPrimero.Disabled = false;
            script.Append("document.getElementById('btnUltimo').disabled = false;");
            script.Append("document.getElementById('btnUltimo').setAttribute('onclick', 'Paginacion(btnUltimo)');");
            // if this is the first page, disable previous page
            if (pageIndex == 1)
            {
                btnAnterior.Disabled = true;
                script.Append("document.getElementById('btnAnterior').disabled = true;");
                btnPrimero.Disabled = true;
                script.Append("document.getElementById('btnPrimero').disabled = true;");
                //if the page count is more than 1, enable next button               
                if (pageCount <= 1)
                {
                    btnSiguiente.Disabled = true;
                    script.Append("document.getElementById('btnSiguiente').disabled = true;");
                    btnPrimero.Disabled = true;
                    script.Append("document.getElementById('btnUltimo').disabled = true;");
                }
            }
            else
            {
                if (pageIndex == pageCount)
                {
                    btnSiguiente.Disabled = true;
                    script.Append("document.getElementById('btnSiguiente').disabled = true;");
                    btnPrimero.Disabled = true;
                    script.Append("document.getElementById('btnUltimo').disabled = true;");
                }
            }
            RegistrarScript(script.ToString(), "Paginacion");
        }

        protected void ddlNumeroRegistros_SelectedIndexChanged(object sender, EventArgs e)
        {
            // tipoFiltro = String.IsNullOrEmpty(Request["filtro"]) ? 0 : Convert.ToInt16(Request["filtro"]);
            ViewState["PageCount"] = null;
            ViewState["PageIndex"] = null;
            //CargarDatos();
        }

        protected void btnPaginacion_Click(object sender, EventArgs e)
        {
            var accion = hdnCommandArgument.Value;
            switch (accion)
            {
                case "First":
                    pageIndex = 1;
                    break;
                case "Prev":
                    pageIndex = pageIndex - 1;
                    break;
                case "Next":
                    pageIndex = pageIndex + 1;
                    break;
                case "Last":
                    pageIndex = pageCount;
                    break;
            }
            ViewState["PageIndex"] = pageIndex;
            //CargarDatos();
            RefreshPageButtons();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
			RegistrarEventoCliente(txtCantidad, Constantes.EventoOnBlur, "return CalcularMontos();");
			RegistrarEventoCliente(txtPrecio, Constantes.EventoOnBlur, "return CalcularMontos();");
			RegistrarEventoCliente(ddlTipoComprobante, Constantes.EventoOnChange, "return ObtenerMotivoIngreso();");

			Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "tasaIgv", "var tasaIgv =" + Negocio.Helper.ObtenerValorParametro("PORCENTAJE_IGV") + ";", true);
			Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Seleccione", "var Seleccione = '" + Constantes.Seleccione + "';", true);
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Seleccione_value", "var Seleccione_value = '" + Constantes.Seleccione_Value + "';", true);
        }

        #region WebMethod
        [WebMethod]
        public static VentaInfo ObtenerVenta(int ventaId)
        {
            return new Negocio.Venta().Listar(ventaId).FirstOrDefault();
        }

		[WebMethod]
		public static List<TablaMaestraInfo> ObtenerMotivoIngreso(string tipoComprobante)
		{
			var tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaMotivoIngresoCompra).Where(x => x.Activo.Equals(1)).ToList();

			if(tipoComprobante.Equals("01") || tipoComprobante.Equals("09"))
			{
				tablaMaestraInfo = tablaMaestraInfo.Where(x => x.Codigo == Constantes.MotivoIngresoCompraPorVenta.ToString()).ToList();
			}
			else
			{
				tablaMaestraInfo = tablaMaestraInfo.Where(x => x.Codigo != Constantes.MotivoIngresoCompraPorVenta.ToString()).ToList();
			}
			return tablaMaestraInfo;
		}
		#endregion

		protected void grvVenta_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add(Constantes.EventoOnClick, "var fila = SeleccionarFila(this); if (typeof GridView_OnClick == 'function'){GridView_OnClick(fila);}");
                e.Row.Attributes.Add(Constantes.EventoOnDblClick, "var fila = SeleccionarFila(this); if (typeof grvVentas_OnDnlClick == 'function'){grvVentas_OnDnlClick(fila);}");
            }
        }
		[WebMethod]
		public static string GenerarComprobante(int compraId,int proveedorId, string tipoComprobanteId, string serie, string numeroComprobante, string fechaEmision, 
												 int monedaId, decimal montoCompra, decimal montoIgv, decimal montoTotal, string glosa, int motivoIngresoId)
		{
			var mensaje = String.Empty;
			try
			{
				var usuarioInfo = ObtenerUsuarioInfo();			

				#region Compra
				var compraInfo = new CompraInfo();

				compraInfo.CompraId = compraId;
				compraInfo.ProveedorId = proveedorId;
				compraInfo.UsuarioCreacionId = usuarioInfo.UsuarioId;				
				compraInfo.TipoDocumentoId = Convert.ToInt32(tipoComprobanteId);
				compraInfo.NumeroComprobante = numeroComprobante;
				compraInfo.NumeroSerie = serie;
				compraInfo.Glosa = glosa;				
				compraInfo.FechaEmision = DateTime.ParseExact(fechaEmision, "dd/MM/yyyy", null);				
				compraInfo.EstadoComprobanteId = Constantes.EstadoComprobanteCompraIngresado;
				compraInfo.SubTotal = montoCompra;
				compraInfo.Igv = montoIgv;
				compraInfo.Total = montoTotal;
				compraInfo.MotivoIngresoId = motivoIngresoId;
			    compraInfo.TipoNegocioId = usuarioInfo.TipoNegocioId;
                compraInfo.MonedaId = monedaId;

				#endregion
				if (compraId > 0)
				{					
					var compraDetalleListaInfo = (List<ComprasDetalleInfo>)HttpContext.Current.Session["CompraDetalle"]; //detalle compra modificado
					var compraDetalleOriginal = new Negocio.Compra().ListarComprasDetalle(0, compraId);//detalle compra original

					var listaIncrementaInventario = new List<ComprasDetalleInfo>();
					var listaReduceInventario = new List<ComprasDetalleInfo>();
					var listaEliminados = new List<ComprasDetalleInfo>();					

					//var ventaDetalleListaInfo = (List<VentaDetalleInfo>)grvItem.DataSource;
					foreach (var cdModificada in compraDetalleListaInfo)
					{
						var detOriginal = (from cdOriginal in compraDetalleOriginal where cdOriginal.ComprasDetalleId == cdModificada.ComprasDetalleId select cdOriginal);
						if (detOriginal.Count() > 0)
						{
							if (cdModificada.AlmacenId == detOriginal.First().AlmacenId)
							{
								if (cdModificada.ProductoId == detOriginal.First().ProductoId)
								{
									if (cdModificada.Cantidad > detOriginal.First().Cantidad)//Incremento de inventario
									{
										var diferencia = cdModificada.Cantidad - detOriginal.First().Cantidad;
										detOriginal.First().Cantidad = diferencia;
										listaIncrementaInventario.Add(detOriginal.First());
									}
									else if (cdModificada.Cantidad < detOriginal.First().Cantidad)//reduccion de inventario
									{
										var diferencia = cdModificada.Cantidad - detOriginal.First().Cantidad;
										detOriginal.First().Cantidad = Math.Abs(diferencia);
										listaReduceInventario.Add(detOriginal.First());
									}
								}
								else
								{
									//Al ser un producto distinto, el inventario original debe de reducirse y el modificado debe de agregarse
									listaIncrementaInventario.Add(cdModificada);
									listaReduceInventario.Add(detOriginal.First());
								}
							}
							else
							{
								//Al ser un alamcen distinto, no importa el producto, el inventario original debe de reducirse y el modificado debe de agregarse
								listaIncrementaInventario.Add(cdModificada);
								listaReduceInventario.Add(detOriginal.First());
							}
						}
						else
						{
							listaIncrementaInventario.Add(cdModificada);
							//detOriginal.First().Eliminado = 1;
							//listaReduceInventario.Add(detOriginal.First());
							//listaEliminados.Add(detOriginal.First());
						}					
						
					}

					//buscamos los items eliminados de la lista original para reducir inventario y eliminar del detalle
					var codigosDetalleCompra = compraDetalleListaInfo.Where(p => p.ComprasDetalleId > 0).Select(x => x.ComprasDetalleId).ToArray();
					
					listaEliminados.AddRange(compraDetalleOriginal.Where(p => !codigosDetalleCompra.Contains(p.ComprasDetalleId)));
					listaReduceInventario.AddRange(compraDetalleOriginal.Where(p => !codigosDetalleCompra.Contains(p.ComprasDetalleId)));

					var tieneSaldoInventario = true;

					var almacenRevisar = listaReduceInventario.Select(p => p.AlmacenId).Distinct();
					foreach (var almacen in almacenRevisar)
					{
						var productosAlmacen = listaReduceInventario.Where(p => p.AlmacenId == almacen).Select(grp => grp.ProductoId).Distinct();
						foreach (var producto in productosAlmacen)
						{
							var inventarioReducir = listaReduceInventario.Where(p => p.AlmacenId == almacen && p.ProductoId == producto).Sum(x => x.Cantidad);

							//Se consulta a BD si hay saldo
							var saldoActual = new Negocio.Inventario().Listar(almacen, producto, usuarioInfo.TipoNegocioId);
							if(saldoActual.Count()>0 && !((saldoActual.First().InventarioActual - inventarioReducir)> 0))
							{
								tieneSaldoInventario = false;
								break;
							}
						}
						if (!tieneSaldoInventario)
						{
							break;
						}
					}

					if (tieneSaldoInventario)
					{
						new Negocio.Compra().ActualizarCompra(compraInfo);

						#region Actualizacion Compra Detalle
						//Actualizacion de detalle de compra
						foreach (var compraDetalle in compraDetalleListaInfo)
						{
							if (compraDetalle.Eliminado == 0)
							{
								var compraDetalleInfo = new ComprasDetalleInfo();
								compraDetalleInfo.CompraId = compraId;
								compraDetalleInfo.ComprasDetalleId = compraDetalle.ComprasDetalleId;
								compraDetalleInfo.ProductoId = compraDetalle.ProductoId;
								compraDetalleInfo.Cantidad = compraDetalle.Cantidad;
								compraDetalleInfo.PrecioUnitario = compraDetalle.PrecioUnitario;
								compraDetalleInfo.SubTotal = compraDetalle.SubTotal;
								compraDetalleInfo.Igv = compraDetalle.Igv;
								compraDetalleInfo.Total = compraDetalle.Total;
								compraDetalleInfo.AlmacenId = compraDetalle.AlmacenId;

								if (compraDetalle.ComprasDetalleId > 0)
								{
									compraDetalleInfo.UsuarioModificacionId = usuarioInfo.UsuarioId;
									new Negocio.Compra().ActualizarCompraDetalle(compraDetalleInfo);
								}
								else
								{
									compraDetalleInfo.UsuarioCreacionId = usuarioInfo.UsuarioId;
									new Negocio.Compra().InsertarCompraDetalle(compraDetalleInfo);
								}
							}
						}
						//Eliminacion de registros de compra detalle
						foreach (var compraDetalle in listaEliminados)
						{
							new Negocio.Compra().EliminarCompraDetalle(compraDetalle.ComprasDetalleId);
						}

						#endregion

						#region Actualizacion Almacen
						//Incremento de Inventario
						var almacenesAsignados = listaIncrementaInventario.Select(p => p.AlmacenId).Distinct();
						foreach (var almacen in almacenesAsignados)
						{
							var productosAlmacen = listaIncrementaInventario.Where(p => p.AlmacenId == almacen).Select(grp => grp.ProductoId).Distinct();
							foreach (var producto in productosAlmacen)
							{
								var inventarioInfo = new InventarioInfo();
								inventarioInfo.AlmacenId = almacen;
								inventarioInfo.ProductoId = producto;
								inventarioInfo.InventarioActual = listaIncrementaInventario.Where(p => p.AlmacenId == almacen && p.ProductoId == producto).Sum(x => x.Cantidad);
								inventarioInfo.UsuarioCreacionId = usuarioInfo.UsuarioId;
								inventarioInfo.TipoNegocioId = usuarioInfo.TipoNegocioId;
								//Se crea el registro en la tabla inventario
								new Negocio.Inventario().Insertar(inventarioInfo);
							}
						}

						//Reduccion de Inventario
						almacenesAsignados = listaReduceInventario.Select(p => p.AlmacenId).Distinct();
						foreach (var almacen in almacenesAsignados)
						{
							var productosAlmacen = listaReduceInventario.Where(p => p.AlmacenId == almacen).Select(grp => grp.ProductoId).Distinct();
							foreach (var producto in productosAlmacen)
							{
								var inventarioInfo = new InventarioInfo();
								inventarioInfo.AlmacenId = almacen;
								inventarioInfo.ProductoId = producto;
								inventarioInfo.InventarioActual = (-1) * listaReduceInventario.Where(p => p.AlmacenId == almacen && p.ProductoId == producto).Sum(x => x.Cantidad);
								inventarioInfo.UsuarioModificacionId = usuarioInfo.UsuarioId;
								inventarioInfo.TipoNegocioId = usuarioInfo.TipoNegocioId;
								//Se crea el registro en la tabla inventario
								new Negocio.Inventario().Actualizar(inventarioInfo);
							}
						}

						#endregion

						mensaje = compraId + "@" + "El Comprobante se actualizó correctamente.";
					}
					else
					{
						mensaje = "-2" + "@" + "No hay saldo disponible en el Inventario.";
					}

				}
				else
				{
					compraId = new Negocio.Compra().InsertarCompra(compraInfo);

					#region Compra Detalle
					var compraDetalleListaInfo = (List<ComprasDetalleInfo>)HttpContext.Current.Session["CompraDetalle"];
					//var ventaDetalleListaInfo = (List<VentaDetalleInfo>)grvItem.DataSource;
					foreach (var vd in compraDetalleListaInfo)
					{
						var compraDetalleInfo = new ComprasDetalleInfo();
						compraDetalleInfo.CompraId = compraId;
						compraDetalleInfo.ProductoId = vd.ProductoId;
						compraDetalleInfo.Cantidad = vd.Cantidad;
						compraDetalleInfo.PrecioUnitario = vd.PrecioUnitario;
						compraDetalleInfo.SubTotal = vd.SubTotal;
						compraDetalleInfo.Igv = vd.Igv;
						compraDetalleInfo.Total = vd.Total;
						//compraDetalleInfo.AsignacionAlmacen = 1;//por defecto estan asignados
						compraDetalleInfo.AlmacenId = vd.AlmacenId;
						compraDetalleInfo.UsuarioCreacionId = usuarioInfo.UsuarioId;

						new Negocio.Compra().InsertarCompraDetalle(compraDetalleInfo);
					}
					#endregion

					#region Asignacion Almacen
					var almacenesAsignados = compraDetalleListaInfo.Select(p => p.AlmacenId).Distinct();
					foreach (var almacen in almacenesAsignados)
					{
						var productosAlmacen = compraDetalleListaInfo.Where(p => p.AlmacenId == almacen).Select(grp => grp.ProductoId).Distinct();
						foreach (var producto in productosAlmacen)
						{
							var inventarioInfo = new InventarioInfo();
							inventarioInfo.AlmacenId = almacen;
							inventarioInfo.ProductoId = producto;
							inventarioInfo.InventarioActual = compraDetalleListaInfo.Where(p => p.AlmacenId == almacen && p.ProductoId == producto).Sum(x => x.Cantidad);
							inventarioInfo.UsuarioCreacionId = usuarioInfo.UsuarioId;
						    inventarioInfo.TipoNegocioId = usuarioInfo.TipoNegocioId;
                            //Se crea el registro en la tabla inventario
                            new Negocio.Inventario().Insertar(inventarioInfo);
						}
					}

					#endregion

					#region Tabla Movimientos

					var movimientosInfo = new MovimientosInfo();
					movimientosInfo.OperacionId = compraId;
					movimientosInfo.TipoMovimientoId = Constantes.TipoMovimientoCompra;
					movimientosInfo.FechaOperacion = DateTime.Now;
					movimientosInfo.Glosa = String.Empty;
					movimientosInfo.UsuarioCreacionId= usuarioInfo.UsuarioId;

					new Negocio.Movimientos().InsertarMovimientos(movimientosInfo);

					#endregion

					mensaje = compraId + "@" + "El Comprobante se registró correctamente.";
				}
				
			}
			catch (Exception ex)
			{
				bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaPresentacion);
				mensaje = "-1";
				mensaje = mensaje + "@" + (rethrow ? ex.Message : Constantes.ExcepcionPoliticaPresentacion);
			}
			return mensaje;
		}

		[WebMethod]
		public static string AnularComprobante(int compraId)
		{
			var mensaje = String.Empty;
			try
			{
				var usuarioInfo = ObtenerUsuarioInfo();
				var compraDetalleOriginal = new Negocio.Compra().ListarComprasDetalle(0, compraId);//detalle compra original

				var tieneSaldoInventario = true;

				var almacenRevisar = compraDetalleOriginal.Select(p => p.AlmacenId).Distinct();
				foreach (var almacen in almacenRevisar)
				{
					var productosAlmacen = compraDetalleOriginal.Where(p => p.AlmacenId == almacen).Select(grp => grp.ProductoId).Distinct();
					foreach (var producto in productosAlmacen)
					{
						var inventarioReducir = compraDetalleOriginal.Where(p => p.AlmacenId == almacen && p.ProductoId == producto).Sum(x => x.Cantidad);

						//Se consulta a BD si hay saldo
						var saldoActual = new Negocio.Inventario().Listar(almacen, producto, usuarioInfo.TipoNegocioId);
						if (saldoActual.Count() > 0 && !((saldoActual.First().InventarioActual - inventarioReducir) > 0))
						{
							tieneSaldoInventario = false;
							break;
						}
					}
					if (!tieneSaldoInventario)
					{
						break;
					}
				}

				if (tieneSaldoInventario)
				{
					var glosa = "Anulación de comprobante";
					Negocio.Helper.ActualizarColumnasTabla("Compras", new string[] { "EstadoComprobanteId", "Glosa", "UsuarioModificacionId", "FechaModificacion" },
						new string[] { Constantes.EstadoComprobanteCompraAnulado.ToString(), glosa, usuarioInfo.UsuarioId.ToString(), DateTime.Now.ToString("yyyyMMdd HH:mm:ss") },
						new string[] { "CompraId" }, new string[] { compraId.ToString() });

					//Al anular el comprobante, se reduce del inventario
					#region Actualizacion Almacen
					//Reduccion de Inventario
					var almacenesAsignados = compraDetalleOriginal.Select(p => p.AlmacenId).Distinct();

					foreach (var almacen in almacenesAsignados)
					{
						var productosAlmacen = compraDetalleOriginal.Where(p => p.AlmacenId == almacen).Select(grp => grp.ProductoId).Distinct();
						foreach (var producto in productosAlmacen)
						{
							var inventarioInfo = new InventarioInfo();
							inventarioInfo.AlmacenId = almacen;
							inventarioInfo.ProductoId = producto;
							inventarioInfo.InventarioActual = (-1) * compraDetalleOriginal.Where(p => p.AlmacenId == almacen && p.ProductoId == producto).Sum(x => x.Cantidad);
							inventarioInfo.UsuarioCreacionId = usuarioInfo.UsuarioId;
							inventarioInfo.TipoNegocioId = usuarioInfo.TipoNegocioId;
							//Se crea el registro en la tabla inventario
							new Negocio.Inventario().Actualizar(inventarioInfo);
						}
					}

					#endregion

					mensaje = compraId + "@" + "El Comprobante se anuló correctamente.";
				}
				else
				{
					mensaje = "-2" + "@" + "No hay saldo disponible en el Inventario.";
				}
				
			}
			catch (Exception ex)
			{
				bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaPresentacion);
				mensaje = "-1";
				mensaje = mensaje + "@" + (rethrow ? ex.Message : Constantes.ExcepcionPoliticaPresentacion);
			}

			return mensaje;
		}

		protected void btnGuardarCompra_OnClick(object sender, EventArgs e)
        {
            var usuarioInfo = ObtenerUsuarioInfo();
            var script = new StringBuilder(String.Empty);
            var mensaje = String.Empty;
            var compraId = Convert.ToInt32(hdnCompraId.Value);

            #region Datos Compra
            var compraInfo = new CompraInfo();
            compraInfo.CompraId = Convert.ToInt32(hdnCompraId.Value);
			compraInfo.ProveedorId = Convert.ToInt32(hdnProveedorId.Value.Trim());

			compraInfo.TipoDocumentoId = Convert.ToInt32(ddlTipoComprobante.SelectedValue);
			compraInfo.NumeroSerie = txtSerie.Text.Trim();
			compraInfo.NumeroComprobante = txtNumeroDocumento.Text.Trim();
			compraInfo.MonedaId = Convert.ToInt32(ddlMoneda.SelectedValue);
			compraInfo.FechaEmision = Convert.ToDateTime(txtFechaEmision.Text.Trim());
			//compraInfo.FechaVencimiento = Convert.ToDateTime(txtFechaVencimiento.Text.Trim());
			compraInfo.Glosa = txtGlosa.Text.Trim();
			#endregion

			if (compraId.Equals(0))
            {
                compraInfo.UsuarioCreacionId = usuarioInfo.UsuarioId;
                compraId = new Negocio.Compra().InsertarCompra(compraInfo);
                if (compraId > 0)
                {
                    script.Append("document.getElementById('hdnCompraId').value = " + compraId + ";");
                    mensaje = "Se registró la Compra correctamente";
                }
                else
                {
                    //mensaje = "Ya existe una Venta registrado con el número de comprobante: " + txtNumeroComprobante.Text.Trim();
                }
            }
            else
            {
                compraInfo.UsuarioModificacionId = usuarioInfo.UsuarioId;
                compraId = new Negocio.Compra().ActualizarCompra(compraInfo);
                if (compraId > 0)
                {
                    mensaje = "Se actualizó la Compra correctamente";
                }
                else
                {
                    //mensaje = "Ya existe una Venta registrada con el número de comprobante: " + txtNumeroComprobante.Text.Trim();
                }
            }
            script.Append("MostrarMensaje('" + mensaje + "');");
            script.Append("LimpiarProductoDetalle();");            

            //CargarDatos();
            RegistrarScript(script.ToString(), "GuardarVenta");
        }

		protected void EliminarItem(object sender, EventArgs e)
		{
			var script = new StringBuilder();

			var lnkRemove = (LinkButton)sender;
			var codigoDetalleProducto = Convert.ToInt32(lnkRemove.CommandArgument);

			var compraDetalleListaInfo = (List<ComprasDetalleInfo>)Session["CompraDetalle"];

			var compraDetalleEliminarInfo = compraDetalleListaInfo.SingleOrDefault(i => i.ComprasDetalleId.Equals(codigoDetalleProducto));
			//var codigoCompra = compraDetalleEliminarInfo.CompraId;

			compraDetalleListaInfo.Remove(compraDetalleEliminarInfo);

			Session["CompraDetalle"] = compraDetalleListaInfo;

			grvVenta.DataSource = compraDetalleListaInfo;
			grvVenta.DataBind();

			if (compraDetalleListaInfo.Count > 0)
			{
				grvVenta.HeaderRow.Attributes["style"] = "display: none";
				grvVenta.UseAccessibleHeader = true;
				grvVenta.HeaderRow.TableSection = TableRowSection.TableHeader;
			}

			//if (codigoCompra > 0)
			//{
			//	var eliminado = new Negocio.Compra().EliminarCompraDetalle(codigoDetalleProducto);
			//}

			script.Append("CalcularMontoFinal(); LimpiarProductoDetalle();");
			RegistrarScript(script.ToString(), "CalcularMontoComprobante");			
			
		}

		protected void GuardarCompraDetalle(object sender, EventArgs e)
		{
			var script = new StringBuilder();
			var usuarioInfo = ObtenerUsuarioInfo();

			var compraDetalleListaInfo = (List<ComprasDetalleInfo>)Session["CompraDetalle"];
			if (compraDetalleListaInfo == null)
			{
				compraDetalleListaInfo = new List<ComprasDetalleInfo>();
			}

			var compraInfo = new ComprasDetalleInfo();
			compraInfo.ComprasDetalleId = (-1) * (compraDetalleListaInfo.Count + 1);
			compraInfo.CompraId = Convert.ToInt32(hdnCompraId.Value);
			compraInfo.ProductoId = Convert.ToInt32(hdnProductoId.Value);
			compraInfo.Cantidad = Convert.ToDecimal(txtCantidad.Text);
			compraInfo.PrecioUnitario = Convert.ToDecimal(txtPrecio.Text);
			compraInfo.SubTotal = Convert.ToDecimal(txtSubTotal.Text.Replace(",", ""));
			compraInfo.Igv = Convert.ToDecimal(txtIgv.Text.Replace(",", ""));
			compraInfo.Total = Convert.ToDecimal(txtTotal.Text.Replace(",", ""));
			compraInfo.UsuarioCreacionId = usuarioInfo.UsuarioId;
			compraInfo.AlmacenId = Convert.ToInt32(ddlAlmacen.SelectedValue);
			compraInfo.Almacen = ddlAlmacen.SelectedItem.Text;

			compraInfo.Codigo = Request["txtProductoCodigo"];
			compraInfo.Producto = Request["txtProductoDescripcion"];
			compraInfo.UnidadMedida = ddlUnidad.SelectedItem.Text;

			compraDetalleListaInfo.Add(compraInfo);

			Session["CompraDetalle"] = compraDetalleListaInfo;

			grvVenta.DataSource = compraDetalleListaInfo;
			grvVenta.DataBind();

			if (compraDetalleListaInfo.Count > 0)
			{
				grvVenta.HeaderRow.Attributes["style"] = "display: none";
				grvVenta.UseAccessibleHeader = true;
				grvVenta.HeaderRow.TableSection = TableRowSection.TableHeader;
			}
			
			script.Append("CalcularMontoFinal();LimpiarProductoDetalle();");
			
			RegistrarScript(script.ToString(), "GuardarCompra");			
		}

		protected void btnBuscarProveedor_Click(object sender, EventArgs e)
		{
			
			var ruc = txtRuc.Text.Trim();
			var razonSocial = txtRazonSocialCliente.Text.Trim();
			var clienteInfoLista = new Negocio.Proveedor().ListarPaginado(0, ruc, razonSocial, 0, 0);
			grvCliente.DataSource = clienteInfoLista;
			grvCliente.DataBind();

			if (clienteInfoLista.Count > 0)
			{
				grvCliente.HeaderRow.Attributes["style"] = "display: none";
				grvCliente.UseAccessibleHeader = true;
				grvCliente.HeaderRow.TableSection = TableRowSection.TableHeader;
			}
			//rowCount = ventaInfoLista.Count > 0 ? ventaInfoLista.First().TotalFilas : 0;
		}

		protected void btnBuscarProducto_Click(object sender, EventArgs e)
		{
		    var usuarioInfo = ObtenerUsuarioInfo();

            var tipoTiendaId = (Request["TipoTiendaId"] == null) ? 0 : Convert.ToInt32(Request["TipoTiendaId"]);
		    var codigo = txtCodigoBuscar.Text.Trim();
		    var descripcion = txtProductoBuscar.Text.Trim();

		    //var productoInfoLista = new Producto().ListarPaginado(0, codigo, razonSocial, 0, 0);
		    var productoInfoLista = new List<ProductoInfo>();

            //switch (tipoTiendaId)
            //{
            //    case 0:
            //        productoInfoLista = new Negocio.Producto().ListarPaginado(0, codigo, descripcion, 0, 0).ToList();
            //        break;
            //    case 1: // Estación
            //        productoInfoLista = new Negocio.Producto().ListarPaginado(0, codigo, descripcion, 0, 0).Where(p => p.DisponibleEstacion.Equals(1)).ToList();
            //        break;
            //    case 2: // Market
            //        productoInfoLista = new Negocio.Producto().ListarPaginado(0, codigo, descripcion, 0, 0).Where(p => p.DisponibleMarket.Equals(1)).ToList();
            //        break;
            //    case 3: // Canastilla
            //        productoInfoLista = new Negocio.Producto().ListarPaginado(0, codigo, descripcion, 0, 0).Where(p => p.DisponibleCanastilla.Equals(1)).ToList();
		    //        break;
		    //}

		    productoInfoLista = new Negocio.Producto().ListarPaginado(0, String.Empty, String.Empty, 0, 0).ToList();
		    if (usuarioInfo.TipoNegocioId > 0)
		    {
		        productoInfoLista = productoInfoLista.Where(p => p.TipoNegocioId.Contains(usuarioInfo.TipoNegocioId.ToString())).ToList();
		    }

            grvProducto.DataSource = productoInfoLista;
			grvProducto.DataBind();

			if (productoInfoLista.Count > 0)
			{
				grvProducto.HeaderRow.Attributes["style"] = "display: none";
				grvProducto.UseAccessibleHeader = true;
				grvProducto.HeaderRow.TableSection = TableRowSection.TableHeader;
			}
		}

		private void CargarDatosDetalle()
		{
			var script = new StringBuilder("");

			grvVenta.DataBind();
			int numeroRegistros = 10;
			int numeroPagina = Convert.ToInt16(ViewState["PageIndex"]);

			int indicePagina = numeroPagina == 0 ? 0 : numeroPagina - 1;
			pageIndex = indicePagina + 1;

			var codigoCompra = Convert.ToInt32(hdnCompraId.Value);
			var ventaInfoLista = new Negocio.Compra().ListarComprasDetalle(0, codigoCompra);
			grvVenta.DataSource = ventaInfoLista;
			grvVenta.DataBind();

			if (ventaInfoLista.Count > 0)
			{
				grvVenta.HeaderRow.Attributes["style"] = "display: none";
				grvVenta.UseAccessibleHeader = true;
				grvVenta.HeaderRow.TableSection = TableRowSection.TableHeader;
			}
			rowCount = ventaInfoLista.Count > 0 ? ventaInfoLista.First().TotalFilas : 0;
			pageCount = CalcPageCount(rowCount);
			ViewState["PageCount"] = pageCount;

			#region Texto del Pie de Página
			if (ventaInfoLista.Count > 0)
			{
				if (numeroRegistros == 0)
				{
					lblPaginacion.Text = "Página " + pageIndex.ToString("") + " de 1, con un Total de " + rowCount.ToString("") + " registros";
					script.Append("document.getElementById('lblPaginacion').innerText = '");
					script.Append("Página " + pageIndex.ToString("") + " de 1, con un Total de " + rowCount.ToString("") + " registros';");
				}
				else
				{
					lblPaginacion.Text = "Página " + pageIndex.ToString("") + " de " + pageCount.ToString("") + ", con un Total de " + rowCount.ToString("") + " registros";
					script.Append("document.getElementById('lblPaginacion').innerText = '");
					script.Append("Página " + pageIndex.ToString("") + " de " + pageCount.ToString("") + ", con un Total de " + rowCount.ToString("") + " registros';");
				}
			}
			else
			{
				lblPaginacion.Text = "No se obtuvieron resultados";
				script.Append("document.getElementById('lblPaginacion').innerText = 'No se obtuvieron resultados';");
			}
			#endregion			

			RefreshPageButtons();
			RegistrarScript(script.ToString(), "Paginacion");
		}
	}
}