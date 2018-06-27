using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI.WebControls;
using APU.Entidad;
using APU.Herramientas;
using APU.Negocio;

namespace APU.Presentacion.Almacen
{
	public partial class Productos : PaginaBase
	{
		protected override void Page_Load(object sender, EventArgs e)
		{
			base.Page_Load(sender, e);

			if (!Page.IsPostBack)
			{
			    var tipoTiendaId = (Request["TipoTiendaId"] == null) ? 0 : Convert.ToInt32(Request["TipoTiendaId"]);
				CargarInicial();
				CargarDatos(tipoTiendaId);
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
			var tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaTamanioPagina).Where(x => x.Activo.Equals(1)).ToList();
			LlenarCombo(ddlNumeroRegistros, tablaMaestraInfo, "NombreCorto", "NombreLargo");
			ddlNumeroRegistros.SelectedValue = "5";

			tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaUnidades).Where(x => x.Activo.Equals(1)).ToList();
			LlenarCombo(ddlUnidadMedida, tablaMaestraInfo, "Codigo", "NombreCorto");
			ddlUnidadMedida.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

		    tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaTipoNegocio).Where(x => x.Activo.Equals(1)).ToList();
		    LlenarLista(lstTipoNegocio, tablaMaestraInfo, "Codigo", "NombreCorto");
		    ddlUnidadMedida.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

            #region Datos Principales
            var tipoProducto = new TipoProducto().Listar(0, 0, Constantes.PrimerNivelProducto).Where(x => x.Activo.Equals(1)).ToList();
			
			LlenarCombo(ddlTipoProducto, tipoProducto, "TipoProductoId", "Nombre");
			ddlTipoProducto.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

			ddlSubTipoProducto.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));
			#endregion
		}
		private void CargarDatos(int tipoTiendaId)
		{
		    var usuarioInfo = ObtenerUsuarioInfo();

			var script = new StringBuilder("");

			grvUsuario.DataBind();
			int numeroRegistros = Convert.ToInt16(ddlNumeroRegistros.SelectedValue);
			int numeroPagina = Convert.ToInt16(ViewState["PageIndex"]);

			int indicePagina = numeroPagina == 0 ? 0 : numeroPagina - 1;
			pageIndex = indicePagina + 1;

            var productoInfoLista = new List<ProductoInfo>();

		    //switch (tipoTiendaId)
		    //{
      //          case 0:
      //              productoInfoLista = new Negocio.Producto().ListarPaginado(0, String.Empty, String.Empty, numeroRegistros, indicePagina).ToList();
      //              break;
		    //    case 1: // Estación
		    //        productoInfoLista = new Negocio.Producto().ListarPaginado(0, String.Empty, String.Empty, numeroRegistros, indicePagina).Where(p => p.DisponibleEstacion.Equals(1)).ToList();
      //              break;
		    //    case 2: // Market
		    //        productoInfoLista = new Negocio.Producto().ListarPaginado(0, String.Empty, String.Empty, numeroRegistros, indicePagina).Where(p => p.DisponibleMarket.Equals(1)).ToList();
      //              break;
		    //    case 3: // Canastilla
		    //        productoInfoLista = new Negocio.Producto().ListarPaginado(0, String.Empty, String.Empty, numeroRegistros, indicePagina).Where(p => p.DisponibleCanastilla.Equals(1)).ToList();
      //              break;
      //      }

		    productoInfoLista = new Negocio.Producto().ListarPaginado(0, String.Empty, String.Empty, numeroRegistros, indicePagina).ToList();
		    if (usuarioInfo.TipoNegocioId > 0)
		    {
		        productoInfoLista = productoInfoLista.Where(p => p.TipoNegocioId.Contains(usuarioInfo.TipoNegocioId.ToString())).ToList();
            }


            grvUsuario.DataSource = productoInfoLista;
			grvUsuario.DataBind();

			if (productoInfoLista.Count > 0)
			{
				grvUsuario.HeaderRow.Attributes["style"] = "display: none";
				grvUsuario.UseAccessibleHeader = true;
				grvUsuario.HeaderRow.TableSection = TableRowSection.TableHeader;
			}
			rowCount = productoInfoLista.Count > 0 ? productoInfoLista.First().TotalFilas : 0;
			pageCount = CalcPageCount(rowCount);
			ViewState["PageCount"] = pageCount;

			#region Texto del Pie de Página
			if (productoInfoLista.Count > 0)
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
			int numeroRegistros = Convert.ToInt16(ddlNumeroRegistros.SelectedValue);
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
		    var tipoTiendaId = String.IsNullOrEmpty(Request["TipoTiendaId"]) ? 0 : Convert.ToInt16(Request["TipoTiendaId"]);
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
			CargarDatos(tipoTiendaId);
			RefreshPageButtons();
		}
		protected void PageSortingEventHandler(object sender, CommandEventArgs e)
		{
		    var tipoTiendaId = String.IsNullOrEmpty(Request["TipoTiendaId"]) ? 0 : Convert.ToInt16(Request["TipoTiendaId"]);
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
			CargarDatos(tipoTiendaId);
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
		    var tipoTiendaId = String.IsNullOrEmpty(Request["TipoTiendaId"]) ? 0 : Convert.ToInt16(Request["TipoTiendaId"]);
            ViewState["PageCount"] = null;
			ViewState["PageIndex"] = null;
			CargarDatos(tipoTiendaId);
		}

		protected void btnPaginacion_Click(object sender, EventArgs e)
		{
		    var tipoTiendaId = String.IsNullOrEmpty(Request["TipoTiendaId"]) ? 0 : Convert.ToInt16(Request["TipoTiendaId"]);
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
			CargarDatos(tipoTiendaId);
			RefreshPageButtons();
		}

		protected void Page_PreRender(object sender, EventArgs e)
		{
			RegistrarEventoCliente(txtBuscar, Constantes.EventoOnKeyUp, "return Buscar();");
			RegistrarEventoCliente(ddlTipoProducto, Constantes.EventoOnChange, "return ObtenerSubTipoProducto();");

			Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Seleccione", "var Seleccione = '" + Constantes.Seleccione + "';", true);
			Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Seleccione_value", "var Seleccione_value = '" + Constantes.Seleccione_Value + "';", true);


		}

		#region WebMethod
		[WebMethod]
		public static ProductoInfo ObtenerProducto(int productoId)
		{
			return new Negocio.Producto().ListarPaginado(productoId, String.Empty, String.Empty, 0, 0).FirstOrDefault();
		}
		[WebMethod]
		public static List<TipoProductoInfo> ObtenerSubTipoProducto(int tipoProductoId)
		{
			return new Negocio.TipoProducto().Listar(0, tipoProductoId, Constantes.SegundoNivelProducto);
		}
				
		#endregion


		protected void grvUsuario_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes.Add(Constantes.EventoOnClick, "var fila = SeleccionarFila(this); if (typeof GridView_OnClick == 'function'){GridView_OnClick(fila);}");
				e.Row.Attributes.Add(Constantes.EventoOnDblClick, "var fila = SeleccionarFila(this); if (typeof grvUsuario_OnDnlClick == 'function'){grvUsuario_OnDnlClick(fila);}");
			}
		}

		protected void btnCargarStock_OnClick(object sender, EventArgs e)
		{
		    var usuarioInfo = ObtenerUsuarioInfo();

            var codigoProducto= Convert.ToInt32(hdnUsuarioId.Value);

			if (codigoProducto > 0)
			{
				var listaStockProducto = new Negocio.Inventario().Listar(0, codigoProducto, usuarioInfo.TipoNegocioId);

				grvStockProducto.DataSource = listaStockProducto.OrderBy(x => x.Almacen);
				grvStockProducto.DataBind();
			}
		}

	    protected void btnGuardarUsuario_OnClick(object sender, EventArgs e)
	    {
	        var tipoTiendaId = String.IsNullOrEmpty(Request["TipoTiendaId"]) ? 0 : Convert.ToInt16(Request["TipoTiendaId"]);
            var script = new StringBuilder(String.Empty);
	        var productoId = Convert.ToInt32(hdnUsuarioId.Value);
	        var mensaje = String.Empty;
			var usuarioInfo = ObtenerUsuarioInfo();

			#region Datos Usuario
			var productoInfo = new ProductoInfo();
	        productoInfo.ProductoId = Convert.ToInt32(hdnUsuarioId.Value);
	        productoInfo.Codigo = txtCodigo.Text.Trim();
	        productoInfo.Producto = txtNombre.Text.Trim();
	        productoInfo.Descripcion = txtDescripcion.Text.Trim();
	        productoInfo.Marca = txtMarca.Text.Trim();
	        productoInfo.TipoProductoId = Convert.ToInt32(ddlTipoProducto.SelectedValue);
	        productoInfo.SubTipoProductoId = Convert.ToInt32(Request.Form[ddlSubTipoProducto.UniqueID]);
			productoInfo.UnidadMedidaId = Convert.ToInt32(ddlUnidadMedida.SelectedValue);
			productoInfo.PrecioNormal = Convert.ToDecimal(txtPrecioNormal.Text);
			productoInfo.PrecioDescuento = Convert.ToDecimal(txtPrecioDescuento.Text);
			productoInfo.PrecioCompra = Convert.ToDecimal(txtPrecioCompra.Text);

			productoInfo.Activo = (chkActivo.Checked) ? 1 : 0;
	        #endregion

	        if (productoId.Equals(0))
	        {
				productoInfo.UsuarioCreacionId = usuarioInfo.UsuarioId;
				productoId = new Negocio.Producto().Insertar(productoInfo);

                if (productoId > 0)
	            {
					#region ProductoTipoTienda
					//if (chkEstacion.Checked)
					//{
					//	var productoTipoTiendaEstacionInfo = new ProductoTipoTiendaInfo();
					//	productoTipoTiendaEstacionInfo.ProductoId = productoId;
					//	productoTipoTiendaEstacionInfo.TipoTiendaId = 1;

					//	new Producto().InsertarProductoTipoTienda(productoTipoTiendaEstacionInfo);
					//}
					//if (chkMarket.Checked)
					//{
					//	var productoTipoTiendaMarketInfo = new ProductoTipoTiendaInfo();
					//	productoTipoTiendaMarketInfo.ProductoId = productoId;
					//	productoTipoTiendaMarketInfo.TipoTiendaId = 2;

					//	new Producto().InsertarProductoTipoTienda(productoTipoTiendaMarketInfo);
					//}
					//if (chkCanastilla.Checked)
					//{
					//	var productoTipoTiendaCanastillaInfo = new ProductoTipoTiendaInfo();
					//	productoTipoTiendaCanastillaInfo.ProductoId = productoId;
					//	productoTipoTiendaCanastillaInfo.TipoTiendaId = 3;

					//	new Producto().InsertarProductoTipoTienda(productoTipoTiendaCanastillaInfo);
					//}

	                foreach (ListItem item in lstTipoNegocio.Items)
	                {
	                    if (item.Selected)
	                    {
	                        var productoTipoTiendaCanastillaInfo = new ProductoTipoTiendaInfo();
	                        productoTipoTiendaCanastillaInfo.ProductoId = productoId;
	                        productoTipoTiendaCanastillaInfo.TipoTiendaId = Convert.ToInt32(item.Value);
                            new Producto().InsertarProductoTipoTienda(productoTipoTiendaCanastillaInfo);
                        }
	                }

					#endregion

					script.Append("document.getElementById('hdnUsuarioId').value = " + productoId + ";");                    
                    mensaje = "Se registró el producto correctamente";
	            }
	            else
	            {
	                mensaje = "Ya existe un producto con el código: " + txtCodigo.Text.Trim();
					script.Append("MostrarMensaje('" + mensaje + "');");
					RegistrarScript(script.ToString(), "GuardarProducto");
					return;
				}
	        }
	        else
	        {
				productoInfo.UsuarioModificacionId = usuarioInfo.UsuarioId;
				productoId = new Negocio.Producto().Actualizar(productoInfo);

                if (productoId > 0)
	            {
					#region ProductoTipoTienda
					new Producto().EliminarProductoTipoTienda(productoId, 0);
                    //if (chkEstacion.Checked)
                    //{
                    //	var productoTipoTiendaEstacionInfo = new ProductoTipoTiendaInfo();
                    //	productoTipoTiendaEstacionInfo.ProductoId = productoId;
                    //	productoTipoTiendaEstacionInfo.TipoTiendaId = 1;

                    //	new Producto().InsertarProductoTipoTienda(productoTipoTiendaEstacionInfo);
                    //}
                    //if (chkMarket.Checked)
                    //{
                    //	var productoTipoTiendaMarketInfo = new ProductoTipoTiendaInfo();
                    //	productoTipoTiendaMarketInfo.ProductoId = productoId;
                    //	productoTipoTiendaMarketInfo.TipoTiendaId = 2;

                    //	new Producto().InsertarProductoTipoTienda(productoTipoTiendaMarketInfo);
                    //}
                    //if (chkCanastilla.Checked)
                    //{
                    //	var productoTipoTiendaCanastillaInfo = new ProductoTipoTiendaInfo();
	                //	productoTipoTiendaCanastillaInfo.ProductoId = productoId;
	                //	productoTipoTiendaCanastillaInfo.TipoTiendaId = 3;

	                //	new Producto().InsertarProductoTipoTienda(productoTipoTiendaCanastillaInfo);
	                //}
	                foreach (ListItem item in lstTipoNegocio.Items)
	                {
	                    if (item.Selected)
	                    {
	                        var productoTipoTiendaCanastillaInfo = new ProductoTipoTiendaInfo();
	                        productoTipoTiendaCanastillaInfo.ProductoId = productoId;
	                        productoTipoTiendaCanastillaInfo.TipoTiendaId = Convert.ToInt32(item.Value);
	                        new Producto().InsertarProductoTipoTienda(productoTipoTiendaCanastillaInfo);
	                    }
	                }
                    #endregion

                    script.Append("document.getElementById('hdnUsuarioId').value = " + productoId + ";");
	                mensaje = "Se actualizó el producto correctamente";
	            }
	            else
	            {
	                mensaje = "Ya existe un producto con el código: " + txtCodigo.Text.Trim();
					script.Append("MostrarMensaje('" + mensaje + "');");
					RegistrarScript(script.ToString(), "GuardarProducto");
					return;
				}
	        }

	        script.Append("MostrarMensaje('" + mensaje + "');");
	        script.Append("LimpiarProducto();");
	        script.Append("var modalDialog = $find('mpeUsuario'); modalDialog.hide();");
	        CargarDatos(tipoTiendaId);
	        RegistrarScript(script.ToString(), "GuardarProducto");
        }
	    [WebMethod]
	    public static List<ProductoTipoTiendaInfo> ObtenerProductoTipoNegocio(int productoId)
	    {
	        return new Producto().ListarProductoTipoTienda(productoId, 0).ToList();
	    }
    }
}