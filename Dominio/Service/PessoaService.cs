using System;
using System.Collections.Generic;
using Pessoas.Dao;
using Pessoas.Models;

namespace Pessoas.Service
{
    public class PessoaService: IDisposable
    {

        private PessoaDAO Dao;

        private BaseDAO BaseDAO;

        public PessoaService()
        {
            this.Dao = new PessoaDAO();

            this.BaseDAO = new BaseDAO();
        }

        public bool insira(Pessoa p) {
            validarSalvarAlterarPessoa(p);

            return Dao.insira(p);
        }

        public bool altere(Pessoa p) {
            validarSalvarAlterarPessoa(p);

            return Dao.altere(p);
        }

        public bool exclua(Pessoa p) {
            validarExclusao(p);

            return Dao.exclua(p);
        }

        public Pessoa consulte(long cpf) {
            return Dao.consulte(cpf);
        }
        
        // Métodos de validação

        private void validarSalvarAlterarPessoa(Pessoa p) {
            ValidacaoException validacao = new ValidacaoException();

            if (String.IsNullOrEmpty(p?.Nome))
                validacao.addMessage("Campo Nome é obrigatório");
            else if (p.Nome.Length > 256)
                validacao.addMessage("Tamanho máximo para campo Nome é de 256 caracteres");

            if (p?.Cpf <= 0)
                validacao.addMessage("Campo CPF é obrigatório");
            else 
            {
                IList<Dictionary<string, object>> rs = this.BaseDAO.executeSql("Select ID From PESSOA Where CPF = " + p.Cpf);
                foreach (var linha in rs)
                {
                    if (linha != null && linha["ID"] != null && Convert.ToInt32(linha["ID"]) != p.Id) {
                        validacao.addMessage("Já existe outra pessoa com o mesmo CPF cadastrada no sistema");
                        break;
                    }
                }
            }

            if (p.Endereco == null)
                validacao.addMessage("Endereço é obrigatório");
            else
                validarSalvarAlterarEndereco(p.Endereco, validacao);

            if (p.Telefones?.Count > 0)
            {
                IList<string> numeros = new List<string>();
                foreach (var tel in p.Telefones)
                {
                    string hash = String.Format("{0}-{1}-{2}", tel?.Ddd, tel?.Numero, tel?.Tipo?.Id);
                    int posicao = p.Telefones.IndexOf(tel) + 1;
                    if (!numeros.Contains(hash))
                    {
                        validarSalvarAlterarTelefone(tel, posicao, validacao);
                        numeros.Add(hash);
                    } else
                        validacao.addMessage(String.Format("Telefone {0}: Já informado para esta pessoa", posicao));
                }
            }

            validacao.NotifyException();
        }

        private void validarSalvarAlterarEndereco(Endereco endereco, ValidacaoException validacao) {
            if (String.IsNullOrEmpty(endereco?.Logradouro))
                validacao.addMessage("Campo Logradouro é obrigatório");
            else if (endereco.Logradouro.Length > 256)
                validacao.addMessage("Tamanho máximo para campo Logradouro é de 256 caracteres");

            if (endereco?.Cep <= 0)
                validacao.addMessage("Campo CEP é obrigatório");

            if (String.IsNullOrEmpty(endereco?.Bairro))
                validacao.addMessage("Campo Bairro é obrigatório");
            else if (endereco.Logradouro.Length > 50)
                validacao.addMessage("Tamanho máximo para campo Bairro é de 50 caracteres");

            if (String.IsNullOrEmpty(endereco?.Cidade))
                validacao.addMessage("Campo Cidade é obrigatório");
            else if (endereco.Cidade.Length > 30)
                validacao.addMessage("Tamanho máximo para campo Cidade é de 30 caracteres");

            if (String.IsNullOrEmpty(endereco?.Estado))
                validacao.addMessage("Campo Estado é obrigatório");
            else if (endereco.Estado.Length > 20)
                validacao.addMessage("Tamanho máximo para campo Estado é de 20 caracteres");
        }

        private void validarSalvarAlterarTelefone(Telefone telefone, int posicao, ValidacaoException validacao) {
            if (telefone?.Numero <= 0)
                validacao.addMessage(String.Format("Telefone {0}: Campo Número do Telefone é obrigatório", posicao));

            if (telefone?.Ddd <= 0)
                validacao.addMessage(String.Format("Telefone {0}: Campo DDD é obrigatório", posicao));

            if (telefone?.Tipo == null || telefone.Tipo.Id <= 0)
                validacao.addMessage(String.Format("Telefone {0}: Campo Tipo é obrigatório", posicao));
            else {
                IList<Dictionary<string, object>> list = this.BaseDAO.executeSql("Select 1 From TELEFONE_TIPO Where ID = " + telefone.Tipo.Id);
                if (list.Count == 0)
                    validacao.addMessage(String.Format("Telefone {0}: Valor inválido para o campo Tipo: " + telefone.Tipo.Id, posicao));
            }
        }
        
        private void validarExclusao(Pessoa p) {
            ValidacaoException validacao = new ValidacaoException();

            if (p?.Id == null)
                validacao.addMessage("ID da Pessoa não informado");

            if (p?.Endereco?.Id == null)
                validacao.addMessage("ID do Endereço não informado");

            validacao.NotifyException();
        }

    public void Dispose()
    {
      if (Dao != null) Dao.Dispose();
      if (BaseDAO != null) BaseDAO.Dispose();
    }
  }

      public class ValidacaoException: Exception {

        private IList<string> messages;

        public IList<string> getMessages() {
            if (this.messages == null) this.messages = new List<string>();

            return this.messages;
        }

        public void addMessage(string msg) {
            getMessages().Add(msg);
        }

        public void NotifyException() {
            if (this.getMessages().Count > 0)
                throw this;
        }

        public override string Message 
        { 
            get
            {
                string retorno = "Não foi possível efetuar as operações devido às seguintes falhas nas regras de validação\n";
                foreach (string msg in this.getMessages()) 
                    retorno += "\n\t" + msg;
                
                return retorno;
            }
        }

    }
}