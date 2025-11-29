
using ComunicacaoGRPC.Grpc.Data;
using ComunicacaoGRPC.Grpc.Models;
using global::Grpc.Core;
using Microsoft.EntityFrameworkCore;
using static ComunicacaoGRPC.Grpc.ApostaService;

namespace ComunicacaoGRPC.Grpc.Services;
public class ApostaService : ApostaServiceBase
{
    private readonly ApplicationDbContext _context;

    public ApostaService(ApplicationDbContext context)
    {
        _context = context;
    }

    public override async Task<ApostaResponse> Inserir(ApostaRequest request, ServerCallContext context)
    {
        var aposta = new Aposta(
            request.CompeticaoId,
            (decimal)request.Valor
        );

        _context.Apostas.Add(aposta);
        await _context.SaveChangesAsync();

        return new ApostaResponse
        {
            Id = aposta.Id,
            CompeticaoId = aposta.CompeticaoId,
            Valor = (double)aposta.Valor,
            Multiplicador = (double)aposta.Multiplicador
        };
    }

    public override async Task<ListaApostasResponse> Listar(ApostaEmpty request, ServerCallContext context)
    {
        var apostas = await _context.Apostas.ToListAsync();

        var resposta = new ListaApostasResponse();
        resposta.Apostas.AddRange(apostas.Select(a => new ApostaResponse
        {
            Id = a.Id,
            CompeticaoId = a.CompeticaoId,
            Valor = (double)a.Valor,
            Multiplicador = (double)a.Multiplicador
        }));

        return resposta;
    }
}
