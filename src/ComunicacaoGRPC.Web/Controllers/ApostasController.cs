
using ComunicacaoGRPC.Grpc;
using ComunicacaoGRPC.Web.ViewModels;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;

namespace ComunicacaoGRPC.Web.Controllers;

public class ApostasController : Controller
{
    private readonly ApostaService.ApostaServiceClient _apostaClient;
    private readonly CompeticaoService.CompeticaoServiceClient _competicaoClient;

    public ApostasController()
    {
        var channel = GrpcChannel.ForAddress("https://localhost:44371");

        _apostaClient = new ApostaService.ApostaServiceClient(channel);
        _competicaoClient = new CompeticaoService.CompeticaoServiceClient(channel);
    }

    public async Task<IActionResult> Index()
    {
        // Buscar apostas
        var apostasResposta = await _apostaClient.ListarAsync(new ApostaEmpty());

        // Buscar competições para o dropdown
        var competicoesResposta = await _competicaoClient.ListarAsync(new CompeticaoEmpty());

        var vm = new ApostaListViewModel
        {
            Apostas = [.. apostasResposta.Apostas],
            Competicoes = [.. competicoesResposta.Competicoes]
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Inserir(int competicaoId, decimal valor)
    {
        var request = new ApostaRequest
        {
            CompeticaoId = competicaoId,
            Valor = (double)valor // GRPC usa double
        };

        await _apostaClient.InserirAsync(request);

        return RedirectToAction("Index");
    }
}