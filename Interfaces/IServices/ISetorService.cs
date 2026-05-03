using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketHub.Contracts.Setor;

namespace TicketHub.Interfaces.IServices
{
    public interface ISetorService
    {
        Task<IEnumerable<SetorResponseDTO>> GetSetores();
        Task<SetorResponseDTO> GetSetorById(int id);
        Task<SetorResponseDTO> CreateSetor(SetorRequestDTO dto);
        Task<SetorResponseDTO> UpdateSetor(int id ,SetorUpdateDTO dto);
        Task Delete(int id);
        Task<IEnumerable<SetorResponseDTO>> SearchByName(string nome);
    }
}