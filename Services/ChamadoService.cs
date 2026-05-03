using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TicketHub.Contracts.Chamado;
using TicketHub.Interfaces.IRepository;
using TicketHub.Interfaces.IServices;
using TicketHub.Mapper;
using TicketHub.Models.Enums;

namespace TicketHub.Services
{
    public class ChamadoService : IChamadoService
    {

        private readonly IChamadoRepository _repository;

        public ChamadoService(IChamadoRepository chamadoRepository)
        {
            _repository = chamadoRepository;
        }

        public async Task<ChamadoResponseDTO> Create(ChamadoRequestDTO dto)
        {
            var entidade = dto.ToEntity();

            await _repository.Create(entidade);
            await _repository.SaveChangesAsync();

            return entidade.ToResponseDTO();
        }

        public async Task<ChamadoResponseDTO> Update(int id, ChamadoUpdateDTO dto)
        {
            var model = await _repository.GetById(id);

            if(model.Status != ChamadoStatus.Aberto)
            {
                throw new InvalidOperationException("Não é possível editar um chamado que possui o atendimento iniciado ou finalizado.");
            }

            dto.UpdateChamadoDTO(model);
            
            await _repository.Update(model);
            await _repository.SaveChangesAsync();

            return model.ToResponseDTO();

        }

        public async Task Delete(int id)
        {
            var chamado = await _repository.GetById(id);
            if (chamado != null)
            {
                await _repository.Delete(chamado);
                await _repository.SaveChangesAsync();
            }
        }

        public async Task<ChamadoResponseDTO> GetById(int id)
        {
            var chamado = await _repository.GetById(id);

            return chamado?.ToResponseDTO();
        }


        public async Task<IEnumerable<ChamadoResponseDTO>> GetAll()
        {
            var chamados = await _repository.GetAll();

            return chamados.ToResponseList();
        }

        public async Task CheckIn(int id)
        {
            var chamado = await _repository.GetById(id);

            chamado.Status = ChamadoStatus.EmAtendimento;
            chamado.DataInicio = DateTime.UtcNow;

            await _repository.Update(chamado);
            await _repository.SaveChangesAsync();
        }

        public async Task CheckOut(int id, ChamadoCheckOutDTO dto)
        {
            var chamado = await _repository.GetById(id);

            dto.ApplyCheckOut(chamado);

            await _repository.Update(chamado);
            await _repository.SaveChangesAsync();
        }

        public async Task Cancelar(int id)
        {
            var chamado = await _repository.GetById(id);


            if(chamado.Status == ChamadoStatus.Finalizado || chamado.Status == ChamadoStatus.Cancelado)
            {
                throw new InvalidOperationException("Um chamado não pode ser cancelado se já foi finalizado ou cancelado");
            }

            chamado.Status = ChamadoStatus.Cancelado;
            chamado.DataFinal = DateTime.UtcNow;


            await  _repository.Update(chamado);
            await _repository.SaveChangesAsync();

        }

    }
}