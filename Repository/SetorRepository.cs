using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketHub.Contracts.Setor;
using TicketHub.Infrastructure.Data;
using TicketHub.Interfaces.IRepository;
using TicketHub.Mapper;
using TicketHub.Models.Entities;

namespace TicketHub.Repository
{
    public class SetorRepository : ISetorRepository
    {

        private readonly AppDbContext _context;

        public SetorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Setor>> GetAll()
        {
            return await _context.Setores.AsTracking().ToListAsync();

        }

        public async Task<Setor> GetById<Tid>(Tid id)
        {
            return await _context.Setores.FindAsync(id);
        }

        public async Task<Setor> Create(Setor model)
        {
            await _context.Setores.AddAsync(model);
            return model; 
        }

        public async Task Update(Setor model)
        {
            _context.Setores.Update(model);
            await Task.CompletedTask; 
        }

        public async Task Delete(Setor model)
        {
            _context.Setores.Remove(model);
            await Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> PossuiChamados<Tid>(Tid id)
        {
            var idInt = Convert.ToInt32(id);
            return await _context.Chamados.AnyAsync(c => c.SetorId == idInt);
        }

    }
}