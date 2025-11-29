namespace ComunicacaoGRPC.Grpc.Models;

public class Competicao
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public DateTime Data { get; set; }
}
