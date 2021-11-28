using Pessoas.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web
{
    public partial class Visualizacao: System.Web.UI.Page
    {
        private static string CPF = "cpf";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var cpf = Convert.ToInt64(Request[CPF]);

                using(var dao = new PessoaDAO())
                {
                    var pessoa = dao.consulte(cpf);
                    lblNome.Text = pessoa.Nome;
                    lblCPF.Text = pessoa.Cpf.ToString();
                    lblLogradouro.Text = pessoa.Endereco?.Logradouro;
                    lblNumero.Text = pessoa.Endereco?.Numero.ToString();
                    lblBairro.Text = pessoa.Endereco?.Bairro;
                    lblCidade.Text = pessoa.Endereco?.Cidade;
                    lblEstado.Text = pessoa.Endereco?.Estado;
                    lblCep.Text = pessoa.Endereco?.Cep.ToString();

                    gridTelefones.DataSource = pessoa.Telefones;
                    gridTelefones.DataBind();

                    ViewState[CPF] = cpf;
                }
            }
        }

        protected void btnExlcuir_Click(object sender, EventArgs e)
        {
            long cpf = Convert.ToInt64(ViewState[CPF]);

            using (var dao = new PessoaDAO())
            {
                var pessoa = dao.consulte(cpf);
                dao.exclua(pessoa);

                Server.Transfer("Mensagem.aspx?mensagem=Pessoa excluída com sucesso!", true);
            }
        }

        protected void btnAlterar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cadastro.aspx?cpf=" + ViewState[CPF]);
        }
    }
}