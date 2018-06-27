using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using APU.Entidad;
using APU.Negocio;

namespace APU.Presentacion.Seguridad
{
    public partial class OpcionPorPerfil : PaginaBase
    {
        private Negocio.Perfil perfil;
        private Negocio.PerfilOpcion perfilOpcion;
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!Page.IsPostBack)
            {
                trvOpciones.Attributes.Add("onclick", "OnTreeClick(event)");
                CargarDatos();
            }
        }
        private void MostrarOpciones(int codigoPerfil)
        {
            trvOpciones.Nodes.Clear();

            var perfilOpcionInfo = new Opcion().Listar(0);
            var perfilOpcionInfo_ = new PerfilOpcion().Listar(codigoPerfil);
            var opcionPadrelista = (from opcionPadre in perfilOpcionInfo where opcionPadre.OpcionPadreId == 0 select opcionPadre);
            var opcionPadrelista_ = (from opcionPadre in perfilOpcionInfo_ where opcionPadre.OpcionPadreId == 0 select opcionPadre);

            foreach (OpcionInfo filaPadre in opcionPadrelista)
            {
                var padreId = filaPadre.OpcionId;

                var masterNode = new TreeNode(filaPadre.Nombre, Convert.ToString(filaPadre.OpcionId));
                if (opcionPadrelista_.Where(o => o.OpcionId.Equals(padreId)).ToList().Count > 0)
                {
                    if (opcionPadrelista_.First(o => o.OpcionId.Equals(padreId)).Activo > 0)
                    {
                        masterNode.Checked = true;
                    }
                }
                // masterNode.Checked = true;
                trvOpciones.Nodes.Add(masterNode);

                int hijos = perfilOpcionInfo.Where(h => h.OpcionPadreId == padreId).ToList().Count;
                int contador = 0;
                foreach (OpcionInfo filaHijo in (from opcionHijo in perfilOpcionInfo where opcionHijo.OpcionPadreId == padreId select opcionHijo).OrderBy(o => o.Orden))
                {
                    var childNode = new TreeNode(filaHijo.Nombre, Convert.ToString(filaHijo.OpcionId));

                    //if (filaHijo.Activo == 1)
                    //{
                    //    childNode.Checked = true;
                    //    contador += 1;
                    //}
                    //else
                    //{
                    //    childNode.Checked = false;
                    //}
                    childNode.Value = Convert.ToString(filaHijo.OpcionId);

                    if (perfilOpcionInfo_.Where(o => o.OpcionId.Equals(filaHijo.OpcionId)).ToList().Count > 0)
                    {
                        if (perfilOpcionInfo_.First(o => o.OpcionId.Equals(filaHijo.OpcionId)).Activo > 0)
                        {
                            childNode.Checked = true;
                        }
                    }
                    masterNode.ChildNodes.Add(childNode);
                }
                //if (hijos > contador)
                //{
                //    masterNode.Checked = false;
                //}
            }

            //var perfilOpcionInfo = new PerfilOpcion().Listar(codigoPerfil);
            //var opcionPadrelista = (from opcionPadre in perfilOpcionInfo where opcionPadre.OpcionPadreId == 0 select opcionPadre);
            //foreach (PerfilOpcionInfo filaPadre in opcionPadrelista)
            //{
            //    var padreId = filaPadre.OpcionId;

            //    var masterNode = new TreeNode(filaPadre.Nombre, Convert.ToString(filaPadre.OpcionId));
            //    masterNode.Checked = true;
            //    trvOpciones.Nodes.Add(masterNode);

            //    int hijos = perfilOpcionInfo.Where(h => h.OpcionPadreId == padreId).ToList().Count;
            //    int contador = 0;
            //    foreach (PerfilOpcionInfo filaHijo in (from opcionHijo in perfilOpcionInfo where opcionHijo.OpcionPadreId == padreId select opcionHijo).OrderBy(o => o.Orden))
            //    {
            //        var childNode = new TreeNode(filaHijo.Nombre, Convert.ToString(filaHijo.OpcionId));

            //        if (filaHijo.Activo == 1)
            //        {
            //            childNode.Checked = true;
            //            contador += 1;
            //        }
            //        else
            //        {
            //            childNode.Checked = false;
            //        }
            //        childNode.Value = Convert.ToString(filaHijo.OpcionId);
            //        masterNode.ChildNodes.Add(childNode);
            //    }
            //    if (hijos > contador)
            //    {
            //        masterNode.Checked = false;
            //    }
            //}
            trvOpciones.ExpandAll();
        }
        private void CargarDatos()
        {
            perfil = new Negocio.Perfil();
            var perfilInfo = perfil.Listar(0);

            ddlNumeroRegistros.DataTextField = "Perfil";
            ddlNumeroRegistros.DataValueField = "PerfilId";
            ddlNumeroRegistros.DataSource = (from perfilActivo in perfilInfo where perfilActivo.Activo == 1 select perfilActivo);
            ddlNumeroRegistros.DataBind();

            MostrarOpciones(Convert.ToInt32(ddlNumeroRegistros.SelectedValue));
        }
        private List<TreeNode> RecorrerNodosHijo(TreeNodeCollection nodes)
        {
            List<TreeNode> list = new List<TreeNode>();

            foreach (TreeNode item in nodes)
            {
                list.Add(item);
            }
            return list;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var list = new List<TreeNode>();
            perfilOpcion = new PerfilOpcion();
            //Recorremos los nodos para grabar
            foreach (TreeNode nodos in trvOpciones.Nodes)
            {
                //Agregamos el nodo padre a la lista
                list.Add(nodos);
                //Recorremos los nodos hijo
                if (nodos.ChildNodes.Count > 0)
                {
                    list.AddRange(RecorrerNodosHijo(nodos.ChildNodes));
                }
            }

            var listaOpciones = new List<PerfilOpcionInfo>();
            PerfilOpcionInfo perfilOpcionInfo;
            var perfilId = Convert.ToInt32(ddlNumeroRegistros.SelectedValue);
            var usuarioInfo = ObtenerUsuarioInfo();

            foreach (TreeNode opciones in list)
            {
                if (opciones.Checked)
                {
                    perfilOpcionInfo = new PerfilOpcionInfo();

                    perfilOpcionInfo.OpcionId = Convert.ToInt32(opciones.Value);
                    perfilOpcionInfo.PerfilId = perfilId;
                    perfilOpcionInfo.Activo = opciones.Checked ? 1 : 0;
                    // perfilOpcionInfo.UsuarioIdModificacion = oUsuarioInfo.UsuarioId;

                    listaOpciones.Add(perfilOpcionInfo);
                }
            }

            if (listaOpciones.Count > 0)
            {
                perfilOpcion.Eliminar(perfilId, 0);
                foreach (var o in listaOpciones)
                {
                    perfilOpcion.Insertar(o);
                }
                // opcionPerfil.Actualizar(listaOpciones);

                MostrarOpciones(Convert.ToInt32(ddlNumeroRegistros.SelectedValue));
                string mensaje = "Se modificó las opciones correctamente.";
                RegistrarScript("MostrarMensaje('" + mensaje + "');", "Mensaje");
            }
        }

        protected void ddlNumeroRegistros_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostrarOpciones(Convert.ToInt32(ddlNumeroRegistros.SelectedValue));
        }
    }
}