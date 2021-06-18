using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace aplicacao_exemplo.Dados
{
    public class ContextoTreinamento : DbContext
    {
        public ContextoTreinamento(DbContextOptions<ContextoTreinamento> options) : base(options)
        {

        }

        public DbSet<Produto> Produtos { get; set; }
    }
}