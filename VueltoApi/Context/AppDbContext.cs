using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VueltoApi.Models;

namespace VueltoApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }
        public DbSet<Coin> Coins { get; set; }
        public DbSet<Banknotes> Banknotes { get; set; }
        public DbSet<Sale> Sale { get; set; }
    }
}
