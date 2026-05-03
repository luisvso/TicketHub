using System.ComponentModel.DataAnnotations;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace TicketHub.Contracts.Setor
{
    public record SetorRequestDTO(
        [Required(ErrorMessage = "O nome do setor é obrigatório")]
        string Nome,

        [Required(ErrorMessage = "A descrição é obrigatória")]
        string Descricao
    );
}