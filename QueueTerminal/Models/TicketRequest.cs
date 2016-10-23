using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QueueTerminal.Models
{
    [DataContract]
    class TicketRequest
    {
        [DataMember(Name = "o")]
        public int who { get; set; }

        [DataMember(Name = "a")]
        public int what { get; set; }

        [DataMember(Name = "i")]
        public int ticketITd { get; set; }

        public TicketRequest(int currentTicket)
        {
            who = 1;
            what = 1;
            ticketITd = currentTicket;
        }
    }
}
