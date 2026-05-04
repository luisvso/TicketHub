using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketHub.Contracts.Prioridade;
using TicketHub.Infrastructure.Data;
using TicketHub.Interfaces.IRepository;
using TicketHub.Models.Entities;

namespace TicketHub.Repository
{
    public class PrioridadeRepository : IPrioridadeRepository
    {

        private readonly AppDbContext _context;

        public PrioridadeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Prioridade> Create(Prioridade model)
        {
            await _context.Prioridades.AddAsync(model);
            return model;
        }

        public async Task Update(Prioridade model)
        {
            _context.Prioridades.Update(model);
            await Task.CompletedTask;
        }

        public async Task Delete(Prioridade model)
        {
            _context.Prioridades.Remove(model);
            await Task.CompletedTask;
        }

        public async Task<Prioridade> GetById<Tid>(Tid id)
        {
            return await _context.Prioridades.FindAsync(id);
        }

        public async Task<IEnumerable<Prioridade>> GetAll()
        {
            return await _context.Prioridades.AsTracking().ToListAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> PossuiChamado<Tid>(Tid id)
        {
            int idInt = Convert.ToInt32(id);
            return await _context.Chamados.AnyAsync(c => c.PrioridadeId == idInt);
        }

    }
}