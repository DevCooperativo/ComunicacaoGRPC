using Alunos.GrpcServer.Models;
using ComunicacaoGRPC.Grpc.Data;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using static ComunicacaoGRPC.Grpc.AlunoService;

namespace ComunicacaoGRPC.Grpc.Services;

public class AlunoService : AlunoServiceBase
{
    private readonly AlunosContext _context;

    public AlunoService(AlunosContext context)
    {
        _context = context;
    }

    public override async Task<AlunoResponse> Inserir(AlunoRequest request, ServerCallContext context)
    {
        var aluno = new Aluno { Nome = request.Nome, Matricula = request.Matricula };
        _context.Alunos.Add(aluno);
        await _context.SaveChangesAsync();

        return new AlunoResponse
        {
            Id = aluno.Id,
            Nome = aluno.Nome,
            Matricula = aluno.Matricula
        };
    }

    public override async Task<ListaAlunosResponse> Listar(Empty request, ServerCallContext context)
    {
        var alunos = await _context.Alunos.ToListAsync();

        var resposta = new ListaAlunosResponse();
        resposta.Alunos.AddRange(alunos.Select(a => new AlunoResponse
        {
            Id = a.Id,
            Nome = a.Nome,
            Matricula = a.Matricula
        }));

        return resposta;
    }
}
