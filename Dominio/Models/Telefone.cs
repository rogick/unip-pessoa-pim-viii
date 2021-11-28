namespace Pessoas.Models
{
  public class Telefone
  {
    public int Id { get; set; }

    public int Numero { get; set; }

    public int Ddd { get; set; }

    public TipoTelefone Tipo { get; set; }
  }
}