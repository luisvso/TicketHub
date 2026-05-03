using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketHub.Contracts.Setor;
using TicketHub.Models.Entities;

namespace TicketHub.Mapper
{
    public static class SetorMapper
    {
        
        public static Setor ToEntity(this SetorRequestDTO dto)
        {
            if(dto == null) return null;

            return new Setor
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao
            };
        }

        public static SetorResponseDTO ToResponseDTO(this Setor setor)
        {
            if(setor == null) return null;

            return new SetorResponseDTO(
                setor.Id,
                setor.Nome,
                setor.Descricao
            );
        }

        public static IEnumerable<SetorResponseDTO> toResponseList(this IEnumerable<Setor> setores)
        {
            if(setores == null) return null;
            return setores.Select(s => s.ToResponseDTO());
        }

        public static void UpdateSetorDTO(this SetorUpdateDTO dto, Setor setor)
        {
            if(dto == null) return;

            setor.Nome = dto.Nome;
            setor.Descricao = dto.Descricao;
        }
    }
}