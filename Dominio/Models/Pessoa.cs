using System;
using System.Collections.Generic;

namespace Pessoas.Models {

  public class Pessoa {

    public int Id { get; set; }

    public string Nome { get; set; }

    public long Cpf { get; set; } 

    public Endereco Endereco { get; set; }

    public IList<Telefone> Telefones { get; set; }
  }
}