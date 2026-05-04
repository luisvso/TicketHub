using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketHub.Contracts.Prioridade;
using TicketHub.Exceptions;
using TicketHub.Interfaces.IRepository;
using TicketHub.Interfaces.IServices;
using TicketHub.Mapper;

namespace TicketHub.Services
{
    public class PrioridadeService : IPrioridadeService
    {
        public readonly IPrioridadeRepository _repository;

        public PrioridadeService(IPrioridadeRepository prioridadeRepository)
        {
            _repository = prioridadeRepository;
        }

        public async Task<PrioridadeResponseDTO> CreatePrioridade(PrioridadeRequestDTO dto)
        {
            var prioridades = await _repository.GetAll();

            var existe = prioridades.Any(p => p.Nome.Trim().ToLower() == dto.Nome.Trim().ToLower());

            if (existe)
            {
                throw new DuplicateResourceException($"Esta prioridade de nome {dto.Nome} já foi registrada");
            }

            var prioridade = dto.ToEntity();

            await _repository.Create(prioridade);
            await _repository.SaveChangesAsync();

            return prioridade.ToResponseDTO();
        }

        public async Task<PrioridadeResponseDTO> UpdatePrioridade(int id, PrioridadeUpdateDTO dto)
        {
            var prioridadeExistente = await _repository.GetById(id);

            if(prioridadeExistente == null)
            {
                return null;
            }

            dto.UpdatePrioridadeDTO(prioridadeExistente);

            await _repository.Update(prioridadeExistente);
            await _repository.SaveChangesAsync();

            return prioridadeExistente.ToResponseDTO();
        }

        public async Task DeletePrioridade(int id)
        {
            
            if(await _repository.PossuiChamado(id)) throw new InvalidOperationException("Esta prioridade esta vinculada a um chamado e não pode ser deletada");
            
            var prioridade = await _repository.GetById(id);

            if(prioridade != null)
            {
                await _repository.Delete(prioridade);
                await _repository.SaveChangesAsync();
            }
        }

        public async Task<PrioridadeResponseDTO> GetPrioridadeById(int id)
        {
            var prioridade = await _repository.GetById(id);

            return prioridade?.ToResponseDTO();
        }

        public async Task<IEnumerable<PrioridadeResponseDTO>> GetAllPrioridades()
        {
            var prioridades = await _repository.GetAll();

            return prioridades.ToResponseList();
        }

        public async Task<IEnumerable<PrioridadeResponseDTO>> SearchPrioridadeByNome(string searchName)
        {

            var todos = await _repository.GetAll();

            return todos.Where(p => p.Nome.Contains(searchName, StringComparison.OrdinalIgnoreCase))
            .Select(p => p.ToResponseDTO());

        }

    }
}