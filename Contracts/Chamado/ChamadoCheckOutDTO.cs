using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketHub.Contracts.Chamado
{
    public record ChamadoCheckOutDTO(

        [Required(ErrorMessage = "O chamado deve ter uma solução")]
        string Solucao
    );
}