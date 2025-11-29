using ComunicacaoGRPC.Grpc.Data;
using ComunicacaoGRPC.Grpc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=alunos.db"));

var app = builder.Build();

app.MapGrpcService<CompeticaoService>();
app.MapGrpcService<ApostaService>();
app.MapGet("/", () => "Servidor gRPC de Alunos está rodando!");

app.Run();
