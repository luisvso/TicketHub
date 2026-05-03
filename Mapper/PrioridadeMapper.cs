using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketHub.Contracts.Prioridade;
using TicketHub.Models.Entities;

namespace TicketHub.Mapper
{
    public static class PrioridadeMapper
    {
        public static Prioridade ToEntity(this PrioridadeRequestDTO dto)
        {
            if(dto == null) return null;

            return new Prioridade 
            {
                Nome = dto.Nome,
                HorasEstimadas = dto.HorasEstimadas
            };
        }

        public static PrioridadeResponseDTO ToResponseDTO(this Prioridade prioridade)
        {
            if(prioridade == null) return null;

            return new PrioridadeResponseDTO(
                prioridade.Id,
                prioridade.Nome,
                prioridade.HorasEstimadas
            );
        }

        public static IEnumerable<PrioridadeResponseDTO> ToResponseList(this IEnumerable<Prioridade> prioridades)
        {
            if(prioridades == null) return null;
            return prioridades.Select(p => p.ToResponseDTO());
        }

        public static void UpdatePrioridadeDTO(this PrioridadeUpdateDTO dto, Prioridade prioridade)
        {
            if(dto == null) return;

            prioridade.Nome = dto.Nome;
            prioridade.HorasEstimadas = dto.HorasEstimadas;
        }
    }
}