using Microsoft.EntityFrameworkCore;
using teste.net.Models;

namespace teste.net.Data
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Estados> Estados { get; set; }
        public DbSet<Cidades> Cidades { get; set; }
        public DbSet<Clientes> Clientes { get; set; }

    }
}