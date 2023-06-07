using Microsoft.EntityFrameworkCore;
using XPTO.Models;

namespace XPTO.Data
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Usuario> Usuarios {get;set;}
        public DbSet<CaixaEletronico> CaixaEletronicos{get;set;}
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}