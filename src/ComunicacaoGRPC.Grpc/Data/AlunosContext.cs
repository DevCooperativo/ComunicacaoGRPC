using ComunicacaoGRPC.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace ComunicacaoGRPC.Grpc.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Competicao> Competicoes { get; set; }
    public DbSet<Aposta> Apostas { get; set; }
}
