using ComunicacaoGRPC.Grpc.Data;
using ComunicacaoGRPC.Grpc.Models;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using static ComunicacaoGRPC.Grpc.CompeticaoService;

namespace ComunicacaoGRPC.Grpc.Services;

public class CompeticaoService : CompeticaoServiceBase
{
    private readonly ApplicationDbContext _context;

    public CompeticaoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public override async Task<CompeticaoResponse> Inserir(CompeticaoRequest request, ServerCallContext context)
    {
        var competicao = new Competicao { Nome = request.Nome, Data = request.Data.ToDateTime() };
        _context.Competicoes.Add(competicao);
        await _context.SaveChangesAsync();

        return new CompeticaoResponse
        {
            Id = competicao.Id,
            Nome = competicao.Nome,
            Data = Timestamp.FromDateTime(competicao.Data.ToUniversalTime())
        };
    }

    public override async Task<ListaCompeticoesResponse> Listar(CompeticaoEmpty request, ServerCallContext context)
    {
        var CompeticaoS = await _context.Competicoes.ToListAsync();

        var resposta = new ListaCompeticoesResponse();
        resposta.Competicoes.AddRange(CompeticaoS.Select(a => new CompeticaoResponse
        {
            Id = a.Id,
            Nome = a.Nome,
            Data = Timestamp.FromDateTime(a.Data.ToUniversalTime())
        }));

        return resposta;
    }
}
