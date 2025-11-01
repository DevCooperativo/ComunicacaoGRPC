using ComunicacaoGRPC.Grpc;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;

namespace ComunicacaoGRPC.Web.Controllers;

public class AlunosController : Controller
{
    private readonly AlunoService.AlunoServiceClient _client;

    public AlunosController()
    {
        var channel = GrpcChannel.ForAddress("https://localhost:44371");
        _client = new AlunoService.AlunoServiceClient(channel);
    }

    public async Task<IActionResult> Index()
    {
        var resposta = await _client.ListarAsync(new Empty());
        return View(resposta.Alunos);
    }

    [HttpPost]
    public async Task<IActionResult> Inserir(string nome, string matricula)
    {
        await _client.InserirAsync(new AlunoRequest { Nome = nome, Matricula = matricula });
        return RedirectToAction("Index");
    }
}
