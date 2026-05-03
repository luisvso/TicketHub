using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketHub.Infrastructure.Data;
using TicketHub.Interfaces.IRepository;
using TicketHub.Models.Entities;

namespace TicketHub.Repository
{
    public class ChamadoRepository : IChamadoRepository
    {

        private readonly AppDbContext _context;

        public ChamadoRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<Chamado> Create(Chamado model)
        {
            await _context.Chamados.AddAsync(model);
            return model;
        }

        public async Task Update(Chamado model)
        {
            _context.Chamados.Update(model);
            await Task.CompletedTask;
        }

        public async Task Delete(Chamado model)
        {
            _context.Chamados.Remove(model);
            await Task.CompletedTask;
        }

        public async Task<Chamado> GetById<Tid>(Tid id)
        {
            var idInt = Convert.ToInt32(id);
            return _context.Chamados
                    .Include(c => c.Setor)
                    .Include(c => c.Prioridade)
                    .FirstOrDefault(c => c.Id == idInt);
        }

        public async Task<IEnumerable<Chamado>> GetAll()
        {
            return await _context.Chamados.Include(c => c.Setor).Include(c => c.Prioridade).AsNoTracking().ToListAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}