using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TicketHub.Models.Enums;

namespace TicketHub.Models.Entities
{
    public class Chamado
    {
        [Key]
        public int Id {get;set;}
        
        [Required(ErrorMessage = "O Titulo deste chamado deve ser Obrigatorio")]
        [StringLength(150, ErrorMessage = "O Titulo não pode passar de 150 caracteres")]
        public string? Titulo {get;set;} 

        public string Descricao {get;set;}

        public ChamadoStatus Status {get;set;} = ChamadoStatus.Aberto;

        [Required]
        public DateTime DataAbertura {get;set;} = DateTime.UtcNow;

        public DateTime? DataInicio {get;set;}
        public DateTime? DataFinal {get;set;}
        public string? Solucao {get;set;}

        [Required]
        public int SetorId {get;set;}

        [ForeignKey("SetorId")]
        public Setor Setor {get;set;}

        [Required]
        public int PrioridadeId {get;set;}

        [ForeignKey("PrioridadeId")]
        public Prioridade Prioridade {get;set;}

    }
}