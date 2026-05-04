using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketHub.Contracts.Setor;
using TicketHub.Exceptions;
using TicketHub.Interfaces;
using TicketHub.Interfaces.IRepository;
using TicketHub.Interfaces.IServices;
using TicketHub.Mapper;
using TicketHub.Models.Entities;

namespace TicketHub.Services
{
    public class SetorService : ISetorService
    {
        private readonly ISetorRepository _repository;

        public SetorService(ISetorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SetorResponseDTO>> GetSetores()
        {
            var setores = await _repository.GetAll();
            return setores.toResponseList();
        }

        public async Task<SetorResponseDTO> CreateSetor(SetorRequestDTO dto)
        {
            var setores = await _repository.GetAll();

            var existe = setores.Any(s => s.Nome.Trim().ToLower() == dto.Nome.Trim().ToLower());

            if(existe){
                throw new DuplicateResourceException($"Este setor de nome {dto.Nome} já esta cadastrado");
            }

            var entidade = dto.ToEntity();

            await _repository.Create(entidade);
            await _repository.SaveChangesAsync();

            return entidade.ToResponseDTO();
        }

        public async Task<SetorResponseDTO> GetSetorById(int id)
        {
            var setor = await _repository.GetById(id);
            return setor?.ToResponseDTO();
        }

        public async Task<SetorResponseDTO> UpdateSetor(int id, SetorUpdateDTO dto)
        {
            var setorExistente = await _repository.GetById(id);

            if (setorExistente == null)
                return null;

            dto.UpdateSetorDTO(setorExistente);

            await _repository.Update(setorExistente);
            await _repository.SaveChangesAsync();

            return setorExistente.ToResponseDTO();
        }

        public async Task Delete(int id)
        {

            if (await _repository.PossuiChamados(id)) throw new InvalidOperationException("Este setor esta vinculado a um chamado e não pode ser excluido");

            var setor = await _repository.GetById(id);
            if (setor != null)
            {
                await _repository.Delete(setor);
                await _repository.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<SetorResponseDTO>> SearchByName(string searchnome)
        {
            
            var todos = await _repository.GetAll();


            return todos.Where(s => s.Nome.Contains(searchnome, StringComparison.OrdinalIgnoreCase))
            .Select(s => s.ToResponseDTO());
        }
    }
}
