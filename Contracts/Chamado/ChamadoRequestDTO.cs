using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TicketHub.Models.Enums;

namespace TicketHub.Contracts.Chamado
{
    public record ChamadoRequestDTO(
        [Required(ErrorMessage = "O chamado deve ter um Titulo")]
        string Titulo,
        [Required(ErrorMessage = "A Descrição do chamado é obrigatorio")]
        string Descricao,
        [Required(ErrorMessage = "Um chamado deve pertencer a um setor")]
        int? SetorId,
        [Required(ErrorMessage = "Um chamado deve ter prioridade")]
        int? PrioridadeId);
}