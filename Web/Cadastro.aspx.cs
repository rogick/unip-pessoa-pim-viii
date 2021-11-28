using Pessoas.Dao;
using Pessoas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web
{
    public partial class Cadastro : System.Web.UI.Page
    {
        private static string TELEFONES = "telefones";

        List<TipoTelefone> tiposTelefone;

        List<Telefone> telefones;

        protected Pessoa pessoa = new Pessoa();

        protected void Page_Load(object sender, EventArgs e)
        {
            inicializarListasAuxiliares();

            if (!Page.IsPostBack)
            {
                if (Request["cpf"] != null)
                {
                    var cpf = Convert.ToInt64(Request["cpf"]);

                    using (var dao = new PessoaDAO())
                    {
                        pessoa = dao.consulte(cpf);
                        hdId.Value = pessoa.Id.ToString();
                        txtNome.Text = pessoa.Nome;
                        txtCPF.Text = pessoa.Cpf.ToString();
                        txtLogradouro.Text = pessoa.Endereco?.Logradouro;
                        txtNumero.Text = pessoa.Endereco?.Numero.ToString();
                        txtBairro.Text = pessoa.Endereco?.Bairro;
                        txtCidade.Text = pessoa.Endereco?.Cidade;
                        ddlEstado.SelectedValue = pessoa.Endereco?.Estado;
                        txtCep.Text = pessoa.Endereco?.Cep.ToString();

                        telefones = new List<Telefone>(pessoa.Telefones);

                    }
                } 
                else
                {
                    telefones = new List<Telefone>();
                    var tel = new Telefone();
                    tel.Tipo = tiposTelefone[0];
                    telefones.Add(tel);
                }

                atualizarGridTelefone();


                txtNome.Focus();
            } else
            {
                string json = ViewState[TELEFONES] as string;
                if (!String.IsNullOrEmpty(json))
                {
                    telefones = JsonSerializer.Deserialize<List<Telefone>>(json);
                }
            }

            litTitulo.Text = "Pessoas - " + (String.IsNullOrEmpty(hdId.Value) ? "Cadastro" : "Alteração");
        }

        private void popularPessoa()
        {
            if (!String.IsNullOrWhiteSpace(hdId.Value))
                pessoa.Id = Convert.ToInt32(hdId.Value);
            pessoa.Nome = txtNome.Text;
            pessoa.Cpf = Convert.ToInt64(txtCPF.Text);
            if (pessoa.Endereco == null)
                pessoa.Endereco = new Endereco();
            pessoa.Endereco.Logradouro = txtLogradouro.Text;
            pessoa.Endereco.Numero = Convert.ToInt32(txtNumero.Text);
            pessoa.Endereco.Bairro = txtBairro.Text;
            pessoa.Endereco.Cidade = txtCidade.Text;
            if (!String.IsNullOrWhiteSpace(ddlEstado.SelectedValue))
                pessoa.Endereco.Estado = ddlEstado.SelectedValue;
            if (!String.IsNullOrWhiteSpace(ddlEstado.SelectedItem?.Value))
                pessoa.Endereco.Estado = ddlEstado.SelectedItem.Value;
            pessoa.Endereco.Cep = Convert.ToInt32(txtCep.Text);

            pessoa.Telefones = new List<Telefone>(telefones);
        }

        private void inicializarListasAuxiliares()
        {
            tiposTelefone = new List<TipoTelefone>();
            tiposTelefone.Add(new TipoTelefone(1, "TELEFONE"));
            tiposTelefone.Add(new TipoTelefone(2, "CELULAR"));
            tiposTelefone.Add(new TipoTelefone(3, "COMERCIAL"));
            tiposTelefone.Add(new TipoTelefone(4, "FAX"));
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            using (PessoaDAO dao = new PessoaDAO())
            {
                popularPessoa();
                if (pessoa.Id == 0)
                {
                    dao.insira(pessoa);
                    Server.Transfer("Mensagem.aspx?msg=Pessoa cadastrada com sucesso!", true);
                }
                else
                {
                    dao.altere(pessoa);
                    Server.Transfer("Mensagem.aspx?msg=Pessoa alterada com sucesso!", true);
                }
            }
        }

        protected void atualizarGridTelefone()
        {
            gridTelefones.DataSource = telefones;
            gridTelefones.DataBind();

            salvarEstadoTelefones();
        }

        private void salvarEstadoTelefones()
        {
            ViewState[TELEFONES] = JsonSerializer.Serialize<List<Telefone>>(telefones);
        }

        protected void btnAdicionarTelefone_Click(object sender, EventArgs e)
        {
            Telefone tel = new Telefone();
            tel.Tipo = tiposTelefone[0];
            telefones.Add(new Telefone());
            atualizarGridTelefone();
        }

        protected void gridTelefones_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            gridTelefones.EditIndex = -1;
        }

        protected void gridTelefones_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            gridTelefones.EditIndex = e.NewEditIndex;
        }

        protected void gridTelefones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var ddl = e.Row.FindControl("ddlTipoTelefone") as DropDownList;
                if (ddl != null)
                {
                    ddl.DataSource = from tp in tiposTelefone select new ListItem(tp.Tipo, tp.Id.ToString());
                    ddl.DataValueField = "Value";
                    ddl.DataTextField = "Text";
                    ddl.DataBind();

                    if (telefones[e.Row.RowIndex]?.Tipo != null)
                        ddl.SelectedValue = telefones[e.Row.RowIndex]?.Tipo.Id.ToString();
                }
            }
        }

        protected void ddlTipoTelefone_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gvr = ((DropDownList)sender).NamingContainer as GridViewRow;
            if (gvr != null)
            {
                var ddl = gvr.FindControl("ddlTipoTelefone") as DropDownList;
                var tipo = tiposTelefone.Where(tp => tp.Id.ToString() == ddl.SelectedValue).First();
                gvr.DataItem = tipo;
                telefones[gvr.DataItemIndex].Tipo = tipo;
                salvarEstadoTelefones();
            }
        }

        protected void gridTelefones_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            telefones.RemoveAt(e.RowIndex);
            atualizarGridTelefone();
        }

        protected void txtDdd_TextChanged(object sender, EventArgs e)
        {
            GridViewRow gvr = ((TextBox)sender).NamingContainer as GridViewRow;
            if (gvr != null)
            {
                telefones[gvr.DataItemIndex].Ddd = Convert.ToInt32(((TextBox)sender).Text);
                salvarEstadoTelefones();
            }
        }

        protected void txtNumero_TextChanged(object sender, EventArgs e)
        {
            GridViewRow gvr = ((TextBox)sender).NamingContainer as GridViewRow;
            if (gvr != null)
            {
                telefones[gvr.DataItemIndex].Numero = Convert.ToInt32(((TextBox)sender).Text);
                salvarEstadoTelefones();
            }
        }

    }
}