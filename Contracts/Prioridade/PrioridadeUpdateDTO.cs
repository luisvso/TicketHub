using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TicketHub.Contracts.Prioridade
{
    public record PrioridadeUpdateDTO
    (
        [Required(ErrorMessage = "O nome da Prioridade é Obrigatorio")]
        string Nome,

        [Required(ErrorMessage = "A Hora estimada para a prioridade é Obrigatoria")]
        [Range(1, 720, ErrorMessage = "O tempo deve ser entre 1 a 720 horas")]
        int HorasEstimadas);
}