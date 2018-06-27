using System;
using System.Linq;
using System.Text;
using System.Web.Services;
using System.Web.UI.WebControls;
using APU.Entidad;
using APU.Herramientas;
using APU.Negocio;

namespace APU.Presentacion.Maestros
{
	public partial class SubTipoProducto : PaginaBase
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
			var tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaTamanioPagina).Where(x => x.Activo.Equals(1)).ToList();
			LlenarCombo(ddlNumeroRegistros, tablaMaestraInfo, "NombreCorto", "NombreLargo");
			ddlNumeroRegistros.SelectedValue = "5";

			var listaTipoProducto = new Negocio.TipoProducto().Listar(0, 0,Constantes.PrimerNivelProducto).Where(x => x.Activo.Equals(1)).ToList();
			LlenarCombo(ddlTipoProducto, listaTipoProducto, "TipoProductoId", "Nombre");
			ddlTipoProducto.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));
		}

		private void CargarDatos()
		{
			var script = new StringBuilder("");

			grvUsuario.DataBind();
			int numeroRegistros = Convert.ToInt16(ddlNumeroRegistros.SelectedValue);
			int numeroPagina = Convert.ToInt16(ViewState["PageIndex"]);

			int indicePagina = numeroPagina == 0 ? 0 : numeroPagina - 1;
			pageIndex = indicePagina + 1;

			var tipoProductoLista = new Negocio.TipoProducto().ListarPaginadoSubProducto(0, numeroRegistros, indicePagina);
			grvUsuario.DataSource = tipoProductoLista;
			grvUsuario.DataBind();

			if (tipoProductoLista.Count > 0)
			{
				grvUsuario.HeaderRow.Attributes["style"] = "display: none";
				grvUsuario.UseAccessibleHeader = true;
				grvUsuario.HeaderRow.TableSection = TableRowSection.TableHeader;
			}
			rowCount = tipoProductoLista.Count > 0 ? tipoProductoLista.First().TotalFilas : 0;
			pageCount = CalcPageCount(rowCount);
			ViewState["PageCount"] = pageCount;

			#region Texto del Pie de Página
			if (tipoProductoLista.Count > 0)
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

			//txtContrasenia.Attributes.Add("type", "password");
		}

		#region WebMethod
		[WebMethod]
		public static TipoProductoInfo ObtenerTipoProducto(int usuarioId)
		{
			return new Negocio.TipoProducto().Listar(usuarioId, 0,Constantes.SegundoNivelProducto).FirstOrDefault();
		}
		[WebMethod]
		public static string GuardarUsuario(int usuarioId, string nombres, string apellidoPaterno, string apellidoMaterno, int sexoId, int estadoCivilId, string correo, string telefono, string celular,
											int tipoDocumentoId, string numeroDocumento, string codigo, string login, string contrasenia, int perfilId, int empresaId, int departamentoId,
											int cargoId, string correoTrabajo, string telefonoTrabajo,
			int activo)
		{
			var mensaje = String.Empty;

			#region Datos Usuario
			var usuarioInfo = new UsuarioInfo();
			usuarioInfo.UsuarioId = usuarioId;
			usuarioInfo.Nombres = nombres;
			usuarioInfo.ApellidoPaterno = apellidoPaterno;
			usuarioInfo.ApellidoMaterno = apellidoMaterno;
			usuarioInfo.SexoId = sexoId;
			usuarioInfo.EstadoCivilId = estadoCivilId;
			usuarioInfo.Correo = correo;
			usuarioInfo.Telefono = telefono;
			usuarioInfo.Celular = celular;
			usuarioInfo.TipoDocumentoId = tipoDocumentoId;
			usuarioInfo.NumeroDocumento = numeroDocumento;

			usuarioInfo.Codigo = codigo;
			usuarioInfo.Login = login;
			usuarioInfo.Password = contrasenia;
			usuarioInfo.PerfilId = perfilId;
			usuarioInfo.EmpresaId = empresaId;
			usuarioInfo.DepartamentoId = departamentoId;
			usuarioInfo.Foto = String.Empty;
			usuarioInfo.CargoId = cargoId;
			usuarioInfo.CorreoTrabajo = correoTrabajo;
			usuarioInfo.TelefonoTrabajo = telefonoTrabajo;
			usuarioInfo.Activo = activo;
			#endregion

			if (usuarioId.Equals(0))
			{
				usuarioId = new Negocio.Usuario().Insertar(usuarioInfo);
				if (usuarioId < 0)
				{
					mensaje = usuarioId + "@" + "Ya existe un cliente registrado con el N° de documento: " + numeroDocumento;
					return mensaje;
				}
			}
			else
			{
				usuarioId = new Negocio.Usuario().Actualizar(usuarioInfo);
				if (usuarioId < 0)
				{
					mensaje = usuarioId + "@" + "Ya existe un cliente registrado con el N° de documento: " + numeroDocumento;
					return mensaje;
				}
			}
			mensaje = usuarioId + "@" + String.Empty;
			return mensaje;
		}
		#endregion

		public void btnGuardarUsuario_Click(object sender, EventArgs e)
		{
			var script = new StringBuilder(String.Empty);
			var usuarioId = Convert.ToInt32(hdnUsuarioId.Value);
			var mensaje = String.Empty;

			#region Datos Usuario
			var subTipoProductoInfo = new TipoProductoInfo();
			subTipoProductoInfo.TipoProductoId = Convert.ToInt32(hdnUsuarioId.Value);
			subTipoProductoInfo.Nombre = txtNombre.Text.Trim();
			subTipoProductoInfo.Descripcion = txtDescripcion.Text.Trim();
			subTipoProductoInfo.Activo = chkActivo.Checked ? 1 : 0;
			subTipoProductoInfo.TipoProductoPadreId = Convert.ToInt32(ddlTipoProducto.SelectedValue);
			subTipoProductoInfo.Nivel = Constantes.SegundoNivelProducto;
			
			#endregion

			if (usuarioId.Equals(0))
			{
				usuarioId = new Negocio.TipoProducto().Insertar(subTipoProductoInfo);
				if (usuarioId > 0)
				{
					script.Append("document.getElementById('hdnUsuarioId').value = " + usuarioId + ";");
					mensaje = "Se registró el SubTipo producto correctamente";
				}
				else
				{
					mensaje = "Ya existe un subtipo de producto registrado con el nombre: " + txtNombre.Text.Trim();
				}
			}
			else
			{
				usuarioId = new Negocio.TipoProducto().Actualizar(subTipoProductoInfo);
				if (usuarioId > 0)
				{
					script.Append("document.getElementById('hdnUsuarioId').value = " + usuarioId + ";");
					mensaje = "Se actualizó el SubTipo producto correctamente";
				}
				else
				{
					mensaje = "Ya existe un subtipo de producto registrado con el nombre: " + txtNombre.Text.Trim();
				}
			}

			script.Append("MostrarMensaje('" + mensaje + "');");
			script.Append("LimpiarUsuario();");
			script.Append("var modalDialog = $find('mpeUsuario'); modalDialog.hide();");
			CargarDatos();
			RegistrarScript(script.ToString(), "GuardarSubTipoProducto");
		}

		protected void grvUsuario_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes.Add(Constantes.EventoOnClick, "var fila = SeleccionarFila(this); if (typeof GridView_OnClick == 'function'){GridView_OnClick(fila);}");
				e.Row.Attributes.Add(Constantes.EventoOnDblClick, "var fila = SeleccionarFila(this); if (typeof grvUsuario_OnDnlClick == 'function'){grvUsuario_OnDnlClick(fila);}");
			}
		}
	}
}