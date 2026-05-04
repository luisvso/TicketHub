using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketHub.Contracts.Chamado
{
    public record ChamadoUpdateDTO(
        [Required(ErrorMessage = "Um chamado deve ter um Titulo")]
        string Titulo,
        [Required(ErrorMessage = "A Descrição precisa ter uma Descricao")]
        string Descicao,
        [Required(ErrorMessage = "O Chamado precisa ter um Setor")]
        int SetorId,
        [Required(ErrorMessage = "O chamado precisa ter uma Prioridade")]
        int PrioridadeId);
}