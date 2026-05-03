using System.ComponentModel.DataAnnotations;

namespace TicketHub.Contracts.Setor
{
    public record SetorUpdateDTO(
        [Required(ErrorMessage = "O nome do setor é obrigatório")]
        string Nome,
        [Required(ErrorMessage = "A descrição é obrigatória")]
        string Descricao
    );
}