using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TicketHub.Contracts.Chamado;
using TicketHub.Models.Entities;
using TicketHub.Models.Enums;

namespace TicketHub.Mapper
{
    public static class ChamadoMapper
    {
        public static Chamado ToEntity(this ChamadoRequestDTO dto)
        {
            if (dto == null) return null;

            return new Chamado
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                SetorId = dto.SetorId.Value,
                PrioridadeId = dto.PrioridadeId.Value,
                Status = ChamadoStatus.Aberto, 
                DataAbertura = DateTime.UtcNow
            };
        }

        public static ChamadoResponseDTO ToResponseDTO(this Chamado chamado)
        {
            if (chamado == null) return null;

            TimeSpan tempoTotal = TimeSpan.Zero;
            if (chamado.DataInicio.HasValue)
            {
                tempoTotal = (chamado.DataFinal ?? DateTime.UtcNow) - chamado.DataInicio.Value;
            }

            var status = chamado.Status == ChamadoStatus.EmAtendimento &&
                        tempoTotal.TotalHours > (chamado.Prioridade?.HorasEstimadas ?? 0)
                        ? ChamadoStatus.Atrasado
                        : chamado.Status;


            return new ChamadoResponseDTO(
                chamado.Id,
                chamado.Titulo,
                chamado.Descricao,
                status,
                chamado.DataAbertura,
                chamado.DataInicio,
                chamado.DataFinal,
                chamado.Solucao,
                chamado.SetorId,
                chamado.PrioridadeId,
                chamado.Setor?.Nome ?? "N/A",      
                chamado.Prioridade?.Nome ?? "N/A",
                tempoTotal
            );
        }

        public static IEnumerable<ChamadoResponseDTO> ToResponseList(this IEnumerable<Chamado> chamados)
        {
            if (chamados == null) return Enumerable.Empty<ChamadoResponseDTO>();
            return chamados.Select(c => c.ToResponseDTO());
        }

        public static void UpdateChamadoDTO(this ChamadoUpdateDTO dto, Chamado chamado)
        {
            if (dto == null || chamado == null) return;

            chamado.Titulo = dto.Titulo;
            chamado.Descricao = dto.Descicao; 
            chamado.SetorId = dto.SetorId;
            chamado.PrioridadeId = dto.PrioridadeId;
        }

        public static void ApplyCheckOut(this ChamadoCheckOutDTO dto, Chamado chamado)
        {
            if (dto == null || chamado == null) return;

            chamado.Solucao = dto.Solucao;
            chamado.DataFinal = DateTime.UtcNow;
            chamado.Status = ChamadoStatus.Finalizado;
        }
        
    }
}