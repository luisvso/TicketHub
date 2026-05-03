using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketHub.Contracts.Prioridade;

namespace TicketHub.Interfaces.IServices
{
    public interface IPrioridadeService
    {
        Task<PrioridadeResponseDTO> CreatePrioridade(PrioridadeRequestDTO dto);
        Task<PrioridadeResponseDTO> UpdatePrioridade(int id, PrioridadeUpdateDTO dto);
        Task DeletePrioridade(int id);
        Task<PrioridadeResponseDTO> GetPrioridadeById(int id);
        Task<IEnumerable<PrioridadeResponseDTO>> GetAllPrioridades();
        Task<IEnumerable<PrioridadeResponseDTO>> SearchPrioridadeByNome(string PrioridadeNome);
    }
}