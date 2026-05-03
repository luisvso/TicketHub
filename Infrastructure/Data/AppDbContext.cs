using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketHub.Models.Entities;

namespace TicketHub.Infrastructure.Data
{
    public class AppDbContext: DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options){}

        public DbSet<Setor> Setores {get;set;}
        public DbSet<Prioridade> Prioridades {get;set;}
        public DbSet<Chamado> Chamados {get;set;}

        internal async Task FindAsync(int id)
        {
            throw new NotImplementedException();
        }
        
        
    }
}