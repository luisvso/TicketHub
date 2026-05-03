using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketHub.Contracts.Chamado;

namespace TicketHub.Interfaces.IServices
{
    public interface IChamadoService
    {
        Task<ChamadoResponseDTO> Create(ChamadoRequestDTO dto);
        Task<ChamadoResponseDTO> Update(int id, ChamadoUpdateDTO dto);
        Task Delete(int id);
        Task<ChamadoResponseDTO> GetById(int id);
        Task<IEnumerable<ChamadoResponseDTO>> GetAll();
        Task CheckIn(int id);
        Task CheckOut(int id, ChamadoCheckOutDTO dto);
        Task Cancelar(int id);
    }
}