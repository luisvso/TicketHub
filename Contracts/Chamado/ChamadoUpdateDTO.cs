using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketHub.Contracts.Chamado
{
    public record ChamadoUpdateDTO(
        string? Titulo,
        [Required(ErrorMessage = "A Descrição precisa ter um titulo")]
        string Descicao,
        [Required(ErrorMessage = "O Chamado precisa ter um setor")]
        int SetorId,
        [Required(ErrorMessage = "O chamado precisa ter uma prioridade")]
        int PrioridadeId);
}