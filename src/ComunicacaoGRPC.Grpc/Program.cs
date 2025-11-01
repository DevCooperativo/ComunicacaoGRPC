using ComunicacaoGRPC.Grpc.Data;
using ComunicacaoGRPC.Grpc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddDbContext<AlunosContext>(options =>
    options.UseSqlite("Data Source=alunos.db"));

var app = builder.Build();

app.MapGrpcService<AlunoService>();
app.MapGet("/", () => "Servidor gRPC de Alunos está rodando!");

app.Run();
