using Alunos.GrpcServer.Models;
using Microsoft.EntityFrameworkCore;

namespace ComunicacaoGRPC.Grpc.Data;

public class AlunosContext : DbContext
{
    public AlunosContext(DbContextOptions<AlunosContext> options) : base(options) { }

    public DbSet<Aluno> Alunos { get; set; }
}
