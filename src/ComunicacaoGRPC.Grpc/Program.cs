using ComunicacaoGRPC.Grpc.Data;
using ComunicacaoGRPC.Grpc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=alunos.db"));
builder.Services.AddCors();
var app = builder.Build();
app.UseCors(builder => builder
    .WithOrigins("http://localhost:5000")
    .AllowAnyHeader()
    .AllowAnyMethod());


app.MapGrpcService<CompeticaoService>();
app.MapGrpcService<ApostaService>();
app.MapGrpcService<IntegrantesService>();
app.MapGet("/", () => "Servidor gRPC de Alunos está rodando!");

app.Run();
