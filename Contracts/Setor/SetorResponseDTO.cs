using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketHub.Contracts.Setor
{
    public record SetorResponseDTO(
        int Id,
        string Nome,
        string Descricao
    );
}