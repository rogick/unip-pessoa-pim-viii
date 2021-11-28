namespace Pessoas.Models
{
  public class TipoTelefone
  {

    public TipoTelefone(int id, string tipo)
    {
      this.Id = id;
      this.Tipo = tipo;

    }
    public int Id { get; set; }

    public string Tipo { get; set; }

  }




}