using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketHub.Models.Entities;

namespace TicketHub.Interfaces.IRepository
{
    public interface IPrioridadeRepository : IBaseRepository<Prioridade>
    {
        Task<bool> PossuiChamado<Tid>(Tid id);
    }
}