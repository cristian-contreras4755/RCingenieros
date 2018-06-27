using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;
using System.Web.UI.WebControls;
using APU.Entidad;
using APU.Herramientas;
using APU.Negocio;

namespace APU.Presentacion.Almacen
{
    public partial class ConsultaStocks : PaginaBase
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!Page.IsPostBack)
            {
                CargarInicial();
                CargarDatos();
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
            var usuarioInfo = ObtenerUsuarioInfo();

            var tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaTamanioPagina).Where(x => x.Activo.Equals(1)).ToList();
            LlenarCombo(ddlNumeroRegistros, tablaMaestraInfo, "NombreCorto", "NombreLargo");
            ddlNumeroRegistros.SelectedValue = "5";

			var listaAlmacen = new Negocio.Almacen().Listar(0);
			LlenarCombo(ddlAlmacen, listaAlmacen, "AlmacenId", "Nombre");
			ddlAlmacen.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

			LlenarCombo(ddlAlmacenOrigen, listaAlmacen, "AlmacenId", "Nombre");

			LlenarCombo(ddlAlmacenDestino, listaAlmacen, "AlmacenId", "Nombre");
			ddlAlmacenDestino.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

			var listaUsuario = new Negocio.Usuario().Listar(0, "", "", "", Constantes.PerfilAdministrador, 0);
			foreach (var usuario in listaUsuario)
			{
				ddlResponsable.Items.Add(new ListItem(usuario.Nombres + "," + usuario.ApellidoPaterno + " " + usuario.ApellidoMaterno, usuario.UsuarioId.ToString()));
			}
			ddlResponsable.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));
		}
        private void CargarDatos()
        {
            var usuarioInfo = ObtenerUsuarioInfo();

            var script = new StringBuilder("");

            grvVenta.DataBind();
            int numeroRegistros = Convert.ToInt16(ddlNumeroRegistros.SelectedValue);
            int numeroPagina = Convert.ToInt16(ViewState["PageIndex"]);

            int indicePagina = numeroPagina == 0 ? 0 : numeroPagina - 1;
            pageIndex = indicePagina + 1;

			int almacenId = Convert.ToInt16(ddlAlmacen.SelectedValue);
			var ventaInfoLista = new Negocio.Inventario().ListarPaginado(0, almacenId, usuarioInfo.TipoNegocioId, numeroRegistros, indicePagina);
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
            CargarDatos();
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
            CargarDatos();
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
            CargarDatos();
        }

		protected void ddlAlmacen_SelectedIndexChanged(object sender, EventArgs e)
		{
			// tipoFiltro = String.IsNullOrEmpty(Request["filtro"]) ? 0 : Convert.ToInt16(Request["filtro"]);
			ViewState["PageCount"] = null;
			ViewState["PageIndex"] = null;
			CargarDatos();
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
            CargarDatos();
            RefreshPageButtons();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            RegistrarEventoCliente(txtBuscar, Constantes.EventoOnKeyUp, "return Buscar();");


            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Seleccione", "var Seleccione = '" + Constantes.Seleccione + "';", true);
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Seleccione_value", "var Seleccione_value = '" + Constantes.Seleccione_Value + "';", true);
        }

        #region WebMethod
        [WebMethod]
        public static InventarioInfo ObtenerInventario(int inventarioId)
        {
            var usuarioInfo = ObtenerUsuarioInfo();
			return new Negocio.Inventario().ListarPaginado(inventarioId, 0, usuarioInfo.TipoNegocioId, 0, 0).FirstOrDefault();
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
        protected void btnGuardarVenta_OnClick(object sender, EventArgs e)
        {
            var usuarioInfo = ObtenerUsuarioInfo();
            var script = new StringBuilder(String.Empty);
            var mensaje = String.Empty;
            var ventaId = Convert.ToInt32(hdnVentaId.Value);

			var stock = txtStock.Text;
			var stockMinimo = txtStockMinimo.Text;

			
			Negocio.Helper.ActualizarColumnasTabla("Inventario", new string[] { "InventarioActual", "InventarioMinimo" }, new string[] { stock, stockMinimo }, 
				new string[] { "InventarioId" }, new string[] { ventaId.ToString() });

			mensaje = "Se actualizó los datos del Inventario correctamente";

			script.Append("MostrarMensaje('" + mensaje + "');");
            script.Append("LimpiarVenta();");
            script.Append("var modalDialog = $find('mpeVenta'); modalDialog.hide();");

            CargarDatos();
            RegistrarScript(script.ToString(), "GuardarVenta");
        }

		protected void btnGuardarTraslado_OnClick(object sender, EventArgs e)
		{
			var usuarioInfo = ObtenerUsuarioInfo();
			var script = new StringBuilder(String.Empty);
			var mensaje = String.Empty;
			var ventaId = Convert.ToInt32(hdnVentaId.Value);
			var productoId= Convert.ToInt32(hdnProductoId.Value);

			var trasladoInfo = new TrasladoAlmacenInfo();

			trasladoInfo.AlmacenOrigenId = Convert.ToInt32(ddlAlmacenOrigen.SelectedValue);
			trasladoInfo.AlmacenDestinoId = Convert.ToInt32(ddlAlmacenDestino.SelectedValue);
			trasladoInfo.ProductoId = productoId;
			trasladoInfo.CantidadProducto= Convert.ToDecimal(txtCantidad.Text);
			trasladoInfo.UsuarioResponsableId= Convert.ToInt32(ddlResponsable.SelectedValue);
			trasladoInfo.UsuarioCreacionId = usuarioInfo.UsuarioId;
			trasladoInfo.TipoNegocioId = usuarioInfo.TipoNegocioId;
			trasladoInfo.FechaTraslado = DateTime.ParseExact(txtFechaTraslado.Text.Trim(), "dd/MM/yyyy", null);

			//se registra el traslado
			var trasladoId = new Negocio.TrasladoAlmacen().Insertar(trasladoInfo);

			//se crea o actualiza el producto en el nuevo almacen destino
			var inventarioInfo = new InventarioInfo();
			inventarioInfo.AlmacenId = Convert.ToInt32(ddlAlmacenDestino.SelectedValue);
			inventarioInfo.ProductoId = productoId;
			inventarioInfo.InventarioActual = Convert.ToDecimal(txtCantidad.Text);
			inventarioInfo.UsuarioCreacionId = usuarioInfo.UsuarioId;
		    inventarioInfo.TipoNegocioId = usuarioInfo.TipoNegocioId;

            var inventarioDestinoId = new Negocio.Inventario().Insertar(inventarioInfo);

			//se crea el movimiento de ingreso de almacen
			var movimientosInfo = new MovimientosInfo();
			movimientosInfo.OperacionId = trasladoId;
			movimientosInfo.TipoMovimientoId = Constantes.TipoMovimientoTrasladoIngreso;
			movimientosInfo.FechaOperacion = DateTime.Now;
			movimientosInfo.Glosa = "Traslado de Inventario Ingreso";
			movimientosInfo.UsuarioCreacionId = usuarioInfo.UsuarioId;

			new Negocio.Movimientos().InsertarMovimientos(movimientosInfo);

			//Se actualiza el saldo del producto en el almacenOriginal
			var inventarioOrigenInfo = new InventarioInfo();
			inventarioOrigenInfo.AlmacenId = Convert.ToInt32(ddlAlmacenOrigen.SelectedValue);
			inventarioOrigenInfo.ProductoId = productoId;
			inventarioOrigenInfo.InventarioActual = (-1) * Convert.ToDecimal(txtCantidad.Text);//se reduce el stock
			inventarioOrigenInfo.UsuarioModificacionId = usuarioInfo.UsuarioId;
		    inventarioOrigenInfo.TipoNegocioId = usuarioInfo.TipoNegocioId;
            var inventarioOrigenId = new Negocio.Inventario().Actualizar(inventarioOrigenInfo);

			//se crea el movimiento de salida de almacen
			movimientosInfo = new MovimientosInfo();
			movimientosInfo.OperacionId = trasladoId;
			movimientosInfo.TipoMovimientoId = Constantes.TipoMovimientoTrasladoEgreso;
			movimientosInfo.FechaOperacion = DateTime.Now;
			movimientosInfo.Glosa = "Traslado de Inventario Egreso";
			movimientosInfo.UsuarioCreacionId = usuarioInfo.UsuarioId;

			new Negocio.Movimientos().InsertarMovimientos(movimientosInfo);

			mensaje = "Se realizo el traslado correctamente";

			script.Append("MostrarMensaje('" + mensaje + "');");
			script.Append("LimpiarVenta();");
			script.Append("var modalDialog = $find('mpeTraslado'); modalDialog.hide();");

			CargarDatos();
			RegistrarScript(script.ToString(), "GuardarTraslado");
		}

	}
}