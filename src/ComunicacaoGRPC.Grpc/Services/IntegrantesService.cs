using Grpc.Core;
using static ComunicacaoGRPC.Grpc.IntegrantesService;

namespace ComunicacaoGRPC.Grpc.Services;

public class IntegrantesService : IntegrantesServiceBase
{
    public override Task<ListaIntegrantesResponse> Listar(IntegrantesEmpty request, ServerCallContext context)
    {
        var resposta = new ListaIntegrantesResponse();

        resposta.Integrantes.AddRange(new[]
        {
            "Nícolas Bassini",
            "Lucas Ferri"
        });

        return Task.FromResult(resposta);
    }
}
