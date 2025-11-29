namespace ComunicacaoGRPC.Grpc.Models;

public class Aposta
{
    public int Id { get; set; }
    public int CompeticaoId { get; set; }
    public decimal Valor { get; set; }
    public decimal Multiplicador { get; set; }

    public Aposta(int competicaoId, decimal valor)
    {
        CompeticaoId = competicaoId;
        Valor = valor;
        var random = new Random();
        Multiplicador = random.Next(1001, 5000) / 1000m;
    }

}
