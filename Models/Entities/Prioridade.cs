using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketHub.Models.Entities
{
    public class Prioridade
    {
        public int Id{get;set;}
        public string Nome{get;set;}
        public int HorasEstimadas{get;set;}
    }
}