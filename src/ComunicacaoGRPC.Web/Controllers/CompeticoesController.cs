using ComunicacaoGRPC.Grpc;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;

namespace ComunicacaoGRPC.Web.Controllers;

public class CompeticoesController : Controller
{
    private readonly CompeticaoService.CompeticaoServiceClient _client;

    public CompeticoesController()
    {
        var channel = GrpcChannel.ForAddress("https://localhost:44371");
        _client = new CompeticaoService.CompeticaoServiceClient(channel);
    }

    public async Task<IActionResult> Index()
    {
        var resposta = await _client.ListarAsync(new CompeticaoEmpty());
        return View(resposta.Competicoes);
    }

    [HttpPost]
    public async Task<IActionResult> Inserir(string nome, DateTime data)
    {
        await _client.InserirAsync(new CompeticaoRequest { Nome = nome, Data = data.ToUniversalTime().ToTimestamp() });
        return RedirectToAction("Index");
    }
}
