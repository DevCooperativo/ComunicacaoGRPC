using ComunicacaoGRPC.Grpc;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;

public class IntegrantesController : Controller
{
    private readonly IntegrantesService.IntegrantesServiceClient _client;

    public IntegrantesController()
    {
        var channel = GrpcChannel.ForAddress("https://localhost:44371");
        _client = new IntegrantesService.IntegrantesServiceClient(channel);
    }

    public async Task<IActionResult> Index()
    {
        var resposta = _client.Listar(new IntegrantesEmpty());
        return View(resposta.Integrantes);
    }
}
