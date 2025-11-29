using ComunicacaoGRPC.Grpc;

namespace ComunicacaoGRPC.Web.ViewModels;

public class ApostaListViewModel
{
    public List<ApostaResponse> Apostas { get; set; } = [];
    public List<CompeticaoResponse> Competicoes { get; set; } = [];
}